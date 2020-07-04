using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MrRunner
{
    public class GameStatsDialogue : MonoBehaviour
    {
        [SerializeField]
        private ScrollRect scrollRect;
        [SerializeField]
        private RectTransform content;
        [SerializeField]
        private SimulationResultRenderer resultRendererPrefab;
        [SerializeField]
        private Button backButton;
        private List<SimulationResultRenderer> renderers = new List<SimulationResultRenderer>();
        private float gap = 5f;

        void Start()
        {
            backButton.onClick.AddListener(()=> {
                EventDispatcher.DispatchEvent<UIEvent, GameObject>(new UIEvent(UIEvent.BACK, gameObject));
            });
        }

        void OnEnable()
        {
            content.sizeDelta = new Vector2(
                content.sizeDelta.x,
                resultRendererPrefab.rectTransform.rect.height*DataStore.Simulations.Count + gap*(DataStore.Simulations.Count-1)
                );
            for (int i = 0; i < DataStore.Simulations.Count; i++)
            {
                if (renderers.Count <= i)
                    renderers.Add(Instantiate<GameObject>(resultRendererPrefab.gameObject).GetComponent<SimulationResultRenderer>());
                SimulationResultRenderer r = renderers[renderers.Count - 1];
                r.transform.SetParent(content, false);
                r.transform.localPosition = new Vector3(
                    r.transform.localPosition.x,
                    i * -(resultRendererPrefab.rectTransform.rect.height + gap),
                    r.transform.localPosition.z
                    );
                r.RendererData = DataStore.Simulations[i];
            }
        }
    }
}
