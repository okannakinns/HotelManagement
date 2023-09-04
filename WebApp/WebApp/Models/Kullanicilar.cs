using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Kullanicilar
{
    public int Id { get; set; }

    public string? Adi { get; set; }

    public string? Sifre { get; set; }

    public int? Durumu { get; set; }

    

    public DateTime? OlusturulmaTarihi { get; set; }

    public DateTime? GuncellenmeTarihi { get; set; }
}
