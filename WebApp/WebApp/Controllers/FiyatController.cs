using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class FiyatController : Controller
    {
        public IActionResult Index()
        {
            BalsenContext context = new BalsenContext();
            List<Fiyat> FiyatListesi = context.Fiyats.Include(x => x.Odalar).ToList();
            List<Odalar> OdalarListesi = context.Odalars.ToList();
            ViewBag.Odalar = OdalarListesi;
            return View(FiyatListesi);
        }


        [HttpPost]
        public IActionResult Ekle(Fiyat Fiyat)
        {
            Fiyat.EklenmeTarihi = DateTime.Now;
            Fiyat.Durumu = 1;
            Fiyat.GuncellenmeTarihi = DateTime.Now;
            BalsenContext context = new BalsenContext();
            if(context.Fiyats.Any(f => f.OdalarId== Fiyat.OdalarId))
            {
                ViewBag.ErrorMessage = "Bu Odaya Ait Fiyat Bilgisi Zaten Kayıtlı.";
                List<Fiyat> FiyatListesi = context.Fiyats.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index",FiyatListesi);

            }
            if (Fiyat.Tutar == null)
            {
                ViewBag.ErrorMessage = "Tutar Bölümü Boş Bırakılamaz.";
                List<Fiyat> FiyatListesi = context.Fiyats.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", FiyatListesi);
            }
            if (Fiyat.BaslangicTarihi == null)
            {
                ViewBag.ErrorMessage = "Başlangıç Tarihi Boş Bırakılamaz.";
                List<Fiyat> FiyatListesi = context.Fiyats.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", FiyatListesi);
            }
            if (Fiyat.BitisTarihi == null)
            {
                ViewBag.ErrorMessage = "Bitiş Tarihi Boş Bırakılamaz.";
                List<Fiyat> FiyatListesi = context.Fiyats.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", FiyatListesi);
            }
            if (Fiyat.BaslangicTarihi == Fiyat.BitisTarihi)
            {
                ViewBag.ErrorMessage = "Başlangıç Tarihi ile Bitiş Tarihi Aynı Olamaz.";
                List<Fiyat> FiyatListesi = context.Fiyats.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", FiyatListesi);
            }
            context.Fiyats.Add(Fiyat);
                context.SaveChanges();

                return RedirectToAction("Index");
           
        }
        [HttpPost]
        public IActionResult Sil(int id)
        {
            BalsenContext dbContext = new BalsenContext();
            var fiyat = dbContext.Fiyats.Find(id);
            var rezerve = dbContext.OtelRezervasyons.FirstOrDefault(x => x.OdalarId == fiyat.OdalarId);
            if (fiyat != null)
            {
                if(rezerve!= null)
                {
                    ViewBag.ErrorMessage = "Önce Bu Fiyata Ait Odanın Rezervasyonunu Silmelisiniz.";
                    List<Fiyat> FiyatListesi = dbContext.Fiyats.Include(x => x.Odalar).ToList();
                    ViewBag.Odalar = dbContext.Odalars.ToList();
                    return View("Index", FiyatListesi);
                }
                dbContext.Remove(fiyat);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int id)
        {
            BalsenContext context = new BalsenContext();
            Fiyat fiyat = context.Fiyats.Where(k => k.Id == id).FirstOrDefault();
            List<Odalar> OdalarListesi = context.Odalars.ToList();
            ViewBag.Odalar = OdalarListesi;
            if (fiyat == null)
            {
                return RedirectToAction("Index");
            }

            return View("Update", fiyat);
        }

        [HttpPost]
        public IActionResult DoUpdate(Fiyat fiyat)
        {
            fiyat.GuncellenmeTarihi = DateTime.Now;
            BalsenContext context = new BalsenContext();
            if (context.Fiyats.Any(f => f.OdalarId == fiyat.OdalarId && f.Id!=fiyat.Id))
            {
                ViewBag.ErrorMessage = "Bu Odaya Ait Fiyat Bilgisi Zaten Kayıtlı.";
                List<Fiyat> FiyatListesi = context.Fiyats.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", FiyatListesi);

            }
            if (fiyat.Tutar == null)
            {
                ViewBag.ErrorMessage = "Tutar Bölümü Boş Bırakılamaz.";
                List<Fiyat> FiyatListesi = context.Fiyats.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", FiyatListesi);
            }
            if (fiyat.BaslangicTarihi == null)
            {
                ViewBag.ErrorMessage = "Başlangıç Tarihi Boş Bırakılamaz.";
                List<Fiyat> FiyatListesi = context.Fiyats.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", FiyatListesi);
            }
            if (fiyat.BitisTarihi == null)
            {
                ViewBag.ErrorMessage = "Bitiş Tarihi Boş Bırakılamaz.";
                List<Fiyat> FiyatListesi = context.Fiyats.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", FiyatListesi);
            }
            if (fiyat.BaslangicTarihi == fiyat.BitisTarihi)
            {
                ViewBag.ErrorMessage = "Başlangıç Tarihi ile Bitiş Tarihi Aynı Olamaz.";
                List<Fiyat> FiyatListesi = context.Fiyats.Include(x => x.Odalar).ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", FiyatListesi);
            }


            context.Update(fiyat);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

           
        }

    }

