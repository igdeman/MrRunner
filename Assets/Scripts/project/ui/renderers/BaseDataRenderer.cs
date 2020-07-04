using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MrRunner
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class BaseDataRenderer<DataType> : MonoBehaviour
    {
        public abstract void Draw();
        public abstract void Clear();
       
        public virtual RectTransform rectTransform { get { return (RectTransform)transform; } }
        [SerializeField]
        protected DataType rendererData;
        public DataType RendererData
        {
            get { return rendererData; }
            set
            {
                rendererData = value;
                if(rendererData != null)
                    Draw();
                else
                    Clear();
            }
        }
    }
}
