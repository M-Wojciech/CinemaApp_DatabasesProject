using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigaKino.Migrations
{
    /// <inheritdoc />
    public partial class _2024_22_06_16_34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bilety_Miejsca_IdMiejsce",
                table: "Bilety");

            migrationBuilder.DropForeignKey(
                name: "FK_Bilety_Seans_IdSeans",
                table: "Bilety");

            migrationBuilder.DropForeignKey(
                name: "FK_Bilety_Transakcje_IdTransakcja",
                table: "Bilety");

            migrationBuilder.DropForeignKey(
                name: "FK_Klienci_Konta_IdKonto",
                table: "Klienci");

            migrationBuilder.DropForeignKey(
                name: "FK_Miejsca_Sala_IdSala",
                table: "Miejsca");

            migrationBuilder.DropForeignKey(
                name: "FK_Pracownicy_Konta_IdKonto",
                table: "Pracownicy");

            migrationBuilder.DropForeignKey(
                name: "FK_Sala_Kina_IdKino",
                table: "Sala");

            migrationBuilder.DropForeignKey(
                name: "FK_Transakcje_Klienci_IdKlient",
                table: "Transakcje");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transakcje",
                table: "Transakcje");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pracownicy",
                table: "Pracownicy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Miejsca",
                table: "Miejsca");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Konta",
                table: "Konta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Klienci",
                table: "Klienci");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kina",
                table: "Kina");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bilety",
                table: "Bilety");

            migrationBuilder.RenameTable(
                name: "Transakcje",
                newName: "Transakcja");

            migrationBuilder.RenameTable(
                name: "Pracownicy",
                newName: "Pracownik");

            migrationBuilder.RenameTable(
                name: "Miejsca",
                newName: "Miejsce");

            migrationBuilder.RenameTable(
                name: "Konta",
                newName: "Konto");

            migrationBuilder.RenameTable(
                name: "Klienci",
                newName: "Klient");

            migrationBuilder.RenameTable(
                name: "Kina",
                newName: "Kino");

            migrationBuilder.RenameTable(
                name: "Bilety",
                newName: "Bilet");

            migrationBuilder.RenameIndex(
                name: "IX_Transakcje_IdKlient",
                table: "Transakcja",
                newName: "IX_Transakcja_IdKlient");

            migrationBuilder.RenameIndex(
                name: "IX_Pracownicy_IdKonto",
                table: "Pracownik",
                newName: "IX_Pracownik_IdKonto");

            migrationBuilder.RenameIndex(
                name: "IX_Miejsca_IdSala",
                table: "Miejsce",
                newName: "IX_Miejsce_IdSala");

            migrationBuilder.RenameIndex(
                name: "IX_Klienci_IdKonto",
                table: "Klient",
                newName: "IX_Klient_IdKonto");

            migrationBuilder.RenameIndex(
                name: "IX_Bilety_IdTransakcja",
                table: "Bilet",
                newName: "IX_Bilet_IdTransakcja");

            migrationBuilder.RenameIndex(
                name: "IX_Bilety_IdSeans",
                table: "Bilet",
                newName: "IX_Bilet_IdSeans");

            migrationBuilder.RenameIndex(
                name: "IX_Bilety_IdMiejsce",
                table: "Bilet",
                newName: "IX_Bilet_IdMiejsce");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transakcja",
                table: "Transakcja",
                column: "IdTransakcja");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pracownik",
                table: "Pracownik",
                column: "IdPracownik");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Miejsce",
                table: "Miejsce",
                column: "IdMiejsce");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Konto",
                table: "Konto",
                column: "IdKonto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Klient",
                table: "Klient",
                column: "IdKlient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kino",
                table: "Kino",
                column: "IdKino");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bilet",
                table: "Bilet",
                column: "IdBilet");

            migrationBuilder.AddForeignKey(
                name: "FK_Bilet_Miejsce_IdMiejsce",
                table: "Bilet",
                column: "IdMiejsce",
                principalTable: "Miejsce",
                principalColumn: "IdMiejsce",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bilet_Seans_IdSeans",
                table: "Bilet",
                column: "IdSeans",
                principalTable: "Seans",
                principalColumn: "idSeans",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bilet_Transakcja_IdTransakcja",
                table: "Bilet",
                column: "IdTransakcja",
                principalTable: "Transakcja",
                principalColumn: "IdTransakcja",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Klient_Konto_IdKonto",
                table: "Klient",
                column: "IdKonto",
                principalTable: "Konto",
                principalColumn: "IdKonto");

            migrationBuilder.AddForeignKey(
                name: "FK_Miejsce_Sala_IdSala",
                table: "Miejsce",
                column: "IdSala",
                principalTable: "Sala",
                principalColumn: "IdSala",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pracownik_Konto_IdKonto",
                table: "Pracownik",
                column: "IdKonto",
                principalTable: "Konto",
                principalColumn: "IdKonto");

            migrationBuilder.AddForeignKey(
                name: "FK_Sala_Kino_IdKino",
                table: "Sala",
                column: "IdKino",
                principalTable: "Kino",
                principalColumn: "IdKino",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcja_Klient_IdKlient",
                table: "Transakcja",
                column: "IdKlient",
                principalTable: "Klient",
                principalColumn: "IdKlient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bilet_Miejsce_IdMiejsce",
                table: "Bilet");

            migrationBuilder.DropForeignKey(
                name: "FK_Bilet_Seans_IdSeans",
                table: "Bilet");

            migrationBuilder.DropForeignKey(
                name: "FK_Bilet_Transakcja_IdTransakcja",
                table: "Bilet");

            migrationBuilder.DropForeignKey(
                name: "FK_Klient_Konto_IdKonto",
                table: "Klient");

            migrationBuilder.DropForeignKey(
                name: "FK_Miejsce_Sala_IdSala",
                table: "Miejsce");

            migrationBuilder.DropForeignKey(
                name: "FK_Pracownik_Konto_IdKonto",
                table: "Pracownik");

            migrationBuilder.DropForeignKey(
                name: "FK_Sala_Kino_IdKino",
                table: "Sala");

            migrationBuilder.DropForeignKey(
                name: "FK_Transakcja_Klient_IdKlient",
                table: "Transakcja");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transakcja",
                table: "Transakcja");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pracownik",
                table: "Pracownik");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Miejsce",
                table: "Miejsce");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Konto",
                table: "Konto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Klient",
                table: "Klient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kino",
                table: "Kino");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bilet",
                table: "Bilet");

            migrationBuilder.RenameTable(
                name: "Transakcja",
                newName: "Transakcje");

            migrationBuilder.RenameTable(
                name: "Pracownik",
                newName: "Pracownicy");

            migrationBuilder.RenameTable(
                name: "Miejsce",
                newName: "Miejsca");

            migrationBuilder.RenameTable(
                name: "Konto",
                newName: "Konta");

            migrationBuilder.RenameTable(
                name: "Klient",
                newName: "Klienci");

            migrationBuilder.RenameTable(
                name: "Kino",
                newName: "Kina");

            migrationBuilder.RenameTable(
                name: "Bilet",
                newName: "Bilety");

            migrationBuilder.RenameIndex(
                name: "IX_Transakcja_IdKlient",
                table: "Transakcje",
                newName: "IX_Transakcje_IdKlient");

            migrationBuilder.RenameIndex(
                name: "IX_Pracownik_IdKonto",
                table: "Pracownicy",
                newName: "IX_Pracownicy_IdKonto");

            migrationBuilder.RenameIndex(
                name: "IX_Miejsce_IdSala",
                table: "Miejsca",
                newName: "IX_Miejsca_IdSala");

            migrationBuilder.RenameIndex(
                name: "IX_Klient_IdKonto",
                table: "Klienci",
                newName: "IX_Klienci_IdKonto");

            migrationBuilder.RenameIndex(
                name: "IX_Bilet_IdTransakcja",
                table: "Bilety",
                newName: "IX_Bilety_IdTransakcja");

            migrationBuilder.RenameIndex(
                name: "IX_Bilet_IdSeans",
                table: "Bilety",
                newName: "IX_Bilety_IdSeans");

            migrationBuilder.RenameIndex(
                name: "IX_Bilet_IdMiejsce",
                table: "Bilety",
                newName: "IX_Bilety_IdMiejsce");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transakcje",
                table: "Transakcje",
                column: "IdTransakcja");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pracownicy",
                table: "Pracownicy",
                column: "IdPracownik");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Miejsca",
                table: "Miejsca",
                column: "IdMiejsce");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Konta",
                table: "Konta",
                column: "IdKonto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Klienci",
                table: "Klienci",
                column: "IdKlient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kina",
                table: "Kina",
                column: "IdKino");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bilety",
                table: "Bilety",
                column: "IdBilet");

            migrationBuilder.AddForeignKey(
                name: "FK_Bilety_Miejsca_IdMiejsce",
                table: "Bilety",
                column: "IdMiejsce",
                principalTable: "Miejsca",
                principalColumn: "IdMiejsce",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bilety_Seans_IdSeans",
                table: "Bilety",
                column: "IdSeans",
                principalTable: "Seans",
                principalColumn: "idSeans",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bilety_Transakcje_IdTransakcja",
                table: "Bilety",
                column: "IdTransakcja",
                principalTable: "Transakcje",
                principalColumn: "IdTransakcja",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Klienci_Konta_IdKonto",
                table: "Klienci",
                column: "IdKonto",
                principalTable: "Konta",
                principalColumn: "IdKonto");

            migrationBuilder.AddForeignKey(
                name: "FK_Miejsca_Sala_IdSala",
                table: "Miejsca",
                column: "IdSala",
                principalTable: "Sala",
                principalColumn: "IdSala",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pracownicy_Konta_IdKonto",
                table: "Pracownicy",
                column: "IdKonto",
                principalTable: "Konta",
                principalColumn: "IdKonto");

            migrationBuilder.AddForeignKey(
                name: "FK_Sala_Kina_IdKino",
                table: "Sala",
                column: "IdKino",
                principalTable: "Kina",
                principalColumn: "IdKino",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcje_Klienci_IdKlient",
                table: "Transakcje",
                column: "IdKlient",
                principalTable: "Klienci",
                principalColumn: "IdKlient");
        }
    }
}
