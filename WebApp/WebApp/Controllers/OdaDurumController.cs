using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class OdaDurumController : Controller
    {
        public IActionResult Index()
        {
            BalsenContext context = new BalsenContext();
            List<OdaDurum> OdaDurumListesi = context.OdaDurums.ToList();
            return View(OdaDurumListesi);
        }
        [HttpPost]
        public IActionResult Ekle(OdaDurum durum)
        {
            if (ModelState.IsValid)
            {
                durum.EklenmeTarihi = DateTime.Now;
                durum.Durumu = 1;
                durum.GuncellenmeTarihi = DateTime.Now;
                BalsenContext context = new BalsenContext();
                if (context.OdaDurums.Any(k => k.Adi ==durum.Adi ))
                {
                    ViewBag.ErrorMessage = "Bu Durum Zaten Kayıtlı.";
                    List<OdaDurum> odadurumListesi = context.OdaDurums.ToList();
                    return View("Index", odadurumListesi);
                }
                if (durum.Adi==null)
                {
                    ViewBag.ErrorMessage = "Durum Adı Boş Bırakılamaz.";
                    List<OdaDurum> odadurumListesi = context.OdaDurums.ToList();
                    return View("Index", odadurumListesi);
                }
                context.OdaDurums.Add(durum);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpPost]
        public IActionResult Sil(int id)
        {
            BalsenContext dbContext = new BalsenContext();
            var durum = dbContext.OdaDurums.Find(id);
            var odalar= dbContext.Odalars.FirstOrDefault(o=>o.OdaDurumId==id);    
            if (durum != null)
            {
                if (odalar != null)
                {
                    ViewBag.ErrorMessage = "Önce Bu Durumun Bulunduğu Oda Kaydının Durumunu Değiştirmelisiniz.";
                    List<OdaDurum> odadurumListesi = dbContext.OdaDurums.ToList();
                    return View("Index", odadurumListesi);
                }
                dbContext.Remove(durum);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int id)
        {
            BalsenContext context = new BalsenContext();
            OdaDurum odaDurumu = context.OdaDurums.Where(k => k.Id == id).FirstOrDefault();
            if (odaDurumu == null)
            {
                return RedirectToAction("Index");
            }

            return View("Update", odaDurumu);
        }

        [HttpPost]
        public IActionResult DoUpdate(OdaDurum odaDurumu)
        {
            if (ModelState.IsValid)
            {
                odaDurumu.GuncellenmeTarihi = DateTime.Now;
                BalsenContext context = new BalsenContext();
                if (context.OdaDurums.Any(k => k.Adi == odaDurumu.Adi && k.Id != odaDurumu.Id))
                {
                    ViewBag.ErrorMessage = "Bu Durum Zaten Kayıtlı.";
                    List<OdaDurum> odadurumListesi = context.OdaDurums.ToList();
                    return View("Index", odadurumListesi);
                }
                if (odaDurumu.Adi == null)
                {
                    ViewBag.ErrorMessage = "Durum Adı Boş Bırakılamaz.";
                    List<OdaDurum> odadurumListesi = context.OdaDurums.ToList();
                    return View("Index", odadurumListesi);
                }
                context.Update(odaDurumu);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Update", odaDurumu);
        }
    }
}
