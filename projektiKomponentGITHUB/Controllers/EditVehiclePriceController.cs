

using projektiKomponentGITHUB.Models;
using System.Web.Mvc;

namespace projektiKomponentGITHUB.Controllers
{
    [Authorize(Roles = "Admin,CarAgencyManager")]
    public class EditVehiclePriceController : Controller
    {
        // GET: EditVehiclePrice/EditPrice/1
        public ActionResult EditPrice(int id)
        {
            using (var db = new MyDbContext())
            {
                var vetura = db.Veturat.Find(id);
                if (vetura == null)
                    return HttpNotFound();

                return View(vetura); // Pass the vehicle entity to the view
            }
        }

        // POST: EditVehiclePrice/EditPrice
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPrice(Veturat model)
        {
            if (!ModelState.IsValid)
            {
                // Return the same view with validation errors
                return View(model);
            }

            using (var db = new MyDbContext())
            {
                var vetura = db.Veturat.Find(model.Id); // <-- Use VeturaId, not Id
                if (vetura == null)
                    return HttpNotFound();

                // Update only the Price property
                vetura.Price = model.Price;

                db.SaveChanges();
            }

            TempData["SuccessMessage"] = "Çmimi u përditësua me sukses!";
            // Redirect to Details action of ListaEMakinave controller
            return RedirectToAction("Details", "ListaEMakinave", new { id = model.Id });
        }
    }
}


