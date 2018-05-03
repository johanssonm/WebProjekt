using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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
            _weatherService.CallService(latitude, longitude);
            var weather = _weatherService.Weather;
            var icon = WeatherCodes.Instance.GetWeatherIcon(weather.WeatherInfo);

            return Content($@"<div><img src='{icon}' />{weather.Temp}</div>");

            return BadRequest("Something went wrong!");
        }
    }
}
