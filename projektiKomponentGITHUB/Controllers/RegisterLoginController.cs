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
                    // Set login session values (optional)
                    Session["UserId"] = user.UserId;
                    Session["Username"] = user.Username;
                    Session["Role"] = user.Role;

                    // Create FormsAuthenticationTicket with roles in UserData
                    string roles = user.Role; // e.g., "Admin" or "Client"
                    var ticket = new System.Web.Security.FormsAuthenticationTicket(
                        1, // version
                        user.Username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        false,
                        roles
                    );

                    string encryptedTicket = System.Web.Security.FormsAuthentication.Encrypt(ticket);
                    var authCookie = new HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);

                    user.LastLogin = DateTime.Now;
                    db.SaveChanges();

                    switch (user.Role)
                    {
                        case "Admin":
                            return RedirectToAction("Roli_Test", "Rolet");
                        case "Client":
                            return RedirectToAction("Roli_Klient", "Rolet");
                        case "HotelManager":
                            return RedirectToAction("Roli_HotelManager", "Rolet");
                        case "CarAgencyManager":
                            return RedirectToAction("Roli_VeturManager", "Rolet");
                        default:
                            return RedirectToAction("LoginView", "RegisterLogin");
                    }
                }

                ViewBag.Error = "Wrong username or password.";
                return View("LoginView");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "An error occurred. Please try again.";
                return View("LoginView");
            }
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult RegisterView(UserCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View(model);
            }

            // Check if username or email already exists
            bool usernameExists = db.Users.Any(u => u.Username == model.Username);
            bool emailExists = db.Users.Any(u => u.Email == model.Email);

            if (usernameExists)
            {
                ViewBag.Error = "Username already taken.";
                return View(model);
            }

            if (emailExists)
            {
                ViewBag.Error = "Email already registered.";
                return View(model);
            }

            // Create new user entity
            var newUser = new User
            {
                Emri = model.Emri,
                Mbiemri = model.Mbiemri,
                Email = model.Email,
                Username = model.Username,
                Password = model.Password,  // Ideally, hash this before saving
                Role = "Client",
                CreatedAt = DateTime.Now
            };

            db.Users.Add(newUser);
            db.SaveChanges();

            ViewBag.Success = "Registration successful! Please login.";
            return RedirectToAction("LoginView");
        }

        public ActionResult Logout()
        {
            Session.Clear();      // removes all keys
            Session.Abandon();    // destroys session
            return RedirectToAction("LoginView", "RegisterLogin");
        }
    }

}
