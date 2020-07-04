using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using Leguar.TotalJSON;

namespace MrRunner
{
    public static class WeatherApi
    {
        private static string apiKey = "4ad0ab1e3bf74078ff96cca9506e0bed";
        public async static Task<Weather> Get()
        {
            if (Input.location.isEnabledByUser)
            {
                Vector2 location = await GetLocation();
                return await Get(location.y, location.x);
            }
            return null;
        }
        public async static Task<Weather> Get(float lat, float lon)
        {
            string uri = $"api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}";
            UnityWebRequest weatherResponse = await SendRequest(uri);
            try
            {
                JSON json = JSON.ParseString(weatherResponse.downloadHandler.text);
                DeserializeSettings ds = new DeserializeSettings();
                ds.RequireAllFieldsArePopulated = false;
                Weather w = json.GetJArray("weather").GetJSON(0).Deserialize<Weather>(ds);
                return w;
            }
            catch (Exception ex)
            {
                Debug.Log($"Something went wrong, unable to load weather!\n{ex.Message}");
            }
            return null;
        }

        private async static Task<Vector2> GetLocation()
        {
            Vector2 result = Vector2.zero;
            Input.location.Start(1000);
            while (Input.location.status == LocationServiceStatus.Initializing)
                await Task.Yield();
            result.x = Input.location.lastData.longitude;
            result.y = Input.location.lastData.latitude;
            Input.location.Stop();
            return result;
        }

        private static async Task<UnityWebRequest> SendRequest(string uri)
        {
            UnityWebRequest request = UnityWebRequest.Get(uri);
            request.SendWebRequest();
            while (!request.isDone)
                await Task.Yield();
            return request;
        }
    }
}
