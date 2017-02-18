using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyATM.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Foo()
        {
            return View("About");
        }
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.TheMessage = "Having problem? Let us know.";
            return View();
        }
        [HttpPost]
        public ActionResult Contact(string messageFormUser)
        {
            ViewBag.TheMessage = "Thank you. We receive your message!";

            return PartialView("_ContactThanks");
        }
        public ActionResult Serial(string letterCase)
        {
            var serial = "ASPNETMVCATM1";
            if (letterCase == "lower")
            {
                return Content(serial.ToLower());
            }
            return Content(serial);
        }
    }
}