using projektiKomponentGITHUB.Models;  // për modelet dhe DbContext
using System;
using System.Linq;
using System.Web.Mvc;

namespace projektiKomponentGITHUB.Controllers
{
    public class HomeController : Controller
    {
        // Krijojmë instance të DbContext për databazën
        private MyDbContext db = new MyDbContext();

        // HomePage ku shfaqen komentet dhe forma
        public ActionResult HomePage()
        {
            // Marrim të gjitha komentet nga databaza, të renditura nga më të fundit
            var reviews = db.Reviews.OrderByDescending(r => r.CreatedAt).ToList();
            return View(reviews);
        }

        // Forma POST që merr komentet dhe ruan në databazë
        [HttpPost]
        public ActionResult SubmitReview(int rating, string comment, string userName)
        {
            // Validim i thjeshtë
            if (rating < 1 || rating > 5 || string.IsNullOrWhiteSpace(comment))
            {
                TempData["Error"] = "Please provide a valid rating (1-5) and comment.";
                return RedirectToAction("HomePage");
            }

            var review = new Review
            {
                Rating = rating,
                Comment = comment,
                UserName = string.IsNullOrWhiteSpace(userName) ? "Anonymous" : userName,
                CreatedAt = DateTime.Now
            };

            db.Reviews.Add(review);
            db.SaveChanges();

            return RedirectToAction("HomePage");
        }

        // Të tjera ActionResult ekzistuese
        public ActionResult AboutPage()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult CarList()
        {
            using (var db = new MyDbContext())
            {
                // Fetch 3 recommended cars, e.g. the first 3 vehicles (or customize your logic)
                var recommendedCars = db.Veturat.Take(3).ToList();

                return View(recommendedCars);
            }
        }

        public ActionResult HotelList()
        {
            return View();
        }

        public ActionResult ContactPage()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        // Mbyllja e controller-it
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
