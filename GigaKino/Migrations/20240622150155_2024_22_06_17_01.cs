using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigaKino.Migrations
{
    /// <inheritdoc />
    public partial class _2024_22_06_17_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CzasZakupu",
                table: "Transakcja",
                newName: "Czas_Zakupu");

            migrationBuilder.RenameColumn(
                name: "CzasRozpoczecia",
                table: "Transakcja",
                newName: "Czas_Rozpoczecia");

            migrationBuilder.RenameColumn(
                name: "CenaLaczna",
                table: "Transakcja",
                newName: "Cena_Laczna");

            migrationBuilder.RenameColumn(
                name: "DataUrodzenia",
                table: "Pracownik",
                newName: "Data_Urodzenia");

            migrationBuilder.RenameColumn(
                name: "Cen_Faktyczna",
                table: "Bilet",
                newName: "Cena_Faktyczna");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Czas_Zakupu",
                table: "Transakcja",
                newName: "CzasZakupu");

            migrationBuilder.RenameColumn(
                name: "Czas_Rozpoczecia",
                table: "Transakcja",
                newName: "CzasRozpoczecia");

            migrationBuilder.RenameColumn(
                name: "Cena_Laczna",
                table: "Transakcja",
                newName: "CenaLaczna");

            migrationBuilder.RenameColumn(
                name: "Data_Urodzenia",
                table: "Pracownik",
                newName: "DataUrodzenia");

            migrationBuilder.RenameColumn(
                name: "Cena_Faktyczna",
                table: "Bilet",
                newName: "Cen_Faktyczna");
        }
    }
}
