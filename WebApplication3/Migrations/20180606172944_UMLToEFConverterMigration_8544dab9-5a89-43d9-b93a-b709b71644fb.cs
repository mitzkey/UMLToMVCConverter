using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication3.Migrations
{
    public partial class UMLToEFConverterMigration_8544dab95a8943d9b93ab709b71644fb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DaneAdresowe",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    KodPocztowy = table.Column<string>(nullable: false),
                    Miejscowosc = table.Column<string>(nullable: false),
                    Numer = table.Column<string>(nullable: false),
                    Telefon = table.Column<string>(nullable: false),
                    Ulica = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaneAdresowe", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ekspert",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Imie = table.Column<string>(nullable: false),
                    Nazwisko = table.Column<string>(nullable: false),
                    Plec = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ekspert", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StatusEdycji",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusEdycji", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StatusPropozycji",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusPropozycji", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StatusRecenzji",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusRecenzji", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StatusZgloszenia",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusZgloszenia", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypJednostkiOrganizacyjnej",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypJednostkiOrganizacyjnej", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DaneAdresoweID = table.Column<int>(nullable: false),
                    DataUrodzenia = table.Column<DateTime>(nullable: false),
                    Imie = table.Column<string>(nullable: false),
                    Korespondent = table.Column<bool>(nullable: false),
                    Nazwisko = table.Column<string>(nullable: false),
                    Pesel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Autor_DaneAdresowe_DaneAdresoweID",
                        column: x => x.DaneAdresoweID,
                        principalTable: "DaneAdresowe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObszarBadan",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EkspertID = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObszarBadan", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ObszarBadan_Ekspert_EkspertID",
                        column: x => x.EkspertID,
                        principalTable: "Ekspert",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recenzent",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EkspertID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Recenzent_Ekspert_EkspertID",
                        column: x => x.EkspertID,
                        principalTable: "Ekspert",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Telefon",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EkspertID = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefon", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Telefon_Ekspert_EkspertID",
                        column: x => x.EkspertID,
                        principalTable: "Ekspert",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EdycjaKonkursu",
                columns: table => new
                {
                    Numer = table.Column<int>(nullable: false),
                    PlanowanaDataOpracowaniaRecenzji = table.Column<DateTime>(nullable: true),
                    PlanowanaDataRozstrzygnieciaKonkursu = table.Column<DateTime>(nullable: true),
                    Rok = table.Column<int>(nullable: false),
                    StatusID = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    WymaganeMinimum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EdycjaKonkursu", x => x.Numer);
                    table.ForeignKey(
                        name: "FK_EdycjaKonkursu_StatusEdycji_StatusID",
                        column: x => x.StatusID,
                        principalTable: "StatusEdycji",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JednostkaOrganizacyjna",
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
                    table.PrimaryKey("PK_JednostkaOrganizacyjna", x => x.NazwaKwalifikowana);
                    table.ForeignKey(
                        name: "FK_JednostkaOrganizacyjna_JednostkaOrganizacyjna_NadrzednaNazwaKwalifikowana",
                        column: x => x.NadrzednaNazwaKwalifikowana,
                        principalTable: "JednostkaOrganizacyjna",
                        principalColumn: "NazwaKwalifikowana",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JednostkaOrganizacyjna_DaneAdresowe_DaneAdresoweID",
                        column: x => x.DaneAdresoweID,
                        principalTable: "DaneAdresowe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JednostkaOrganizacyjna_TypJednostkiOrganizacyjnej_TypID",
                        column: x => x.TypID,
                        principalTable: "TypJednostkiOrganizacyjnej",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nagroda",
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
                    table.PrimaryKey("PK_Nagroda", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Nagroda_EdycjaKonkursu_EdycjaKonkursuPrzydzielanaWRamachNumer",
                        column: x => x.EdycjaKonkursuPrzydzielanaWRamachNumer,
                        principalTable: "EdycjaKonkursu",
                        principalColumn: "Numer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Skrot",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JednostkaOrganizacyjnaNazwaKwalifikowana = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skrot", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Skrot_JednostkaOrganizacyjna_JednostkaOrganizacyjnaNazwaKwalifikowana",
                        column: x => x.JednostkaOrganizacyjnaNazwaKwalifikowana,
                        principalTable: "JednostkaOrganizacyjna",
                        principalColumn: "NazwaKwalifikowana",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatusZatrudnienia",
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
                    table.PrimaryKey("PK_StatusZatrudnienia", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StatusZatrudnienia_Ekspert_EkspertID",
                        column: x => x.EkspertID,
                        principalTable: "Ekspert",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatusZatrudnienia_JednostkaOrganizacyjna_JednostkaOrganizacyjnaNazwaKwalifikowana",
                        column: x => x.JednostkaOrganizacyjnaNazwaKwalifikowana,
                        principalTable: "JednostkaOrganizacyjna",
                        principalColumn: "NazwaKwalifikowana",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZgloszeniePracy",
                columns: table => new
                {
                    Numer = table.Column<int>(nullable: false),
                    DataObrony = table.Column<DateTime>(nullable: false),
                    DataPrzekazaniaInformacjiOOdrzuceniu = table.Column<DateTime>(nullable: true),
                    DataZgloszenia = table.Column<DateTime>(nullable: false),
                    EdycjaNumer = table.Column<int>(nullable: false),
                    ElementyUzyteczneDlaNaukiPraktyki = table.Column<string>(nullable: false),
                    KierunekDalszychPrac = table.Column<string>(nullable: false),
                    NagrodaID = table.Column<int>(nullable: true),
                    NajwiekszeOsiagnieciaWlasneWPracy = table.Column<string>(nullable: false),
                    ObszarBadan = table.Column<string>(nullable: false),
                    PowodOdrzucenia = table.Column<string>(nullable: true),
                    PromotorID = table.Column<int>(nullable: false),
                    SredniaOcenaKomisji = table.Column<double>(nullable: false),
                    SredniaOcenaRecenzentow = table.Column<double>(nullable: false),
                    StatusID = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    Tytul = table.Column<string>(nullable: false),
                    UczelniaNazwaKwalifikowana = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZgloszeniePracy", x => x.Numer);
                    table.ForeignKey(
                        name: "FK_ZgloszeniePracy_EdycjaKonkursu_EdycjaNumer",
                        column: x => x.EdycjaNumer,
                        principalTable: "EdycjaKonkursu",
                        principalColumn: "Numer",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZgloszeniePracy_Nagroda_NagrodaID",
                        column: x => x.NagrodaID,
                        principalTable: "Nagroda",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZgloszeniePracy_Ekspert_PromotorID",
                        column: x => x.PromotorID,
                        principalTable: "Ekspert",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZgloszeniePracy_StatusZgloszenia_StatusID",
                        column: x => x.StatusID,
                        principalTable: "StatusZgloszenia",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZgloszeniePracy_JednostkaOrganizacyjna_UczelniaNazwaKwalifikowana",
                        column: x => x.UczelniaNazwaKwalifikowana,
                        principalTable: "JednostkaOrganizacyjna",
                        principalColumn: "NazwaKwalifikowana",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AutorzyPraca",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AutorzyID = table.Column<int>(nullable: true),
                    PracaNumer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorzyPraca", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AutorzyPraca_Autor_AutorzyID",
                        column: x => x.AutorzyID,
                        principalTable: "Autor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AutorzyPraca_ZgloszeniePracy_PracaNumer",
                        column: x => x.PracaNumer,
                        principalTable: "ZgloszeniePracy",
                        principalColumn: "Numer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Propozycja",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataPrzeslaniaProsby = table.Column<DateTime>(nullable: true),
                    EkspertID = table.Column<int>(nullable: false),
                    StatusID = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    ZgloszeniePracyNumer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propozycja", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Propozycja_Ekspert_EkspertID",
                        column: x => x.EkspertID,
                        principalTable: "Ekspert",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Propozycja_StatusPropozycji_StatusID",
                        column: x => x.StatusID,
                        principalTable: "StatusPropozycji",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Propozycja_ZgloszeniePracy_ZgloszeniePracyNumer",
                        column: x => x.ZgloszeniePracyNumer,
                        principalTable: "ZgloszeniePracy",
                        principalColumn: "Numer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recenzja",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataPrzeslaniaPonaglenia = table.Column<DateTime>(nullable: true),
                    DataZatwierdzenia = table.Column<DateTime>(nullable: true),
                    DodatkoweUwagi = table.Column<string>(nullable: true),
                    Ocena = table.Column<int>(nullable: true),
                    PlanowanaDataOpracowania = table.Column<DateTime>(nullable: false),
                    RecenzentID = table.Column<int>(nullable: false),
                    StatusID = table.Column<int>(nullable: false),
                    ZgloszeniePracyNumer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzja", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Recenzja_Recenzent_RecenzentID",
                        column: x => x.RecenzentID,
                        principalTable: "Recenzent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recenzja_ZgloszeniePracy_ZgloszeniePracyNumer",
                        column: x => x.ZgloszeniePracyNumer,
                        principalTable: "ZgloszeniePracy",
                        principalColumn: "Numer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SlowaKluczowe",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true),
                    ZgloszeniePracyNumer = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlowaKluczowe", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SlowaKluczowe_ZgloszeniePracy_ZgloszeniePracyNumer",
                        column: x => x.ZgloszeniePracyNumer,
                        principalTable: "ZgloszeniePracy",
                        principalColumn: "Numer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Autor_DaneAdresoweID",
                table: "Autor",
                column: "DaneAdresoweID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AutorzyPraca_AutorzyID",
                table: "AutorzyPraca",
                column: "AutorzyID");

            migrationBuilder.CreateIndex(
                name: "IX_AutorzyPraca_PracaNumer",
                table: "AutorzyPraca",
                column: "PracaNumer");

            migrationBuilder.CreateIndex(
                name: "IX_EdycjaKonkursu_StatusID",
                table: "EdycjaKonkursu",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_JednostkaOrganizacyjna_NadrzednaNazwaKwalifikowana",
                table: "JednostkaOrganizacyjna",
                column: "NadrzednaNazwaKwalifikowana");

            migrationBuilder.CreateIndex(
                name: "IX_JednostkaOrganizacyjna_DaneAdresoweID",
                table: "JednostkaOrganizacyjna",
                column: "DaneAdresoweID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JednostkaOrganizacyjna_TypID",
                table: "JednostkaOrganizacyjna",
                column: "TypID");

            migrationBuilder.CreateIndex(
                name: "IX_Nagroda_EdycjaKonkursuPrzydzielanaWRamachNumer",
                table: "Nagroda",
                column: "EdycjaKonkursuPrzydzielanaWRamachNumer");

            migrationBuilder.CreateIndex(
                name: "IX_ObszarBadan_EkspertID",
                table: "ObszarBadan",
                column: "EkspertID");

            migrationBuilder.CreateIndex(
                name: "IX_Propozycja_EkspertID",
                table: "Propozycja",
                column: "EkspertID");

            migrationBuilder.CreateIndex(
                name: "IX_Propozycja_StatusID",
                table: "Propozycja",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Propozycja_ZgloszeniePracyNumer",
                table: "Propozycja",
                column: "ZgloszeniePracyNumer");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzent_EkspertID",
                table: "Recenzent",
                column: "EkspertID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recenzja_RecenzentID",
                table: "Recenzja",
                column: "RecenzentID");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzja_ZgloszeniePracyNumer",
                table: "Recenzja",
                column: "ZgloszeniePracyNumer");

            migrationBuilder.CreateIndex(
                name: "IX_Skrot_JednostkaOrganizacyjnaNazwaKwalifikowana",
                table: "Skrot",
                column: "JednostkaOrganizacyjnaNazwaKwalifikowana");

            migrationBuilder.CreateIndex(
                name: "IX_SlowaKluczowe_ZgloszeniePracyNumer",
                table: "SlowaKluczowe",
                column: "ZgloszeniePracyNumer");

            migrationBuilder.CreateIndex(
                name: "IX_StatusZatrudnienia_EkspertID",
                table: "StatusZatrudnienia",
                column: "EkspertID");

            migrationBuilder.CreateIndex(
                name: "IX_StatusZatrudnienia_JednostkaOrganizacyjnaNazwaKwalifikowana",
                table: "StatusZatrudnienia",
                column: "JednostkaOrganizacyjnaNazwaKwalifikowana");

            migrationBuilder.CreateIndex(
                name: "IX_Telefon_EkspertID",
                table: "Telefon",
                column: "EkspertID");

            migrationBuilder.CreateIndex(
                name: "IX_ZgloszeniePracy_EdycjaNumer",
                table: "ZgloszeniePracy",
                column: "EdycjaNumer");

            migrationBuilder.CreateIndex(
                name: "IX_ZgloszeniePracy_NagrodaID",
                table: "ZgloszeniePracy",
                column: "NagrodaID");

            migrationBuilder.CreateIndex(
                name: "IX_ZgloszeniePracy_PromotorID",
                table: "ZgloszeniePracy",
                column: "PromotorID");

            migrationBuilder.CreateIndex(
                name: "IX_ZgloszeniePracy_StatusID",
                table: "ZgloszeniePracy",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ZgloszeniePracy_UczelniaNazwaKwalifikowana",
                table: "ZgloszeniePracy",
                column: "UczelniaNazwaKwalifikowana");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutorzyPraca");

            migrationBuilder.DropTable(
                name: "ObszarBadan");

            migrationBuilder.DropTable(
                name: "Propozycja");

            migrationBuilder.DropTable(
                name: "Recenzja");

            migrationBuilder.DropTable(
                name: "Skrot");

            migrationBuilder.DropTable(
                name: "SlowaKluczowe");

            migrationBuilder.DropTable(
                name: "StatusRecenzji");

            migrationBuilder.DropTable(
                name: "StatusZatrudnienia");

            migrationBuilder.DropTable(
                name: "Telefon");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "StatusPropozycji");

            migrationBuilder.DropTable(
                name: "Recenzent");

            migrationBuilder.DropTable(
                name: "ZgloszeniePracy");

            migrationBuilder.DropTable(
                name: "Nagroda");

            migrationBuilder.DropTable(
                name: "Ekspert");

            migrationBuilder.DropTable(
                name: "StatusZgloszenia");

            migrationBuilder.DropTable(
                name: "JednostkaOrganizacyjna");

            migrationBuilder.DropTable(
                name: "EdycjaKonkursu");

            migrationBuilder.DropTable(
                name: "DaneAdresowe");

            migrationBuilder.DropTable(
                name: "TypJednostkiOrganizacyjnej");

            migrationBuilder.DropTable(
                name: "StatusEdycji");
        }
    }
}
