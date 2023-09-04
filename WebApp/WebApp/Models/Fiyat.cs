using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Fiyat
{
    public int Id { get; set; }

    public int? OdalarId { get; set; }

    public decimal? Tutar { get; set; }

    public DateTime? BaslangicTarihi { get; set; }

    public DateTime? BitisTarihi { get; set; }

    public DateTime? GuncellenmeTarihi { get; set; }

    public DateTime? EklenmeTarihi { get; set; }

    public string? Aciklama { get; set; }

    public int? Durumu { get; set; }

    public virtual Odalar Odalar { get; set; }

    public virtual ICollection<OtelRezervasyon> OtelRezervasyons { get; set; } = new List<OtelRezervasyon>();
}
