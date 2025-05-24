using projektiKomponentGITHUB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projektiKomponentGITHUB.Controllers
{
    public class EditVehiclePriceController : Controller
    {
        // GET: EditVehiclePrice
        [Authorize(Roles = "Admin,CarAgencyManager")]
        public ActionResult EditPrice(int id)
        {
            using (var db = new MyDbContext())
            {
                var vetura = db.Veturat.Find(id);
                if (vetura == null)
                    return HttpNotFound();

                return View(vetura); // pass vehicle to view for editing
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,CarAgencyManager")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPrice(Veturat model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var db = new MyDbContext())
            {
                var vetura = db.Veturat.Find(model.Id);
                if (vetura == null)
                    return HttpNotFound();

                vetura.Price = model.Price;
                db.SaveChanges();
            }

            TempData["SuccessMessage"] = "Çmimi u përditësua me sukses!";
            return RedirectToAction("Details", new { id = model.Id });
        }

    }
}