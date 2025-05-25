using System;
using System.Linq;
using System.Web.Mvc;
using projektiKomponentGITHUB.Models;

namespace projektiKomponentGITHUB.Controllers
{
    public class ReservationsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.ToList();
            return View(reservations);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.CreatedAt = DateTime.Now;  // Siguro që të vendoset data aktuale
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

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
