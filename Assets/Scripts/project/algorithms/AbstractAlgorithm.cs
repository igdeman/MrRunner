using System;
using System.Collections.Generic;
using UnityEngine;

namespace MrRunner
{
    [Serializable]
    public abstract class AbstractAlgorithm
    {
        public abstract void Run(Tile[,] tiles);
        public abstract void Destroy();

        private string name;
        public string Name { get => name; }
        protected Config config;

        public AbstractAlgorithm(string name, Config config)
        {
            this.name = name;
            this.config = config;
        }


        public static Tile[] GetAdjacentTiles(Tile t, Tile[,] tiles, Config config)
        {
            List<Tile> adjList = new List<Tile>();
            for (int c = t.C - 1; c <= Mathf.Min(config.TotalColumns - 1, t.C + 1); c++)
            {
                if (c >= 0)
                {
                    Tile adj = tiles[c, t.R];
                    if (adj != t)
                        adjList.Add(adj);
                }
            }
            for (int r = t.R - 1; r <= Mathf.Min(config.TotalRows - 1, t.R + 1); r++)
            {
                if (r >= 0)
                {
                    Tile adj = tiles[t.C, r];
                    if (adj != t)
                        adjList.Add(adj);
                }
            }
            return adjList.ToArray();
        }

        public static Vector2Int MinimalDistanceIndex(float[,] distance, bool[,] visited, Config config)
        {
            Vector2Int result = new Vector2Int();
            float min_dist = float.MaxValue;
            for (int c = 0; c < config.TotalColumns; c++)
            {
                for (int r = 0; r < config.TotalRows; r++)
                {
                    if (distance[c, r] < min_dist && !visited[c, r])
                    {
                        result = new Vector2Int(c, r);
                        min_dist = distance[c, r];
                    }
                }
            }
            return result;
        }

        public static Vector2Int MinimalDistanceIndex(int[,] distance, bool[,] visited, Config config)
        {
            Vector2Int result = new Vector2Int();
            int min_dist = int.MaxValue;
            for (int c = 0; c < config.TotalColumns; c++)
            {
                for (int r = 0; r < config.TotalRows; r++)
                {
                    if (distance[c, r] < min_dist && !visited[c, r])
                    {
                        result = new Vector2Int(c, r);
                        min_dist = distance[c, r];
                    }
                }
            }
            return result;
        }
    }
}
