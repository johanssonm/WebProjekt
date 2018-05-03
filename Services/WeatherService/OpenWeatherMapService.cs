using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;

namespace Services
{
    public class OpenWeatherMapService : IWeatherService
    {
        private string _apiKey = "8f223e25c904271c4cafd522189e725b";
        private HttpClient _client;
        private string _apiLink;
        public IWeather Weather { get; private set; }

        public OpenWeatherMapService()
        {
            Weather = new Weather();

            _client = new HttpClient
            {
                BaseAddress =
                    new Uri("http://api.openweathermap.org")
            };
        }
        // api.openweathermap.org/data/2.5/weather?lat=57.708944599999995&lon=11.966970799999999

        // x-api-key

        public void CallService(double latitude, double longitude)
        {


            _apiLink = "/data/2.5/weather?lat=" + $"{latitude.ToString(new CultureInfo("en-US").NumberFormat)}" + "&lon=" + $"{longitude.ToString(new CultureInfo("en-US").NumberFormat)}";

            // Add a new Request Message
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, _apiLink);

            // Add our custom headers
            requestMessage.Headers.Add("x-api-key", _apiKey);

            PopulateWeatherWithCoords(requestMessage);
        }


        private async void PopulateWeatherWithCoords(HttpRequestMessage requestMessage)
        {
            HttpResponseMessage response = _client.SendAsync(requestMessage).Result;

            var contents = await response.Content.ReadAsStringAsync();

            var root = JsonConvert.DeserializeObject<Rootobject>(contents);
            Weather.SetTemp(root.main.temp - 273.15);
            Weather.WeatherInfo = root.weather[0].id;
        }
    }



    public class Rootobject
    {
        public Coord coord { get; set; }
        public JsonWeather[] weather { get; set; }
        public string _base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }

    public class Coord
    {
        public float lon { get; set; }
        public float lat { get; set; }
    }

    public class Main
    {
        public float temp { get; set; }
        public float pressure { get; set; }
        public int humidity { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
    }

    public class Wind
    {
        public float speed { get; set; }
        public float deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public float message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class JsonWeather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
}