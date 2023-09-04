using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class OdaKontenjan
{
    public int Id { get; set; }

    public int? OdalarId { get; set; }

    public int? Miktar { get; set; }

    public DateTime? BaslangicTarihi { get; set; }

    public DateTime? BitisTarihi { get; set; }

    public int? Durumu { get; set; }

    public DateTime? EklenmeTarihi { get; set; }

    public DateTime? GuncellenmeTarihi { get; set; }
    public virtual Odalar Odalar { get; set; }
}
