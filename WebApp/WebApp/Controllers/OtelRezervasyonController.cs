using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class OtelRezervasyonController : Controller
    {
        public IActionResult Index()
        {
            BalsenContext context = new BalsenContext();
            List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
            List<Musteri> MusteriListesi = context.Musteris.ToList();
            ViewBag.Musteri = MusteriListesi;
            List<Fiyat> FiyatListesi = context.Fiyats.ToList();
            ViewBag.Fiyat = FiyatListesi;
            List<Odalar> OdalarListesi = context.Odalars.ToList();
            ViewBag.Odalar = OdalarListesi;
            return View(OtelRezervasyonListesi);
        }


        [HttpPost]
        public IActionResult Ekle(OtelRezervasyon rezerve, decimal odafiyat, int toplamkisi)
        {
            using (BalsenContext context = new BalsenContext())
            {
                Fiyat fiyat = context.Fiyats.FirstOrDefault(f => f.OdalarId == rezerve.OdalarId);
                if (fiyat == null)
                {
                   
                    ViewBag.ErrorMessage = "Bu Odaya Ait Fiyat Verisi Bulunamadı.";
                    List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                    ViewBag.Musteri = context.Musteris.ToList();
                    ViewBag.Fiyat = context.Fiyats.ToList();
                    ViewBag.Odalar = context.Odalars.ToList();
                    return View("Index", OtelRezervasyonListesi);

                }
                FiyatCarpan carpan = context.FiyatCarpans.FirstOrDefault(c => c.OdalarId == rezerve.OdalarId);

                if (carpan == null)
                {
                    ViewBag.ErrorMessage = "Bu Odaya Ait Fiyat Çarpanı Verisi Bulunamadı.";
                    List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                    ViewBag.Musteri = context.Musteris.ToList();
                    ViewBag.Fiyat = context.Fiyats.ToList();
                    ViewBag.Odalar = context.Odalars.ToList();
                    return View("Index", OtelRezervasyonListesi);

                }


                rezerve.EklenmeTarihi = DateTime.Now;
            rezerve.Durumu = 1;
            rezerve.GuncellenmeTarihi = DateTime.Now;
            //rezerve.Cocuk_2 = 0;
            //rezerve.Cocuk_3 = 0;
            rezerve.ParaBirimi = "TL";
            rezerve.MusteriTuru = "Belirtilmedi";
            rezerve.Onay = 0;
           
                if(context.OtelRezervasyons.Any(r => r.OdalarId == rezerve.OdalarId))
                {
                    ViewBag.ErrorMessage = "Bu Odaya Ait Bir Rezervasyon Zaten Var.";
                    List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                    ViewBag.Musteri=context.Musteris.ToList();
                    ViewBag.Fiyat= context.Fiyats.ToList();
                    ViewBag.Odalar= context.Odalars.ToList();
                    return View("Index", OtelRezervasyonListesi);
                }
                if (context.OtelRezervasyons.Any(r => r.MusteriId == rezerve.MusteriId))
                {
                    ViewBag.ErrorMessage = "Bu Müşteriye Ait Bir Rezervasyon Zaten Var.";
                    List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                    ViewBag.Musteri = context.Musteris.ToList();
                    ViewBag.Fiyat = context.Fiyats.ToList();
                    ViewBag.Odalar = context.Odalars.ToList();
                    return View("Index", OtelRezervasyonListesi);
                }
                if (rezerve.GirisTarihi==null)
                {
                    ViewBag.ErrorMessage = "Giriş Tarihi Boş Bırakılamaz.";
                    List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                    ViewBag.Musteri = context.Musteris.ToList();
                    ViewBag.Fiyat = context.Fiyats.ToList();
                    ViewBag.Odalar = context.Odalars.ToList();
                    return View("Index", OtelRezervasyonListesi);
                }
                if (rezerve.CikisTarihi == null)
                {
                    ViewBag.ErrorMessage = "Çıkış Tarihi Boş Bırakılamaz.";
                    List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                    ViewBag.Musteri = context.Musteris.ToList();
                    ViewBag.Fiyat = context.Fiyats.ToList();
                    ViewBag.Odalar = context.Odalars.ToList();
                    return View("Index", OtelRezervasyonListesi);
                }
                if (rezerve.GirisTarihi==rezerve.CikisTarihi)
                {
                    ViewBag.ErrorMessage = "Giriş Tarihi İle Çıkış Tarihi Aynı Olamaz.";
                    List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                    ViewBag.Musteri = context.Musteris.ToList();
                    ViewBag.Fiyat = context.Fiyats.ToList();
                    ViewBag.Odalar = context.Odalars.ToList();
                    return View("Index", OtelRezervasyonListesi);
                }
                TimeSpan GunHesaplama = rezerve.CikisTarihi.Value - rezerve.GirisTarihi.Value;
                int GunSayisi = GunHesaplama.Days;
                rezerve.KonaklamaGunSayisi = GunSayisi;
                if (GunSayisi < 1)
                {
                    ViewBag.ErrorMessage = "İki Tarih Arasında En Az 1 Gün Fark Olmalıdır.";
                    List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                    ViewBag.Musteri = context.Musteris.ToList();
                    ViewBag.Fiyat = context.Fiyats.ToList();
                    ViewBag.Odalar = context.Odalars.ToList();
                    return View("Index", OtelRezervasyonListesi);
                }
                if (rezerve.Yetiskin == null)
                {
                    ViewBag.ErrorMessage = "Yetişkin Sayısı Boş Bırakılamaz.";
                    List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                    ViewBag.Musteri = context.Musteris.ToList();
                    ViewBag.Fiyat = context.Fiyats.ToList();
                    ViewBag.Odalar = context.Odalars.ToList();
                    return View("Index", OtelRezervasyonListesi);
                }
                if (rezerve.CocukSayisi == null)
                {
                    ViewBag.ErrorMessage = "Çocuk Sayısı Boş Bırakılamaz.";
                    List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                    ViewBag.Musteri = context.Musteris.ToList();
                    ViewBag.Fiyat = context.Fiyats.ToList();
                    ViewBag.Odalar = context.Odalars.ToList();
                    return View("Index", OtelRezervasyonListesi);
                }
                if (rezerve.UcretliCocuk == null)
                {
                    ViewBag.ErrorMessage = "Ücretli Çocuk Sayısı Boş Bırakılamaz.";
                    List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                    ViewBag.Musteri = context.Musteris.ToList();
                    ViewBag.Fiyat = context.Fiyats.ToList();
                    ViewBag.Odalar = context.Odalars.ToList();
                    return View("Index", OtelRezervasyonListesi);
                }
               

               
                if (fiyat != null && carpan != null)
                {
                    odafiyat = (decimal)fiyat.Tutar;
                    toplamkisi = rezerve.Yetiskin.Value + rezerve.CocukSayisi.Value + rezerve.UcretliCocuk.Value;
                    if (toplamkisi == 1)
                    {
                        decimal tekKisiFiyati = carpan.TekKisilik.Value * odafiyat;
                        rezerve.GecelikUcret = tekKisiFiyati;
                        rezerve.ToplamUcret = rezerve.GecelikUcret * GunSayisi;




                    }
                    if (toplamkisi == 2)
                    {
                        decimal ciftKisiFiyati = carpan.CiftKisilik.Value * odafiyat;
                        rezerve.GecelikUcret = ciftKisiFiyati;
                        rezerve.ToplamUcret = rezerve.GecelikUcret * GunSayisi;




                    }
                    if (toplamkisi == 3)
                    {
                        decimal UcKisiFiyati = carpan.UcKisilik.Value * odafiyat;
                        rezerve.GecelikUcret = UcKisiFiyati;
                        rezerve.ToplamUcret = rezerve.GecelikUcret * GunSayisi;




                    }
                    if (toplamkisi == 4)
                    {
                        decimal dortKisiFiyati = carpan.DortKisilik.Value * odafiyat;
                        rezerve.GecelikUcret = dortKisiFiyati;
                        rezerve.ToplamUcret = rezerve.GecelikUcret * GunSayisi;




                    }
                    if (toplamkisi == 5)
                    {
                        decimal besKisiFiyati = carpan.BesKisilik.Value * odafiyat;
                        rezerve.GecelikUcret = besKisiFiyati;
                        rezerve.ToplamUcret = rezerve.GecelikUcret * GunSayisi;




                    }
                    if (toplamkisi == 6)
                    {
                        decimal altiKisiFiyati = carpan.AltiKisilik.Value * odafiyat;
                        rezerve.GecelikUcret = altiKisiFiyati;
                        rezerve.ToplamUcret = rezerve.GecelikUcret * GunSayisi;




                    }
                    context.OtelRezervasyons.Add(rezerve);
                    context.SaveChanges();
                  int odaAdi = rezerve.OdalarId;
                    Odalar odaliste = context.Odalars.FirstOrDefault(x => x.Id == rezerve.OdalarId);

                    if (odaliste != null)
                    {
                        odaliste.OdaDurumId = 4;
                    }
                    context.SaveChanges();
                }


            }

            return RedirectToAction("Index");



        }
        [HttpPost]
        public IActionResult Sil(int id)
        {
            BalsenContext dbContext = new BalsenContext();
            var rezerve = dbContext.OtelRezervasyons.Find(id);
           
            Odalar odaliste = dbContext.Odalars.FirstOrDefault(x => x.Id == rezerve.OdalarId);
           
           
            if (rezerve != null)
            {
                dbContext.Remove(rezerve);
                dbContext.SaveChanges();
                if (odaliste!=null)
                {
                    odaliste.OdaDurumId = 2;
                }
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int id)
        {
            BalsenContext context = new BalsenContext();
            OtelRezervasyon rezerve = context.OtelRezervasyons.Where(k => k.Id == id).FirstOrDefault();
            List<Musteri> MusteriListesi = context.Musteris.ToList();
            ViewBag.Musteri = MusteriListesi;
            List<Fiyat> FiyatListesi = context.Fiyats.ToList();
            ViewBag.Fiyat = FiyatListesi;
            List<Odalar> OdalarListesi = context.Odalars.ToList();
            ViewBag.Odalar = OdalarListesi;
            if (rezerve == null)
            {
                return RedirectToAction("Index");
            }

            return View("Update", rezerve);
        }

        [HttpPost]
        public IActionResult DoUpdate(OtelRezervasyon rezerve,decimal odafiyat,int toplamkisi)
        {
            

            BalsenContext context = new BalsenContext();
            Fiyat fiyat = context.Fiyats.FirstOrDefault(f => f.OdalarId == rezerve.OdalarId);
            if (fiyat == null)
            {
                ViewBag.ErrorMessage = "Bu Odaya Ait Fiyat Verisi Bulunamadı.";
                List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                ViewBag.Musteri = context.Musteris.ToList();
                ViewBag.Fiyat = context.Fiyats.ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", OtelRezervasyonListesi);

            }
            FiyatCarpan carpan = context.FiyatCarpans.FirstOrDefault(c => c.OdalarId == rezerve.OdalarId);

            if (carpan == null)
            {
                ViewBag.ErrorMessage = "Bu Odaya Ait Fiyat Çarpanı Verisi Bulunamadı.";
                List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                ViewBag.Musteri = context.Musteris.ToList();
                ViewBag.Fiyat = context.Fiyats.ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", OtelRezervasyonListesi);

            }
            if (context.OtelRezervasyons.Any(r => r.OdalarId == rezerve.OdalarId && r.Id!=rezerve.Id))
            {
                ViewBag.ErrorMessage = "Bu Odaya Ait Bir Rezervasyon Zaten Var.";
                List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                ViewBag.Musteri = context.Musteris.ToList();
                ViewBag.Fiyat = context.Fiyats.ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", OtelRezervasyonListesi);
            }
            if (context.OtelRezervasyons.Any(r => r.MusteriId == rezerve.MusteriId && r.Id != rezerve.Id))
            {
                ViewBag.ErrorMessage = "Bu Müşteriye Ait Bir Rezervasyon Zaten Var.";
                List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                ViewBag.Musteri = context.Musteris.ToList();
                ViewBag.Fiyat = context.Fiyats.ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", OtelRezervasyonListesi);
            }
            if (rezerve.GirisTarihi == null)
            {
                ViewBag.ErrorMessage = "Giriş Tarihi Boş Bırakılamaz.";
                List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                ViewBag.Musteri = context.Musteris.ToList();
                ViewBag.Fiyat = context.Fiyats.ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", OtelRezervasyonListesi);
            }
            if (rezerve.CikisTarihi == null)
            {
                ViewBag.ErrorMessage = "Çıkış Tarihi Boş Bırakılamaz.";
                List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                ViewBag.Musteri = context.Musteris.ToList();
                ViewBag.Fiyat = context.Fiyats.ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", OtelRezervasyonListesi);
            }
            if (rezerve.GirisTarihi == rezerve.CikisTarihi)
            {
                ViewBag.ErrorMessage = "Giriş Tarihi İle Çıkış Tarihi Aynı Olamaz.";
                List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                ViewBag.Musteri = context.Musteris.ToList();
                ViewBag.Fiyat = context.Fiyats.ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", OtelRezervasyonListesi);
            }
            TimeSpan GunHesaplama = rezerve.CikisTarihi.Value - rezerve.GirisTarihi.Value;
            int GunSayisi = GunHesaplama.Days;
            rezerve.KonaklamaGunSayisi = GunSayisi;
            rezerve.GuncellenmeTarihi = DateTime.Now;
            if (GunSayisi<1)
            {
                ViewBag.ErrorMessage = "İki Tarih Arasında En Az 1 Gün Fark Olmalıdır.";
                List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                ViewBag.Musteri = context.Musteris.ToList();
                ViewBag.Fiyat = context.Fiyats.ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", OtelRezervasyonListesi);
            }
            if (rezerve.Yetiskin == null)
            {
                ViewBag.ErrorMessage = "Yetişkin Sayısı Boş Bırakılamaz.";
                List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                ViewBag.Musteri = context.Musteris.ToList();
                ViewBag.Fiyat = context.Fiyats.ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", OtelRezervasyonListesi);
            }
            if (rezerve.CocukSayisi == null)
            {
                ViewBag.ErrorMessage = "Çocuk Sayısı Boş Bırakılamaz.";
                List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                ViewBag.Musteri = context.Musteris.ToList();
                ViewBag.Fiyat = context.Fiyats.ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", OtelRezervasyonListesi);
            }
            if (rezerve.UcretliCocuk == null)
            {
                ViewBag.ErrorMessage = "Ücretli Çocuk Sayısı Boş Bırakılamaz.";
                List<OtelRezervasyon> OtelRezervasyonListesi = context.OtelRezervasyons.Include(x => x.Musteri).Include(x => x.Odalar).ToList();
                ViewBag.Musteri = context.Musteris.ToList();
                ViewBag.Fiyat = context.Fiyats.ToList();
                ViewBag.Odalar = context.Odalars.ToList();
                return View("Index", OtelRezervasyonListesi);
            }
            
            if (fiyat != null && carpan != null)
            {
                odafiyat = (decimal)fiyat.Tutar;
                toplamkisi = rezerve.Yetiskin.Value + rezerve.CocukSayisi.Value + rezerve.UcretliCocuk.Value;
                if (toplamkisi == 1)
                {
                    decimal tekKisiFiyati = carpan.TekKisilik.Value * odafiyat;
                    rezerve.GecelikUcret = tekKisiFiyati;
                    rezerve.ToplamUcret = rezerve.GecelikUcret * GunSayisi;




                }
                if (toplamkisi == 2)
                {
                    decimal ciftKisiFiyati = carpan.CiftKisilik.Value * odafiyat;
                    rezerve.GecelikUcret = ciftKisiFiyati;
                    rezerve.ToplamUcret = rezerve.GecelikUcret * GunSayisi;




                }
                if (toplamkisi == 3)
                {
                    decimal UcKisiFiyati = carpan.UcKisilik.Value * odafiyat;
                    rezerve.GecelikUcret = UcKisiFiyati;
                    rezerve.ToplamUcret = rezerve.GecelikUcret * GunSayisi;




                }
                if (toplamkisi == 4)
                {
                    decimal dortKisiFiyati = carpan.DortKisilik.Value * odafiyat;
                    rezerve.GecelikUcret = dortKisiFiyati;
                    rezerve.ToplamUcret = rezerve.GecelikUcret * GunSayisi;




                }
                if (toplamkisi == 5)
                {
                    decimal besKisiFiyati = carpan.BesKisilik.Value * odafiyat;
                    rezerve.GecelikUcret = besKisiFiyati;
                    rezerve.ToplamUcret = rezerve.GecelikUcret * GunSayisi;




                }
                if (toplamkisi == 6)
                {
                    decimal altiKisiFiyati = carpan.AltiKisilik.Value * odafiyat;
                    rezerve.GecelikUcret = altiKisiFiyati;
                    rezerve.ToplamUcret = rezerve.GecelikUcret * GunSayisi;




                }
                context.Update(rezerve);
                context.SaveChanges();
                int odaAdi = rezerve.OdalarId;
                Odalar odaliste = context.Odalars.FirstOrDefault(x => x.Id == rezerve.OdalarId);
                if (odaliste != null)
                {
                    odaliste.OdaDurumId = 4;
                }

                short UcretliCocukSayac = 0;
                    if(rezerve.CocukYasi1 > 8){
                    UcretliCocukSayac++;
                    }
                    if (rezerve.CocukYasi2 > 8)
                    {
                    UcretliCocukSayac++;
                    }
                    if (rezerve.CocukYasi3 > 8)
                    {
                    UcretliCocukSayac++;
                    }
                    if (rezerve.CocukYasi4 > 8)
                    {
                    UcretliCocukSayac++;
                    }
                rezerve.UcretliCocuk = UcretliCocukSayac;
            }
                context.SaveChanges();



            
            return RedirectToAction("Index");

        }
    }
}
