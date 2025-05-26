using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using projektiKomponentGITHUB.Models;

namespace projektiKomponentGITHUB.Controllers
{
    public class RoletController : Controller
    {
        // GET: Rolet
        public ActionResult Roli_Klient()
        {
            using (var db = new MyDbContext())
            {
                string currentUsername = Session["Username"] as string;

                if (string.IsNullOrEmpty(currentUsername))
                {
                    return RedirectToAction("LoginView", "RegisterLogin");
                }

                var currentUser = db.Users.FirstOrDefault(u => u.Username == currentUsername);
                if (currentUser == null)
                {
                    return RedirectToAction("HomePage", "Home");
                }

                var userHotelPayments = db.Payments2Hotel
    .Include(p => p.Reservation)  // need "using System.Data.Entity;" or EF Core equivalent
    .Where(p => p.Emaili == currentUser.Email)
    .ToList();

                // Vehicle payments
                var userPayments = db.Payments
                    .Where(p => p.Emaili == currentUser.Email)
                    .ToList();

                // Hotel payments (new)
                var userHotelPayments2 = db.Payments2Hotel
                    .Where(p => p.Emaili == currentUser.Email)
                    .ToList();

                ViewBag.Payments = userPayments;
                ViewBag.HotelPayments = userHotelPayments;
                ViewBag.HotelPayments = userHotelPayments2;

                return View();
            }
        }
        [HttpPost]
        
        public ActionResult ChangePassword(string Username, string CurrentPassword, string NewPassword)
        {
            using (var db = new MyDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == Username);

                if (user == null || user.Password != CurrentPassword)
                {
                    ViewBag.PasswordChangeError = "Perdoruesi ose fjalëkalimi i tanishëm është i gabuar.";
                    return Roli_Klient(); // Show error and stay on page
                }

                // Update the password
                user.Password = NewPassword;
                db.SaveChanges();

                // Clear session to force re-login
                Session.Clear();

                // Show popup and redirect
                return View("ChangePassword");
            }
        }





        public ActionResult Roli_Admin()
        {
            using (var db = new MyDbContext())
            {
                var users = db.Users.Select(u => new UserViewModel
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Emri = u.Emri,
                    Mbiemri = u.Mbiemri,
                    Email = u.Email,
                    Role = u.Role,
                    CreatedAt = u.CreatedAt,
                    LastLogin = u.LastLogin
                }).ToList();

                var payments = db.Payments.OrderByDescending(p => p.DataPageses).ToList();
                ViewBag.Payments = payments;

                var hotelPayments = db.Payments2Hotel.OrderByDescending(p => p.DataPageses).ToList();
                ViewBag.HotelPayments = hotelPayments;

                return View(users);
            }
        }

        [HttpPost]
        public ActionResult UpdateHotelPaymentStatus(int id, string status)
        {
            using (var db = new MyDbContext())
            {
                var hotelPayment = db.Payments2Hotel.Find(id);
                if (hotelPayment == null)
                    return HttpNotFound();

                hotelPayment.PaymentStatus = status; // "Pranuar" or "Refuzuar"
                db.SaveChanges();
            }
            return RedirectToAction("Roli_Admin");
        }


        // GET: Admin/EditUser/5
        public ActionResult EditUser(int id)
        {
            using (var db = new MyDbContext())
            {
                var user = db.Users.Find(id);
                if (user == null) return HttpNotFound();

                var viewModel = new UserViewModel
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Emri = user.Emri,
                    Mbiemri = user.Mbiemri,
                    Email = user.Email,
                    Role = user.Role
                };

                return View(viewModel);
            }
        }

        // POST: Admin/EditUser/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MyDbContext())
                {
                    var user = db.Users.Find(model.UserId);
                    if (user == null) return HttpNotFound();

                    user.Emri = model.Emri;
                    user.Mbiemri = model.Mbiemri;
                    user.Email = model.Email;
                    user.Role = model.Role;

                    db.SaveChanges();
                    return RedirectToAction("Roli_Admin");
                }
            }

            return View(model);
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MyDbContext())
                {
                    bool exists = db.Users.Any(u => u.Username == model.Username || u.Email == model.Email);
                    if (exists)
                    {
                        ModelState.AddModelError("", "Username or Email already exists.");
                        return View(model);
                    }

                    var user = new User
                    {
                        Username = model.Username,
                        Email = model.Email,
                        Emri = model.Emri,
                        Mbiemri = model.Mbiemri,
                        Role = model.Role,
                        Password = model.Password // Hash this in production!
                    };

                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Roli_Admin");
                }
            }

            return View(model);
        }

        // GET: Rolet/DeleteUser/5
        public ActionResult DeleteUser(int id)
        {
            using (var db = new MyDbContext())
            {
                var user = db.Users.Find(id);
                if (user == null) return HttpNotFound();

                var viewModel = new UserViewModel
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Emri = user.Emri,
                    Mbiemri = user.Mbiemri,
                    Email = user.Email,
                    Role = user.Role
                };

                return View(viewModel);
            }
        }

        // POST: Rolet/DeleteUser/5
        [HttpPost, ActionName("DeleteUser")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(int id)
        {
            using (var db = new MyDbContext())
            {
                var user = db.Users.Find(id);
                if (user == null) return HttpNotFound();

                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Roli_Admin");
            }
        }

        // POST: Rolet/UpdatePaymentStatus/5
        [HttpPost]
        public ActionResult UpdatePaymentStatus(int id, string status)
        {
            using (var db = new MyDbContext())
            {
                var payment = db.Payments.Find(id);
                if (payment == null)
                    return HttpNotFound();

                payment.PaymentStatus = status; // "Pranuar" or "Refuzuar"
                db.SaveChanges();
            }
            return RedirectToAction("Roli_Admin");
        }

        public ActionResult Roli_HotelManager()
        {
            using (var db = new MyDbContext())
            {
                var hotels = db.Hotels.ToList();   // Load hotels if needed

                var reservations = db.Reservations.ToList();  // your reservations
                var payments = db.Payments.ToList();          // payments

                ViewBag.Reservations = reservations;
                ViewBag.Payments = payments;

                return View(hotels);  // pass hotels as model or any other model you want
            }
        }
        public ActionResult Roli_VeturManager()
        {
            using (var db = new MyDbContext())
            {
                var veturat = db.Veturat.ToList(); // assuming you have a DbSet<Vetura> Veturat
                return View(veturat);
            }
        }
        public ActionResult Roli_Test()
        {
            return View();
        }
    }
}
