using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using projektiKomponentGITHUB.Models;

namespace projektiKomponentGITHUB.Controllers
{
    public class PagesaTransaksionetController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Pagesa/PagesaView
        // bookingId is nullable because it might not be passed
        public ActionResult PagesatView(decimal shuma, int? bookingId)
        {
            if (!bookingId.HasValue)
            {
                // You could redirect or show an error here if bookingId is required
                return new HttpStatusCodeResult(400, "Booking ID is required.");
            }

            ViewBag.BookingID = bookingId;

            var payment = new Payment
            {
                Shuma = shuma,
                BookingID = bookingId
            };

            return View(payment);
        }

        // POST: Pagesa/PagesaView
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PagesatView(Payment payment)
        {
            Console.WriteLine("POST PagesaView called.");

            if (payment == null)
            {
                Console.WriteLine("Payment model is null!");
                return View(payment);
            }

            Console.WriteLine($"Payment received: Emri={payment.Emri}, Mbiemri={payment.Mbiemri}, Emaili={payment.Emaili}, BookingID={payment.BookingID}");

            // In case BookingID is null here, try to get from form or ViewBag (optional fallback)
            if (!payment.BookingID.HasValue || payment.BookingID == 0)
            {
                var bookingIdFromForm = Request.Form["BookingID"];
                if (int.TryParse(bookingIdFromForm, out int bookingIdParsed))
                {
                    payment.BookingID = bookingIdParsed;
                    Console.WriteLine($"BookingID was null, assigned from form: {bookingIdParsed}");
                }
                else
                {
                    Console.WriteLine("BookingID is missing or invalid.");
                    ModelState.AddModelError("", "Booking ID is required.");
                }
            }

            if (ModelState.IsValid)
            {
                Console.WriteLine("ModelState is valid.");

                payment.DataPageses = DateTime.Now;
                payment.PaymentStatus = "Pending";

                try
                {
                    db.Payments.Add(payment);
                    db.SaveChanges();
                    Console.WriteLine("Payment saved to database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception on SaveChanges: {ex.Message}");
                    Console.WriteLine(ex.StackTrace);
                    ViewBag.Errors = new List<string> { "Database error: " + ex.Message };
                    return View(payment);
                }

                ViewBag.Message = "Pagesa u krye me sukses!";
                return RedirectToAction("SuksesPagesa");
            }
            else
            {
                Console.WriteLine("ModelState is NOT valid.");
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                Console.WriteLine("Validation errors:");
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }

                ViewBag.Errors = errors;
                return View(payment);
            }
        }

        public ActionResult SuksesPagesa()
        {
            return View();
        }
        public ActionResult PagesatView2(decimal shuma, int? reservationId)
        {
            if (!reservationId.HasValue)
            {
                return new HttpStatusCodeResult(400, "Reservation ID is required.");
            }

            ViewBag.ReservationID = reservationId;

            var payment = new Payments2Hotel
            {
                Shuma = shuma,
                ReservationID = reservationId
            };

            return View(payment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PagesatView2(Payments2Hotel payment)
        {
            if (!payment.ReservationID.HasValue || payment.ReservationID == 0)
            {
                ModelState.AddModelError("", "Reservation ID is required.");
            }

            if (ModelState.IsValid)
            {
                payment.DataPageses = DateTime.Now;
                payment.PaymentStatus = "Pending";

                try
                {
                    db.Payments2Hotel.Add(payment);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ViewBag.Errors = new List<string> { "Database error: " + ex.Message };
                    return View(payment);
                }

                return RedirectToAction("SuksesPagesa");
            }

            return View(payment);
        }
    }
}
