using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrRunner
{
    public struct ColorPalette
    {
        public Color BackgroundColor;
        public Color EmptyTileColor;
        public Color ObstacleTileColor;
        public Color StartTileColor;
        public Color EndTileColor;

        public ColorPalette(Color backgroundColor, Color emptyTileColor, Color obstacleTileColor, Color startTileColor, Color endTileColor)
        {
            BackgroundColor = backgroundColor;
            EmptyTileColor = emptyTileColor;
            ObstacleTileColor = obstacleTileColor;
            StartTileColor = startTileColor;
            EndTileColor = endTileColor;
        }

        public ColorPalette(Weather weather)
        {
            BackgroundColor = new Color(0, .7f, 1, 1);
            EmptyTileColor = new Color(1, 1, 1, 1);
            ObstacleTileColor = new Color(0, 0, 1);
            StartTileColor = new Color(1, 0, 0);
            EndTileColor = new Color(0, 1, 0);

            if (weather.id > 199 && weather.id < 300)
            {
                BackgroundColor = new Color32(27, 56, 101, 255);
                EmptyTileColor = new Color(1, .9f, 1, .8f);
                ObstacleTileColor = new Color32(39, 39, 39, 255);
            }
            if (weather.id > 299 && weather.id < 400)
            {
                BackgroundColor = new Color32(80, 104, 128, 255);
                EmptyTileColor = new Color32(69, 105, 144, 255);
                ObstacleTileColor = new Color32(56, 82, 68, 255);
            }
            if (weather.id > 499 && weather.id < 600)
            {
                BackgroundColor = new Color32(0, 98, 162, 255);
                EmptyTileColor = new Color32(20, 107, 164, 255);
                ObstacleTileColor = new Color32(70, 55, 23, 255);
            }
            if (weather.id > 599 && weather.id < 700)
            {
                BackgroundColor = new Color32(154, 195, 222, 255);
                EmptyTileColor = new Color32(206, 223, 233, 255);
                ObstacleTileColor = new Color32(240, 250, 255, 255);
            }
            if (weather.id == 800)
            {
                BackgroundColor = new Color32(0, 227, 255, 255);
                EmptyTileColor = new Color32(203, 212, 107, 255);
                ObstacleTileColor = new Color32(85, 64, 41, 255);
            }
            if (weather.id > 800 && weather.id < 900)
            {
                BackgroundColor = new Color32(153, 216, 255, 255);
                EmptyTileColor = new Color32(0, 120, 27, 255);
                ObstacleTileColor = new Color32(92, 20, 30, 255);
            }
        }
    }
}
