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
            return View();
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
                    // Check if username or email already exists (optional but recommended)
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
                        Password = model.Password // **Hash this in production!**
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



        public ActionResult Roli_HotelManager()
        {
            return View();
        }
        public ActionResult Roli_VeturManager()
        {
            return View();
        }
        public ActionResult Roli_Test()
        {
            return View();
        }
    }
}