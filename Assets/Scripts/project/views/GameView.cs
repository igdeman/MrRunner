using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MrRunner
{
    public class GameView : MonoBehaviour
    {
        public Board Board;
        public WeatherRenderer WeatherRenderer;
        public GameOverDialogue GameOverDialogue;
        public GameStatsDialogue GameStatsDialogue;
        public Button GoButton;
    }
}
