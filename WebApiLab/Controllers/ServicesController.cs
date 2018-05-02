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

        [Route("location"), HttpPost]
        public IActionResult GetUserLocation(long longitude, long latitude)
        {
            var weather = _weatherService.Weather;

            return BadRequest("Something went wrong!");
        }
    }
}
