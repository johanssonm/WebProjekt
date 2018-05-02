using Microsoft.AspNetCore.Mvc;

namespace WebApiLab.Controllers
{
    [Route("api/services")]
    public class ServicesController : Controller
    {
        [Route("location"), HttpPost]
        public IActionResult GetUserLocation(long longitude, long latitude)
        {


            return BadRequest("Something went wrong!");
        }
    }
}
