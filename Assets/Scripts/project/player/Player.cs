using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MrRunner
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private TextMeshProUGUI id;

        public RectTransform rectTransform { get => (RectTransform)transform; }

        public Color Color
        {
            get => image.color;
            set => image.color = value;
        }

        public string Id
        {
            get => id.text;
            set => id.text = value;
        }
    }
}