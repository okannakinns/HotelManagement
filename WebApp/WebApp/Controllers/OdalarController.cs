using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class OdalarController : Controller
    {



        public IActionResult Index()
        {
            BalsenContext context = new BalsenContext();
            List<Odalar> OdalarListesi = context.Odalars.Include(x => x.YatakTip).Include(x => x.OdaTip).Include(x => x.OdaDurum).ToList();
            List<OdaDurum> OdaDurumListesi = context.OdaDurums.ToList();
            ViewBag.OdaDurumlari = OdaDurumListesi;
            List<YatakTip> YatakTipListesi = context.YatakTips.ToList();
            ViewBag.YatakTipleri = YatakTipListesi;
            List<OdaTip> OdaTipListesi = context.OdaTips.ToList();
            ViewBag.OdaTipleri = OdaTipListesi;

            return View(OdalarListesi);
        }

        

        [HttpPost]
        public IActionResult Ekle(Odalar oda)
        {
  
           
                BalsenContext context = new BalsenContext();
            oda.EklenmeTarihi = DateTime.Now;
            oda.Durumu = 1;
            oda.GuncellenmeTarihi = DateTime.Now;
            oda.Manzara = "Girilmedi";
            oda.KapasiteHarici = 0;
            if (context.Odalars.Any(o => o.Adi == oda.Adi))
            {
                ViewBag.ErrorMessage = "Bu Oda Zaten Kayıtlı.";
                List<Odalar> OdalarListesi = context.Odalars.Include(x => x.YatakTip).Include(x => x.OdaTip).Include(x => x.OdaDurum).ToList();
                ViewBag.OdaDurumlari = context.OdaDurums.ToList();
                ViewBag.YatakTipleri = context.YatakTips.ToList();
                ViewBag.OdaTipleri = context.OdaTips.ToList();
                return View("Index", OdalarListesi);
            }
            if (oda.Adi == null)
            {
                ViewBag.ErrorMessage = "Oda İsmi Boş Bırakılamaz.";
                List<Odalar> OdalarListesi = context.Odalars.Include(x => x.YatakTip).Include(x => x.OdaTip).Include(x => x.OdaDurum).ToList();
                ViewBag.OdaDurumlari = context.OdaDurums.ToList();
                ViewBag.YatakTipleri = context.YatakTips.ToList();
                ViewBag.OdaTipleri = context.OdaTips.ToList();
                return View("Index", OdalarListesi);
            }
            if (oda.Kat == null)
            {
                ViewBag.ErrorMessage = "Kat Sayısı Boş Bırakılamaz.";
                List<Odalar> OdalarListesi = context.Odalars.Include(x => x.YatakTip).Include(x => x.OdaTip).Include(x => x.OdaDurum).ToList();
                ViewBag.OdaDurumlari = context.OdaDurums.ToList();
                ViewBag.YatakTipleri = context.YatakTips.ToList();
                ViewBag.OdaTipleri = context.OdaTips.ToList();
                return View("Index", OdalarListesi);
            }
            if (oda.OdaSayisi == null)
            {
                ViewBag.ErrorMessage = "Oda Sayısı Boş Bırakılamaz.";
                List<Odalar> OdalarListesi = context.Odalars.Include(x => x.YatakTip).Include(x => x.OdaTip).Include(x => x.OdaDurum).ToList();
                ViewBag.OdaDurumlari = context.OdaDurums.ToList();
                ViewBag.YatakTipleri = context.YatakTips.ToList();
                ViewBag.OdaTipleri = context.OdaTips.ToList();
                return View("Index", OdalarListesi);
            }
            context.Odalars.Add(oda);
           
            context.SaveChanges();

                return RedirectToAction("Index");
            }


        

        [HttpPost]
        public IActionResult Sil(int id)
        {
            BalsenContext dbContext = new BalsenContext();
            var oda = dbContext.Odalars.Find(id);
            var rezervasyon = dbContext.OtelRezervasyons.FirstOrDefault(r => r.OdalarId == id);
            var fiyat = dbContext.Fiyats.FirstOrDefault(r => r.OdalarId == id);
            var fiyatcarpan = dbContext.FiyatCarpans.FirstOrDefault(r => r.OdalarId == id);
            var odaKontenjan = dbContext.OdaKontenjans.FirstOrDefault(r => r.OdalarId == id);


            if (oda != null)
            {
                if (rezervasyon != null)
                {
                    ViewBag.ErrorMessage = "Önce Bu Odaya Ait Rezervasyon Kaydını Silmelisiniz.";
                    List<Odalar> OdalarListesi = dbContext.Odalars.Include(x => x.YatakTip).Include(x => x.OdaTip).Include(x => x.OdaDurum).ToList();
                    ViewBag.OdaDurumlari = dbContext.OdaDurums.ToList();
                    ViewBag.YatakTipleri = dbContext.YatakTips.ToList();
                    ViewBag.OdaTipleri = dbContext.OdaTips.ToList();
                    return View("Index", OdalarListesi);
                }

                if (fiyat != null)
                {
                    ViewBag.ErrorMessage = "Önce Bu Odaya Ait Fiyat Kaydını Silmelisiniz.";
                    List<Odalar> OdalarListesi = dbContext.Odalars.Include(x => x.YatakTip).Include(x => x.OdaTip).Include(x => x.OdaDurum).ToList();
                    ViewBag.OdaDurumlari = dbContext.OdaDurums.ToList();
                    ViewBag.YatakTipleri = dbContext.YatakTips.ToList();
                    ViewBag.OdaTipleri = dbContext.OdaTips.ToList();
                    return View("Index", OdalarListesi);
                }
                if (fiyatcarpan != null)
                {
                    ViewBag.ErrorMessage = "Önce Bu Odaya Ait Fiyat Çarpanı Kaydını Silmelisiniz.";
                    List<Odalar> OdalarListesi = dbContext.Odalars.Include(x => x.YatakTip).Include(x => x.OdaTip).Include(x => x.OdaDurum).ToList();
                    ViewBag.OdaDurumlari = dbContext.OdaDurums.ToList();
                    ViewBag.YatakTipleri = dbContext.YatakTips.ToList();
                    ViewBag.OdaTipleri = dbContext.OdaTips.ToList();
                    return View("Index", OdalarListesi);
                }
                if (odaKontenjan != null)
                {
                    ViewBag.ErrorMessage = "Önce Bu Odaya Ait Kontenjan Kaydını Silmelisiniz.";
                    List<Odalar> OdalarListesi = dbContext.Odalars.Include(x => x.YatakTip).Include(x => x.OdaTip).Include(x => x.OdaDurum).ToList();
                    ViewBag.OdaDurumlari = dbContext.OdaDurums.ToList();
                    ViewBag.YatakTipleri = dbContext.YatakTips.ToList();
                    ViewBag.OdaTipleri = dbContext.OdaTips.ToList();
                    return View("Index", OdalarListesi);
                }
                dbContext.Remove(oda);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int id)
        {
            BalsenContext context = new BalsenContext();
            List<OdaDurum> OdaDurumListesi = context.OdaDurums.ToList();
            ViewBag.OdaDurumlari = OdaDurumListesi;
            List<OdaTip> OdaTipListesi = context.OdaTips.ToList();
            ViewBag.OdaTipleri = OdaTipListesi;
            List<YatakTip> YatakTipListesi = context.YatakTips.ToList();
            ViewBag.YatakTipleri = YatakTipListesi;
            Odalar odalar = context.Odalars.Where(k => k.Id == id).FirstOrDefault();
            if (odalar == null)
            {
                return RedirectToAction("Index");
            }

            return View("Update", odalar);
        }

        [HttpPost]
        public IActionResult DoUpdate(Odalar oda)
        {
           
                BalsenContext context = new BalsenContext();
            oda.GuncellenmeTarihi = DateTime.Now;
            if (context.Odalars.Any(o => o.Adi == oda.Adi && o.Id!=oda.Id))
            {
                ViewBag.ErrorMessage = "Bu Oda Zaten Kayıtlı.";
                List<Odalar> OdalarListesi = context.Odalars.Include(x => x.YatakTip).Include(x => x.OdaTip).Include(x => x.OdaDurum).ToList();
                ViewBag.OdaDurumlari = context.OdaDurums.ToList();
                ViewBag.YatakTipleri = context.YatakTips.ToList();
                ViewBag.OdaTipleri = context.OdaTips.ToList();
                return View("Index", OdalarListesi);
            }
            if (oda.Adi == null)
            {
                ViewBag.ErrorMessage = "Oda İsmi Boş Bırakılamaz.";
                List<Odalar> OdalarListesi = context.Odalars.Include(x => x.YatakTip).Include(x => x.OdaTip).Include(x => x.OdaDurum).ToList();
                ViewBag.OdaDurumlari = context.OdaDurums.ToList();
                ViewBag.YatakTipleri = context.YatakTips.ToList();
                ViewBag.OdaTipleri = context.OdaTips.ToList();
                return View("Index", OdalarListesi);
            }
            if (oda.Kat == null)
            {
                ViewBag.ErrorMessage = "Kat Sayısı Boş Bırakılamaz.";
                List<Odalar> OdalarListesi = context.Odalars.Include(x => x.YatakTip).Include(x => x.OdaTip).Include(x => x.OdaDurum).ToList();
                ViewBag.OdaDurumlari = context.OdaDurums.ToList();
                ViewBag.YatakTipleri = context.YatakTips.ToList();
                ViewBag.OdaTipleri = context.OdaTips.ToList();
                return View("Index", OdalarListesi);
            }
            if (oda.OdaSayisi == null)
            {
                ViewBag.ErrorMessage = "Oda Sayısı Boş Bırakılamaz.";
                List<Odalar> OdalarListesi = context.Odalars.Include(x => x.YatakTip).Include(x => x.OdaTip).Include(x => x.OdaDurum).ToList();
                ViewBag.OdaDurumlari = context.OdaDurums.ToList();
                ViewBag.YatakTipleri = context.YatakTips.ToList();
                ViewBag.OdaTipleri = context.OdaTips.ToList();
                return View("Index", OdalarListesi);
            }
           
            context.Update(oda);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

           
        

    }
}
