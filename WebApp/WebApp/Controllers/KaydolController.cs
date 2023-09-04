using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class KaydolController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Kullanicilar user)
        {
            BalsenContext context = new BalsenContext();
           
                if (ModelState.IsValid)
                {
                    if (context.Kullanicilars.Any(k => k.Adi == user.Adi))
                    {
                        ViewBag.ErrorMessage = "Bu Kullanıcı Adı Zaten Kullanılıyor";
                        return View("Index");
                    }

                    context.Kullanicilars.Add(user);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View("Index", user);
            }

        }
    }
