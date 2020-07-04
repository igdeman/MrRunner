using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Leguar.TotalJSON;

namespace MrRunner
{
    public class EntryController : MonoBehaviour
    {
        [SerializeField]
        private EntryView view;

        void Awake()
        {
            if (!WeatherService.IsRunning)
                WeatherService.Run();

            // Test
            //DataStore.Weather = new Weather();
            //DataStore.Weather.id = 800;
            //DataStore.ColorPalette = new ColorPalette(DataStore.Weather);

            view.PlayButton.onClick.AddListener(()=> {
                view.PlayButton.enabled = false;
                view.OptionsButton.enabled = false;
                SceneManager.LoadScene("Game");
            });

            view.OptionsButton.onClick.AddListener(() => {
                view.Dialogue.gameObject.SetActive(true);
            });
        }
    }
}
