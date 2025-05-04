using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projektiKomponentGITHUB.Controllers
{
    public class CarsListController : Controller
{
    public ActionResult Kerkoveturat(string lokacioni, DateTime? pickupDate, string pickupTime, DateTime? dropoffDate, string dropoffTime, string llojiMakines)
    {
        // Mund të shtosh logjikën e filtrimit këtu...
        return View();
    }
}
}