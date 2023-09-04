using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class FiyatCarpan
{
    public int Id { get; set; }

    public int? OdalarId { get; set; }

    public decimal? TekKisilik { get; set; }

    public decimal? CiftKisilik { get; set; }

    public decimal? UcKisilik { get; set; }

    public decimal? DortKisilik { get; set; }

    public decimal? BesKisilik { get; set; }

    public decimal? AltiKisilik { get; set; }

    public decimal? Cocuk { get; set; }

    public int? Durumu { get; set; }

    public DateTime? GuncellenmeTarihi { get; set; }

    public DateTime? EklenmeTarihi { get; set; }
    public virtual Odalar Odalar { get; set; } 
}
