using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication4.Migrations
{
    public partial class UMLToEFConverterMigration_7747c2c168ee4b4b858fa6f49a205684 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JednostkaLeksykalna",
                columns: table => new
                {
                    IdentyfikatorWordNet = table.Column<int>(nullable: false),
                    Lemat = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JednostkaLeksykalna", x => x.IdentyfikatorWordNet);
                });

            migrationBuilder.CreateTable(
                name: "Skala",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(nullable: false),
                    WartoscOd = table.Column<int>(nullable: false),
                    Wielkosc = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skala", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Slownik",
                columns: table => new
                {
                    Nazwa = table.Column<string>(nullable: false),
                    DataUtworzenia = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slownik", x => x.Nazwa);
                });

            migrationBuilder.CreateTable(
                name: "Tryb",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tryb", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Widok",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Widok", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PrzykladUzyc",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JednostkaLeksykalnaIdentyfikatorWordNet = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrzykladUzyc", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PrzykladUzyc_JednostkaLeksykalna_JednostkaLeksykalnaIdentyfikatorWordNet",
                        column: x => x.JednostkaLeksykalnaIdentyfikatorWordNet,
                        principalTable: "JednostkaLeksykalna",
                        principalColumn: "IdentyfikatorWordNet",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pozycja",
                columns: table => new
                {
                    Wartosc = table.Column<int>(nullable: false),
                    SkalaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pozycja", x => x.Wartosc);
                    table.ForeignKey(
                        name: "FK_Pozycja_Skala_SkalaID",
                        column: x => x.SkalaID,
                        principalTable: "Skala",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JednostkaWSlowniku",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JednostkaLeksykalnaIdentyfikatorWordNet = table.Column<int>(nullable: false),
                    NrPorzadkowy = table.Column<long>(nullable: false),
                    SlownikNazwa = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JednostkaWSlowniku", x => x.ID);
                    table.ForeignKey(
                        name: "FK_JednostkaWSlowniku_JednostkaLeksykalna_JednostkaLeksykalnaIdentyfikatorWordNet",
                        column: x => x.JednostkaLeksykalnaIdentyfikatorWordNet,
                        principalTable: "JednostkaLeksykalna",
                        principalColumn: "IdentyfikatorWordNet",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JednostkaWSlowniku_Slownik_SlownikNazwa",
                        column: x => x.SlownikNazwa,
                        principalTable: "Slownik",
                        principalColumn: "Nazwa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZestawTreningowySlownik",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SlownikNazwa = table.Column<string>(nullable: true),
                    ZestawTreningowyIdentyfikatorWordNet = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZestawTreningowySlownik", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ZestawTreningowySlownik_Slownik_SlownikNazwa",
                        column: x => x.SlownikNazwa,
                        principalTable: "Slownik",
                        principalColumn: "Nazwa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZestawTreningowySlownik_JednostkaLeksykalna_ZestawTreningowyIdentyfikatorWordNet",
                        column: x => x.ZestawTreningowyIdentyfikatorWordNet,
                        principalTable: "JednostkaLeksykalna",
                        principalColumn: "IdentyfikatorWordNet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eksperyment",
                columns: table => new
                {
                    Identyfikator = table.Column<string>(nullable: false),
                    DataRozpoczecia = table.Column<DateTime>(nullable: true),
                    DataZakonczenia = table.Column<DateTime>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    LiczbaJednostekWZestawie = table.Column<long>(nullable: false),
                    MaksymalnyCzasBadania = table.Column<int>(nullable: false),
                    MaksymalnyCzasPojedynczejOdpowiedzi = table.Column<int>(nullable: true),
                    Nazwa = table.Column<string>(nullable: false),
                    SkalaEmocjiID = table.Column<int>(nullable: false),
                    SlownikNazwa = table.Column<string>(nullable: false),
                    TrybID = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    WidokEmocjiID = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    WymaganePokrycie = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eksperyment", x => x.Identyfikator);
                    table.ForeignKey(
                        name: "FK_Eksperyment_Skala_SkalaEmocjiID",
                        column: x => x.SkalaEmocjiID,
                        principalTable: "Skala",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eksperyment_Slownik_SlownikNazwa",
                        column: x => x.SlownikNazwa,
                        principalTable: "Slownik",
                        principalColumn: "Nazwa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eksperyment_Tryb_TrybID",
                        column: x => x.TrybID,
                        principalTable: "Tryb",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Eksperyment_Widok_WidokEmocjiID",
                        column: x => x.WidokEmocjiID,
                        principalTable: "Widok",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ankieta",
                columns: table => new
                {
                    Numer = table.Column<long>(nullable: false),
                    EksperymentIdentyfikator = table.Column<string>(nullable: false),
                    PrzekroczonyCzasBadania = table.Column<bool>(nullable: false),
                    TerminRozpoczecia = table.Column<DateTime>(nullable: false),
                    TerminZakonczenia = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ankieta", x => x.Numer);
                    table.ForeignKey(
                        name: "FK_Ankieta_Eksperyment_EksperymentIdentyfikator",
                        column: x => x.EksperymentIdentyfikator,
                        principalTable: "Eksperyment",
                        principalColumn: "Identyfikator",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrzedmiotBadania",
                columns: table => new
                {
                    EksperymentLokalnyIdentyfikator = table.Column<string>(nullable: true),
                    ZModeluPK = table.Column<bool>(nullable: true),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    Nazwa = table.Column<string>(nullable: false),
                    Opis = table.Column<string>(nullable: true),
                    ZakresDo = table.Column<string>(nullable: true),
                    ZakresOd = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrzedmiotBadania", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PrzedmiotBadania_Eksperyment_EksperymentLokalnyIdentyfikator",
                        column: x => x.EksperymentLokalnyIdentyfikator,
                        principalTable: "Eksperyment",
                        principalColumn: "Identyfikator",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Odpowiedz",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnkietaNumer = table.Column<long>(nullable: false),
                    CzasOdpowiedzi = table.Column<int>(nullable: false),
                    JednostkaLeksykalnaIdentyfikatorWordNet = table.Column<int>(nullable: false),
                    Kompletna = table.Column<bool>(nullable: false),
                    NumerPorzadkowy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odpowiedz", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Odpowiedz_Ankieta_AnkietaNumer",
                        column: x => x.AnkietaNumer,
                        principalTable: "Ankieta",
                        principalColumn: "Numer",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Odpowiedz_JednostkaLeksykalna_JednostkaLeksykalnaIdentyfikatorWordNet",
                        column: x => x.JednostkaLeksykalnaIdentyfikatorWordNet,
                        principalTable: "JednostkaLeksykalna",
                        principalColumn: "IdentyfikatorWordNet",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BadaneEmocjeEksperyment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BadaneEmocjeID = table.Column<int>(nullable: true),
                    EksperymentIdentyfikator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadaneEmocjeEksperyment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BadaneEmocjeEksperyment_PrzedmiotBadania_BadaneEmocjeID",
                        column: x => x.BadaneEmocjeID,
                        principalTable: "PrzedmiotBadania",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BadaneEmocjeEksperyment_Eksperyment_EksperymentIdentyfikator",
                        column: x => x.EksperymentIdentyfikator,
                        principalTable: "Eksperyment",
                        principalColumn: "Identyfikator",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WymiarWBadaniu",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EksperymentIdentyfikator = table.Column<string>(nullable: false),
                    SkalaID = table.Column<int>(nullable: false),
                    WymiarID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WymiarWBadaniu", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WymiarWBadaniu_Eksperyment_EksperymentIdentyfikator",
                        column: x => x.EksperymentIdentyfikator,
                        principalTable: "Eksperyment",
                        principalColumn: "Identyfikator",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WymiarWBadaniu_Skala_SkalaID",
                        column: x => x.SkalaID,
                        principalTable: "Skala",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WymiarWBadaniu_PrzedmiotBadania_WymiarID",
                        column: x => x.WymiarID,
                        principalTable: "PrzedmiotBadania",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ocena",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OdpowiedzID = table.Column<int>(nullable: false),
                    PrzedmiotBadaniaID = table.Column<int>(nullable: false),
                    WybranaWartoscWartosc = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocena", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ocena_Odpowiedz_OdpowiedzID",
                        column: x => x.OdpowiedzID,
                        principalTable: "Odpowiedz",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ocena_PrzedmiotBadania_PrzedmiotBadaniaID",
                        column: x => x.PrzedmiotBadaniaID,
                        principalTable: "PrzedmiotBadania",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ocena_Pozycja_WybranaWartoscWartosc",
                        column: x => x.WybranaWartoscWartosc,
                        principalTable: "Pozycja",
                        principalColumn: "Wartosc",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ankieta_EksperymentIdentyfikator",
                table: "Ankieta",
                column: "EksperymentIdentyfikator");

            migrationBuilder.CreateIndex(
                name: "IX_BadaneEmocjeEksperyment_BadaneEmocjeID",
                table: "BadaneEmocjeEksperyment",
                column: "BadaneEmocjeID");

            migrationBuilder.CreateIndex(
                name: "IX_BadaneEmocjeEksperyment_EksperymentIdentyfikator",
                table: "BadaneEmocjeEksperyment",
                column: "EksperymentIdentyfikator");

            migrationBuilder.CreateIndex(
                name: "IX_Eksperyment_SkalaEmocjiID",
                table: "Eksperyment",
                column: "SkalaEmocjiID");

            migrationBuilder.CreateIndex(
                name: "IX_Eksperyment_SlownikNazwa",
                table: "Eksperyment",
                column: "SlownikNazwa");

            migrationBuilder.CreateIndex(
                name: "IX_Eksperyment_TrybID",
                table: "Eksperyment",
                column: "TrybID");

            migrationBuilder.CreateIndex(
                name: "IX_Eksperyment_WidokEmocjiID",
                table: "Eksperyment",
                column: "WidokEmocjiID");

            migrationBuilder.CreateIndex(
                name: "IX_JednostkaWSlowniku_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "JednostkaWSlowniku",
                column: "JednostkaLeksykalnaIdentyfikatorWordNet");

            migrationBuilder.CreateIndex(
                name: "IX_JednostkaWSlowniku_SlownikNazwa",
                table: "JednostkaWSlowniku",
                column: "SlownikNazwa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ocena_OdpowiedzID",
                table: "Ocena",
                column: "OdpowiedzID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ocena_PrzedmiotBadaniaID",
                table: "Ocena",
                column: "PrzedmiotBadaniaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ocena_WybranaWartoscWartosc",
                table: "Ocena",
                column: "WybranaWartoscWartosc");

            migrationBuilder.CreateIndex(
                name: "IX_Odpowiedz_AnkietaNumer",
                table: "Odpowiedz",
                column: "AnkietaNumer");

            migrationBuilder.CreateIndex(
                name: "IX_Odpowiedz_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "Odpowiedz",
                column: "JednostkaLeksykalnaIdentyfikatorWordNet");

            migrationBuilder.CreateIndex(
                name: "IX_Pozycja_SkalaID",
                table: "Pozycja",
                column: "SkalaID");

            migrationBuilder.CreateIndex(
                name: "IX_PrzedmiotBadania_EksperymentLokalnyIdentyfikator",
                table: "PrzedmiotBadania",
                column: "EksperymentLokalnyIdentyfikator");

            migrationBuilder.CreateIndex(
                name: "IX_PrzykladUzyc_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "PrzykladUzyc",
                column: "JednostkaLeksykalnaIdentyfikatorWordNet");

            migrationBuilder.CreateIndex(
                name: "IX_WymiarWBadaniu_EksperymentIdentyfikator",
                table: "WymiarWBadaniu",
                column: "EksperymentIdentyfikator");

            migrationBuilder.CreateIndex(
                name: "IX_WymiarWBadaniu_SkalaID",
                table: "WymiarWBadaniu",
                column: "SkalaID");

            migrationBuilder.CreateIndex(
                name: "IX_WymiarWBadaniu_WymiarID",
                table: "WymiarWBadaniu",
                column: "WymiarID");

            migrationBuilder.CreateIndex(
                name: "IX_ZestawTreningowySlownik_SlownikNazwa",
                table: "ZestawTreningowySlownik",
                column: "SlownikNazwa");

            migrationBuilder.CreateIndex(
                name: "IX_ZestawTreningowySlownik_ZestawTreningowyIdentyfikatorWordNet",
                table: "ZestawTreningowySlownik",
                column: "ZestawTreningowyIdentyfikatorWordNet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BadaneEmocjeEksperyment");

            migrationBuilder.DropTable(
                name: "JednostkaWSlowniku");

            migrationBuilder.DropTable(
                name: "Ocena");

            migrationBuilder.DropTable(
                name: "PrzykladUzyc");

            migrationBuilder.DropTable(
                name: "WymiarWBadaniu");

            migrationBuilder.DropTable(
                name: "ZestawTreningowySlownik");

            migrationBuilder.DropTable(
                name: "Odpowiedz");

            migrationBuilder.DropTable(
                name: "Pozycja");

            migrationBuilder.DropTable(
                name: "PrzedmiotBadania");

            migrationBuilder.DropTable(
                name: "Ankieta");

            migrationBuilder.DropTable(
                name: "JednostkaLeksykalna");

            migrationBuilder.DropTable(
                name: "Eksperyment");

            migrationBuilder.DropTable(
                name: "Skala");

            migrationBuilder.DropTable(
                name: "Slownik");

            migrationBuilder.DropTable(
                name: "Tryb");

            migrationBuilder.DropTable(
                name: "Widok");
        }
    }
}
