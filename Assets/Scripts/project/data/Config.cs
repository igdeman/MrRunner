using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrRunner
{
    [CreateAssetMenu(fileName = "Config", menuName = "MrRunner/Config", order = 1)]
    public class Config : ScriptableObject
    {
        public int TotalColumns = 1;
        public int TotalRows = 1;
        public int TotalObstacles = 0;
        public int StepDelay;
        public Vector2Int StartPosition;
        public Vector2Int EndPosition;
    }
}
