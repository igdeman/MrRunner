using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MrRunner
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private GameView view;
        [SerializeField]
        private Player playerPrefab;
        [SerializeField]
        private Config config;
        private Tile[,] tiles;
        private List<Player> players = new List<Player>();
        private List<AbstractAlgorithm> algorithms = new List<AbstractAlgorithm>();
        private int completeCounter = 0;
        private SimulationResult stats;
        
        void Start()
        {
            config = Instantiate<Config>(config);
            Create();
            view.GoButton.onClick.AddListener(()=> {
                Run();
            });
            AddEventListener();
        }

        void AddEventListener()
        {
            EventDispatcher.AddEventListener<AlgorithmEvent, AbstractAlgorithm>(AlgorithmEvent.MARK_VISITED, AlgorithmEventHandler);
            EventDispatcher.AddEventListener<AlgorithmEvent, AbstractAlgorithm>(AlgorithmEvent.MOVE_PLAYER, AlgorithmEventHandler);
            EventDispatcher.AddEventListener<AlgorithmEvent, AbstractAlgorithm>(AlgorithmEvent.COMPLETE, AlgorithmEventHandler);

            EventDispatcher.AddEventListener<UIEvent, GameObject>(UIEvent.NEXT, UIEventEventHandler);
            EventDispatcher.AddEventListener<UIEvent, GameObject>(UIEvent.FINISH, UIEventEventHandler);
            EventDispatcher.AddEventListener<UIEvent, GameObject>(UIEvent.HOME, UIEventEventHandler);
            EventDispatcher.AddEventListener<UIEvent, GameObject>(UIEvent.BACK, UIEventEventHandler);
        }

        void RemoveEventListeners()
        {
            EventDispatcher.RemoveEventListener<AlgorithmEvent>(AlgorithmEvent.MARK_VISITED, AlgorithmEventHandler);
            EventDispatcher.RemoveEventListener<AlgorithmEvent>(AlgorithmEvent.MOVE_PLAYER, AlgorithmEventHandler);
            EventDispatcher.RemoveEventListener<AlgorithmEvent>(AlgorithmEvent.COMPLETE, AlgorithmEventHandler);

            EventDispatcher.RemoveEventListener<UIEvent>(UIEvent.NEXT, UIEventEventHandler);
            EventDispatcher.RemoveEventListener<UIEvent>(UIEvent.FINISH, UIEventEventHandler);
            EventDispatcher.RemoveEventListener<UIEvent>(UIEvent.HOME, UIEventEventHandler);
            EventDispatcher.RemoveEventListener<UIEvent>(UIEvent.BACK, UIEventEventHandler);
        }

        void Create()
        {
            DataStore.Simulations.Add(new SimulationResult());
            tiles = view.Board.Create(config);

            algorithms.Add(new Dijkstras(config));
            algorithms.Add(new Greedy(config));

            view.GoButton.gameObject.SetActive(true);
            stats = DataStore.Simulations[DataStore.Simulations.Count-1];
            stats.RunNumber = DataStore.Simulations.Count;
            stats.BoardSize = new Vector2Int(config.TotalColumns, config.TotalRows);
            stats.TotalObstacles = view.Board.TotalObstacles;

            for (int i = 0; i < algorithms.Count; i++)
            {
                Player p = Instantiate<GameObject>(playerPrefab.gameObject).GetComponent<Player>();
                p.transform.SetParent(view.transform, false);
                p.transform.localPosition = tiles[config.StartPosition.x, config.StartPosition.y].transform.localPosition;
                p.rectTransform.sizeDelta = new Vector2(view.Board.TileSize, view.Board.TileSize);
                p.gameObject.SetActive(false);
                p.Id = $"P{i + 1}";
                p.Color = new Color32(
                    (byte)UnityEngine.Random.Range(0, 255),
                    (byte)UnityEngine.Random.Range(0, 255),
                    (byte)UnityEngine.Random.Range(0, 255),
                    255
                    );
                players.Add(p);
                stats.Algorithms.Add(algorithms[i].Name);
                stats.StartTime.Add(0);
                stats.CompleteTime.Add(0);
                stats.VisitsCount.Add(0);
            }
        }

        void Run()
        {
            view.GoButton.gameObject.SetActive(false);
            for (int i = 0; i < algorithms.Count; i++)
            {
                algorithms[i].Run(tiles);
                players[i].gameObject.SetActive(true);
                players[i].transform.localPosition = tiles[config.StartPosition.x, config.StartPosition.y].transform.localPosition;
                stats.StartTime[i] = Time.realtimeSinceStartup;
            }
        }

        void Clear()
        {
            view.Board.Clear();
            for (int i = 0; i < algorithms.Count; i++)
            {
                Destroy(players[i].gameObject);
                algorithms[i].Destroy();
            }
            players.Clear();
            algorithms.Clear();
            completeCounter = 0;
        }

        void AlgorithmEventHandler(AlgorithmEvent e)
        {
            Player p = GetPlayer(e.Target);
            switch (e.Name)
            {
                case AlgorithmEvent.MARK_VISITED:
                    tiles[e.Position.x, e.Position.y].Visit(p.Color);
                    stats.VisitsCount[algorithms.IndexOf(e.Target)]++;
                    break;
                case AlgorithmEvent.MOVE_PLAYER:
                    MovePlayer(p, e.Position);
                    break;
                case AlgorithmEvent.COMPLETE:
                    completeCounter++;
                    stats.CompleteTime[algorithms.IndexOf(e.Target)] = Time.realtimeSinceStartup;
                    MovePlayer(p, e.Position);
                    if (completeCounter >= algorithms.Count)
                        view.GameOverDialogue.gameObject.SetActive(true);
                    break;
            }
        }

        void UIEventEventHandler(UIEvent e)
        {
            switch (e.Name)
            {
                case UIEvent.NEXT:
                    view.GameOverDialogue.gameObject.SetActive(false);
                    config.TotalObstacles++;
                    Clear();
                    Create();
                    break;
                case UIEvent.HOME:
                    RemoveEventListeners();
                    SceneManager.LoadScene("Entry");
                    break;
                case UIEvent.BACK:
                    view.GameOverDialogue.gameObject.SetActive(true);
                    view.GameStatsDialogue.gameObject.SetActive(false);
                    break;
                case UIEvent.FINISH:
                    view.GameOverDialogue.gameObject.SetActive(false);
                    view.GameStatsDialogue.gameObject.SetActive(true);
                    break;
            }
        }

        void MovePlayer(Player player, Vector2Int position)
        {
            player.transform.localPosition = tiles[position.x, position.y].transform.localPosition;
        }

        Player GetPlayer(AbstractAlgorithm algorithm)
        {
            for (int i = 0; i < algorithms.Count; i++)
            {
                if (algorithm.GetType() == algorithms[i].GetType())
                {
                    return players[i];
                }
            }
            return null;
        }
    }

}