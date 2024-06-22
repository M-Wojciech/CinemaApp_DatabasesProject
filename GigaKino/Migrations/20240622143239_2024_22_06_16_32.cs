using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigaKino.Migrations
{
    /// <inheritdoc />
    public partial class _2024_22_06_16_32 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Miejsca_Sale_IdSala",
                table: "Miejsca");

            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Kina_IdKino",
                table: "Sale");

            migrationBuilder.DropForeignKey(
                name: "FK_Seans_Sale_IdSala",
                table: "Seans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sale",
                table: "Sale");

            migrationBuilder.RenameTable(
                name: "Sale",
                newName: "Sala");

            migrationBuilder.RenameIndex(
                name: "IX_Sale_IdKino",
                table: "Sala",
                newName: "IX_Sala_IdKino");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sala",
                table: "Sala",
                column: "IdSala");

            migrationBuilder.AddForeignKey(
                name: "FK_Miejsca_Sala_IdSala",
                table: "Miejsca",
                column: "IdSala",
                principalTable: "Sala",
                principalColumn: "IdSala",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sala_Kina_IdKino",
                table: "Sala",
                column: "IdKino",
                principalTable: "Kina",
                principalColumn: "IdKino",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seans_Sala_IdSala",
                table: "Seans",
                column: "IdSala",
                principalTable: "Sala",
                principalColumn: "IdSala",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Miejsca_Sala_IdSala",
                table: "Miejsca");

            migrationBuilder.DropForeignKey(
                name: "FK_Sala_Kina_IdKino",
                table: "Sala");

            migrationBuilder.DropForeignKey(
                name: "FK_Seans_Sala_IdSala",
                table: "Seans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sala",
                table: "Sala");

            migrationBuilder.RenameTable(
                name: "Sala",
                newName: "Sale");

            migrationBuilder.RenameIndex(
                name: "IX_Sala_IdKino",
                table: "Sale",
                newName: "IX_Sale_IdKino");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sale",
                table: "Sale",
                column: "IdSala");

            migrationBuilder.AddForeignKey(
                name: "FK_Miejsca_Sale_IdSala",
                table: "Miejsca",
                column: "IdSala",
                principalTable: "Sale",
                principalColumn: "IdSala",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Kina_IdKino",
                table: "Sale",
                column: "IdKino",
                principalTable: "Kina",
                principalColumn: "IdKino",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seans_Sale_IdSala",
                table: "Seans",
                column: "IdSala",
                principalTable: "Sale",
                principalColumn: "IdSala",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
