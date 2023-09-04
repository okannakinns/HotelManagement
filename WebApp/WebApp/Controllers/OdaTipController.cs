using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class OdaTipController : Controller
    {
        public IActionResult Index()
        {
            BalsenContext context = new BalsenContext();
            List<OdaTip> OdaTipListesi = context.OdaTips.ToList();

            return View(OdaTipListesi);
        }


        [HttpPost]
        public IActionResult Ekle(OdaTip odatip)
        {
            if (ModelState.IsValid)
            {
                odatip.EklenmeTarihi = DateTime.Now;
                odatip.Durumu = 1;
                odatip.GuncellenmeTarihi = DateTime.Now;
                BalsenContext context = new BalsenContext();
                if (context.OdaTips.Any(k => k.Adi == odatip.Adi ))
                {
                    ViewBag.ErrorMessage = "Bu Oda Tipi Zaten Kayıtlı.";
                    List<OdaTip>odatipListesi = context.OdaTips.ToList();
                    return View("Index", odatipListesi);
                }
                if (odatip.Adi == null)
                {
                 ViewBag.ErrorMessage = " Oda Tip Adı Boş Bırakılamaz.";
                    List<OdaTip> odatipListesi = context.OdaTips.ToList();
                    return View("Index", odatipListesi);
                }
                context.OdaTips.Add(odatip);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpPost]
        public IActionResult Sil(int id)
        {
            BalsenContext dbContext = new BalsenContext();
            var odatip = dbContext.OdaTips.Find(id);
            var odalar = dbContext.Odalars.FirstOrDefault(o => o.OdaTipId == id);
            if (odatip != null)
            {
                if (odalar != null)
                {
                    ViewBag.ErrorMessage = "Önce Bu Tipin Bulunduğu Odalar Kaydını Değiştirmelisiniz. ";
                    List<OdaTip> odatipListesi = dbContext.OdaTips.ToList();
                    return View("Index", odatipListesi);
                }
                dbContext.Remove(odatip);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int id)
        {
            BalsenContext context = new BalsenContext();
            OdaTip odatip = context.OdaTips.Where(k => k.Id == id).FirstOrDefault();
            if (odatip == null)
            {
                return RedirectToAction("Index");
            }

            return View("Update", odatip);
        }

        [HttpPost]
        public IActionResult DoUpdate(OdaTip odatip)
        {
            if (ModelState.IsValid)
            {
                odatip.GuncellenmeTarihi = DateTime.Now;
                BalsenContext context = new BalsenContext();
                if (context.OdaTips.Any(k => k.Adi == odatip.Adi && k.Id!=odatip.Id))
                {
                    ViewBag.ErrorMessage = "Bu Oda Tipi Zaten Kayıtlı.";
                    List<OdaTip> odatipListesi = context.OdaTips.ToList();
                    return View("Index", odatipListesi);
                }
                if (odatip.Adi == null)
                {
                    ViewBag.ErrorMessage = " Oda Tip Adı Boş Bırakılamaz.";
                    List<OdaTip> odatipListesi = context.OdaTips.ToList();
                    return View("Index", odatipListesi);
                }

                context.Update(odatip);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Update", odatip);
        }

    }
}
