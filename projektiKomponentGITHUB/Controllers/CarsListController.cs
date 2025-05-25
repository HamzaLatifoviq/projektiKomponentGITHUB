using projektiKomponentGITHUB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projektiKomponentGITHUB.Controllers
{


    public class CarsListController : Controller
    {
        public ActionResult Kerkoveturat(string lokacioni, string llojiMakines)
        {
            using (var db = new MyDbContext())
            {
                var veturat = db.Veturat.AsQueryable();

                if (!string.IsNullOrEmpty(lokacioni))
                    veturat = veturat.Where(v => v.Qyteti == lokacioni);

                if (!string.IsNullOrEmpty(llojiMakines))
                    veturat = veturat.Where(v => v.Kategoria == llojiMakines);

                // Send back selected filters
                ViewBag.Lokacioni = lokacioni;
                ViewBag.LlojiMakines = llojiMakines;

                return View("Kerkoveturat", veturat.ToList());
            }
        }


        public ActionResult Details(int id)
        {

            using (var db = new MyDbContext())
            {
                var vetura = db.Veturat.Find(id);

                if (vetura == null)
                    return HttpNotFound();

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

                return View(model);
            }
        }

    }



}