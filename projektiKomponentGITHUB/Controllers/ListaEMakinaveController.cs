using projektiKomponentGITHUB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace projektiKomponentGITHUB.Controllers
{
    public class ListaEMakinaveController : Controller
    {
        // GET: ListaEMakinave
        public ActionResult View1()
        {
            return View();
        }
        public ActionResult View2()
        {
            return View();
        }
        public ActionResult View3()
        {
            return View();
        }
        public ActionResult View4()
        {
            return View();
        }
        public ActionResult View5()
        {
            return View();
        }
        public ActionResult View6()
        {
            return View();
        }
        public ActionResult View7()
        {
            return View();
        }
        public ActionResult View8()
        {
            return View();
        }
        public ActionResult View9()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            using (var db = new MyDbContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var vetura = db.Veturat.Find(id);
                if (vetura == null)
                {
                    return HttpNotFound();
                }

                var model = new BookingViewModel
                {
                    VeturaId = vetura.Id,
                    Emri = vetura.Emri,
                    Kategoria = vetura.Kategoria,
                    Qyteti = vetura.Qyteti,
                    Distanca = vetura.Distanca,
                    Transmetimi = vetura.Transmetimi,
                    LlojiKarburantit = vetura.LlojiKarburantit,
                    Pershkrimi = vetura.Pershkrimi,
                    FotoPath = vetura.FotoPath
                };

                return View(model); // return the ViewModel expected by the view
            }
        }

        // POST: CreateBooking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBooking(BookingViewModel model)
        {
            using (var db = new MyDbContext())
            {
                if (!ModelState.IsValid)
                {
                    return View("Details", model);
                }

                if (!model.PickupDate.HasValue)
                {
                    ModelState.AddModelError("PickupDate", "Ju lutem zgjidhni datën e marrjes së makinës.");
                    return View("Details", model);
                }

                // Compose add-ons string
                var addOns = string.Join(",", new[]
                {
            model.GPS ? "GPS" : null,
            model.BabySeat ? "BabySeat" : null,
            model.ExtraInsurance ? "ExtraInsurance" : null,
            model.AdditionalDriver ? "AdditionalDriver" : null
        }.Where(a => a != null));

                // Get logged in user ID from session
                int? currentUserId = null;
                if (Session["UserId"] != null)
                {
                    currentUserId = (int)Session["UserId"];
                }

                var booking = new VeturBooking
                {
                    VeturaID = model.VeturaId,
                    DropOffLocation = model.DropOffLocation,
                    AddOns = addOns,
                    BookingDate = DateTime.Now,
                    PickupDate = model.PickupDate,
                    DropoffDate = model.DropoffDate,
                    GPS = model.GPS,
                    BabySeat = model.BabySeat,
                    ExtraInsurance = model.ExtraInsurance,
                    AdditionalDriver = model.AdditionalDriver,
                    UserID = currentUserId
                };

                db.Bookings.Add(booking);
                db.SaveChanges();
            }

            TempData["SuccessMessage"] = "Rezervimi u krye me sukses!";
            return RedirectToAction("Details", new { id = model.VeturaId });
        }
    

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // No need to dispose here since using statements do it already
            }
            base.Dispose(disposing);
        }
    }
}
