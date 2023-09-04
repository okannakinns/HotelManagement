using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class OdaKontenjanController : Controller
    {
        public IActionResult Index()
        {
            BalsenContext context = new BalsenContext();
            List<OdaKontenjan> OdaKontenjanListesi = context.OdaKontenjans.Include(x => x.Odalar).ToList();
            List<Odalar> OdalarListesi = context.Odalars.ToList();
            ViewBag.Odalar = OdalarListesi;

            return View(OdaKontenjanListesi);
        }


        [HttpPost]
        public IActionResult Ekle(OdaKontenjan odaKontenjan)
        {
            odaKontenjan.EklenmeTarihi = DateTime.Now;
            odaKontenjan.Durumu = 1;
            odaKontenjan.GuncellenmeTarihi = DateTime.Now;
            BalsenContext context = new BalsenContext();
            if (context.OdaKontenjans.Any(k => k.OdalarId == odaKontenjan.OdalarId))
            {
                ViewBag.ErrorMessage = "Bu Odaya Ait Bir Kontenjan Zaten Var.";
                List<OdaKontenjan> odakontenjanListesi = context.OdaKontenjans.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", odakontenjanListesi);
            }
          
            if (odaKontenjan.Miktar == null)
            {
                ViewBag.ErrorMessage = "Kontenjan Miktarı Boş Bırakılamaz.";
                List<OdaKontenjan> odakontenjanListesi = context.OdaKontenjans.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", odakontenjanListesi);
            }
            if (odaKontenjan.BaslangicTarihi == null)
            {
                ViewBag.ErrorMessage = "Başlangıç Tarihi Boş Bırakılamaz.";
                List<OdaKontenjan> odakontenjanListesi = context.OdaKontenjans.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", odakontenjanListesi);
            }
            if (odaKontenjan.BitisTarihi == null)
            {
                ViewBag.ErrorMessage = "Bitiş Tarihi Boş Bırakılamaz.";
                List<OdaKontenjan> odakontenjanListesi = context.OdaKontenjans.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", odakontenjanListesi);
            }
            if (odaKontenjan.BaslangicTarihi == odaKontenjan.BitisTarihi)
            {
                ViewBag.ErrorMessage = "Başlangıç Tarihi İle Bitiş Tarihi Aynı Olamaz.";
                List<OdaKontenjan> odakontenjanListesi = context.OdaKontenjans.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", odakontenjanListesi);
            }

            context.OdaKontenjans.Add(odaKontenjan);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

           
        
        [HttpPost]
        public IActionResult Sil(int id)
        {
            BalsenContext dbContext = new BalsenContext();
            var odaKontenjan = dbContext.OdaKontenjans.Find(id);
            if (odaKontenjan != null)
            {
                dbContext.Remove(odaKontenjan);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int id)
        {
            BalsenContext context = new BalsenContext();
            List<Odalar> OdalarListesi = context.Odalars.ToList();
            ViewBag.Odalar = OdalarListesi;
            OdaKontenjan kontenjan = context.OdaKontenjans.Where(k => k.Id == id).FirstOrDefault();
            if (kontenjan == null)
            {
                return RedirectToAction("Index");
            }

            return View("Update", kontenjan);
        }

        [HttpPost]
        public IActionResult DoUpdate(OdaKontenjan kontenjan)
        {
            kontenjan.GuncellenmeTarihi = DateTime.Now;
            BalsenContext context = new BalsenContext();
            if (context.OdaKontenjans.Any(k => k.OdalarId == kontenjan.OdalarId && k.Id!=kontenjan.Id))
            {
                ViewBag.ErrorMessage = "Bu Odaya Ait Bir Kontenjan Zaten Var.";
                List<OdaKontenjan> odakontenjanListesi = context.OdaKontenjans.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", odakontenjanListesi);
            }
           
            if (kontenjan.Miktar == null)
            {
                ViewBag.ErrorMessage = "Kontenjan Miktarı Boş Bırakılamaz.";
                List<OdaKontenjan> odakontenjanListesi = context.OdaKontenjans.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", odakontenjanListesi);
            }
            context.Update(kontenjan);
                context.SaveChanges();
                return RedirectToAction("Index");
            

           
        }

    }
}
