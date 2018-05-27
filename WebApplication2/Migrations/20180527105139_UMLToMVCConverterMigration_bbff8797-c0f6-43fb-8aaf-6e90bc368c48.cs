using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication2.Migrations
{
    public partial class UMLToMVCConverterMigration_bbff8797c0f643fb8aaf6e90bc368c48 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dyscyplina",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dyscyplina", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Grafik",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktualny = table.Column<bool>(nullable: true),
                    Rok = table.Column<int>(nullable: true),
                    Semestr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grafik", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Instruktor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruktor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PoziomZaawansowania",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoziomZaawansowania", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Adres = table.Column<string>(nullable: true),
                    Nazwa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StatusWniosku",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusWniosku", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Wojewodztwo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktualna = table.Column<bool>(nullable: true),
                    Nazwa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wojewodztwo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Wyposażenie",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Koszt = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wyposażenie", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kurs",
                columns: table => new
                {
                    Kod = table.Column<string>(nullable: false),
                    GrafikID = table.Column<int>(nullable: false),
                    KosztTygodniowy = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurs", x => x.Kod);
                    table.ForeignKey(
                        name: "FK_Kurs_Grafik_GrafikID",
                        column: x => x.GrafikID,
                        principalTable: "Grafik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Termin",
                columns: table => new
                {
                    Dzien = table.Column<DateTime>(nullable: false),
                    GodzinaRozpoczecia = table.Column<DateTime>(nullable: false),
                    GrafikID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termin", x => new { x.Dzien, x.GodzinaRozpoczecia });
                    table.ForeignKey(
                        name: "FK_Termin_Grafik_GrafikID",
                        column: x => x.GrafikID,
                        principalTable: "Grafik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DzienTygodnia",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    InstruktorID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DzienTygodnia", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DzienTygodnia_Instruktor_InstruktorID",
                        column: x => x.InstruktorID,
                        principalTable: "Instruktor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DyscyplinaZPoziomem",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DyscyplinaID = table.Column<int>(nullable: false),
                    Nazwa = table.Column<string>(nullable: true),
                    PoziomZaawansowaniaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyscyplinaZPoziomem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DyscyplinaZPoziomem_Dyscyplina_DyscyplinaID",
                        column: x => x.DyscyplinaID,
                        principalTable: "Dyscyplina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DyscyplinaZPoziomem_PoziomZaawansowania_PoziomZaawansowaniaID",
                        column: x => x.PoziomZaawansowaniaID,
                        principalTable: "PoziomZaawansowania",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PoziomyDyscypliny",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DyscyplinyID = table.Column<int>(nullable: false),
                    PoziomyID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoziomyDyscypliny", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PoziomyDyscypliny_Dyscyplina_DyscyplinyID",
                        column: x => x.DyscyplinyID,
                        principalTable: "Dyscyplina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoziomyDyscypliny_PoziomZaawansowania_PoziomyID",
                        column: x => x.PoziomyID,
                        principalTable: "PoziomZaawansowania",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrzystosowaneSalePrzeznaczenie",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PrzeznaczenieID = table.Column<int>(nullable: true),
                    PrzystosowaneSaleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrzystosowaneSalePrzeznaczenie", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PrzystosowaneSalePrzeznaczenie_Dyscyplina_PrzeznaczenieID",
                        column: x => x.PrzeznaczenieID,
                        principalTable: "Dyscyplina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrzystosowaneSalePrzeznaczenie_Sala_PrzystosowaneSaleID",
                        column: x => x.PrzystosowaneSaleID,
                        principalTable: "Sala",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrzystosowanieSali",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DyscyplinaID = table.Column<int>(nullable: false),
                    Pojemnosc = table.Column<int>(nullable: true),
                    PoziomID = table.Column<int>(nullable: false),
                    SalaID = table.Column<int>(nullable: false),
                    StawkaZaZajecia = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrzystosowanieSali", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PrzystosowanieSali_Dyscyplina_DyscyplinaID",
                        column: x => x.DyscyplinaID,
                        principalTable: "Dyscyplina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrzystosowanieSali_PoziomZaawansowania_PoziomID",
                        column: x => x.PoziomID,
                        principalTable: "PoziomZaawansowania",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrzystosowanieSali_Sala_SalaID",
                        column: x => x.SalaID,
                        principalTable: "Sala",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Miejscowosc",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktualna = table.Column<bool>(nullable: true),
                    Nazwa = table.Column<string>(nullable: true),
                    WojewodztwoMiejscowosciID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miejscowosc", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Miejscowosc_Wojewodztwo_WojewodztwoMiejscowosciID",
                        column: x => x.WojewodztwoMiejscowosciID,
                        principalTable: "Wojewodztwo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WymaganeWyposazenieDyscyplina",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DyscyplinaID = table.Column<int>(nullable: true),
                    WymaganeWyposazenieID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WymaganeWyposazenieDyscyplina", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WymaganeWyposazenieDyscyplina_Dyscyplina_DyscyplinaID",
                        column: x => x.DyscyplinaID,
                        principalTable: "Dyscyplina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WymaganeWyposazenieDyscyplina_Wyposażenie_WymaganeWyposazenieID",
                        column: x => x.WymaganeWyposazenieID,
                        principalTable: "Wyposażenie",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zajecia",
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
                    table.PrimaryKey("PK_Zajecia", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Zajecia_Kurs_KursKod",
                        column: x => x.KursKod,
                        principalTable: "Kurs",
                        principalColumn: "Kod",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zajecia_Sala_SalaID",
                        column: x => x.SalaID,
                        principalTable: "Sala",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zajecia_Termin_TerminDzien_TerminGodzinaRozpoczecia",
                        columns: x => new { x.TerminDzien, x.TerminGodzinaRozpoczecia },
                        principalTable: "Termin",
                        principalColumns: new[] { "Dzien", "GodzinaRozpoczecia" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CertyfikowaneKwalifikacjeInstruktor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CertyfikowaneKwalifikacjeID = table.Column<int>(nullable: true),
                    InstruktorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertyfikowaneKwalifikacjeInstruktor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CertyfikowaneKwalifikacjeInstruktor_DyscyplinaZPoziomem_CertyfikowaneKwalifikacjeID",
                        column: x => x.CertyfikowaneKwalifikacjeID,
                        principalTable: "DyscyplinaZPoziomem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertyfikowaneKwalifikacjeInstruktor_Instruktor_InstruktorID",
                        column: x => x.InstruktorID,
                        principalTable: "Instruktor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KwalifikacjeUprawnieni",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KwalifikacjeID = table.Column<int>(nullable: true),
                    UprawnieniID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KwalifikacjeUprawnieni", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KwalifikacjeUprawnieni_DyscyplinaZPoziomem_KwalifikacjeID",
                        column: x => x.KwalifikacjeID,
                        principalTable: "DyscyplinaZPoziomem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KwalifikacjeUprawnieni_Instruktor_UprawnieniID",
                        column: x => x.UprawnieniID,
                        principalTable: "Instruktor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SzczegolyKwalifikacji",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Certyfikat = table.Column<bool>(nullable: true),
                    DyscyplinaZPoziomemID = table.Column<int>(nullable: false),
                    InstruktorID = table.Column<int>(nullable: false),
                    Priorytet = table.Column<int>(nullable: true),
                    StawkaZaZajecia = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SzczegolyKwalifikacji", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SzczegolyKwalifikacji_DyscyplinaZPoziomem_DyscyplinaZPoziomemID",
                        column: x => x.DyscyplinaZPoziomemID,
                        principalTable: "DyscyplinaZPoziomem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SzczegolyKwalifikacji_Instruktor_InstruktorID",
                        column: x => x.InstruktorID,
                        principalTable: "Instruktor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BrakujaceWyposazeniePrzystosowanieSali",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrakujaceWyposazenieID = table.Column<int>(nullable: true),
                    PrzystosowanieSaliID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrakujaceWyposazeniePrzystosowanieSali", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BrakujaceWyposazeniePrzystosowanieSali_Wyposażenie_BrakujaceWyposazenieID",
                        column: x => x.BrakujaceWyposazenieID,
                        principalTable: "Wyposażenie",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BrakujaceWyposazeniePrzystosowanieSali_PrzystosowanieSali_PrzystosowanieSaliID",
                        column: x => x.PrzystosowanieSaliID,
                        principalTable: "PrzystosowanieSali",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Adres",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MiejscowoscID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adres", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Adres_Miejscowosc_MiejscowoscID",
                        column: x => x.MiejscowoscID,
                        principalTable: "Miejscowosc",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wniosek",
                columns: table => new
                {
                    Pesel = table.Column<string>(nullable: false),
                    AdresDoKorespondencjiID = table.Column<int>(nullable: true),
                    AdresZameldowaniaID = table.Column<int>(nullable: true),
                    DataRozpatrzenia = table.Column<DateTime>(nullable: true),
                    DataZlozenia = table.Column<DateTime>(nullable: true),
                    StatusID = table.Column<int>(nullable: false, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wniosek", x => x.Pesel);
                    table.ForeignKey(
                        name: "FK_Wniosek_Adres_AdresDoKorespondencjiID",
                        column: x => x.AdresDoKorespondencjiID,
                        principalTable: "Adres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wniosek_Adres_AdresZameldowaniaID",
                        column: x => x.AdresZameldowaniaID,
                        principalTable: "Adres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wniosek_StatusWniosku_StatusID",
                        column: x => x.StatusID,
                        principalTable: "StatusWniosku",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Osoba",
                columns: table => new
                {
                    WniosekPrzyjetyNaPodstawiePesel = table.Column<string>(nullable: true),
                    Pesel = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osoba", x => x.Pesel);
                    table.ForeignKey(
                        name: "FK_Osoba_Wniosek_WniosekPrzyjetyNaPodstawiePesel",
                        column: x => x.WniosekPrzyjetyNaPodstawiePesel,
                        principalTable: "Wniosek",
                        principalColumn: "Pesel",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adres_MiejscowoscID",
                table: "Adres",
                column: "MiejscowoscID");

            migrationBuilder.CreateIndex(
                name: "IX_BrakujaceWyposazeniePrzystosowanieSali_BrakujaceWyposazenieID",
                table: "BrakujaceWyposazeniePrzystosowanieSali",
                column: "BrakujaceWyposazenieID");

            migrationBuilder.CreateIndex(
                name: "IX_BrakujaceWyposazeniePrzystosowanieSali_PrzystosowanieSaliID",
                table: "BrakujaceWyposazeniePrzystosowanieSali",
                column: "PrzystosowanieSaliID");

            migrationBuilder.CreateIndex(
                name: "IX_CertyfikowaneKwalifikacjeInstruktor_CertyfikowaneKwalifikacjeID",
                table: "CertyfikowaneKwalifikacjeInstruktor",
                column: "CertyfikowaneKwalifikacjeID");

            migrationBuilder.CreateIndex(
                name: "IX_CertyfikowaneKwalifikacjeInstruktor_InstruktorID",
                table: "CertyfikowaneKwalifikacjeInstruktor",
                column: "InstruktorID");

            migrationBuilder.CreateIndex(
                name: "IX_DyscyplinaZPoziomem_DyscyplinaID",
                table: "DyscyplinaZPoziomem",
                column: "DyscyplinaID");

            migrationBuilder.CreateIndex(
                name: "IX_DyscyplinaZPoziomem_PoziomZaawansowaniaID",
                table: "DyscyplinaZPoziomem",
                column: "PoziomZaawansowaniaID");

            migrationBuilder.CreateIndex(
                name: "IX_DzienTygodnia_InstruktorID",
                table: "DzienTygodnia",
                column: "InstruktorID");

            migrationBuilder.CreateIndex(
                name: "IX_Kurs_GrafikID",
                table: "Kurs",
                column: "GrafikID");

            migrationBuilder.CreateIndex(
                name: "IX_KwalifikacjeUprawnieni_KwalifikacjeID",
                table: "KwalifikacjeUprawnieni",
                column: "KwalifikacjeID");

            migrationBuilder.CreateIndex(
                name: "IX_KwalifikacjeUprawnieni_UprawnieniID",
                table: "KwalifikacjeUprawnieni",
                column: "UprawnieniID");

            migrationBuilder.CreateIndex(
                name: "IX_Miejscowosc_WojewodztwoMiejscowosciID",
                table: "Miejscowosc",
                column: "WojewodztwoMiejscowosciID");

            migrationBuilder.CreateIndex(
                name: "IX_Osoba_WniosekPrzyjetyNaPodstawiePesel",
                table: "Osoba",
                column: "WniosekPrzyjetyNaPodstawiePesel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PoziomyDyscypliny_DyscyplinyID",
                table: "PoziomyDyscypliny",
                column: "DyscyplinyID");

            migrationBuilder.CreateIndex(
                name: "IX_PoziomyDyscypliny_PoziomyID",
                table: "PoziomyDyscypliny",
                column: "PoziomyID");

            migrationBuilder.CreateIndex(
                name: "IX_PrzystosowaneSalePrzeznaczenie_PrzeznaczenieID",
                table: "PrzystosowaneSalePrzeznaczenie",
                column: "PrzeznaczenieID");

            migrationBuilder.CreateIndex(
                name: "IX_PrzystosowaneSalePrzeznaczenie_PrzystosowaneSaleID",
                table: "PrzystosowaneSalePrzeznaczenie",
                column: "PrzystosowaneSaleID");

            migrationBuilder.CreateIndex(
                name: "IX_PrzystosowanieSali_DyscyplinaID",
                table: "PrzystosowanieSali",
                column: "DyscyplinaID");

            migrationBuilder.CreateIndex(
                name: "IX_PrzystosowanieSali_PoziomID",
                table: "PrzystosowanieSali",
                column: "PoziomID");

            migrationBuilder.CreateIndex(
                name: "IX_PrzystosowanieSali_SalaID",
                table: "PrzystosowanieSali",
                column: "SalaID");

            migrationBuilder.CreateIndex(
                name: "IX_SzczegolyKwalifikacji_DyscyplinaZPoziomemID",
                table: "SzczegolyKwalifikacji",
                column: "DyscyplinaZPoziomemID");

            migrationBuilder.CreateIndex(
                name: "IX_SzczegolyKwalifikacji_InstruktorID",
                table: "SzczegolyKwalifikacji",
                column: "InstruktorID");

            migrationBuilder.CreateIndex(
                name: "IX_Termin_GrafikID",
                table: "Termin",
                column: "GrafikID");

            migrationBuilder.CreateIndex(
                name: "IX_Wniosek_AdresDoKorespondencjiID",
                table: "Wniosek",
                column: "AdresDoKorespondencjiID");

            migrationBuilder.CreateIndex(
                name: "IX_Wniosek_AdresZameldowaniaID",
                table: "Wniosek",
                column: "AdresZameldowaniaID");

            migrationBuilder.CreateIndex(
                name: "IX_Wniosek_StatusID",
                table: "Wniosek",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_WymaganeWyposazenieDyscyplina_DyscyplinaID",
                table: "WymaganeWyposazenieDyscyplina",
                column: "DyscyplinaID");

            migrationBuilder.CreateIndex(
                name: "IX_WymaganeWyposazenieDyscyplina_WymaganeWyposazenieID",
                table: "WymaganeWyposazenieDyscyplina",
                column: "WymaganeWyposazenieID");

            migrationBuilder.CreateIndex(
                name: "IX_Zajecia_KursKod",
                table: "Zajecia",
                column: "KursKod");

            migrationBuilder.CreateIndex(
                name: "IX_Zajecia_SalaID",
                table: "Zajecia",
                column: "SalaID");

            migrationBuilder.CreateIndex(
                name: "IX_Zajecia_TerminDzien_TerminGodzinaRozpoczecia",
                table: "Zajecia",
                columns: new[] { "TerminDzien", "TerminGodzinaRozpoczecia" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrakujaceWyposazeniePrzystosowanieSali");

            migrationBuilder.DropTable(
                name: "CertyfikowaneKwalifikacjeInstruktor");

            migrationBuilder.DropTable(
                name: "DzienTygodnia");

            migrationBuilder.DropTable(
                name: "KwalifikacjeUprawnieni");

            migrationBuilder.DropTable(
                name: "Osoba");

            migrationBuilder.DropTable(
                name: "PoziomyDyscypliny");

            migrationBuilder.DropTable(
                name: "PrzystosowaneSalePrzeznaczenie");

            migrationBuilder.DropTable(
                name: "SzczegolyKwalifikacji");

            migrationBuilder.DropTable(
                name: "WymaganeWyposazenieDyscyplina");

            migrationBuilder.DropTable(
                name: "Zajecia");

            migrationBuilder.DropTable(
                name: "PrzystosowanieSali");

            migrationBuilder.DropTable(
                name: "Wniosek");

            migrationBuilder.DropTable(
                name: "DyscyplinaZPoziomem");

            migrationBuilder.DropTable(
                name: "Instruktor");

            migrationBuilder.DropTable(
                name: "Wyposażenie");

            migrationBuilder.DropTable(
                name: "Kurs");

            migrationBuilder.DropTable(
                name: "Termin");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Adres");

            migrationBuilder.DropTable(
                name: "StatusWniosku");

            migrationBuilder.DropTable(
                name: "Dyscyplina");

            migrationBuilder.DropTable(
                name: "PoziomZaawansowania");

            migrationBuilder.DropTable(
                name: "Grafik");

            migrationBuilder.DropTable(
                name: "Miejscowosc");

            migrationBuilder.DropTable(
                name: "Wojewodztwo");
        }
    }
}
