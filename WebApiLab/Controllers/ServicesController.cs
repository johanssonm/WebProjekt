using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApiLab.Controllers
{
    [Route("api/services")]
    public class ServicesController : Controller
    {
        private readonly IWeatherService _weatherService;

        public ServicesController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [Route("location"), HttpGet]
        public IActionResult GetUserLocation(double latitude, double longitude)
        {
            //TODO Gör ett objekt av positionen istället för att använda typvärden.
            _weatherService.CallService(latitude, longitude);
            var weather = _weatherService.Weather;
            var icon = WeatherCodes.Instance.GetWeatherIcon(weather.WeatherInfo);

            return Content($@"<img src='{icon}' />{weather.Temp}");

            return BadRequest("Something went wrong!");
        }
    }
}
