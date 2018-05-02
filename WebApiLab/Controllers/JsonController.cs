using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiLab.Controllers
{

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

    }

    public class PersonWithAttributes
    {
        [Required(ErrorMessage = "Förnamn måste anges")]
        [StringLength(20)]
        [RegularExpression(@"^[A-Za-z]+$")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ålder måste anges")]
        [Range(0,120,ErrorMessage = "Ålder måste vara 0-120.")]
        public int? Age { get; set; }
    }


    [Route("person")]
    public class JsonController : Controller
    {
        [Route("AddPerson")]
        public IActionResult AddPerson(Person person)
        {
            string okmessage = String.Format("Personen {0} som är {1} lades till i databasen.", person.Name, person.Age);
            return Ok(okmessage);
        }
        [Route("AddPersonValidate")]
        public IActionResult AddPersonValidate(Person person)
        {
            if (string.IsNullOrEmpty(person.Name))
            {
                ModelState.AddModelError("Name", "Name Required");
            }

            if (person.Age == 0 || person.Age > 120)
            {
                ModelState.AddModelError("Age", "Please Enter Valid Age between 1-120");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string okmessage = String.Format("Personen {0} som är {1} lades till i databasen.", person.Name, person.Age);
            return Ok(okmessage);
        }


        [Route("AddPersonValidateAttribute")]
        public IActionResult AddPersonValidateAttribute(PersonWithAttributes person)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string okmessage = String.Format("Personen {0} som är {1} lades till i databasen.", person.Name, person.Age);
            return Ok(okmessage);
        }
    }

}