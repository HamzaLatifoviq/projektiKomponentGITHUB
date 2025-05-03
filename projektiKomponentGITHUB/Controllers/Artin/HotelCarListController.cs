using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projektiKomponentGITHUB.Controllers.Artin
{
    public class HotelCarListController : Controller
    {
        // GET: HotelCarList
        public ActionResult CarList()
        {
            return View("~/Views/Artin/HotelCarList/CarList.cshtml");
        }
        public ActionResult HotelList()
        {
            return View("~/Views/Artin/HotelCarList/HotelList.cshtml");
        }
    }
}