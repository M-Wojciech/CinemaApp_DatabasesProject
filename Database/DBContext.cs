using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Database
{
    public class Kino
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }
        public string Miasto { get; set; }
        public string Ulica { get; set; }
        public int Numer_budynku { get; set; }
    }

    public class Film
    {
        public int ID { get; set; }
        public string Tytul { get; set; }
        public int Dlugosc { get; set; }
        public string Gatunek { get; set; }
        public string Rezyser { get; set; }
        public int Ogr_wiekowe { get; set; }
        public string Trailer {  get; set; }
        public string Opis { get; set; }
    }

    public class Sala 
    {
        public int ID { get; set; }
        public int Numer {  get; set; }
        public int KinoID { get; set; }
        public virtual Kino Kino { get; set; }
    }

    public class Miejsce
    {
        public int ID { get; set; }
        public int Rzad { get; set; }
        public int Kolumna { get; set; }
        public int SalaID { get; set; }
        public virtual Sala Sala { get; set; }  
    }

    public class Seans
    {
        public int ID { get; set; }
        public DateTime Termin { get; set; }
        public string Technologia { get; set; }
        public string Wersja_jezykowa { get; set; }
        public int Cena_domyslna { get; set; }
        public int FilmID { get; set; }
        public int SalaID { get; set;}

        public virtual Film Film { get; set; }
        public virtual Sala Sala { get; set; }

    }

    public class Bilet
    {
        public int ID { get; set; }
        public int Cena_faktyczna { get; set; } 
        public string Ulga { get; set; }
        public int SeansID { get; set; }
        public int MiejsceID { get; set; }
        public int TransakcjaID { get; set; }
        
        public virtual Seans Seans { get; set; }
        public virtual Miejsce Miejsce { get; set; }
        public virtual Transakcja Transakcja { get; set; }
    }

    public class Transakcja
    {
        public int ID { get; set; }
        public int Cena_laczna { get; set; }
        public DateTime Czas_zakupu { get; set; }
        public bool Status { get; set; }
    }

    public class Klient
    {
        public int ID { get; set; }
        public string Mail { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int KontoID { get; set; }
        public virtual Konto Konto { get; set; }
    }

    public class Pracownik
    { 
        public int ID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime Data_urodzenia { get; set; }
        public string Stanowisko { get; set; }
        public int KontoID {  get; set; }
        public virtual Konto Konto { get; set; }
    }

    public class Konto
    {
        public int ID { get; set; }
        public string Typ { get; set; }
        public string Login { get; set; }
        public string Haslo { get; set; }
        public string Sol { get; set; }
    }

    public class DBContext : DbContext
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
    }
}
