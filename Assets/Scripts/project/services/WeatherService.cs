using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace MrRunner
{
    public static class WeatherService
    {
        public static Action<Weather> OnWeatherUpdate;

        private static bool isRunning;
        public static bool IsRunning{ get => isRunning; }
        public static int UpdateInterval = 1000*60*10;

        public static void Run()
        {
            if (!isRunning)
            {
                isRunning = true;
                RunUpdateLoop();
            }
        }

        public static void Stop()
        {
            isRunning = false;
        }

        private static async void RunUpdateLoop()
        {
            while (isRunning)
            {
                if (Application.internetReachability != NetworkReachability.NotReachable)
                {
                    try
                    {
                        Weather w = await WeatherApi.Get();
                        if (w != null)
                        {
                            Debug.Log($"Weather {w.id} received! It's {w.main}");
                            DataStore.Weather = w;
                            DataStore.ColorPalette = new ColorPalette(w);
                            OnWeatherUpdate?.Invoke(w);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Log("Something went wrong, unable to load weather!");
                        Debug.Log(ex.Message);
                    }
                }
                await Task.Delay(UpdateInterval);
            }
        }

    }
}
