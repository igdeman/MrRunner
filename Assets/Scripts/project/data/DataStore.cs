using System.Collections.Generic;
using UnityEngine;

namespace MrRunner
{
    public static class DataStore
    {
        public static List<SimulationResult> Simulations = new List<SimulationResult>();
        public static Weather Weather;
        public static ColorPalette ColorPalette = new ColorPalette(
            new Color(0, .7f, 1, 1),
            new Color(1, 1, 1, 1),
            new Color(0, 0, 1),
            new Color(1, 0, 0),
            new Color(0, 1, 0)
            );
    }
}
