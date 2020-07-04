using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MrRunner
{
    public class GameOptionsDialogue : MonoBehaviour
    {
        [SerializeField]
        Config config;
        [SerializeField]
        private TMP_InputField N;
        [SerializeField]
        private TMP_InputField Y;
        [SerializeField]
        private TMP_InputField StartX;
        [SerializeField]
        private TMP_InputField StartY;
        [SerializeField]
        private TMP_InputField EndX;
        [SerializeField]
        private TMP_InputField EndY;
        [SerializeField]
        private TMP_InputField StepDelay;
        [SerializeField]
        private Button CloseButton;


        void Awake()
        {
            N.onValueChanged.AddListener((string value) => {
                if (isValid(N.text))
                {
                    config.TotalColumns = config.TotalRows = Convert.ToInt16(N.text);
                    N.text = config.TotalColumns.ToString();
                }
            });
            N.onDeselect.AddListener((string value) => {
                if (config.TotalColumns < 2)
                {
                    config.TotalColumns = config.TotalRows = 2;
                    N.text = config.TotalColumns.ToString();
                }
                if (config.TotalObstacles > config.TotalColumns * config.TotalRows)
                {
                    config.TotalObstacles = config.TotalColumns * config.TotalRows;
                    Y.text = config.TotalObstacles.ToString();
                }
            });

            Y.onValueChanged.AddListener((string value) => {
                if (isValid(Y.text))
                {
                    config.TotalObstacles = Mathf.Min(Convert.ToInt16(Y.text), config.TotalColumns * config.TotalRows);
                    Y.text = config.TotalObstacles.ToString();
                }
            });

            StartX.onValueChanged.AddListener((string value) => {
                if (isValid(StartX.text))
                {
                    config.StartPosition = new Vector2Int(Mathf.Min(Convert.ToInt16(StartX.text), config.TotalColumns-1), config.StartPosition.y);
                    StartX.text = config.StartPosition.x.ToString();
                }
            });
            StartY.onValueChanged.AddListener((string value) => {
                if (isValid(StartY.text))
                {
                    config.StartPosition = new Vector2Int(config.StartPosition.x, Mathf.Min(Convert.ToInt16(StartY.text), config.TotalRows - 1));
                    StartY.text = config.StartPosition.y.ToString();
                }
            });

            EndX.onValueChanged.AddListener((string value) => {
                if (isValid(EndX.text))
                {
                    config.EndPosition = new Vector2Int(Mathf.Min(Convert.ToInt16(EndX.text), config.TotalColumns - 1), config.EndPosition.y);
                    EndX.text = config.EndPosition.x.ToString();
                }
            });
            EndY.onValueChanged.AddListener((string value) => {
                if (isValid(EndY.text))
                {
                    config.EndPosition = new Vector2Int(config.EndPosition.x, Mathf.Min(Convert.ToInt16(EndY.text), config.TotalRows - 1));
                    EndY.text = config.EndPosition.y.ToString();
                }
            });

            StepDelay.onValueChanged.AddListener((string value) => {
                if (isValid(StepDelay.text))
                {
                    config.StepDelay = Convert.ToInt16(StepDelay.text);
                }
            });

            CloseButton.onClick.AddListener(()=> {
                gameObject.SetActive(false);
            });
        }

        private void OnEnable()
        {
            N.text = config.TotalColumns.ToString();
            Y.text = config.TotalObstacles.ToString();
            StartX.text = config.StartPosition.x.ToString();
            StartY.text = config.StartPosition.y.ToString();
            EndX.text = config.EndPosition.x.ToString();
            EndY.text = config.EndPosition.y.ToString();
            StepDelay.text = config.StepDelay.ToString();
        }

        bool isValid(string s)
        {
            if (s != null && s != "")
                return true;
            return false;
        }
    }
}
