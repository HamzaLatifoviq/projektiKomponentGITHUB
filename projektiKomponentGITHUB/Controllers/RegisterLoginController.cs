using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projektiKomponentGITHUB.Models;

namespace projektiKomponentGITHUB.Controllers
{
    public class RegisterLoginController : Controller
    {
        private MyDbContext db = new MyDbContext();
        // GET: RegisterLogin
        public ActionResult RegisterView()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LoginView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginView(string username, string password)
        {
            try
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    Session["UserId"] = user.UserId;
                    Session["Username"] = user.Username;
                    Session["Role"] = user.Role;

                    user.LastLogin = DateTime.Now;
                    db.SaveChanges();

                    switch (user.Role)
                    {
                        case "Admin":
                            return RedirectToAction("Roli_Test", "Rolet");
                        case "Client":
                            return RedirectToAction("Roli_Test", "Rolet");
                        case "HotelManager":
                            return RedirectToAction("Roli_Test", "Rolet");
                        case "CarAgencyManager":
                            return RedirectToAction("Roli_Test", "Rolet");
                        default:
                            return RedirectToAction("LoginView", "RegisterLogin");
                    }
                }

                // If no user found
                ViewBag.Error = "Wrong username or password.";
                return View("LoginView"); // << FIXED
            }
            catch (Exception ex)
            {
                ViewBag.Error = "An error occurred. Please try again.";
                return View("LoginView"); // << FIXED
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();      // removes all keys
            Session.Abandon();    // destroys session
            return RedirectToAction("LoginView", "RegisterLogin");
        }
    }

}
