using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class RezervasyonEkranController : Controller
    {
        public IActionResult Index()
        {
            BalsenContext context = new BalsenContext();
            List<Odalar> OdalarListesi = context.Odalars.Include(x => x.YatakTip).Include(x => x.OdaTip).Include(x => x.OdaDurum).ToList();
            ViewBag.OdaDurum = context.OdaDurums.ToList();
            ViewBag.Tipler=context.OdaTips.ToList();
            ViewBag.Musteri=context.Musteris.ToList();
            List<short> KatListesi = new List<short>();
            KatListesi=OdalarListesi.Select(x=>x.Kat).Distinct().OrderBy(x=>x).ToList();
            ViewBag.KatSayisi = KatListesi;
            OdaKatDurumVM odaKatDurum=new OdaKatDurumVM();
            List<OdaKatDurumVM> odaKatDurumListesi = new List<OdaKatDurumVM>();
            foreach (int kat in KatListesi)
            {
                List<Odalar> odalars = new List<Odalar>();
                odalars=OdalarListesi.Where(x=>x.Durumu==1&&x.Kat==kat).ToList();
                odaKatDurum = new OdaKatDurumVM();
                odaKatDurum.Kat = kat;
                odaKatDurum.OdaListesi = odalars;
                odaKatDurumListesi.Add(odaKatDurum);
                ViewBag.Katlar = odaKatDurumListesi;

            }







            return View(OdalarListesi);
        }
    }
}
