using Microsoft.EntityFrameworkCore;
using GigaKino.Models;
using System.Text.Json.Nodes;
using MySqlConnector;
using System.Data;

namespace Database
{
    public class KinoContext : DbContext
    {
        public DbSet<Kino> Kina { get; set; }
        public DbSet<Sala> Sale { get; set; }
        public DbSet<Film> Filmy { get; set; }
        public DbSet<Seans> Seanse { get; set; }
        public DbSet<Miejsce> Miejsca { get; set; }
        public DbSet<Bilet> Bilety { get; set; }
        public DbSet<Transakcja> Transakcje { get; set; }
        public DbSet<Klient> Klienci { get; set; }
        public DbSet<Pracownik> Pracownicy { get; set; }
        public DbSet<Konto> Konta { get; set; }

        public KinoContext(DbContextOptions options) : base(options)
        {
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't estabilish connection with database: {ex.Message}");
            }
        }
    }
}