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
        public IActionResult GetUserLocation(long longitude, long latitude)
        {
            var weather = _weatherService.Weather;
            var icon = WeatherCodes.Instance.GetWeatherIcon(weather.WeatherInfo);

            return Content($@"<div><img src='{icon}'</div>");

            return BadRequest("Something went wrong!");
        }
    }
}
