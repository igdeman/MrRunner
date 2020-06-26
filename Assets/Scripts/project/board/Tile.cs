using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MrRunner
{
    public enum TileType { Empty, Obstacle, Start, End, Path }

    [RequireComponent(typeof(RectTransform))]
    public class Tile : MonoBehaviour
    {
        public RectTransform rectTransform { get => (RectTransform)transform; }
        public int C;
        public int R;

        [SerializeField]
        private Image fill;
        private List<GameObject> visits = new List<GameObject>();
        [SerializeField]
        private TileType type = TileType.Empty;
        private float outlineTicknes = 5;

        public TileType Type
        {
            get => type;
            set
            {
                type = value;
                switch (type)
                {
                    case TileType.Empty:
                        fill.color = new Color(1, 1, 1);
                        break;
                    case TileType.Obstacle:
                        fill.color = new Color(0, 0, 1);
                        break;
                    case TileType.Start:
                        fill.color = new Color(1, 0, 0);
                        break;
                    case TileType.End:
                        fill.color = new Color(0, 1, 0);
                        break;
                    case TileType.Path:
                        fill.color = new Color(1, 1, 0);
                        break;
                }
            }
        }

        public void Clear()
        {
            Type = TileType.Empty;
            for (int i = 0; i < visits.Count; i++)
                Destroy(visits[i]);
            visits.Clear();
        }

        public void Visit(Color c)
        {
            if (type == TileType.Empty)
            {
                GameObject go = new GameObject();
                go.AddComponent<Image>();
                go.name = "TileOutline";
                visits.Add(go);

                float offset = outlineTicknes * 3f * visits.Count;
                RectTransform rt = go.GetComponent<RectTransform>();
                rt.SetParent(transform, false);
                rt.sizeDelta = new Vector2(rectTransform.sizeDelta.x - offset, rectTransform.sizeDelta.y - offset);
                rt.SetAsLastSibling();
                Outline o = go.AddComponent<Outline>();
                o.effectDistance = new Vector2(outlineTicknes, outlineTicknes);
                o.effectColor = c;
            }
        }
    }
}
