using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrRunner
{
    public class UIEvent : Event<GameObject>
    {
        public const string NEXT = "on_game_next_level";
        public const string FINISH = "on_game_finish";
        public const string HOME = "on_game_home";
        public const string BACK = "on_game_back";
        public UIEvent(string name, GameObject target):base(name, target) { }
    }
}
