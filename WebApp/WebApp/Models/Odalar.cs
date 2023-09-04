using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Odalar
{
    public int Id { get; set; }

    public string? Adi { get; set; }

    public int? OdaTipId { get; set; }
    public int? OdaDurumId { get; set; }

    public string? Manzara { get; set; }

    public DateTime? EklenmeTarihi { get; set; }

    public DateTime? GuncellenmeTarihi { get; set; }
    public string? Aciklama { get; set; }

    public short? KisiSayisi { get; set; }

    public int? YatakTipId { get; set; }

    public short Kat { get; set; }

    public short? KapasiteHarici { get; set; }

    public string? Ozellikler { get; set; }

    public int? OdaSayisi { get; set; }

    public int? Durumu { get; set; }

    public virtual YatakTip YatakTip { get; set; }
    public virtual OdaTip OdaTip { get; set; }

    public virtual OdaDurum OdaDurum { get; set; }

    public virtual ICollection<Fiyat> Fiyats { get; set; } = new List<Fiyat>();

    public virtual ICollection<OtelRezervasyon> OtelRezervasyons { get; set; } = new List<OtelRezervasyon>();
}
