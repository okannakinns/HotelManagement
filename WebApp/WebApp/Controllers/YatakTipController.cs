using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class YatakTipController : Controller
    {
        public IActionResult Index()
        {
            BalsenContext context = new BalsenContext();
            List<YatakTip> YatakTipListesi = context.YatakTips.ToList();

            return View(YatakTipListesi);
        }


        [HttpPost]
        public IActionResult Ekle(YatakTip yatak)
        {
            if (ModelState.IsValid)
            {
                BalsenContext context = new BalsenContext();
                yatak.EklenmeTarihi = DateTime.Now;
                yatak.Durumu = 1;
                yatak.GuncellenmeTarihi= DateTime.Now;
                if(context.YatakTips.Any(y=> y.Adi==yatak.Adi))
                {
                    ViewBag.ErrorMessage = "Bu Yatak Tipi Zaten Kayıtlı.";
                    List<YatakTip> YataktipListesi = context.YatakTips.ToList();
                    return View ("Index", YataktipListesi);
                }
                if (yatak.Adi == null)
                {
                    ViewBag.ErrorMessage = " Yatak Tip Adı Boş Bırakılamaz.";
                    List<YatakTip> YataktipListesi = context.YatakTips.ToList();
                    return View("Index", YataktipListesi);
                }
                context.YatakTips.Add(yatak);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpPost]
        public IActionResult Sil(int id)
        {
            BalsenContext dbContext = new BalsenContext();
            var yatak1 = dbContext.YatakTips.Find(id);
            var oda =dbContext.Odalars.FirstOrDefault(o => o.YatakTipId == id);
            if (yatak1 != null)
            {
                if (oda != null)
                {
                    ViewBag.ErrorMessage = "Bu Yatak Tipinin Bulunduğu Odalar Kaydını Değiştirmelisiniz.";
                    List<YatakTip> YataktipListesi = dbContext.YatakTips.ToList();
                    return View("Index", YataktipListesi);
                }
                dbContext.Remove(yatak1);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Update(int id)
        {
            BalsenContext context = new BalsenContext();
            YatakTip yataktip = context.YatakTips.Where(k => k.Id == id).FirstOrDefault();
            if (yataktip == null)
            {
                return RedirectToAction("Index");
            }

            return View("Update", yataktip);
        }

        [HttpPost]
        public IActionResult DoUpdate(YatakTip yataktip)
        {
            if (ModelState.IsValid)
            {
                BalsenContext context = new BalsenContext();
                yataktip.GuncellenmeTarihi=DateTime.Now;
                if (context.YatakTips.Any(y => y.Adi == yataktip.Adi && y.Id != yataktip.Id))
                {
                    ViewBag.ErrorMessage = "Bu Yatak Tipi Zaten Kayıtlı.";
                    List<YatakTip> YataktipListesi = context.YatakTips.ToList();
                    return View("Index", YataktipListesi);
                }
                if (yataktip.Adi == null)
                {
                    ViewBag.ErrorMessage = " Yatak Tip Adı Boş Bırakılamaz.";
                    List<YatakTip> YataktipListesi = context.YatakTips.ToList();
                    return View("Index", YataktipListesi);
                }
                context.Update(yataktip);
               
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Update", yataktip);
        }

    }
}
