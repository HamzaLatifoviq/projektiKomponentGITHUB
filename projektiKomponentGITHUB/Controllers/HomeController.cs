using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projektiKomponentGITHUB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult AboutPage()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ContactPage()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}