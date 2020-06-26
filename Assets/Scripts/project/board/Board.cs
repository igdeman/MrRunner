using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MrRunner
{
    [RequireComponent(typeof(RectTransform))]
    public class Board : MonoBehaviour
    {
        [SerializeField]
        private Config config;
        [SerializeField]
        private Tile tilePrefab;
        private float tileSize;
        private Tile[,] tiles;
        private Tile start;
        private Tile end;
        private int totalObstacles = 0;

        public int TotalObstacles { get => totalObstacles; }
        public RectTransform rectTransform { get => (RectTransform)transform; }
        public float TileSize { get => tileSize; }

        public Tile[,] Create(Config config)
        {
            this.config = config;
            tileSize = Mathf.Min(rectTransform.rect.width / config.TotalColumns, rectTransform.rect.height / config.TotalRows);
            tiles = CreateTiles();
            start = tiles[config.StartPosition.x, config.StartPosition.y];
            start.Type = TileType.Start;
            end = tiles[config.EndPosition.x, config.EndPosition.y];
            end.Type = TileType.End;

            float st = Time.realtimeSinceStartup;
            CreateObstacles(tiles);
            Debug.Log($"CREATE OBSTACLES DURATION: {Time.realtimeSinceStartup - st}");
            return tiles;
        }

        public void Clear()
        {
            if (config)
            {
                for (int c = 0; c < config.TotalColumns; c++)
                {
                    for (int r = 0; r < config.TotalRows; r++)
                    {
                        tiles[c, r].Clear();
                    }
                }
            }
        }

        Tile[,] CreateTiles()
        {
            Tile[,] tiles = new Tile[config.TotalColumns, config.TotalRows];
            for (int c = 0; c < config.TotalColumns; c++)
            {
                for (int r = 0; r < config.TotalRows; r++)
                {
                    Tile t = CreateTile(c, r);
                    tiles[c, r] = t;
                }
            }
            return tiles;
        }

        Tile CreateTile(int c, int r)
        {
            Tile t = Instantiate<GameObject>(tilePrefab.gameObject).GetComponent<Tile>();
            t.C = c;
            t.R = r;
            t.rectTransform.sizeDelta = new Vector2(tileSize, tileSize);
            t.transform.SetParent(transform, false);
            t.transform.localPosition = GetTilePosition(c, r);
            return t;
        }

        void CreateObstacles(Tile[,] tiles)
        {
            List<Tile> availableObstacles = new List<Tile>();
            for (int c = 0; c < config.TotalColumns; c++)
            {
                for (int r = 0; r < config.TotalRows; r++)
                {
                    if (tiles[c, r] != start && tiles[c, r] != end)
                        availableObstacles.Add(tiles[c, r]);
                }
            }

            totalObstacles = 0;
            while (totalObstacles < config.TotalObstacles && availableObstacles.Count > 0)
            {
                int i = Random.Range(0, availableObstacles.Count);
                Tile o = availableObstacles[i];
                o.Type = TileType.Obstacle;
                availableObstacles.RemoveAt(i);

                if (!isPercolate())
                    o.Type = TileType.Empty;

                totalObstacles++;
            }
        }

        bool isPercolate()
        {
            int sI = start.R * config.TotalColumns + start.C;
            int eI = end.R * config.TotalColumns + end.C;
            UnionFind uf = new UnionFind(config.TotalColumns * config.TotalRows);
            Tile[] adjList;
            Tile adj;
            Vector2Int adjPos = new Vector2Int();
            Tile t = tiles[config.StartPosition.x, config.StartPosition.y];

            bool[,] visited = new bool[config.TotalColumns, config.TotalRows];
            float[,] distance = new float[config.TotalColumns, config.TotalRows];
            for (int c = 0; c < config.TotalColumns; c++)
            {
                for (int r = 0; r < config.TotalRows; r++)
                    distance[c, r] = float.MaxValue;
            }
            distance[config.StartPosition.x, config.StartPosition.y] = 0;

            for (int i = 0; i < config.TotalColumns * config.TotalRows; i++)
            {
                visited[t.C, t.R] = true;
                adjList = AbstractAlgorithm.GetAdjacentTiles(t, tiles, config);
                for (int a = 0; a < adjList.Length; a++)
                {
                    adj = adjList[a];
                    if (adj.Type != TileType.Obstacle && distance[adj.C, adj.R] == float.MaxValue)
                    {
                        adjPos.x = adj.C;
                        adjPos.y = adj.R;
                        distance[adj.C, adj.R] = Vector2Int.Distance(adjPos, config.EndPosition);
                        uf.Union(t.R * config.TotalColumns + t.C, adj.R * config.TotalColumns + adj.C);
                    }
                }
                Vector2Int index = AbstractAlgorithm.MinimalDistanceIndex(distance, visited, config);
                t = tiles[index.x, index.y];

                if (uf.IsConnected(sI, eI))
                    return true;
            }

            return false;
        }

        Vector2 GetTilePosition(int C, int R)
        {
            float bw = (float)config.TotalColumns * tileSize;
            float bh = (float)config.TotalRows * tileSize;
            float x = -bw / 2f + tileSize / 2f;
            float y = bh / 2f - tileSize / 2f;
            return new Vector3(x + tileSize * (float)C, y - tileSize * (float)R);
        }

        

    }
}
