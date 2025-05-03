using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projektiKomponentGITHUB.Controllers
{
    public class RoletController : Controller
    {
        // GET: Rolet
        public ActionResult Roli_Klient()
        {
            return View();
        }
        public ActionResult Roli_Admin()
        {
            return View();
        }
        public ActionResult Roli_HotelManager()
        {
            return View();
        }
        public ActionResult Roli_VeturManager()
        {
            return View();
        }
        public ActionResult Roli_Test()
        {
            return View();
        }
    }
}