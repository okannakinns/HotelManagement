using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class FiyatCarpanController : Controller
    {
        public IActionResult Index()
        {
            BalsenContext context = new BalsenContext();
            List<FiyatCarpan> FiyatListesi = context.FiyatCarpans.Include(x=>x.Odalar).ToList();
            List<Odalar> OdalarListesi = context.Odalars.ToList();
            ViewBag.Odalar = OdalarListesi;
            return View(FiyatListesi);
        }


        [HttpPost]
        public IActionResult Ekle(FiyatCarpan carpan)
        {
            carpan.EklenmeTarihi = DateTime.Now;
            carpan.Durumu = 1;
            carpan.GuncellenmeTarihi = DateTime.Now;
            BalsenContext context = new BalsenContext();
            if(context.FiyatCarpans.Any(f =>f.OdalarId == carpan.OdalarId))
            {
                ViewBag.ErrorMessage = "Bu Odaya Ait Fiyat Çarpanı Zaten Kayıtlı";
                List<FiyatCarpan> FiyatListesi = context.FiyatCarpans.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index",FiyatListesi);
            }
                context.FiyatCarpans.Add(carpan);
                context.SaveChanges();

                return RedirectToAction("Index");
            

        }
        [HttpPost]
        public IActionResult Sil(int id)
        {
            BalsenContext dbContext = new BalsenContext();
            var fiyat = dbContext.FiyatCarpans.Find(id);
            var rezerve = dbContext.OtelRezervasyons.FirstOrDefault(x => x.OdalarId == fiyat.OdalarId);
            if (fiyat != null)
            {
                    if (rezerve != null)
                    {
                        ViewBag.ErrorMessage = "Önce Bu Fiyat Çarpanına Ait Odanın Rezervasyonunu Silmelisiniz.";
                    List<FiyatCarpan> FiyatListesi = dbContext.FiyatCarpans.Include(x => x.Odalar).ToList();
                    ViewBag.Odalar = dbContext.Odalars.ToList();
                    return View("Index", FiyatListesi);
                }
                    dbContext.Remove(fiyat);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Guncelle(FiyatCarpan carpan)
        {
            BalsenContext dbContext = new BalsenContext();
            if (ModelState.IsValid)
            {
                dbContext.Update(carpan);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carpan);
        }
        [HttpPost]
        public IActionResult Update(int id)
        {
            BalsenContext context = new BalsenContext();
            FiyatCarpan fiyatcarpan = context.FiyatCarpans.Where(k => k.Id == id).FirstOrDefault();
            List<Odalar> OdalarListesi = context.Odalars.ToList();
            ViewBag.Odalar = OdalarListesi;
            if (fiyatcarpan == null)
            {
                return RedirectToAction("Index");
            }

            return View("Update", fiyatcarpan);
        }

        [HttpPost]
        public IActionResult DoUpdate(FiyatCarpan fiyatcarpan)
        {
            
            fiyatcarpan.GuncellenmeTarihi = DateTime.Now;
            BalsenContext context = new BalsenContext();
            if (context.FiyatCarpans.Any(f => f.OdalarId == fiyatcarpan.OdalarId && f.Id!=fiyatcarpan.Id))
            {
                ViewBag.ErrorMessage = "Bu Odaya Ait Fiyat Çarpanı Zaten Kayıtlı";
                List<FiyatCarpan> FiyatListesi = context.FiyatCarpans.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", FiyatListesi);
            }
            context.Update(fiyatcarpan);
                context.SaveChanges();
                return RedirectToAction("Index");
           
        }
    }
}
