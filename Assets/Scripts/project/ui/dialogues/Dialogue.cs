using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Dialogue : MonoBehaviour
{
    void Awake()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        transform.SetAsLastSibling();
    }
}
