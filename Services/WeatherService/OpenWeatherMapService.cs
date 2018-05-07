using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;

namespace Services
{
    public class OpenWeatherMapService : IWeatherService
    {
        private string _apiKey = "8f223e25c904271c4cafd522189e725b";
        private readonly HttpClient _client;
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

        public void CallService(double latitude, double longitude)
        {
            _apiLink = "/data/2.5/weather?lat=" + $"{latitude.ToString(new CultureInfo("en-US").NumberFormat)}"
                                                + "&lon=" + $"{longitude.ToString(new CultureInfo("en-US").NumberFormat)}";

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, _apiLink);

            requestMessage.Headers.Add("x-api-key", _apiKey);

            PopulateWeatherWithCoords(requestMessage);
        }


        private async void PopulateWeatherWithCoords(HttpRequestMessage requestMessage)
        {
            HttpResponseMessage response = _client.SendAsync(requestMessage).Result;

            var contents = await response.Content.ReadAsStringAsync();

            var root = JsonConvert.DeserializeObject<Rootobject>(contents);
            Weather.SetTemp(root.main.temp - 273.15); //TODO Magic numbers
            Weather.WeatherInfo = root.weather[0].id;
        }
    }
}