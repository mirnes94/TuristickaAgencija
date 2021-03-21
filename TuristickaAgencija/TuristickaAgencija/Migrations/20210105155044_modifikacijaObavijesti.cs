using Microsoft.EntityFrameworkCore.Migrations;

namespace TuristickaAgencija.Migrations
{
    public partial class modifikacijaObavijesti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KlijentId",
                table: "Obavijesti",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Obavijesti_KlijentId",
                table: "Obavijesti",
                column: "KlijentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Obavijesti_Klijent_KlijentId",
                table: "Obavijesti",
                column: "KlijentId",
                principalTable: "Klijent",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Obavijesti_Klijent_KlijentId",
                table: "Obavijesti");

            migrationBuilder.DropIndex(
                name: "IX_Obavijesti_KlijentId",
                table: "Obavijesti");

            migrationBuilder.DropColumn(
                name: "KlijentId",
                table: "Obavijesti");
        }
    }
}
