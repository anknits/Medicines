using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MedicinesApp.Models;
using Newtonsoft.Json;

namespace MedicinesApp.Controllers
{
    public class MedicinesController : ApiController
    {
        List<Medicine> medicines = new List<Medicine>();
        
        [HttpGet]
        public IEnumerable<Medicine> GetAllmedicines()
        {
            var path = HttpContext.Current.Server.MapPath(@"~/Controllers/medicines.json");
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                medicines = JsonConvert.DeserializeObject<List<Medicine>>(json);
            }
            return medicines;
        }

        [HttpGet]
        public IHttpActionResult GetMedicine(string name)
        {
            medicines = GetAllmedicines().ToList();
            var medicine = medicines.FindAll((p) => p.Name.ToLower().StartsWith(name.ToLower()));
            if (medicine == null)
            {
                return NotFound();
            }
            return Ok(medicine);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddMedicine(Medicine medicine)
        {
            //ValidateMedicine(medicine);
            medicines.Add(medicine);
            return Ok(medicine);
        }



    }
}
