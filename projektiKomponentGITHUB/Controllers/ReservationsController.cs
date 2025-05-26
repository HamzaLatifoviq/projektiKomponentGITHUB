using System;
using System.Data.Entity; // or Microsoft.EntityFrameworkCore if EF Core
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;  // Install Newtonsoft.Json package if needed
using projektiKomponentGITHUB.Models;

public class ReservationsController : Controller
{
    private MyDbContext db = new MyDbContext();

    // GET: Reservations
    public ActionResult Index()
    {
        var reservations = db.Reservations
            .Include(r => r.Hotel)
            .Include(r => r.Room)
            .Select(r => new ReservationViewModel
            {
                ReservationID = r.ReservationID,
                Emri = r.Emri,
                Mbiemri = r.Mbiemri,
                Emaili = r.Emaili,
                HotelEmri = r.Hotel.Emri,
                RoomLlojiDhomes = r.Room.LlojiDhomes,
                DataCheckIn = r.DataCheckIn,
                DataCheckOut = r.DataCheckOut,
                NumriTeRriturve = r.NumriTeRriturve,
                NumriFemijeve = r.NumriFemijeve,
                NumriDhomave = r.NumriDhomave,
                CmimiTotal = r.CmimiTotal
            }).ToList();

        return View(reservations);
    }

    // GET: Reservations/Create
    public ActionResult Create()
    {
        ViewBag.HotelID = new SelectList(db.Hotels, "HotelID", "Emri");

        var rooms = db.Rooms
            .Select(r => new RoomPriceDto
            {
                RoomID = r.RoomID,
                LlojiDhomes = r.LlojiDhomes,
                CmimiPerNate = r.CmimiPerNate
            }).ToList();

        // Serialize to JSON string for safe use in JS in the view
        ViewBag.RoomsJson = JsonConvert.SerializeObject(rooms);

        ViewBag.RoomID = new SelectList(rooms, "RoomID", "LlojiDhomes");

        return View();
    }

    // POST: Reservations/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    
    public ActionResult Create(Reservation reservation)
    {
        decimal CmimiTotal;
        if (ModelState.IsValid)
        {
            
            var room = db.Rooms.Find(reservation.RoomID);
            if (room == null)
            {
                ModelState.AddModelError("RoomID", "Selected room does not exist.");
                ReloadDropdowns(reservation);
                return View(reservation);
            }

            int nights = (reservation.DataCheckOut - reservation.DataCheckIn).Days;
            if (nights <= 0)
            {
                ModelState.AddModelError("DataCheckOut", "Check-out date must be after check-in date.");
                ReloadDropdowns(reservation);
                return View(reservation);
            }

            reservation.CmimiTotal = room.CmimiPerNate * nights * reservation.NumriDhomave;

            db.Reservations.Add(reservation);
            db.SaveChanges();

            // Redirect to payment page with total price
            return RedirectToAction("PagesatView2", "PagesaTransaksionet", new
            {
                shuma = reservation.CmimiTotal,
                bookingId = reservation.ReservationID
            });
        }

        // If ModelState is not valid, reload dropdowns and return the same view for correction
        ReloadDropdowns(reservation);
        return View(reservation);
    }

    private void ReloadDropdowns(Reservation reservation)
    {
        ViewBag.HotelID = new SelectList(db.Hotels, "HotelID", "Emri", reservation.HotelID);

        var rooms = db.Rooms
            .Select(r => new RoomPriceDto
            {
                RoomID = r.RoomID,
                LlojiDhomes = r.LlojiDhomes,
                CmimiPerNate = r.CmimiPerNate
            }).ToList();

        ViewBag.RoomsJson = JsonConvert.SerializeObject(rooms);

        ViewBag.RoomID = new SelectList(rooms, "RoomID", "LlojiDhomes", reservation.RoomID);
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

// A DTO class for room prices, to avoid anonymous types in ViewBag
public class RoomPriceDto
{
    public int RoomID { get; set; }
    public string LlojiDhomes { get; set; }
    public decimal CmimiPerNate { get; set; }
}
