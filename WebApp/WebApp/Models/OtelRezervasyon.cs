using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class OtelRezervasyon
{
    public int Id { get; set; }

    public DateTime? GirisTarihi { get; set; }

    public DateTime? CikisTarihi { get; set; }

    public short? Yetiskin { get; set; }

    public short? UcretliCocuk { get; set; }

    public short? CocukSayisi { get; set; }

    public short? CocukYasi1 { get; set; }

    public short? CocukYasi2 { get; set; }
    public short? CocukYasi3 { get; set; }

    public short? CocukYasi4{ get; set; }


    public int OdalarId { get; set; }

    public decimal GecelikUcret { get; set; }

    public string? ParaBirimi { get; set; }

    public short? Durumu { get; set; }

    public DateTime? GuncellenmeTarihi { get; set; }

    public string? MusteriTuru { get; set; }

    public int MusteriId { get; set; }

    public DateTime? EklenmeTarihi { get; set; }

    public int? Onay { get; set; }

    public int? FiyatId { get; set; }

    public decimal ToplamUcret { get; set; }

    public int? KonaklamaGunSayisi { get; set; }

    //public virtual Fiyat Fiyat { get; set; }

    public virtual Musteri Musteri { get; set; }

    public virtual Odalar Odalar { get; set; }
}
