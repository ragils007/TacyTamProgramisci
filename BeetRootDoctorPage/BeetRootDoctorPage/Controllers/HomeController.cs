using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeetRootDoctorPage.Models;
using Db.Pgsql;
using Db.Pgsql.Factory;

namespace BeetRootDoctorPage.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult History()
        {
            return View("History");
        }

        [HttpPost]
        public IActionResult Save(string img)
        {
            var dd = img.Split(',');
            var teraz = DateTime.Now;
            string filePath = $"{teraz.Year}_{teraz.Month}_{teraz.Day}_{teraz.Hour}_{teraz.Minute}_{teraz.Second}_{teraz.Millisecond}.jpg";
            System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(dd[1]));
            var db = new Postgres();
            var log = new cameralogFactory(db);
            log.Add(1, 1, filePath,"1","1","nazwa");

            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
