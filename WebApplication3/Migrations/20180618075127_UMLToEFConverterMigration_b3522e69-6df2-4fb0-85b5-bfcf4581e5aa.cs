using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication3.Migrations
{
    public partial class UMLToEFConverterMigration_b3522e696df24fb085b5bfcf4581e5aa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DaneAdresoweSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    KodPocztowy = table.Column<string>(nullable: false),
                    Miejscowosc = table.Column<string>(nullable: false),
                    Numer = table.Column<string>(nullable: false),
                    Telefon = table.Column<string>(nullable: false),
                    Ulica = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaneAdresoweSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EkspertSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    Imie = table.Column<string>(nullable: false),
                    Nazwisko = table.Column<string>(nullable: false),
                    Plec = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EkspertSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StatusEdycjiSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusEdycjiSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StatusPropozycjiSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusPropozycjiSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StatusRecenzjiSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusRecenzjiSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StatusZgloszeniaSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusZgloszeniaSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypJednostkiOrganizacyjnejSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypJednostkiOrganizacyjnejSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AutorSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DaneAdresoweID = table.Column<int>(nullable: false),
                    DataUrodzenia = table.Column<DateTime>(nullable: false),
                    Imie = table.Column<string>(nullable: false),
                    Korespondent = table.Column<bool>(nullable: false),
                    Nazwisko = table.Column<string>(nullable: false),
                    Pesel = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AutorSet_DaneAdresoweSet_DaneAdresoweID",
                        column: x => x.DaneAdresoweID,
                        principalTable: "DaneAdresoweSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObszarBadanSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EkspertID = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObszarBadanSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ObszarBadanSet_EkspertSet_EkspertID",
                        column: x => x.EkspertID,
                        principalTable: "EkspertSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecenzentSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EkspertID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecenzentSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RecenzentSet_EkspertSet_EkspertID",
                        column: x => x.EkspertID,
                        principalTable: "EkspertSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelefonSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EkspertID = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelefonSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelefonSet_EkspertSet_EkspertID",
                        column: x => x.EkspertID,
                        principalTable: "EkspertSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EdycjaKonkursuSet",
                columns: table => new
                {
                    Numer = table.Column<int>(nullable: false),
                    PlanowanaDataOpracowaniaRecenzji = table.Column<DateTime>(nullable: false),
                    PlanowanaDataRozstrzygnieciaKonkursu = table.Column<DateTime>(nullable: false),
                    Rok = table.Column<int>(nullable: false),
                    StatusID = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    WymaganeMinimum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EdycjaKonkursuSet", x => x.Numer);
                    table.ForeignKey(
                        name: "FK_EdycjaKonkursuSet_StatusEdycjiSet_StatusID",
                        column: x => x.StatusID,
                        principalTable: "StatusEdycjiSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JednostkaOrganizacyjnaSet",
                columns: table => new
                {
                    NadrzednaNazwaKwalifikowana = table.Column<string>(nullable: true),
                    NazwaKwalifikowana = table.Column<string>(nullable: false),
                    DaneAdresoweID = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Nazwa = table.Column<string>(nullable: false),
                    TypID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JednostkaOrganizacyjnaSet", x => x.NazwaKwalifikowana);
                    table.ForeignKey(
                        name: "FK_JednostkaOrganizacyjnaSet_JednostkaOrganizacyjnaSet_NadrzednaNazwaKwalifikowana",
                        column: x => x.NadrzednaNazwaKwalifikowana,
                        principalTable: "JednostkaOrganizacyjnaSet",
                        principalColumn: "NazwaKwalifikowana",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JednostkaOrganizacyjnaSet_DaneAdresoweSet_DaneAdresoweID",
                        column: x => x.DaneAdresoweID,
                        principalTable: "DaneAdresoweSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JednostkaOrganizacyjnaSet_TypJednostkiOrganizacyjnejSet_TypID",
                        column: x => x.TypID,
                        principalTable: "TypJednostkiOrganizacyjnejSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NagrodaSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EdycjaKonkursuPrzydzielanaWRamachNumer = table.Column<int>(nullable: false),
                    Rodzaj = table.Column<string>(nullable: false),
                    Wartosc = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NagrodaSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NagrodaSet_EdycjaKonkursuSet_EdycjaKonkursuPrzydzielanaWRamachNumer",
                        column: x => x.EdycjaKonkursuPrzydzielanaWRamachNumer,
                        principalTable: "EdycjaKonkursuSet",
                        principalColumn: "Numer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkrotSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JednostkaOrganizacyjnaNazwaKwalifikowana = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkrotSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SkrotSet_JednostkaOrganizacyjnaSet_JednostkaOrganizacyjnaNazwaKwalifikowana",
                        column: x => x.JednostkaOrganizacyjnaNazwaKwalifikowana,
                        principalTable: "JednostkaOrganizacyjnaSet",
                        principalColumn: "NazwaKwalifikowana",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatusZatrudnieniaSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktualne = table.Column<bool>(nullable: false),
                    EkspertID = table.Column<int>(nullable: false),
                    JednostkaOrganizacyjnaNazwaKwalifikowana = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusZatrudnieniaSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StatusZatrudnieniaSet_EkspertSet_EkspertID",
                        column: x => x.EkspertID,
                        principalTable: "EkspertSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatusZatrudnieniaSet_JednostkaOrganizacyjnaSet_JednostkaOrganizacyjnaNazwaKwalifikowana",
                        column: x => x.JednostkaOrganizacyjnaNazwaKwalifikowana,
                        principalTable: "JednostkaOrganizacyjnaSet",
                        principalColumn: "NazwaKwalifikowana",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZgloszeniePracySet",
                columns: table => new
                {
                    Numer = table.Column<int>(nullable: false),
                    DataObrony = table.Column<DateTime>(nullable: false),
                    DataPrzekazaniaInformacjiOOdrzuceniu = table.Column<DateTime>(nullable: false),
                    DataZgloszenia = table.Column<DateTime>(nullable: false),
                    EdycjaNumer = table.Column<int>(nullable: false),
                    ElementyUzyteczneDlaNaukiPraktyki = table.Column<string>(nullable: false),
                    KierunekDalszychPrac = table.Column<string>(nullable: false),
                    NagrodaID = table.Column<int>(nullable: false),
                    NajwiekszeOsiagnieciaWlasneWPracy = table.Column<string>(nullable: false),
                    ObszarBadan = table.Column<string>(nullable: false),
                    PowodOdrzucenia = table.Column<string>(nullable: false),
                    PromotorID = table.Column<int>(nullable: false),
                    SredniaOcenaKomisji = table.Column<double>(nullable: false),
                    SredniaOcenaRecenzentow = table.Column<double>(nullable: false),
                    StatusID = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    Tytul = table.Column<string>(nullable: false),
                    UczelniaNazwaKwalifikowana = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZgloszeniePracySet", x => x.Numer);
                    table.ForeignKey(
                        name: "FK_ZgloszeniePracySet_EdycjaKonkursuSet_EdycjaNumer",
                        column: x => x.EdycjaNumer,
                        principalTable: "EdycjaKonkursuSet",
                        principalColumn: "Numer",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZgloszeniePracySet_NagrodaSet_NagrodaID",
                        column: x => x.NagrodaID,
                        principalTable: "NagrodaSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZgloszeniePracySet_EkspertSet_PromotorID",
                        column: x => x.PromotorID,
                        principalTable: "EkspertSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZgloszeniePracySet_StatusZgloszeniaSet_StatusID",
                        column: x => x.StatusID,
                        principalTable: "StatusZgloszeniaSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZgloszeniePracySet_JednostkaOrganizacyjnaSet_UczelniaNazwaKwalifikowana",
                        column: x => x.UczelniaNazwaKwalifikowana,
                        principalTable: "JednostkaOrganizacyjnaSet",
                        principalColumn: "NazwaKwalifikowana",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AutorzyPracaSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AutorzyID = table.Column<int>(nullable: true),
                    PracaNumer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorzyPracaSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AutorzyPracaSet_AutorSet_AutorzyID",
                        column: x => x.AutorzyID,
                        principalTable: "AutorSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AutorzyPracaSet_ZgloszeniePracySet_PracaNumer",
                        column: x => x.PracaNumer,
                        principalTable: "ZgloszeniePracySet",
                        principalColumn: "Numer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropozycjaSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataPrzeslaniaProsby = table.Column<DateTime>(nullable: false),
                    EkspertID = table.Column<int>(nullable: false),
                    StatusID = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    ZgloszeniePracyNumer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropozycjaSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropozycjaSet_EkspertSet_EkspertID",
                        column: x => x.EkspertID,
                        principalTable: "EkspertSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropozycjaSet_StatusPropozycjiSet_StatusID",
                        column: x => x.StatusID,
                        principalTable: "StatusPropozycjiSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropozycjaSet_ZgloszeniePracySet_ZgloszeniePracyNumer",
                        column: x => x.ZgloszeniePracyNumer,
                        principalTable: "ZgloszeniePracySet",
                        principalColumn: "Numer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecenzjaSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataPrzeslaniaPonaglenia = table.Column<DateTime>(nullable: false),
                    DataZatwierdzenia = table.Column<DateTime>(nullable: false),
                    DodatkoweUwagi = table.Column<string>(nullable: false),
                    Ocena = table.Column<int>(nullable: false),
                    PlanowanaDataOpracowania = table.Column<DateTime>(nullable: false),
                    RecenzentID = table.Column<int>(nullable: false),
                    StatusID = table.Column<int>(nullable: false),
                    ZgloszeniePracyNumer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecenzjaSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RecenzjaSet_RecenzentSet_RecenzentID",
                        column: x => x.RecenzentID,
                        principalTable: "RecenzentSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecenzjaSet_ZgloszeniePracySet_ZgloszeniePracyNumer",
                        column: x => x.ZgloszeniePracyNumer,
                        principalTable: "ZgloszeniePracySet",
                        principalColumn: "Numer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SlowaKluczoweSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true),
                    ZgloszeniePracyNumer = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlowaKluczoweSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SlowaKluczoweSet_ZgloszeniePracySet_ZgloszeniePracyNumer",
                        column: x => x.ZgloszeniePracyNumer,
                        principalTable: "ZgloszeniePracySet",
                        principalColumn: "Numer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutorSet_DaneAdresoweID",
                table: "AutorSet",
                column: "DaneAdresoweID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AutorzyPracaSet_AutorzyID",
                table: "AutorzyPracaSet",
                column: "AutorzyID");

            migrationBuilder.CreateIndex(
                name: "IX_AutorzyPracaSet_PracaNumer",
                table: "AutorzyPracaSet",
                column: "PracaNumer");

            migrationBuilder.CreateIndex(
                name: "IX_EdycjaKonkursuSet_StatusID",
                table: "EdycjaKonkursuSet",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_JednostkaOrganizacyjnaSet_NadrzednaNazwaKwalifikowana",
                table: "JednostkaOrganizacyjnaSet",
                column: "NadrzednaNazwaKwalifikowana");

            migrationBuilder.CreateIndex(
                name: "IX_JednostkaOrganizacyjnaSet_DaneAdresoweID",
                table: "JednostkaOrganizacyjnaSet",
                column: "DaneAdresoweID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JednostkaOrganizacyjnaSet_TypID",
                table: "JednostkaOrganizacyjnaSet",
                column: "TypID");

            migrationBuilder.CreateIndex(
                name: "IX_NagrodaSet_EdycjaKonkursuPrzydzielanaWRamachNumer",
                table: "NagrodaSet",
                column: "EdycjaKonkursuPrzydzielanaWRamachNumer");

            migrationBuilder.CreateIndex(
                name: "IX_ObszarBadanSet_EkspertID",
                table: "ObszarBadanSet",
                column: "EkspertID");

            migrationBuilder.CreateIndex(
                name: "IX_PropozycjaSet_EkspertID",
                table: "PropozycjaSet",
                column: "EkspertID");

            migrationBuilder.CreateIndex(
                name: "IX_PropozycjaSet_StatusID",
                table: "PropozycjaSet",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PropozycjaSet_ZgloszeniePracyNumer",
                table: "PropozycjaSet",
                column: "ZgloszeniePracyNumer");

            migrationBuilder.CreateIndex(
                name: "IX_RecenzentSet_EkspertID",
                table: "RecenzentSet",
                column: "EkspertID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecenzjaSet_RecenzentID",
                table: "RecenzjaSet",
                column: "RecenzentID");

            migrationBuilder.CreateIndex(
                name: "IX_RecenzjaSet_ZgloszeniePracyNumer",
                table: "RecenzjaSet",
                column: "ZgloszeniePracyNumer");

            migrationBuilder.CreateIndex(
                name: "IX_SkrotSet_JednostkaOrganizacyjnaNazwaKwalifikowana",
                table: "SkrotSet",
                column: "JednostkaOrganizacyjnaNazwaKwalifikowana");

            migrationBuilder.CreateIndex(
                name: "IX_SlowaKluczoweSet_ZgloszeniePracyNumer",
                table: "SlowaKluczoweSet",
                column: "ZgloszeniePracyNumer");

            migrationBuilder.CreateIndex(
                name: "IX_StatusZatrudnieniaSet_EkspertID",
                table: "StatusZatrudnieniaSet",
                column: "EkspertID");

            migrationBuilder.CreateIndex(
                name: "IX_StatusZatrudnieniaSet_JednostkaOrganizacyjnaNazwaKwalifikowana",
                table: "StatusZatrudnieniaSet",
                column: "JednostkaOrganizacyjnaNazwaKwalifikowana");

            migrationBuilder.CreateIndex(
                name: "IX_TelefonSet_EkspertID",
                table: "TelefonSet",
                column: "EkspertID");

            migrationBuilder.CreateIndex(
                name: "IX_ZgloszeniePracySet_EdycjaNumer",
                table: "ZgloszeniePracySet",
                column: "EdycjaNumer");

            migrationBuilder.CreateIndex(
                name: "IX_ZgloszeniePracySet_NagrodaID",
                table: "ZgloszeniePracySet",
                column: "NagrodaID");

            migrationBuilder.CreateIndex(
                name: "IX_ZgloszeniePracySet_PromotorID",
                table: "ZgloszeniePracySet",
                column: "PromotorID");

            migrationBuilder.CreateIndex(
                name: "IX_ZgloszeniePracySet_StatusID",
                table: "ZgloszeniePracySet",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ZgloszeniePracySet_UczelniaNazwaKwalifikowana",
                table: "ZgloszeniePracySet",
                column: "UczelniaNazwaKwalifikowana");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutorzyPracaSet");

            migrationBuilder.DropTable(
                name: "ObszarBadanSet");

            migrationBuilder.DropTable(
                name: "PropozycjaSet");

            migrationBuilder.DropTable(
                name: "RecenzjaSet");

            migrationBuilder.DropTable(
                name: "SkrotSet");

            migrationBuilder.DropTable(
                name: "SlowaKluczoweSet");

            migrationBuilder.DropTable(
                name: "StatusRecenzjiSet");

            migrationBuilder.DropTable(
                name: "StatusZatrudnieniaSet");

            migrationBuilder.DropTable(
                name: "TelefonSet");

            migrationBuilder.DropTable(
                name: "AutorSet");

            migrationBuilder.DropTable(
                name: "StatusPropozycjiSet");

            migrationBuilder.DropTable(
                name: "RecenzentSet");

            migrationBuilder.DropTable(
                name: "ZgloszeniePracySet");

            migrationBuilder.DropTable(
                name: "NagrodaSet");

            migrationBuilder.DropTable(
                name: "EkspertSet");

            migrationBuilder.DropTable(
                name: "StatusZgloszeniaSet");

            migrationBuilder.DropTable(
                name: "JednostkaOrganizacyjnaSet");

            migrationBuilder.DropTable(
                name: "EdycjaKonkursuSet");

            migrationBuilder.DropTable(
                name: "DaneAdresoweSet");

            migrationBuilder.DropTable(
                name: "TypJednostkiOrganizacyjnejSet");

            migrationBuilder.DropTable(
                name: "StatusEdycjiSet");
        }
    }
}
