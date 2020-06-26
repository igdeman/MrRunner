using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MrRunner
{
    public class GameView : MonoBehaviour
    {
        public Board Board;
        public GameOverDialogue GameOverDialogue;
        public GameStatsDialogue GameStatsDialogue;
        public Button GoButton;

        private void Awake()
        {
            //GameOverDialogue.gameObject.SetActive(false);
            //GameStatsDialogue.gameObject.SetActive(false);
        }
    }
}
