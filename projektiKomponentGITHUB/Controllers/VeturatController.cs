
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projektiKomponentGITHUB.Models;

public class VeturatController : Controller
{
    // LIST
    public ActionResult Roli_VeturManager()
    {
        using (var db = new MyDbContext())
        {
            var veturat = db.Veturat.ToList();
            return View(veturat);
        }
    }

    // CREATE - GET
    public ActionResult Shto()
    {
        return View();
    }

    // CREATE - POST
    [HttpPost]
    public ActionResult Shto(Veturat vetura, HttpPostedFileBase imageFile)
    {
        using (var db = new MyDbContext())
        {
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(imageFile.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Images/Veturat"), fileName);
                imageFile.SaveAs(path);
                vetura.FotoPath = "~/Content/Images/Veturat/" + fileName;
            }

            db.Veturat.Add(vetura);
            db.SaveChanges();
            return RedirectToAction("Roli_VeturManager");
        }
    }

    // EDIT - GET
    public ActionResult Edito(int id)
    {
        using (var db = new MyDbContext())
        {
            var vetura = db.Veturat.Find(id);
            if (vetura == null)
                return HttpNotFound();
            return View(vetura);
        }
    }

    // EDIT - POST
    [HttpPost]
    public ActionResult Edito(Veturat vetura, HttpPostedFileBase imageFile)
    {
        using (var db = new MyDbContext())
        {
            var existing = db.Veturat.Find(vetura.Id);
            if (existing == null)
                return HttpNotFound();

            existing.Emri = vetura.Emri;
            existing.Kategoria = vetura.Kategoria;
            existing.Price = vetura.Price;

            if (imageFile != null && imageFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(imageFile.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Images/Veturat"), fileName);
                imageFile.SaveAs(path);
                existing.FotoPath = "~/Content/Images/Veturat/" + fileName;
            }

            db.SaveChanges();
            return RedirectToAction("Roli_VeturManager");
        }
    }

    // DELETE - GET
    public ActionResult Fshij(int id)
    {
        using (var db = new MyDbContext())
        {
            var vetura = db.Veturat.Find(id);
            if (vetura == null) return HttpNotFound();
            return View(vetura);
        }
    }

    // DELETE - POST
    [HttpPost, ActionName("Fshij")]
    public ActionResult FshijConfirmed(int id)
    {
        using (var db = new MyDbContext())
        {
            var vetura = db.Veturat.Find(id);
            if (vetura == null) return HttpNotFound();

            db.Veturat.Remove(vetura);
            db.SaveChanges();
            return RedirectToAction("Roli_VeturManager");
        }
    }
}
