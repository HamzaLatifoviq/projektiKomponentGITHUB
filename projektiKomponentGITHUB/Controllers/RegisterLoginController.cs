using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projektiKomponentGITHUB.Controllers
{
    public class RegisterLoginController : Controller
    {
        // GET: RegisterLogin
        public ActionResult RegisterView()
        {
            return View();
        }
        public ActionResult LoginView()
        {
            return View();
        }
    }
}