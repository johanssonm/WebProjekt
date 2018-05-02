using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiLab.Controllers
{
    [Route("jquery")]
    public class CalculatorController : Controller
    {

        [Route("Calc")]
        public IActionResult Calc(int? n)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (n == 0 || n >= 100)
            {
                ModelState.AddModelError("Number", "Valid numbers are between 0-100.");
                return BadRequest(ModelState);
            }

            var result = n * n;

            return Json(new
            {
                success = true,
                Result = result
            });
        }

        [Route("ValidateUsername")]
        public IActionResult ValidateUsername(string username)
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

            var badRequest = "Meg";


            string oknamesMessage = String.Format("{0} är ett ok namn.", titlecase);

            string badnamesMessage = String.Format("{0} är inte ett ok namn.", titlecase);

            string badrequestMessage = String.Format("{0} är inte tillåtet.", titlecase);

            var isokName = okNames.Contains(titlecase);
            var isbadName = badNames.Contains(titlecase);

            if (badRequest == titlecase)
            {
                ModelState.AddModelError("Username", badrequestMessage);
                return BadRequest(ModelState);
            }

            //if (isokName == true)
            //{
            //    return Ok(oknamesMessage);
            //}

            //if (isbadName == true)
            //{
            //    return Ok(badnamesMessage);
            //}

            return Json(new
            {
                success = true,
                Username = oknamesMessage
            });
        }
    }
}