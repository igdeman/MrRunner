using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MrRunner
{
    public enum TileType { Empty, Obstacle, Start, End }

    [RequireComponent(typeof(RectTransform))]
    public class Tile : MonoBehaviour
    {
        public RectTransform rectTransform { get => (RectTransform)transform; }
        public int C;
        public int R;
        public ColorPalette ColorPalette;

        [SerializeField]
        private Image fill;
        [SerializeField]
        TileFrame tileFramePrefab;
        private List<GameObject> visits = new List<GameObject>();
        [SerializeField]
        private TileType type = TileType.Empty;
        private float outlineTicknes = 9;

        void Awake()
        {
            Type = type;
        }

        public TileType Type
        {
            get => type;
            set
            {
                type = value;
                switch (type)
                {
                    case TileType.Empty:
                        fill.color = ColorPalette.EmptyTileColor;
                        break;
                    case TileType.Obstacle:
                        fill.color = ColorPalette.ObstacleTileColor;
                        break;
                    case TileType.Start:
                        fill.color = ColorPalette.StartTileColor;
                        break;
                    case TileType.End:
                        fill.color = ColorPalette.EndTileColor;
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
                TileFrame tf = Instantiate<GameObject>(tileFramePrefab.gameObject).GetComponent<TileFrame>();
                tf.Color = c;
                tf.Ticknes = outlineTicknes;
                tf.gameObject.name = "TileOutline";
                visits.Add(tf.gameObject);

                float offset = outlineTicknes * visits.Count * 2;
                tf.rectTransform.SetParent(transform, false);
                tf.rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x - offset, rectTransform.sizeDelta.y - offset);
            }
        }
    }
}
