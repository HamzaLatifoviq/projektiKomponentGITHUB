//using System;
//using System.Linq;
//using System.Web.Mvc;
//using projektiKomponentGITHUB.Models;

//namespace projektiKomponentGITHUB.Controllers
//{
//    public class AccountController : Controller
//    {
//        private MyDbContext db = new MyDbContext();

//        [HttpGet]
//        public ActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult Login(string username, string password)
//        {
//            try
//            {
//                var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

//                if (user != null)
//                {
//                    Session["UserId"] = user.UserId;
//                    Session["Username"] = user.Username;
//                    Session["Role"] = user.Role;

//                    user.LastLogin = DateTime.Now;
//                    db.SaveChanges();

//                    switch (user.Role)
//                    {
//                        case "Admin":
//                            return RedirectToAction("Roli_Test", "Rolet");
//                        case "Client":
//                            return RedirectToAction("Roli_Test", "Rolet");
//                        case "HotelManager":
//                            return RedirectToAction("Roli_Test", "Rolet");
//                        case "CarAgencyManager":
//                            return RedirectToAction("Roli_Test", "Rolet");
//                        default:
//                            return RedirectToAction("LoginView", "RegisterLogin");
//                    }
//                }

//                // If no user found
//                ViewBag.Error = "Wrong username or password.";
//                return View();
//            }
//            catch (Exception ex)
//            {
//                // Optional: log the error, but don't show it to the user
//                ViewBag.Error = "An error occurred. Please try again.";
//                return View();
//            }
//        }

//        public ActionResult Logout()
//        {
//            Session.Clear();
//            return RedirectToAction("Login");
//        }
//    }
//}
