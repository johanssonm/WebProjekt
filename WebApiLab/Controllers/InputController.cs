using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiLab.Controllers
{
    public class Breakfast
    {
        public string Food { get; set; }

    }

    public class Background
    {
        public string Color { get; set; }

    }


    [Route("api/input")]
    public class InputController : Controller
    {

        [Route("Breakfast")]
        public IActionResult Breakfast(string input)
        {
            Breakfast breakfast = new Breakfast();

            breakfast.Food = Request.Query["Breakfast"]; ;

            return Ok(breakfast.Food);
        }

        [Route("Background")]
        public IActionResult Background(string input)
        {
            var background = Request.Query["Background"]; ;

            return Content("<html><body style=\"background-color:" + background + ";\"><p></p></body></html>", "text/html");
        }

        [Route("Square")]
        public IActionResult Square(string input)
        {
            var n = Convert.ToInt32(Request.Query["Square"]); ;

            var r = n * n;

            return Content(n + " * " + n + " = " + r, "text/html");

        }

        [Route("Enumerate")]
        public IActionResult Enumerate(int start, int end)
        {
            int[] range = Enumerable.Range(start, end + 1 - start).ToArray();

            return Content(string.Join(", ", range), "text/html");

        }

        [Route("Chocolatecheck")]
        public IActionResult Enumerate(int people)
        {
            int chocolate = 25;

            int result = chocolate / people;

            string message = String.Format("Det blir {0} bitar per person om det är {1} personer.", result, people);

            if (result != 0)
            {
                return Ok(message);
            }

            else
            {
                return Content(HttpStatusCode.BadRequest.ToString());

            }
        }

        [Route("Checkorder")]
        public IActionResult Checkorder(string order)
        {
            string pattern = @"(^[a-zA-Z]{2}-[0-9]{4})";

            bool match = Regex.IsMatch(order, pattern);

            string okMessage = String.Format("{0} är ordernummer.", order);
            string okNumber = String.Format("{0} är ordernummer.", order);

            string badrequestMessage = String.Format("{0} är inte ett ordernummer.", order);
            string badNumber = String.Format("{0} är inte ett giltigt ordernumber.", order);

            if (match == true)
            {
                var tmpstring = order.Split('-');

                var n = Convert.ToInt32(tmpstring[1]);


                if (n <= 3000)
                {
                    return Ok(okNumber);
                }

                if (n > 3000)
                {
                    return Ok(badNumber);
                }

            }

            if (match == false)
            {
                return Ok(badrequestMessage);
            }


            else
            {
                return Content(HttpStatusCode.BadRequest.ToString());

            }
        }


        [Route("Checkusername")]
        public IActionResult Username(string username)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            var titlecase = ti.ToTitleCase(username);

            var okNames = new List<string>()
            {
                "Peter",
                "Chris",
                "Brian"
            };

            var badNames = new List<string>()
            {
                "Stewie",
                "Belsebub",
            };

            var badRequest = "meg";


            string oknamesMessage = String.Format("{0} är ett ok namn.", titlecase);

            string badnamesMessage = String.Format("{0} är inte ett ok namn.", titlecase);

            string badrequestMessage = String.Format("{0} är inte tillåtet.", titlecase);

            var isokName = okNames.Contains(titlecase);
            var isbadName = badNames.Contains(titlecase);

            if (username.ToLower() == badRequest)
            {
                return Ok(badrequestMessage);
            }

            if (isokName == true)
            {
                return Ok(oknamesMessage);
            }

            if (isbadName == true)
            {
                return Ok(badnamesMessage);
            }


            else
            {
                return Content(HttpStatusCode.NotFound.ToString());

            }
        }

        [Route("Checkbox")]
            public IActionResult Checkbox(bool checkbox)
            {
                string okRequest = "OK";

                string badRequest = "Ej ok";


                if (checkbox == true)
                {
                    return Ok(okRequest);
                }

                    return Ok(badRequest);
            }
    }
    }
