using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApp.Models
{
    public partial class BalsenContext : DbContext
    {
        public BalsenContext()
        {
        }

        public BalsenContext(DbContextOptions<BalsenContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Fiyat> Fiyats { get; set; }
        public virtual DbSet<FiyatCarpan> FiyatCarpans { get; set; }
        public virtual DbSet<Kullanicilar> Kullanicilars { get; set; }
        public virtual DbSet<Musteri> Musteris { get; set; }
        public virtual DbSet<OdaKontenjan> OdaKontenjans { get; set; }
        public virtual DbSet<OdaTip> OdaTips { get; set; }
        public virtual DbSet<Odalar> Odalars { get; set; }
        public virtual DbSet<OdaDurum> OdaDurums { get; set; }
        public virtual DbSet<OtelRezervasyon> OtelRezervasyons { get; set; }
        public virtual DbSet<YatakTip> YatakTips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=OKAN-LAPTOP\\OKANAKIN;Initial Catalog=Otel;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fiyat>(entity =>
            {
                entity.ToTable("Fiyat");

       

                entity.HasOne(d => d.Odalar).WithMany(p => p.Fiyats)
                    .HasForeignKey(d => d.OdalarId)
                    .HasConstraintName("FK_Fiyat_OdaId");
            });

            modelBuilder.Entity<FiyatCarpan>(entity =>
            {
                entity.ToTable("FiyatCarpan");


            });

            modelBuilder.Entity<Kullanicilar>(entity =>
            {
                entity.ToTable("Kullanicilar");

            });

            modelBuilder.Entity<Musteri>(entity =>
            {
                entity.ToTable("Musteri");

            });

            modelBuilder.Entity<OdaKontenjan>(entity =>
            {
                entity.ToTable("OdaKontenjan");

            });

            modelBuilder.Entity<OdaTip>(entity =>
            {
                entity.ToTable("OdaTip");

            });

            modelBuilder.Entity<Odalar>(entity =>
            {
                entity.ToTable("Odalar");

            });
            modelBuilder.Entity<OdaDurum>(entity =>
            {
                entity.ToTable("OdaDurum");

            });

            modelBuilder.Entity<OtelRezervasyon>(entity =>
            {
                entity.ToTable("OtelRezervasyon");

              
                //entity.HasOne(d => d.Fiyat).WithMany(p => p.OtelRezervasyons)
                //    .HasForeignKey(d => d.FiyatId)
                //    .HasConstraintName("FK_OtelRezervasyon_FiyatId");

                entity.HasOne(d => d.Musteri).WithMany(p => p.OtelRezervasyons)
                    .HasForeignKey(d => d.MusteriId)
                    .HasConstraintName("FK_OtelRezervasyon_MusteriId");

                entity.HasOne(d => d.Odalar).WithMany(p => p.OtelRezervasyons)
                    .HasForeignKey(d => d.OdalarId)
                    .HasConstraintName("FK_OtelRezervasyon_OdaId");
            });

            modelBuilder.Entity<YatakTip>(entity =>
            {
                entity.ToTable("YatakTip");

            });
            

        }
    }
}
