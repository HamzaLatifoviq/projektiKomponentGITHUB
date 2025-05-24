using projektiKomponentGITHUB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace projektiKomponentGITHUB.Controllers
{
    public class ListaEMakinaveController : Controller
    {
        // GET: ListaEMakinave
        public ActionResult View1()
        {
            return View();
        }
        public ActionResult View2()
        {
            return View();
        }
        public ActionResult View3()
        {
            return View();
        }
        public ActionResult View4()
        {
            return View();
        }
        public ActionResult View5()
        {
            return View();
        }
        public ActionResult View6()
        {
            return View();
        }
        public ActionResult View7()
        {
            return View();
        }
        public ActionResult View8()
        {
            return View();
        }
        public ActionResult View9()
        {
            return View();
        }
        public ActionResult Details(int? id)
        {
            using (var db = new MyDbContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var vetura = db.Veturat.Find(id);
                if (vetura == null)
                {
                    return HttpNotFound();
                }

                var model = new BookingViewModel
                {
                    VeturaId = vetura.Id,
                    Emri = vetura.Emri,
                    Kategoria = vetura.Kategoria,
                    Qyteti = vetura.Qyteti,
                    Distanca = vetura.Distanca,
                    Transmetimi = vetura.Transmetimi,
                    LlojiKarburantit = vetura.LlojiKarburantit,
                    Pershkrimi = vetura.Pershkrimi,
                    FotoPath = vetura.FotoPath
                };

                return View(model); // return the ViewModel expected by the view
            }
        }

    }
}