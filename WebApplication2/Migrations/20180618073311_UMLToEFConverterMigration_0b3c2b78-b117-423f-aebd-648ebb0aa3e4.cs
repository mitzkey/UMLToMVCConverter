using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication2.Migrations
{
    public partial class UMLToEFConverterMigration_0b3c2b78b117423faebd648ebb0aa3e4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DyscyplinaSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyscyplinaSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DzienTygodniaSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DzienTygodniaSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GrafikSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Rok = table.Column<int>(nullable: false),
                    Semestr = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrafikSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InstruktorSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstruktorSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PoziomZaawansowaniaSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoziomZaawansowaniaSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SalaSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Adres = table.Column<string>(nullable: false),
                    Nazwa = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StatusWnioskuSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusWnioskuSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WojewodztwoSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktualna = table.Column<bool>(nullable: false),
                    Nazwa = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WojewodztwoSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WyposażenieSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Koszt = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WyposażenieSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KursSet",
                columns: table => new
                {
                    Kod = table.Column<string>(nullable: false),
                    GrafikID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursSet", x => x.Kod);
                    table.ForeignKey(
                        name: "FK_KursSet_GrafikSet_GrafikID",
                        column: x => x.GrafikID,
                        principalTable: "GrafikSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TerminSet",
                columns: table => new
                {
                    Dzien = table.Column<DateTime>(nullable: false),
                    GodzinaRozpoczecia = table.Column<DateTime>(nullable: false),
                    GrafikID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminSet", x => new { x.Dzien, x.GodzinaRozpoczecia });
                    table.ForeignKey(
                        name: "FK_TerminSet_GrafikSet_GrafikID",
                        column: x => x.GrafikID,
                        principalTable: "GrafikSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DyscyplinaZPoziomemSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DyscyplinaID = table.Column<int>(nullable: false),
                    PoziomZaawansowaniaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyscyplinaZPoziomemSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DyscyplinaZPoziomemSet_DyscyplinaSet_DyscyplinaID",
                        column: x => x.DyscyplinaID,
                        principalTable: "DyscyplinaSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DyscyplinaZPoziomemSet_PoziomZaawansowaniaSet_PoziomZaawansowaniaID",
                        column: x => x.PoziomZaawansowaniaID,
                        principalTable: "PoziomZaawansowaniaSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrzystosowanieSaliSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DyscyplinaID = table.Column<int>(nullable: false),
                    Pojemnosc = table.Column<int>(nullable: false),
                    PoziomID = table.Column<int>(nullable: false),
                    SalaID = table.Column<int>(nullable: false),
                    StawkaZaZajecia = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrzystosowanieSaliSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PrzystosowanieSaliSet_DyscyplinaSet_DyscyplinaID",
                        column: x => x.DyscyplinaID,
                        principalTable: "DyscyplinaSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrzystosowanieSaliSet_PoziomZaawansowaniaSet_PoziomID",
                        column: x => x.PoziomID,
                        principalTable: "PoziomZaawansowaniaSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrzystosowanieSaliSet_SalaSet_SalaID",
                        column: x => x.SalaID,
                        principalTable: "SalaSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MiejscowoscSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktualna = table.Column<bool>(nullable: false),
                    Nazwa = table.Column<string>(nullable: false),
                    WojewodztwoMiejscowosciID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiejscowoscSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MiejscowoscSet_WojewodztwoSet_WojewodztwoMiejscowosciID",
                        column: x => x.WojewodztwoMiejscowosciID,
                        principalTable: "WojewodztwoSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WymaganeWyposazenieDyscyplinaSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DyscyplinaID = table.Column<int>(nullable: true),
                    WymaganeWyposazenieID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WymaganeWyposazenieDyscyplinaSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WymaganeWyposazenieDyscyplinaSet_DyscyplinaSet_DyscyplinaID",
                        column: x => x.DyscyplinaID,
                        principalTable: "DyscyplinaSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WymaganeWyposazenieDyscyplinaSet_WyposażenieSet_WymaganeWyposazenieID",
                        column: x => x.WymaganeWyposazenieID,
                        principalTable: "WyposażenieSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZajeciaSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KursKod = table.Column<string>(nullable: false),
                    SalaID = table.Column<int>(nullable: false),
                    TerminDzien = table.Column<DateTime>(nullable: false),
                    TerminGodzinaRozpoczecia = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZajeciaSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ZajeciaSet_KursSet_KursKod",
                        column: x => x.KursKod,
                        principalTable: "KursSet",
                        principalColumn: "Kod",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZajeciaSet_SalaSet_SalaID",
                        column: x => x.SalaID,
                        principalTable: "SalaSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZajeciaSet_TerminSet_TerminDzien_TerminGodzinaRozpoczecia",
                        columns: x => new { x.TerminDzien, x.TerminGodzinaRozpoczecia },
                        principalTable: "TerminSet",
                        principalColumns: new[] { "Dzien", "GodzinaRozpoczecia" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CertyfikowaneKwalifikacjeInstruktorSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CertyfikowaneKwalifikacjeID = table.Column<int>(nullable: true),
                    InstruktorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertyfikowaneKwalifikacjeInstruktorSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CertyfikowaneKwalifikacjeInstruktorSet_DyscyplinaZPoziomemSet_CertyfikowaneKwalifikacjeID",
                        column: x => x.CertyfikowaneKwalifikacjeID,
                        principalTable: "DyscyplinaZPoziomemSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertyfikowaneKwalifikacjeInstruktorSet_InstruktorSet_InstruktorID",
                        column: x => x.InstruktorID,
                        principalTable: "InstruktorSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SzczegolyKwalifikacjiSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Certyfikat = table.Column<bool>(nullable: false),
                    DyscyplinaZPoziomemID = table.Column<int>(nullable: false),
                    InstruktorID = table.Column<int>(nullable: false),
                    Priorytet = table.Column<int>(nullable: false),
                    StawkaZaZajecia = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SzczegolyKwalifikacjiSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SzczegolyKwalifikacjiSet_DyscyplinaZPoziomemSet_DyscyplinaZPoziomemID",
                        column: x => x.DyscyplinaZPoziomemID,
                        principalTable: "DyscyplinaZPoziomemSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SzczegolyKwalifikacjiSet_InstruktorSet_InstruktorID",
                        column: x => x.InstruktorID,
                        principalTable: "InstruktorSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BrakujaceWyposazeniePrzystosowanieSaliSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrakujaceWyposazenieID = table.Column<int>(nullable: true),
                    PrzystosowanieSaliID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrakujaceWyposazeniePrzystosowanieSaliSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BrakujaceWyposazeniePrzystosowanieSaliSet_WyposażenieSet_BrakujaceWyposazenieID",
                        column: x => x.BrakujaceWyposazenieID,
                        principalTable: "WyposażenieSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BrakujaceWyposazeniePrzystosowanieSaliSet_PrzystosowanieSaliSet_PrzystosowanieSaliID",
                        column: x => x.PrzystosowanieSaliID,
                        principalTable: "PrzystosowanieSaliSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdresSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MiejscowoscID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdresSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AdresSet_MiejscowoscSet_MiejscowoscID",
                        column: x => x.MiejscowoscID,
                        principalTable: "MiejscowoscSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WniosekSet",
                columns: table => new
                {
                    Pesel = table.Column<string>(nullable: false),
                    AdresDoKorespondencjiID = table.Column<int>(nullable: false),
                    AdresZameldowaniaID = table.Column<int>(nullable: false),
                    DataRozpatrzenia = table.Column<DateTime>(nullable: false),
                    DataZlozenia = table.Column<DateTime>(nullable: false),
                    StatusID = table.Column<int>(nullable: false, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WniosekSet", x => x.Pesel);
                    table.ForeignKey(
                        name: "FK_WniosekSet_AdresSet_AdresDoKorespondencjiID",
                        column: x => x.AdresDoKorespondencjiID,
                        principalTable: "AdresSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WniosekSet_AdresSet_AdresZameldowaniaID",
                        column: x => x.AdresZameldowaniaID,
                        principalTable: "AdresSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WniosekSet_StatusWnioskuSet_StatusID",
                        column: x => x.StatusID,
                        principalTable: "StatusWnioskuSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OsobaSet",
                columns: table => new
                {
                    WniosekPrzyjetyNaPodstawiePesel = table.Column<string>(nullable: true),
                    Pesel = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsobaSet", x => x.Pesel);
                    table.ForeignKey(
                        name: "FK_OsobaSet_WniosekSet_WniosekPrzyjetyNaPodstawiePesel",
                        column: x => x.WniosekPrzyjetyNaPodstawiePesel,
                        principalTable: "WniosekSet",
                        principalColumn: "Pesel",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdresSet_MiejscowoscID",
                table: "AdresSet",
                column: "MiejscowoscID");

            migrationBuilder.CreateIndex(
                name: "IX_BrakujaceWyposazeniePrzystosowanieSaliSet_BrakujaceWyposazenieID",
                table: "BrakujaceWyposazeniePrzystosowanieSaliSet",
                column: "BrakujaceWyposazenieID");

            migrationBuilder.CreateIndex(
                name: "IX_BrakujaceWyposazeniePrzystosowanieSaliSet_PrzystosowanieSaliID",
                table: "BrakujaceWyposazeniePrzystosowanieSaliSet",
                column: "PrzystosowanieSaliID");

            migrationBuilder.CreateIndex(
                name: "IX_CertyfikowaneKwalifikacjeInstruktorSet_CertyfikowaneKwalifikacjeID",
                table: "CertyfikowaneKwalifikacjeInstruktorSet",
                column: "CertyfikowaneKwalifikacjeID");

            migrationBuilder.CreateIndex(
                name: "IX_CertyfikowaneKwalifikacjeInstruktorSet_InstruktorID",
                table: "CertyfikowaneKwalifikacjeInstruktorSet",
                column: "InstruktorID");

            migrationBuilder.CreateIndex(
                name: "IX_DyscyplinaZPoziomemSet_DyscyplinaID",
                table: "DyscyplinaZPoziomemSet",
                column: "DyscyplinaID");

            migrationBuilder.CreateIndex(
                name: "IX_DyscyplinaZPoziomemSet_PoziomZaawansowaniaID",
                table: "DyscyplinaZPoziomemSet",
                column: "PoziomZaawansowaniaID");

            migrationBuilder.CreateIndex(
                name: "IX_KursSet_GrafikID",
                table: "KursSet",
                column: "GrafikID");

            migrationBuilder.CreateIndex(
                name: "IX_MiejscowoscSet_WojewodztwoMiejscowosciID",
                table: "MiejscowoscSet",
                column: "WojewodztwoMiejscowosciID");

            migrationBuilder.CreateIndex(
                name: "IX_OsobaSet_WniosekPrzyjetyNaPodstawiePesel",
                table: "OsobaSet",
                column: "WniosekPrzyjetyNaPodstawiePesel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrzystosowanieSaliSet_DyscyplinaID",
                table: "PrzystosowanieSaliSet",
                column: "DyscyplinaID");

            migrationBuilder.CreateIndex(
                name: "IX_PrzystosowanieSaliSet_PoziomID",
                table: "PrzystosowanieSaliSet",
                column: "PoziomID");

            migrationBuilder.CreateIndex(
                name: "IX_PrzystosowanieSaliSet_SalaID",
                table: "PrzystosowanieSaliSet",
                column: "SalaID");

            migrationBuilder.CreateIndex(
                name: "IX_SzczegolyKwalifikacjiSet_DyscyplinaZPoziomemID",
                table: "SzczegolyKwalifikacjiSet",
                column: "DyscyplinaZPoziomemID");

            migrationBuilder.CreateIndex(
                name: "IX_SzczegolyKwalifikacjiSet_InstruktorID",
                table: "SzczegolyKwalifikacjiSet",
                column: "InstruktorID");

            migrationBuilder.CreateIndex(
                name: "IX_TerminSet_GrafikID",
                table: "TerminSet",
                column: "GrafikID");

            migrationBuilder.CreateIndex(
                name: "IX_WniosekSet_AdresDoKorespondencjiID",
                table: "WniosekSet",
                column: "AdresDoKorespondencjiID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WniosekSet_AdresZameldowaniaID",
                table: "WniosekSet",
                column: "AdresZameldowaniaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WniosekSet_StatusID",
                table: "WniosekSet",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_WymaganeWyposazenieDyscyplinaSet_DyscyplinaID",
                table: "WymaganeWyposazenieDyscyplinaSet",
                column: "DyscyplinaID");

            migrationBuilder.CreateIndex(
                name: "IX_WymaganeWyposazenieDyscyplinaSet_WymaganeWyposazenieID",
                table: "WymaganeWyposazenieDyscyplinaSet",
                column: "WymaganeWyposazenieID");

            migrationBuilder.CreateIndex(
                name: "IX_ZajeciaSet_KursKod",
                table: "ZajeciaSet",
                column: "KursKod");

            migrationBuilder.CreateIndex(
                name: "IX_ZajeciaSet_SalaID",
                table: "ZajeciaSet",
                column: "SalaID");

            migrationBuilder.CreateIndex(
                name: "IX_ZajeciaSet_TerminDzien_TerminGodzinaRozpoczecia",
                table: "ZajeciaSet",
                columns: new[] { "TerminDzien", "TerminGodzinaRozpoczecia" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrakujaceWyposazeniePrzystosowanieSaliSet");

            migrationBuilder.DropTable(
                name: "CertyfikowaneKwalifikacjeInstruktorSet");

            migrationBuilder.DropTable(
                name: "DzienTygodniaSet");

            migrationBuilder.DropTable(
                name: "OsobaSet");

            migrationBuilder.DropTable(
                name: "SzczegolyKwalifikacjiSet");

            migrationBuilder.DropTable(
                name: "WymaganeWyposazenieDyscyplinaSet");

            migrationBuilder.DropTable(
                name: "ZajeciaSet");

            migrationBuilder.DropTable(
                name: "PrzystosowanieSaliSet");

            migrationBuilder.DropTable(
                name: "WniosekSet");

            migrationBuilder.DropTable(
                name: "DyscyplinaZPoziomemSet");

            migrationBuilder.DropTable(
                name: "InstruktorSet");

            migrationBuilder.DropTable(
                name: "WyposażenieSet");

            migrationBuilder.DropTable(
                name: "KursSet");

            migrationBuilder.DropTable(
                name: "TerminSet");

            migrationBuilder.DropTable(
                name: "SalaSet");

            migrationBuilder.DropTable(
                name: "AdresSet");

            migrationBuilder.DropTable(
                name: "StatusWnioskuSet");

            migrationBuilder.DropTable(
                name: "DyscyplinaSet");

            migrationBuilder.DropTable(
                name: "PoziomZaawansowaniaSet");

            migrationBuilder.DropTable(
                name: "GrafikSet");

            migrationBuilder.DropTable(
                name: "MiejscowoscSet");

            migrationBuilder.DropTable(
                name: "WojewodztwoSet");
        }
    }
}
