using System.Linq;
using System.Web.Mvc;
using projektiKomponentGITHUB.Models;

namespace projektiKomponentGITHUB.Controllers
{
    public class AccountController : Controller
    {
        private MyDbContext db = new MyDbContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                Session["UserId"] = user.UserId;
                Session["Username"] = user.Username;
                Session["Role"] = user.Role;

                // Example redirection based on role
                switch (user.Role)
                {
                    case "Admin":
                        return RedirectToAction("Roli_test", "Rolet");
                    case "Client":
                        return RedirectToAction("Roli_test", "Rolet");
                    case "HotelManager":
                        return RedirectToAction("Roli_test", "Rolet");
                    case "CarAgencyManager":
                        return RedirectToAction("Roli_test", "Rolet");
                }
            }

            ViewBag.Error = "Invalid email or password.";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
