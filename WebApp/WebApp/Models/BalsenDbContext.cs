using Microsoft.EntityFrameworkCore;
using WebApp.Models;


namespace WebApp.Models
{
    public class BalsenDbContext : DbContext
    {
        public DbSet<Odalar> Odalar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=OKAN-LAPTOP\\OKANAKIN;Initial Catalog=Otel;Integrated Security=True;");
            }
        }

        public BalsenDbContext(DbContextOptions<BalsenDbContext> options) : base(options)
        {
        }
    }

}
