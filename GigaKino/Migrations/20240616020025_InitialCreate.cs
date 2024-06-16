using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigaKino.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    IdFilm = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tytul = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dlugosc = table.Column<int>(type: "int", nullable: false),
                    Gatunek = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rezyser = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ogr_Wiekowe = table.Column<int>(type: "int", nullable: false),
                    Trailer = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Opis = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BannerPath = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PosterPath = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.IdFilm);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Kina",
                columns: table => new
                {
                    IdKino = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Miasto = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ulica = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumerBudynku = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kina", x => x.IdKino);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Konta",
                columns: table => new
                {
                    IdKonto = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Typ = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Login = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Haslo = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sol = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konta", x => x.IdKonto);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    IdSala = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Numer = table.Column<int>(type: "int", nullable: false),
                    IdKino = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.IdSala);
                    table.ForeignKey(
                        name: "FK_Sale_Kina_IdKino",
                        column: x => x.IdKino,
                        principalTable: "Kina",
                        principalColumn: "IdKino",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Klienci",
                columns: table => new
                {
                    IdKlient = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Mail = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Imie = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nazwisko = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdKonto = table.Column<uint>(type: "int unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klienci", x => x.IdKlient);
                    table.ForeignKey(
                        name: "FK_Klienci_Konta_IdKonto",
                        column: x => x.IdKonto,
                        principalTable: "Konta",
                        principalColumn: "IdKonto");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pracownicy",
                columns: table => new
                {
                    IdPracownik = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Imie = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nazwisko = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataUrodzenia = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Stanowisko = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdKonto = table.Column<uint>(type: "int unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownicy", x => x.IdPracownik);
                    table.ForeignKey(
                        name: "FK_Pracownicy_Konta_IdKonto",
                        column: x => x.IdKonto,
                        principalTable: "Konta",
                        principalColumn: "IdKonto");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Miejsca",
                columns: table => new
                {
                    IdMiejsce = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Rzad = table.Column<int>(type: "int", nullable: false),
                    Kolumna = table.Column<int>(type: "int", nullable: false),
                    IdSala = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miejsca", x => x.IdMiejsce);
                    table.ForeignKey(
                        name: "FK_Miejsca_Sale_IdSala",
                        column: x => x.IdSala,
                        principalTable: "Sale",
                        principalColumn: "IdSala",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Seanse",
                columns: table => new
                {
                    IdSeans = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Termin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Technologia = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WersjaJezykowa = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CenaDomyslna = table.Column<int>(type: "int", nullable: false),
                    IdFilm = table.Column<uint>(type: "int unsigned", nullable: false),
                    IdSala = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seanse", x => x.IdSeans);
                    table.ForeignKey(
                        name: "FK_Seanse_Film_IdFilm",
                        column: x => x.IdFilm,
                        principalTable: "Film",
                        principalColumn: "IdFilm",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seanse_Sale_IdSala",
                        column: x => x.IdSala,
                        principalTable: "Sale",
                        principalColumn: "IdSala",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transakcje",
                columns: table => new
                {
                    IdTransakcja = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CenaLaczna = table.Column<int>(type: "int", nullable: true),
                    CzasRozpoczecia = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CzasZakupu = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IdKlient = table.Column<uint>(type: "int unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transakcje", x => x.IdTransakcja);
                    table.ForeignKey(
                        name: "FK_Transakcje_Klienci_IdKlient",
                        column: x => x.IdKlient,
                        principalTable: "Klienci",
                        principalColumn: "IdKlient");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bilety",
                columns: table => new
                {
                    IdBilet = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CenaFaktyczna = table.Column<int>(type: "int", nullable: false),
                    Ulga = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdSeans = table.Column<uint>(type: "int unsigned", nullable: false),
                    IdMiejsce = table.Column<uint>(type: "int unsigned", nullable: false),
                    IdTransakcja = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bilety", x => x.IdBilet);
                    table.ForeignKey(
                        name: "FK_Bilety_Miejsca_IdMiejsce",
                        column: x => x.IdMiejsce,
                        principalTable: "Miejsca",
                        principalColumn: "IdMiejsce",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bilety_Seanse_IdSeans",
                        column: x => x.IdSeans,
                        principalTable: "Seanse",
                        principalColumn: "IdSeans",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bilety_Transakcje_IdTransakcja",
                        column: x => x.IdTransakcja,
                        principalTable: "Transakcje",
                        principalColumn: "IdTransakcja",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Bilety_IdMiejsce",
                table: "Bilety",
                column: "IdMiejsce");

            migrationBuilder.CreateIndex(
                name: "IX_Bilety_IdSeans",
                table: "Bilety",
                column: "IdSeans");

            migrationBuilder.CreateIndex(
                name: "IX_Bilety_IdTransakcja",
                table: "Bilety",
                column: "IdTransakcja");

            migrationBuilder.CreateIndex(
                name: "IX_Klienci_IdKonto",
                table: "Klienci",
                column: "IdKonto");

            migrationBuilder.CreateIndex(
                name: "IX_Miejsca_IdSala",
                table: "Miejsca",
                column: "IdSala");

            migrationBuilder.CreateIndex(
                name: "IX_Pracownicy_IdKonto",
                table: "Pracownicy",
                column: "IdKonto");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_IdKino",
                table: "Sale",
                column: "IdKino");

            migrationBuilder.CreateIndex(
                name: "IX_Seanse_IdFilm",
                table: "Seanse",
                column: "IdFilm");

            migrationBuilder.CreateIndex(
                name: "IX_Seanse_IdSala",
                table: "Seanse",
                column: "IdSala");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcje_IdKlient",
                table: "Transakcje",
                column: "IdKlient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bilety");

            migrationBuilder.DropTable(
                name: "Pracownicy");

            migrationBuilder.DropTable(
                name: "Miejsca");

            migrationBuilder.DropTable(
                name: "Seanse");

            migrationBuilder.DropTable(
                name: "Transakcje");

            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Klienci");

            migrationBuilder.DropTable(
                name: "Kina");

            migrationBuilder.DropTable(
                name: "Konta");
        }
    }
}
