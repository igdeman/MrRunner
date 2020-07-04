using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrRunner
{
    public class WeatherRenderer : BaseDataRenderer<Weather>
    {
        [SerializeField]
        private GameObject sun;
        [SerializeField]
        private CloudRenderer[] clouds;

        void Awake()
        {
            Clear();
        }

        void Start()
        {
            if (rendererData != null)
                Draw();
        }

        public override void Draw()
        {
            if ((rendererData.id > 499 && rendererData.id < 505) || (rendererData.id > 799 && rendererData.id < 802))
            {
                DrawSun();
            }
            if (rendererData.id != 800)
            {
                DrawClouds();
            }

        }

        public override void Clear()
        {
            sun.SetActive(false);
            for (int i = 0; i < clouds.Length; i++)
            {
                clouds[i].Clear();
                clouds[i].gameObject.SetActive(false);
            }
        }

        void DrawSun()
        {
            sun.SetActive(true);
        }

        void DrawClouds()
        {
            for (int i = 0; i < clouds.Length; i++)
            {
                clouds[i].gameObject.SetActive(true);
                clouds[i].RendererData = rendererData.id;
            }
        }

    }
}
