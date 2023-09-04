using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Musteri
{
    public int Id { get; set; }

    public string? AdiSoyadi { get; set; }

    public long? TcNo { get; set; }
    public string? Telefon { get; set; }

    public string? Aciklama { get; set; }

    public DateTime? EklenmeTarihi { get; set; }

    public DateTime? GuncellenmeTarihi { get; set; }

    public string? Email { get; set; }

    public short? KaraListe { get; set; }

    public string? KaraListeAciklama { get; set; }

    public string? MusteriTuru { get; set; }

    public int? Durumu { get; set; }

    public virtual ICollection<OtelRezervasyon> OtelRezervasyons { get; set; } = new List<OtelRezervasyon>();
}
