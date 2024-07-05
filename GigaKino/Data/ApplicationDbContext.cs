using GigaKino.Models;
using Microsoft.EntityFrameworkCore;

namespace GigaKino.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Transakcja> Transakcje { get; set; }
        public DbSet<Klient> Klienci { get; set; }
        public DbSet<Bilet> Bilety { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация модели Transakcja
            modelBuilder.Entity<Transakcja>(entity =>
            {
                entity.HasKey(e => e.IdTransakcja);
                entity.HasOne(e => e.Klient)
                    .WithMany(k => k.Transakcje)
                    .HasForeignKey(e => e.IdKlient);
            });

            // Конфигурация модели Klient
            modelBuilder.Entity<Klient>(entity =>
            {
                entity.HasKey(e => e.IdKlient);
                entity.Property(e => e.Mail).IsRequired();
            });

            // Конфигурация модели Bilet
            modelBuilder.Entity<Bilet>(entity =>
            {
                entity.HasKey(e => e.IdBilet);
                entity.HasOne(e => e.Transakcja)
                    .WithMany(t => t.Bilety)
                    .HasForeignKey(e => e.IdTransakcja);
            });
        }
    }
}
