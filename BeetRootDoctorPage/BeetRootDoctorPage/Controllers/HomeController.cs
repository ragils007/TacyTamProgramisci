using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeetRootDoctorPage.Models;

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

<<<<<<< HEAD
        [HttpPost]
        public IActionResult Save(int test)
        {

            return Ok();
=======
        public IActionResult History()
        {
            return View("History");
>>>>>>> 7127c9a7c5fa02c67b7917cd27cfabc786148007
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
