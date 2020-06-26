using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationResult
{
    public int RunNumber;
    public Vector2Int BoardSize;
    public int TotalObstacles;
    public List<string> Algorithms = new List<string>();
    public List<int> VisitsCount = new List<int>();
    public List<float> StartTime = new List<float>();
    public List<float> CompleteTime = new List<float>();
}
