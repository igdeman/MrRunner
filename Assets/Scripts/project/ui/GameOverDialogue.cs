using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MrRunner
{
    public class GameOverDialogue : MonoBehaviour
    {
        [SerializeField]
        private Button NextButton;
        [SerializeField]
        private Button FinishButton;
        [SerializeField]
        private Button HomeButton;

        void Start()
        {
            NextButton.onClick.AddListener(()=> {
                EventDispatcher.DispatchEvent<UIEvent, GameObject>(new UIEvent(UIEvent.NEXT, gameObject));
            });
            FinishButton.onClick.AddListener(() => {
                EventDispatcher.DispatchEvent<UIEvent, GameObject>(new UIEvent(UIEvent.FINISH, gameObject));
            });
            HomeButton.onClick.AddListener(() => {
                EventDispatcher.DispatchEvent<UIEvent, GameObject>(new UIEvent(UIEvent.HOME, gameObject));
            });
        }
    }
}
