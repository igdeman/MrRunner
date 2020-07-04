using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MrRunner
{
    [RequireComponent(typeof(RectTransform))]
    public class SimulationResultRenderer : BaseDataRenderer<SimulationResult>
    {
        [SerializeField]
        private TextMeshProUGUI LeftTextField;
        [SerializeField]
        private TextMeshProUGUI RightTextField;

        void Start()
        {
            if (rendererData != null)
                Draw();
        }

        public override void Draw()
        {
            LeftTextField.rectTransform.sizeDelta = new Vector2(
                rectTransform.rect.width/2f,
                rectTransform.rect.height
                );
            RightTextField.rectTransform.sizeDelta = new Vector2(
                rectTransform.rect.width / 2f,
                rectTransform.rect.height
                );

            string ls = $"Run Number:{rendererData.RunNumber}";
            ls += $"\nBoard size: {rendererData.BoardSize.x} x {rendererData.BoardSize.y}";
            ls += $"\nTotal obstacles: {rendererData.TotalObstacles}\n";

            string a = "Algorithms used:";
            string v = "\nVisits count:";
            string d = "\nRun durarion:";

            for (int i = 0; i < rendererData.Algorithms.Count; i++)
            {
                a += $"\n{rendererData.Algorithms[i]}";
                v += $"\n{rendererData.Algorithms[i]}: {rendererData.VisitsCount[i]}";
                d += $"\n{rendererData.Algorithms[i]}: {rendererData.CompleteTime[i] - rendererData.StartTime[i]}s ";
            }

            a += "\n";
            v += "\n";
            d += "\n";

            LeftTextField.text = ls;
            RightTextField.text = a + v + d;
        }

        public override void Clear()
        {

        }
    }
}
