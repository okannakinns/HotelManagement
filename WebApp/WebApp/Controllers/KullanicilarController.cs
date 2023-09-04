using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class KullanicilarController : Controller
    {
        public IActionResult Index()
        {
            BalsenContext context = new BalsenContext();
            List<Kullanicilar> KullanicilarListesi = context.Kullanicilars.ToList();

            return View(KullanicilarListesi);
        }


        [HttpPost]
        public IActionResult Ekle(Kullanicilar user)
        {
            if (ModelState.IsValid)
            {
                BalsenContext context = new BalsenContext();
               
                user.OlusturulmaTarihi = DateTime.Now;
                user.Durumu = 1;
                
                user.GuncellenmeTarihi = DateTime.Now;
                //if (user.Sifre == null)
                //{
                //    List<Kullanicilar> kullaniciListesi = context.Kullanicilars.ToList();
                //    return View("Index", kullaniciListesi);
                //}
                if (context.Kullanicilars.Any(k => k.Adi == user.Adi))
                {
                    ViewBag.ErrorMessage = "Bu Kullanıcı Adı Zaten Kullanılıyor.";
                    List<Kullanicilar> kullaniciListesi = context.Kullanicilars.ToList();
                    return View("Index", kullaniciListesi);
                }
                if (user.Sifre == null)
                {
                    ViewBag.ErrorMessage = "Şifre Boş Bırakılamaz.";
                    List<Kullanicilar> kullaniciListesi = context.Kullanicilars.ToList();
                    return View("Index", kullaniciListesi);
                }

                context.Kullanicilars.Add(user);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpPost]
        public IActionResult Sil(int id)
        {
            BalsenContext dbContext = new BalsenContext();
            var user = dbContext.Kullanicilars.Find(id);
            if (user != null)
            {
                dbContext.Remove(user);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int id)
        {
            BalsenContext context = new BalsenContext();
            Kullanicilar kullanici = context.Kullanicilars.Where(k => k.Id == id).FirstOrDefault();
            if (kullanici == null)
            {
                return RedirectToAction("Index");
            }

            return View("Update", kullanici);
        }

        [HttpPost]
        public IActionResult DoUpdate(Kullanicilar kullanici)
        {
            if (ModelState.IsValid)
            {
                kullanici.GuncellenmeTarihi = DateTime.Now;
                BalsenContext context = new BalsenContext();
                
                if (context.Kullanicilars.Any(k => k.Adi == kullanici.Adi && k.Id != kullanici.Id))
                {
                    ViewBag.ErrorMessage = "Bu Kullanıcı Adı Zaten Kullanılıyor.";
                    List<Kullanicilar> kullaniciListesi = context.Kullanicilars.ToList();
                    return View("Index", kullaniciListesi);
                }
                if (kullanici.Sifre == null)
                {
                    ViewBag.ErrorMessage = "Şifre Boş Bırakılamaz.";
                    List<Kullanicilar> kullaniciListesi = context.Kullanicilars.ToList();
                    return View("Index", kullaniciListesi);
                }
                context.Update(kullanici);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Update", kullanici);
        }

    }
}
