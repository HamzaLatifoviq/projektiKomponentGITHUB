using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projektiKomponentGITHUB.Models;

namespace projektiKomponentGITHUB.Controllers
{
    public class PagesaTransaksionetController : Controller
    {
        // GET: PagesaTransaksionet
        public ActionResult PagesatTransaksionetView()
        {
            return View();
        }

        public ActionResult SuksesPagesa()
        {
            return View();
        }

        private MyDbContext db = new MyDbContext();

        // GET: Pagesa/PagesaView
        public ActionResult PagesatView(decimal shuma)
        {
            var payment = new Payment
            {
                Shuma = shuma
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
            }
            else
            {
                Console.WriteLine($"Payment received: Emri={payment.Emri}, Mbiemri={payment.Mbiemri}, Emaili={payment.Emaili}");
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
                    // Optionally add more details:
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

        // Optional: Success Page
        public ActionResult PagesaSukses()
        {
            return View();
        }
    }
}
