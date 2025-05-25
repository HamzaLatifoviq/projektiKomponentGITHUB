using System;
using System.Collections.Generic;
using System.Linq;
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
                // Get the logged-in username from session
                string currentUsername = Session["Username"] as string;

                if (string.IsNullOrEmpty(currentUsername))
                {
                    // If no username found in session, redirect to login
                    return RedirectToAction("LoginView", "RegisterLogin");
                }

                // Find the current user by username
                var currentUser = db.Users.FirstOrDefault(u => u.Username == currentUsername);

                if (currentUser == null)
                {
                    // If user not found in DB, redirect to home
                    return RedirectToAction("HomePage", "Home");
                }

                // Get only this user's payments based on email
                var userPayments = db.Payments
                    .Where(p => p.Emaili == currentUser.Email)
                    .ToList();

                ViewBag.Payments = userPayments;

                return View();
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

                // Use the correct column name here - DataPageses instead of PaymentDate
                var payments = db.Payments.OrderByDescending(p => p.DataPageses).ToList();
                ViewBag.Payments = payments;

                return View(users);
            }
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
            return View();
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
