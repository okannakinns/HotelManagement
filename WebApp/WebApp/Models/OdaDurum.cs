using System;
using System.Collections.Generic;


namespace WebApp.Models
{
    public partial class OdaDurum
    {
        public int Id { get; set; }

        public string? Adi { get; set; }

        public int? Durumu { get; set; }

        public DateTime? EklenmeTarihi { get; set; }

        public DateTime? GuncellenmeTarihi { get; set; }


    }
}
