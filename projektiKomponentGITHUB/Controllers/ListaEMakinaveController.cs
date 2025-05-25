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
        public ActionResult View1() => View();
        public ActionResult View2() => View();
        public ActionResult View3() => View();
        public ActionResult View4() => View();
        public ActionResult View5() => View();
        public ActionResult View6() => View();
        public ActionResult View7() => View();
        public ActionResult View8() => View();
        public ActionResult View9() => View();

        [Authorize]
        public ActionResult Details(int? id)
        {
            using (var db = new MyDbContext())
            {
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var vetura = db.Veturat.Find(id);
                if (vetura == null)
                    return HttpNotFound();

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
                    FotoPath = vetura.FotoPath,
                    Price = vetura.Price
                };

                return View(model);
            }
        }

        // POST: CreateBooking
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateBooking(BookingViewModel model)
        {
            // If the user is not logged in, redirect to login page
            if (Session["UserId"] == null)
            {
                TempData["ErrorMessage"] = "Duhet të jeni i kyçur për të rezervuar një makinë.";
                return RedirectToAction("LoginView", "RegisterLogin"); // Adjust controller/action as needed
            }

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

                var vehicle = db.Veturat.Find(model.VeturaId);
                if (vehicle == null)
                {
                    ModelState.AddModelError("", "Makina nuk u gjet.");
                    return View("Details", model);
                }

                // Calculate rental days (minimum 1)
                int rentalDays = 1;
                if (model.PickupDate.HasValue && model.DropoffDate.HasValue)
                {
                    rentalDays = (model.DropoffDate.Value.Date - model.PickupDate.Value.Date).Days;
                    if (rentalDays <= 0) rentalDays = 1;
                }

                // Calculate total price for vehicle
                decimal vehicleTotalPrice = vehicle.Price * rentalDays;

                // Calculate addons price PER DAY
                decimal addonsTotalPrice = 0m;
                if (model.GPS) addonsTotalPrice += 5m;
                if (model.BabySeat) addonsTotalPrice += 10m;
                if (model.ExtraInsurance) addonsTotalPrice += 20;
                if (model.AdditionalDriver) addonsTotalPrice += 50m;

                decimal totalPriceAtBooking = vehicleTotalPrice + addonsTotalPrice;

                string addOns = string.Join(",", new[] {
                    model.GPS ? "GPS" : null,
                    model.BabySeat ? "BabySeat" : null,
                    model.ExtraInsurance ? "ExtraInsurance" : null,
                    model.AdditionalDriver ? "AdditionalDriver" : null
                }.Where(x => x != null));

                int currentUserId = (int)Session["UserId"]; // Safe now since we checked above

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
                    UserID = currentUserId,
                    PriceAtBooking = totalPriceAtBooking
                };

                db.Bookings.Add(booking);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Rezervimi u krye me sukses!";

                // Pass bookingId and total price to payment page
                return RedirectToAction("PagesatView", "PagesaTransaksionet", new { bookingId = booking.BookingID, shuma = totalPriceAtBooking });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // nothing to dispose manually
            }
            base.Dispose(disposing);
        }
    }
}
