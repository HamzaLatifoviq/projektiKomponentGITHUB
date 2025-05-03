using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projektiKomponentGITHUB.Controllers
{
    public class RegisterLoginController : Controller
    {
        public ActionResult RegisterView()
        {
            return View("~/Views/Hamza/RegisterLogin/RegisterView.cshtml");
        }

        public ActionResult LoginView()
        {
            return View("~/Views/Hamza/RegisterLogin/LoginView.cshtml");
        }
    }
}