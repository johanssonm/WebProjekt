using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiLab.Controllers
{

    public class Ex1Controller : Controller
    {
        [Route("api/ex1")]
        public IActionResult HelloWorld()
        {
            return Ok("Hello world!");
        }

        public IActionResult Today()
        {
            return Ok(DateTime.Now.ToString());
        }

        public IActionResult Random()
        {
            List<String> list = new List<String>();

            list.Add("Snart är det fredag");
            list.Add("Snart är det torsdag");
            list.Add("Snart är det onsdag");
            list.Add("Snart är det tisdag");
            list.Add("Snart är det måndag");

            return Ok("Random");
        }
    }
}