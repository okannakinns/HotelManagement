using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class MusteriController : Controller
    {
        public IActionResult Index()
        {
            BalsenContext context = new BalsenContext();
            List<Musteri> MusteriListesi = context.Musteris.ToList();

            return View(MusteriListesi);
        }


        [HttpPost]
        public IActionResult Ekle(Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                musteri.EklenmeTarihi = DateTime.Now;
                musteri.Durumu = 1;
                musteri.GuncellenmeTarihi = DateTime.Now;
                musteri.KaraListe = 0;
                musteri.KaraListeAciklama = "Girilmedi";
                musteri.MusteriTuru = "Girilmedi";
                BalsenContext context = new BalsenContext();
                if (context.Musteris.Any(k => k.AdiSoyadi == musteri.AdiSoyadi && k.Telefon == musteri.Telefon))
                {
                    ViewBag.ErrorMessage = "Bu Müşteri Zaten Kayıtlı.";
                    List<Musteri> musteriListesi = context.Musteris.ToList();
                    return View("Index", musteriListesi);
                }
                if (context.Musteris.Any(k => k.TcNo == musteri.TcNo ))
                {
                    ViewBag.ErrorMessage = "Bu TC Kimlik Numarası Zaten Kullanılıyor.";
                    List<Musteri> musteriListesi = context.Musteris.ToList();
                    return View("Index", musteriListesi);
                }
                if (context.Musteris.Any(k => k.Email == musteri.Email))
                {
                    ViewBag.ErrorMessage = "Bu E-posta Zaten Kullanılıyor.";
                    List<Musteri> musteriListesi = context.Musteris.ToList();
                    return View("Index", musteriListesi);
                }
                if (musteri.Email==null)
                {
                    ViewBag.ErrorMessage = "E-posta Boş Bırakılamaz.";
                    List<Musteri> musteriListesi = context.Musteris.ToList();
                    return View("Index", musteriListesi);
                }
                if (musteri.AdiSoyadi == null)
                {
                    ViewBag.ErrorMessage = "Müşteri Adı Soyadı Boş Bırakılamaz.";
                    List<Musteri> musteriListesi = context.Musteris.ToList();
                    return View("Index", musteriListesi);
                }
                if (musteri.Telefon == null)
                {
                    ViewBag.ErrorMessage = "Telefon Numarası Boş Bırakılamaz.";
                    List<Musteri> musteriListesi = context.Musteris.ToList();
                    return View("Index", musteriListesi);
                }
                context.Musteris.Add(musteri);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpPost]
        public IActionResult Sil(int id)
        {
            BalsenContext dbContext = new BalsenContext();

            var musteri = dbContext.Musteris.Find(id);
            var rezervasyon = dbContext.OtelRezervasyons.FirstOrDefault(r => r.MusteriId == id);

            if (musteri != null)
            {
                if (rezervasyon != null)
                {
                    ViewBag.ErrorMessage = "Önce Bu Müşteriye Ait Rezervasyon Kaydını Silmelisiniz.";
                    List<Musteri> musteriListesi = dbContext.Musteris.ToList();
                    return View("Index", musteriListesi);
                }

                dbContext.Remove(musteri);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int id)
        {
            BalsenContext context = new BalsenContext();
            Musteri musteri = context.Musteris.Where(k => k.Id == id).FirstOrDefault();
            if (musteri == null)
            {
                return RedirectToAction("Index");
            }

            return View("Update", musteri);
        }

        [HttpPost]
        public IActionResult DoUpdate(Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                musteri.GuncellenmeTarihi = DateTime.Now;
                BalsenContext context = new BalsenContext();
                if (context.Musteris.Any(k => k.AdiSoyadi == musteri.AdiSoyadi && k.Telefon == musteri.Telefon && k.Id != musteri.Id))
                {
                    ViewBag.ErrorMessage = "Bu Müşteri Zaten Kayıtlı.";
                    List<Musteri> musteriListesi = context.Musteris.ToList();
                    return View("Index", musteriListesi);
                }
                if (context.Musteris.Any(k => k.TcNo == musteri.TcNo && k.Id != musteri.Id))
                {
                    ViewBag.ErrorMessage = "Bu TC Kimlik Numarası Zaten Kullanılıyor.";
                    List<Musteri> musteriListesi = context.Musteris.ToList();
                    return View("Index", musteriListesi);
                }
                if (context.Musteris.Any(k => k.Email == musteri.Email && k.Id != musteri.Id))
                {
                    ViewBag.ErrorMessage = "Bu E-posta Zaten Kullanılıyor.";
                    List<Musteri> musteriListesi = context.Musteris.ToList();
                    return View("Index", musteriListesi);
                }
                if (musteri.Email == null)
                {
                    ViewBag.ErrorMessage = "E-posta Boş Bırakılamaz.";
                    List<Musteri> musteriListesi = context.Musteris.ToList();
                    return View("Index", musteriListesi);
                }
                if (musteri.AdiSoyadi == null)
                {
                    ViewBag.ErrorMessage = "Müşteri Adı Soyadı Boş Bırakılamaz.";
                    List<Musteri> musteriListesi = context.Musteris.ToList();
                    return View("Index", musteriListesi);
                }
                if (musteri.Telefon == null)
                {
                    ViewBag.ErrorMessage = "Telefon Numarası Boş Bırakılamaz.";
                    List<Musteri> musteriListesi = context.Musteris.ToList();
                    return View("Index", musteriListesi);
                }
                context.Update(musteri);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Update", musteri);
        }

    }
}
