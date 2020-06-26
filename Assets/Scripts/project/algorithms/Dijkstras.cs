using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrRunner
{
    public class Dijkstras : AbstractAlgorithm
    {
        public Dijkstras(Config config) : base("Dijkstra's", config) { }

        public override void Run(Tile[,] tiles)
        {
            Execute(tiles);
        }

        public override void Destroy()
        {
            config = null;
        }

        async void Execute(Tile[,] tiles)
        {
            List<Vector2Int> result = new List<Vector2Int>();

            int[,] distance = new int[config.TotalColumns, config.TotalRows];
            bool[,] visited = new bool[config.TotalColumns, config.TotalRows];

            for (int c = 0; c < config.TotalColumns; c++)
            {
                for (int r = 0; r < config.TotalRows; r++)
                {
                    distance[c, r] = int.MaxValue;
                }
            }
            distance[config.StartPosition.x, config.StartPosition.y] = 0;

            for (int i = 0; i < config.TotalColumns * config.TotalRows; i++)
            {
                Vector2Int index = AbstractAlgorithm.MinimalDistanceIndex(distance, visited, config);
                visited[index.x, index.y] = true;
                int d = distance[index.x, index.y] + 1;

                EventDispatcher.DispatchEvent<AlgorithmEvent, AbstractAlgorithm>(
                    new AlgorithmEvent(AlgorithmEvent.MOVE_PLAYER, this, index)
                    );
                await Task.Delay(config.StepDelay);

                Tile[] adjList = GetAdjacentTiles(tiles[index.x, index.y], tiles, config);
                for (int a = 0; a < adjList.Length; a++)
                {
                    if (!visited[adjList[a].C, adjList[a].R] && d < distance[adjList[a].C, adjList[a].R] && adjList[a].Type != TileType.Obstacle)
                    {
                        distance[adjList[a].C, adjList[a].R] = d;

                        EventDispatcher.DispatchEvent<AlgorithmEvent, AbstractAlgorithm>(
                            new AlgorithmEvent(AlgorithmEvent.MARK_VISITED, this, new Vector2Int(adjList[a].C, adjList[a].R))
                            );
                        await Task.Delay(config.StepDelay);
                    }

                    if (adjList[a].C == config.EndPosition.x && adjList[a].R == config.EndPosition.y)
                    {
                        EventDispatcher.DispatchEvent<AlgorithmEvent, AbstractAlgorithm>(
                            new AlgorithmEvent(AlgorithmEvent.COMPLETE, this, config.EndPosition)
                            );
                        return;
                    }
                }

            }
        }
    }
}