using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiLab.Controllers
{
    [Route("api/ex0")]
    public class Ex0Controller : Controller
    {

        [Route("AddPlanet")]
        public IActionResult AddPlanet()
        {
            List<string> list = new List<string>();
            List<string> planeter = new List<string>();
            List<string> storlekar = new List<string>();

            string formContent = "";

            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                formContent = reader.ReadToEndAsync().Result;

                var tmpString = formContent.Split('&');



                foreach (var newstring in tmpString)
                {
                    list.Add(newstring);
                }

                foreach (var tmpstring in list)
                {
                    if (tmpstring.ToLower().StartsWith("planet"))
                    {
                        var newstring = tmpstring.Split('=');

                        planeter.Add(newstring[1]);

                    }

                    else if (tmpstring.ToLower().StartsWith("size"))
                    {
                        var newstring = tmpstring.Split('=');

                        storlekar.Add(newstring[1]);

                    }
                }

            }



            return Content(planeter[0] + "\n" + storlekar[0]);

        }

        [Route("GetPlanet")]
        public IActionResult GetPlanet()
        {
            List<string> planeter = new List<string>()
            {
                "Merkurius",
                "Venus",
                "Tellus",
                "Mars",
                "Jupiter",
                "Saturnus",
                "Uranus",
                "Neptunus",
                "Pluto"

            };

            var storlekar = new List<string>();

            string formContent = "";

            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                formContent = reader.ReadToEndAsync().Result;

                var tmpString = formContent.Split('&');



                foreach (var newstring in tmpString)
                {
                    planeter.Add(newstring);
                }

                foreach (var tmpstring in planeter)
                {
                    if (tmpstring.ToLower().StartsWith("planet"))
                    {
                        var newstring = tmpstring.Split('=');

                        planeter.Add(newstring[1]);

                    }

                    else if (tmpstring.ToLower().StartsWith("size"))
                    {
                        var newstring = tmpstring.Split('=');

                        storlekar.Add(newstring[1]);

                    }
                }

            }



            return Content(planeter[0] + "\n" + storlekar[0]);

        }
    }
}