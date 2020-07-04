using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrRunner
{
    public class CloudRenderer : BaseDataRenderer<int>
    {
        [SerializeField]
        private GameObject thunders;
        [SerializeField]
        private ParticleSystem rain;
        [SerializeField]
        private ParticleSystem snow;

        public override void Draw()
        {
            if (rendererData > 199 && rendererData < 300)
            {
                // Draw Tunder
                thunders.SetActive(true);
            }
            if (rendererData > 599 && rendererData < 700)
            {
                // Draw Snow
                snow.gameObject.SetActive(true);
            }
            switch (rendererData)
            {
                case 200:
                case 201:
                case 202:
                case 230:
                case 231:
                case 232:
                case 310:
                case 311:
                case 312:
                case 313:
                case 314:
                case 321:
                case 500:
                case 501:
                case 502:
                case 503:
                case 504:
                case 511:
                case 520:
                case 521:
                case 522:
                case 532:
                    // Draw Rain
                    rain.gameObject.SetActive(true);
                    break;
            }
        }

        public override void Clear()
        {
            thunders.SetActive(false);
            rain.gameObject.SetActive(false);
            snow.gameObject.SetActive(false);
        }
    }
}
