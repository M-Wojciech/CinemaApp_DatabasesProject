using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using GigaKino.Models;

namespace Database
{
    public class KinoContext : DbContext
    {
        // public DbSet<Film> Kina { get; set; }
        // public DbSet<Sala> Sale { get; set; }
        public DbSet<Film> Filmy { get; set; }
        // public DbSet<Seans> Seanse { get; set; }
        // public DbSet<Miejsce> Miejsca { get; set; }
        // public DbSet<Bilet> Bilety { get; set; }
        // public DbSet<Transakcja> Transakcje { get; set; }
        // public DbSet<Klient> Klienci { get; set; }
        // public DbSet<Pracownik> Pracownicy { get; set; }
        // public DbSet<Konto> Konta { get; set; }

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