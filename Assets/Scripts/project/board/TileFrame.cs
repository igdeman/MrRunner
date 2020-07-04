using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TileFrame : MonoBehaviour
{
    public RectTransform rectTransform { get => (RectTransform)transform; }

    private Color color;
    public Color Color
    {
        get => color;
        set
        {
            color = value;
            Left.color = color;
            Top.color = color;
            Right.color = color;
            Bottom.color = color;
        }
    }

    private float ticknes;
    public float Ticknes
    {
        get => ticknes;
        set
        {
            ticknes = value;
            Left.rectTransform.sizeDelta = new Vector2(ticknes, Left.rectTransform.sizeDelta.y);
            Top.rectTransform.sizeDelta = new Vector2(Top.rectTransform.sizeDelta.x, ticknes);
            Right.rectTransform.sizeDelta = new Vector2(ticknes, Right.rectTransform.sizeDelta.y);
            Bottom.rectTransform.sizeDelta = new Vector2(Bottom.rectTransform.sizeDelta.x, ticknes);
        }
    }

    [SerializeField]
    private Image Left;
    [SerializeField]
    private Image Top;
    [SerializeField]
    private Image Right;
    [SerializeField]
    private Image Bottom;
}
