using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TuristickaAgencija.Migrations
{
    public partial class Osnovna_migracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drzava",
                columns: table => new
                {
                    DrzavaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzava", x => x.DrzavaId);
                });

            migrationBuilder.CreateTable(
                name: "KorisnickiNalozi",
                columns: table => new
                {
                    KorisnickiNalogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    Lozinka = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnickiNalozi", x => x.KorisnickiNalogId);
                });

            migrationBuilder.CreateTable(
                name: "Obavijesti",
                columns: table => new
                {
                    ObavijestiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Sadrzaj = table.Column<string>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    Slika = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavijesti", x => x.ObavijestiId);
                });

            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivGrada = table.Column<string>(nullable: true),
                    DrzavaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gradovi_Drzava_DrzavaId",
                        column: x => x.DrzavaId,
                        principalTable: "Drzava",
                        principalColumn: "DrzavaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AutorizacijskiToken",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vrijednost = table.Column<string>(nullable: true),
                    KorisnickiNaloziId = table.Column<int>(nullable: false),
                    VrijemeEvidentiranja = table.Column<DateTime>(nullable: false),
                    IpAdresa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorizacijskiToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutorizacijskiToken_KorisnickiNalozi_KorisnickiNaloziId",
                        column: x => x.KorisnickiNaloziId,
                        principalTable: "KorisnickiNalozi",
                        principalColumn: "KorisnickiNalogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Firma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    GradID = table.Column<int>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    BrojZiroracuna = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Firma_Gradovi_GradID",
                        column: x => x.GradID,
                        principalTable: "Gradovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Klijent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    DatumRodjenja = table.Column<DateTime>(nullable: false),
                    Kontakt = table.Column<string>(nullable: true),
                    JMBG = table.Column<string>(nullable: true),
                    GradID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klijent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Klijent_Gradovi_GradID",
                        column: x => x.GradID,
                        principalTable: "Gradovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Smjestaj",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivSmjestaja = table.Column<string>(nullable: true),
                    OpisSmjestaja = table.Column<string>(nullable: true),
                    GradID = table.Column<int>(nullable: true),
                    CijenaNocenja = table.Column<float>(nullable: false),
                    Tip_sobe = table.Column<string>(nullable: true),
                    Slika = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smjestaj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Smjestaj_Gradovi_GradID",
                        column: x => x.GradID,
                        principalTable: "Gradovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Prevoz",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmaID = table.Column<int>(nullable: true),
                    TipPrevoza = table.Column<string>(nullable: true),
                    BrojMjesta = table.Column<int>(nullable: false),
                    CijenaPoMjestu = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prevoz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prevoz_Firma_FirmaID",
                        column: x => x.FirmaID,
                        principalTable: "Firma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PutnikKorisnik",
                columns: table => new
                {
                    PutnikKorisnikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    KorisnickiNalogId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PutnikKorisnik", x => x.PutnikKorisnikId);
                    table.ForeignKey(
                        name: "FK_PutnikKorisnik_KorisnickiNalozi_KorisnickiNalogId",
                        column: x => x.KorisnickiNalogId,
                        principalTable: "KorisnickiNalozi",
                        principalColumn: "KorisnickiNalogId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PutnikKorisnik_Klijent_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Klijent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zaposlenici",
                columns: table => new
                {
                    ZaposlenikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrucnaSprema = table.Column<string>(nullable: true),
                    KorisnikID = table.Column<int>(nullable: false),
                    KorisnickiNalogId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposlenici", x => x.ZaposlenikId);
                    table.ForeignKey(
                        name: "FK_Zaposlenici_KorisnickiNalozi_KorisnickiNalogId",
                        column: x => x.KorisnickiNalogId,
                        principalTable: "KorisnickiNalozi",
                        principalColumn: "KorisnickiNalogId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zaposlenici_Klijent_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Klijent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Putovanja",
                columns: table => new
                {
                    PutovanjaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivPutovanja = table.Column<string>(nullable: true),
                    OpisPutovanja = table.Column<string>(nullable: true),
                    CijenaPutovanja = table.Column<float>(nullable: false),
                    DatumPolaska = table.Column<DateTime>(nullable: false),
                    DatumDolaska = table.Column<DateTime>(nullable: false),
                    BrojPutnika = table.Column<int>(nullable: false),
                    PopisPutnika = table.Column<string>(nullable: true),
                    PrevozId = table.Column<int>(nullable: true),
                    SmjestajId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Putovanja", x => x.PutovanjaId);
                    table.ForeignKey(
                        name: "FK_Putovanja_Prevoz_PrevozId",
                        column: x => x.PrevozId,
                        principalTable: "Prevoz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Putovanja_Smjestaj_SmjestajId",
                        column: x => x.SmjestajId,
                        principalTable: "Smjestaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Uplate",
                columns: table => new
                {
                    UplataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iznos = table.Column<float>(nullable: false),
                    DatumUplate = table.Column<DateTime>(nullable: false),
                    KlijentId = table.Column<int>(nullable: true),
                    PutovanjaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplate", x => x.UplataId);
                    table.ForeignKey(
                        name: "FK_Uplate_Klijent_KlijentId",
                        column: x => x.KlijentId,
                        principalTable: "Klijent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Uplate_Putovanja_PutovanjaID",
                        column: x => x.PutovanjaID,
                        principalTable: "Putovanja",
                        principalColumn: "PutovanjaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vodic",
                columns: table => new
                {
                    VodicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Kontakt = table.Column<string>(nullable: true),
                    JMBG = table.Column<string>(nullable: true),
                    PutovanjaID = table.Column<int>(nullable: true),
                    Slika = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vodic", x => x.VodicId);
                    table.ForeignKey(
                        name: "FK_Vodic_Putovanja_PutovanjaID",
                        column: x => x.PutovanjaID,
                        principalTable: "Putovanja",
                        principalColumn: "PutovanjaId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutorizacijskiToken_KorisnickiNaloziId",
                table: "AutorizacijskiToken",
                column: "KorisnickiNaloziId");

            migrationBuilder.CreateIndex(
                name: "IX_Firma_GradID",
                table: "Firma",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_Gradovi_DrzavaId",
                table: "Gradovi",
                column: "DrzavaId");

            migrationBuilder.CreateIndex(
                name: "IX_Klijent_GradID",
                table: "Klijent",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_Prevoz_FirmaID",
                table: "Prevoz",
                column: "FirmaID");

            migrationBuilder.CreateIndex(
                name: "IX_PutnikKorisnik_KorisnickiNalogId",
                table: "PutnikKorisnik",
                column: "KorisnickiNalogId");

            migrationBuilder.CreateIndex(
                name: "IX_PutnikKorisnik_KorisnikId",
                table: "PutnikKorisnik",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Putovanja_PrevozId",
                table: "Putovanja",
                column: "PrevozId");

            migrationBuilder.CreateIndex(
                name: "IX_Putovanja_SmjestajId",
                table: "Putovanja",
                column: "SmjestajId");

            migrationBuilder.CreateIndex(
                name: "IX_Smjestaj_GradID",
                table: "Smjestaj",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_Uplate_KlijentId",
                table: "Uplate",
                column: "KlijentId");

            migrationBuilder.CreateIndex(
                name: "IX_Uplate_PutovanjaID",
                table: "Uplate",
                column: "PutovanjaID");

            migrationBuilder.CreateIndex(
                name: "IX_Vodic_PutovanjaID",
                table: "Vodic",
                column: "PutovanjaID");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenici_KorisnickiNalogId",
                table: "Zaposlenici",
                column: "KorisnickiNalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenici_KorisnikID",
                table: "Zaposlenici",
                column: "KorisnikID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutorizacijskiToken");

            migrationBuilder.DropTable(
                name: "Obavijesti");

            migrationBuilder.DropTable(
                name: "PutnikKorisnik");

            migrationBuilder.DropTable(
                name: "Uplate");

            migrationBuilder.DropTable(
                name: "Vodic");

            migrationBuilder.DropTable(
                name: "Zaposlenici");

            migrationBuilder.DropTable(
                name: "Putovanja");

            migrationBuilder.DropTable(
                name: "KorisnickiNalozi");

            migrationBuilder.DropTable(
                name: "Klijent");

            migrationBuilder.DropTable(
                name: "Prevoz");

            migrationBuilder.DropTable(
                name: "Smjestaj");

            migrationBuilder.DropTable(
                name: "Firma");

            migrationBuilder.DropTable(
                name: "Gradovi");

            migrationBuilder.DropTable(
                name: "Drzava");
        }
    }
}
