using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace WebApp.Controllers
{
    public class GirisYapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Kullanicilar user)
        {
            if (ModelState.IsValid)
            {
                BalsenContext context = new BalsenContext();
                context.Kullanicilars.Add(user);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        

        [HttpPost]
        public IActionResult GirisKontrol(string adi, string sifre)
        {
            BalsenContext dbContext = new BalsenContext();
            var kullanici = dbContext.Kullanicilars.FirstOrDefault(k => k.Adi == adi && k.Sifre == sifre && k.Durumu == 1);

            if (kullanici != null)
            {
               
                // Kullanıcı doğrulandı, yönlendirme yap
                HttpContext.Session.SetString("KullaniciAdi", kullanici.Adi);

                dbContext.SaveChanges();
                return RedirectToAction("Index", "Kullanicilar");
            }
            else
            {
                // Kullanıcı bulunamadı veya durumu 1 değil, hata mesajı gösterebilirsiniz
                
                ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre.";
                return View("Index");
            }
        }
    }
}
