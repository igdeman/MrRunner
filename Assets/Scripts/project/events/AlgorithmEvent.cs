using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrRunner
{
    public class AlgorithmEvent : Event<AbstractAlgorithm>
    {
        public const string MOVE_PLAYER = "on_move_player";
        public const string MARK_VISITED = "on_mark_visited";
        public const string COMPLETE = "on_search_complete";

        private Vector2Int position;
        public Vector2Int Position { get => position; }

        public AlgorithmEvent(string name, AbstractAlgorithm target, Vector2Int position) : base(name, target)
        {
            this.position = position;
        }
    }
}