using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TuristickaAgencija.Migrations
{
    public partial class rezervacija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rezervacija",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    KlijentID = table.Column<int>(nullable: true),
                    PutovanjeID = table.Column<int>(nullable: true),
                    DatumRezervacije = table.Column<DateTime>(nullable: false),
                    BrojOsoba = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Napomena = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Klijent_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "Klijent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Putovanja_PutovanjeID",
                        column: x => x.PutovanjeID,
                        principalTable: "Putovanja",
                        principalColumn: "PutovanjaId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_KlijentID",
                table: "Rezervacija",
                column: "KlijentID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_PutovanjeID",
                table: "Rezervacija",
                column: "PutovanjeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rezervacija");
        }
    }
}
