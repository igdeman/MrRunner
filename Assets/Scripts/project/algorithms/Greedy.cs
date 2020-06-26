using System.Threading.Tasks;
using System;
using UnityEngine;

namespace MrRunner
{
    [Serializable]
    public class Greedy : AbstractAlgorithm
    {
        public Greedy(Config config) : base("Greedy",config){}

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
            float[,] distance = new float[config.TotalColumns, config.TotalRows];
            bool[,] visited = new bool[config.TotalColumns, config.TotalRows];

            for (int c = 0; c < config.TotalColumns; c++)
            {
                for (int r = 0; r < config.TotalRows; r++)
                {
                    distance[c, r] = float.MaxValue;
                }
            }
            distance[config.StartPosition.x, config.StartPosition.y] = 0;

            for (int i = 0; i < config.TotalColumns * config.TotalRows; i++)
            {
                Vector2Int index = AbstractAlgorithm.MinimalDistanceIndex(distance, visited, config);
                visited[index.x, index.y] = true;

                EventDispatcher.DispatchEvent<AlgorithmEvent, AbstractAlgorithm>(
                    new AlgorithmEvent(AlgorithmEvent.MOVE_PLAYER, this, index)
                    );
                await Task.Delay(config.StepDelay);

                Tile[] adjList = GetAdjacentTiles(tiles[index.x, index.y], tiles, config);
                for (int a = 0; a < adjList.Length; a++)
                {
                    if (!visited[adjList[a].C, adjList[a].R] && distance[adjList[a].C, adjList[a].R] == float.MaxValue && adjList[a].Type != TileType.Obstacle)
                    {
                        distance[adjList[a].C, adjList[a].R] = Vector2Int.Distance(new Vector2Int(adjList[a].C, adjList[a].R), config.EndPosition);
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