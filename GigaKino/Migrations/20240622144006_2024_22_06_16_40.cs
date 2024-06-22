using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigaKino.Migrations
{
    /// <inheritdoc />
    public partial class _2024_22_06_16_40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumerBudynku",
                table: "Kino",
                newName: "Numer_Budynku");

            migrationBuilder.RenameColumn(
                name: "CenaFaktyczna",
                table: "Bilet",
                newName: "Cen_Faktyczna");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Numer_Budynku",
                table: "Kino",
                newName: "NumerBudynku");

            migrationBuilder.RenameColumn(
                name: "Cen_Faktyczna",
                table: "Bilet",
                newName: "CenaFaktyczna");
        }
    }
}
