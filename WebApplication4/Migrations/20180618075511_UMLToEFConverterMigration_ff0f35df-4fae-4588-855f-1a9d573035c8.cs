using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication4.Migrations
{
    public partial class UMLToEFConverterMigration_ff0f35df4fae4588855f1a9d573035c8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ankieta_Eksperyment_EksperymentIdentyfikator",
                table: "Ankieta");

            migrationBuilder.DropForeignKey(
                name: "FK_BadaneEmocjeEksperyment_PrzedmiotBadania_BadaneEmocjeID",
                table: "BadaneEmocjeEksperyment");

            migrationBuilder.DropForeignKey(
                name: "FK_BadaneEmocjeEksperyment_Eksperyment_EksperymentIdentyfikator",
                table: "BadaneEmocjeEksperyment");

            migrationBuilder.DropForeignKey(
                name: "FK_Eksperyment_Skala_SkalaEmocjiID",
                table: "Eksperyment");

            migrationBuilder.DropForeignKey(
                name: "FK_Eksperyment_Slownik_SlownikNazwa",
                table: "Eksperyment");

            migrationBuilder.DropForeignKey(
                name: "FK_Eksperyment_Tryb_TrybID",
                table: "Eksperyment");

            migrationBuilder.DropForeignKey(
                name: "FK_Eksperyment_Widok_WidokEmocjiID",
                table: "Eksperyment");

            migrationBuilder.DropForeignKey(
                name: "FK_JednostkaWSlowniku_JednostkaLeksykalna_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "JednostkaWSlowniku");

            migrationBuilder.DropForeignKey(
                name: "FK_JednostkaWSlowniku_Slownik_SlownikNazwa",
                table: "JednostkaWSlowniku");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocena_Odpowiedz_OdpowiedzID",
                table: "Ocena");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocena_PrzedmiotBadania_PrzedmiotBadaniaID",
                table: "Ocena");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocena_Pozycja_WybranaWartoscWartosc",
                table: "Ocena");

            migrationBuilder.DropForeignKey(
                name: "FK_Odpowiedz_Ankieta_AnkietaNumer",
                table: "Odpowiedz");

            migrationBuilder.DropForeignKey(
                name: "FK_Odpowiedz_JednostkaLeksykalna_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "Odpowiedz");

            migrationBuilder.DropForeignKey(
                name: "FK_Pozycja_Skala_SkalaID",
                table: "Pozycja");

            migrationBuilder.DropForeignKey(
                name: "FK_PrzedmiotBadania_Eksperyment_EksperymentLokalnyIdentyfikator",
                table: "PrzedmiotBadania");

            migrationBuilder.DropForeignKey(
                name: "FK_PrzykladUzyc_JednostkaLeksykalna_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "PrzykladUzyc");

            migrationBuilder.DropForeignKey(
                name: "FK_WymiarWBadaniu_Eksperyment_EksperymentIdentyfikator",
                table: "WymiarWBadaniu");

            migrationBuilder.DropForeignKey(
                name: "FK_WymiarWBadaniu_Skala_SkalaID",
                table: "WymiarWBadaniu");

            migrationBuilder.DropForeignKey(
                name: "FK_WymiarWBadaniu_PrzedmiotBadania_WymiarID",
                table: "WymiarWBadaniu");

            migrationBuilder.DropForeignKey(
                name: "FK_ZestawTreningowySlownik_Slownik_SlownikNazwa",
                table: "ZestawTreningowySlownik");

            migrationBuilder.DropForeignKey(
                name: "FK_ZestawTreningowySlownik_JednostkaLeksykalna_ZestawTreningowyIdentyfikatorWordNet",
                table: "ZestawTreningowySlownik");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZestawTreningowySlownik",
                table: "ZestawTreningowySlownik");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WymiarWBadaniu",
                table: "WymiarWBadaniu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Widok",
                table: "Widok");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tryb",
                table: "Tryb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slownik",
                table: "Slownik");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skala",
                table: "Skala");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrzykladUzyc",
                table: "PrzykladUzyc");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrzedmiotBadania",
                table: "PrzedmiotBadania");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pozycja",
                table: "Pozycja");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Odpowiedz",
                table: "Odpowiedz");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ocena",
                table: "Ocena");

            migrationBuilder.DropIndex(
                name: "IX_Ocena_OdpowiedzID",
                table: "Ocena");

            migrationBuilder.DropIndex(
                name: "IX_Ocena_PrzedmiotBadaniaID",
                table: "Ocena");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JednostkaWSlowniku",
                table: "JednostkaWSlowniku");

            migrationBuilder.DropIndex(
                name: "IX_JednostkaWSlowniku_SlownikNazwa",
                table: "JednostkaWSlowniku");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JednostkaLeksykalna",
                table: "JednostkaLeksykalna");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Eksperyment",
                table: "Eksperyment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BadaneEmocjeEksperyment",
                table: "BadaneEmocjeEksperyment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ankieta",
                table: "Ankieta");

            migrationBuilder.DropColumn(
                name: "OdpowiedzID",
                table: "Ocena");

            migrationBuilder.DropColumn(
                name: "PrzedmiotBadaniaID",
                table: "Ocena");

            migrationBuilder.DropColumn(
                name: "SlownikNazwa",
                table: "JednostkaWSlowniku");

            migrationBuilder.RenameTable(
                name: "ZestawTreningowySlownik",
                newName: "ZestawTreningowySlownikSet");

            migrationBuilder.RenameTable(
                name: "WymiarWBadaniu",
                newName: "WymiarWBadaniuSet");

            migrationBuilder.RenameTable(
                name: "Widok",
                newName: "WidokSet");

            migrationBuilder.RenameTable(
                name: "Tryb",
                newName: "TrybSet");

            migrationBuilder.RenameTable(
                name: "Slownik",
                newName: "SlownikSet");

            migrationBuilder.RenameTable(
                name: "Skala",
                newName: "SkalaSet");

            migrationBuilder.RenameTable(
                name: "PrzykladUzyc",
                newName: "PrzykladUzycSet");

            migrationBuilder.RenameTable(
                name: "PrzedmiotBadania",
                newName: "PrzedmiotBadaniaSet");

            migrationBuilder.RenameTable(
                name: "Pozycja",
                newName: "PozycjaSet");

            migrationBuilder.RenameTable(
                name: "Odpowiedz",
                newName: "OdpowiedzSet");

            migrationBuilder.RenameTable(
                name: "Ocena",
                newName: "OcenaSet");

            migrationBuilder.RenameTable(
                name: "JednostkaWSlowniku",
                newName: "JednostkaWSlownikuSet");

            migrationBuilder.RenameTable(
                name: "JednostkaLeksykalna",
                newName: "JednostkaLeksykalnaSet");

            migrationBuilder.RenameTable(
                name: "Eksperyment",
                newName: "EksperymentSet");

            migrationBuilder.RenameTable(
                name: "BadaneEmocjeEksperyment",
                newName: "BadaneEmocjeEksperymentSet");

            migrationBuilder.RenameTable(
                name: "Ankieta",
                newName: "AnkietaSet");

            migrationBuilder.RenameIndex(
                name: "IX_ZestawTreningowySlownik_ZestawTreningowyIdentyfikatorWordNet",
                table: "ZestawTreningowySlownikSet",
                newName: "IX_ZestawTreningowySlownikSet_ZestawTreningowyIdentyfikatorWordNet");

            migrationBuilder.RenameIndex(
                name: "IX_ZestawTreningowySlownik_SlownikNazwa",
                table: "ZestawTreningowySlownikSet",
                newName: "IX_ZestawTreningowySlownikSet_SlownikNazwa");

            migrationBuilder.RenameIndex(
                name: "IX_WymiarWBadaniu_WymiarID",
                table: "WymiarWBadaniuSet",
                newName: "IX_WymiarWBadaniuSet_WymiarID");

            migrationBuilder.RenameIndex(
                name: "IX_WymiarWBadaniu_SkalaID",
                table: "WymiarWBadaniuSet",
                newName: "IX_WymiarWBadaniuSet_SkalaID");

            migrationBuilder.RenameIndex(
                name: "IX_WymiarWBadaniu_EksperymentIdentyfikator",
                table: "WymiarWBadaniuSet",
                newName: "IX_WymiarWBadaniuSet_EksperymentIdentyfikator");

            migrationBuilder.RenameIndex(
                name: "IX_PrzykladUzyc_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "PrzykladUzycSet",
                newName: "IX_PrzykladUzycSet_JednostkaLeksykalnaIdentyfikatorWordNet");

            migrationBuilder.RenameIndex(
                name: "IX_PrzedmiotBadania_EksperymentLokalnyIdentyfikator",
                table: "PrzedmiotBadaniaSet",
                newName: "IX_PrzedmiotBadaniaSet_EksperymentLokalnyIdentyfikator");

            migrationBuilder.RenameIndex(
                name: "IX_Pozycja_SkalaID",
                table: "PozycjaSet",
                newName: "IX_PozycjaSet_SkalaID");

            migrationBuilder.RenameIndex(
                name: "IX_Odpowiedz_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "OdpowiedzSet",
                newName: "IX_OdpowiedzSet_JednostkaLeksykalnaIdentyfikatorWordNet");

            migrationBuilder.RenameIndex(
                name: "IX_Odpowiedz_AnkietaNumer",
                table: "OdpowiedzSet",
                newName: "IX_OdpowiedzSet_AnkietaNumer");

            migrationBuilder.RenameIndex(
                name: "IX_Ocena_WybranaWartoscWartosc",
                table: "OcenaSet",
                newName: "IX_OcenaSet_WybranaWartoscWartosc");

            migrationBuilder.RenameIndex(
                name: "IX_JednostkaWSlowniku_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "JednostkaWSlownikuSet",
                newName: "IX_JednostkaWSlownikuSet_JednostkaLeksykalnaIdentyfikatorWordNet");

            migrationBuilder.RenameIndex(
                name: "IX_Eksperyment_WidokEmocjiID",
                table: "EksperymentSet",
                newName: "IX_EksperymentSet_WidokEmocjiID");

            migrationBuilder.RenameIndex(
                name: "IX_Eksperyment_TrybID",
                table: "EksperymentSet",
                newName: "IX_EksperymentSet_TrybID");

            migrationBuilder.RenameIndex(
                name: "IX_Eksperyment_SlownikNazwa",
                table: "EksperymentSet",
                newName: "IX_EksperymentSet_SlownikNazwa");

            migrationBuilder.RenameIndex(
                name: "IX_Eksperyment_SkalaEmocjiID",
                table: "EksperymentSet",
                newName: "IX_EksperymentSet_SkalaEmocjiID");

            migrationBuilder.RenameIndex(
                name: "IX_BadaneEmocjeEksperyment_EksperymentIdentyfikator",
                table: "BadaneEmocjeEksperymentSet",
                newName: "IX_BadaneEmocjeEksperymentSet_EksperymentIdentyfikator");

            migrationBuilder.RenameIndex(
                name: "IX_BadaneEmocjeEksperyment_BadaneEmocjeID",
                table: "BadaneEmocjeEksperymentSet",
                newName: "IX_BadaneEmocjeEksperymentSet_BadaneEmocjeID");

            migrationBuilder.RenameIndex(
                name: "IX_Ankieta_EksperymentIdentyfikator",
                table: "AnkietaSet",
                newName: "IX_AnkietaSet_EksperymentIdentyfikator");

            migrationBuilder.AddColumn<int>(
                name: "JednostkiID",
                table: "SlownikSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Opis",
                table: "PrzedmiotBadaniaSet",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OdpowiedzOcenaID",
                table: "PrzedmiotBadaniaSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrzedmiotBadaniaOcenaID",
                table: "OdpowiedzSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MaksymalnyCzasPojedynczejOdpowiedzi",
                table: "EksperymentSet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataZakonczenia",
                table: "EksperymentSet",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataRozpoczecia",
                table: "EksperymentSet",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TerminZakonczenia",
                table: "AnkietaSet",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZestawTreningowySlownikSet",
                table: "ZestawTreningowySlownikSet",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WymiarWBadaniuSet",
                table: "WymiarWBadaniuSet",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WidokSet",
                table: "WidokSet",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrybSet",
                table: "TrybSet",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SlownikSet",
                table: "SlownikSet",
                column: "Nazwa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkalaSet",
                table: "SkalaSet",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrzykladUzycSet",
                table: "PrzykladUzycSet",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrzedmiotBadaniaSet",
                table: "PrzedmiotBadaniaSet",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PozycjaSet",
                table: "PozycjaSet",
                column: "Wartosc");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OdpowiedzSet",
                table: "OdpowiedzSet",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OcenaSet",
                table: "OcenaSet",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JednostkaWSlownikuSet",
                table: "JednostkaWSlownikuSet",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JednostkaLeksykalnaSet",
                table: "JednostkaLeksykalnaSet",
                column: "IdentyfikatorWordNet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EksperymentSet",
                table: "EksperymentSet",
                column: "Identyfikator");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BadaneEmocjeEksperymentSet",
                table: "BadaneEmocjeEksperymentSet",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnkietaSet",
                table: "AnkietaSet",
                column: "Numer");

            migrationBuilder.CreateIndex(
                name: "IX_SlownikSet_JednostkiID",
                table: "SlownikSet",
                column: "JednostkiID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrzedmiotBadaniaSet_OdpowiedzOcenaID",
                table: "PrzedmiotBadaniaSet",
                column: "OdpowiedzOcenaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OdpowiedzSet_PrzedmiotBadaniaOcenaID",
                table: "OdpowiedzSet",
                column: "PrzedmiotBadaniaOcenaID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnkietaSet_EksperymentSet_EksperymentIdentyfikator",
                table: "AnkietaSet",
                column: "EksperymentIdentyfikator",
                principalTable: "EksperymentSet",
                principalColumn: "Identyfikator",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BadaneEmocjeEksperymentSet_PrzedmiotBadaniaSet_BadaneEmocjeID",
                table: "BadaneEmocjeEksperymentSet",
                column: "BadaneEmocjeID",
                principalTable: "PrzedmiotBadaniaSet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BadaneEmocjeEksperymentSet_EksperymentSet_EksperymentIdentyfikator",
                table: "BadaneEmocjeEksperymentSet",
                column: "EksperymentIdentyfikator",
                principalTable: "EksperymentSet",
                principalColumn: "Identyfikator",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EksperymentSet_SkalaSet_SkalaEmocjiID",
                table: "EksperymentSet",
                column: "SkalaEmocjiID",
                principalTable: "SkalaSet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EksperymentSet_SlownikSet_SlownikNazwa",
                table: "EksperymentSet",
                column: "SlownikNazwa",
                principalTable: "SlownikSet",
                principalColumn: "Nazwa",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EksperymentSet_TrybSet_TrybID",
                table: "EksperymentSet",
                column: "TrybID",
                principalTable: "TrybSet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EksperymentSet_WidokSet_WidokEmocjiID",
                table: "EksperymentSet",
                column: "WidokEmocjiID",
                principalTable: "WidokSet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JednostkaWSlownikuSet_JednostkaLeksykalnaSet_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "JednostkaWSlownikuSet",
                column: "JednostkaLeksykalnaIdentyfikatorWordNet",
                principalTable: "JednostkaLeksykalnaSet",
                principalColumn: "IdentyfikatorWordNet",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OcenaSet_PozycjaSet_WybranaWartoscWartosc",
                table: "OcenaSet",
                column: "WybranaWartoscWartosc",
                principalTable: "PozycjaSet",
                principalColumn: "Wartosc",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OdpowiedzSet_AnkietaSet_AnkietaNumer",
                table: "OdpowiedzSet",
                column: "AnkietaNumer",
                principalTable: "AnkietaSet",
                principalColumn: "Numer",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OdpowiedzSet_JednostkaLeksykalnaSet_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "OdpowiedzSet",
                column: "JednostkaLeksykalnaIdentyfikatorWordNet",
                principalTable: "JednostkaLeksykalnaSet",
                principalColumn: "IdentyfikatorWordNet",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OdpowiedzSet_OcenaSet_PrzedmiotBadaniaOcenaID",
                table: "OdpowiedzSet",
                column: "PrzedmiotBadaniaOcenaID",
                principalTable: "OcenaSet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PozycjaSet_SkalaSet_SkalaID",
                table: "PozycjaSet",
                column: "SkalaID",
                principalTable: "SkalaSet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrzedmiotBadaniaSet_EksperymentSet_EksperymentLokalnyIdentyfikator",
                table: "PrzedmiotBadaniaSet",
                column: "EksperymentLokalnyIdentyfikator",
                principalTable: "EksperymentSet",
                principalColumn: "Identyfikator",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrzedmiotBadaniaSet_OcenaSet_OdpowiedzOcenaID",
                table: "PrzedmiotBadaniaSet",
                column: "OdpowiedzOcenaID",
                principalTable: "OcenaSet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrzykladUzycSet_JednostkaLeksykalnaSet_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "PrzykladUzycSet",
                column: "JednostkaLeksykalnaIdentyfikatorWordNet",
                principalTable: "JednostkaLeksykalnaSet",
                principalColumn: "IdentyfikatorWordNet",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SlownikSet_JednostkaWSlownikuSet_JednostkiID",
                table: "SlownikSet",
                column: "JednostkiID",
                principalTable: "JednostkaWSlownikuSet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WymiarWBadaniuSet_EksperymentSet_EksperymentIdentyfikator",
                table: "WymiarWBadaniuSet",
                column: "EksperymentIdentyfikator",
                principalTable: "EksperymentSet",
                principalColumn: "Identyfikator",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WymiarWBadaniuSet_SkalaSet_SkalaID",
                table: "WymiarWBadaniuSet",
                column: "SkalaID",
                principalTable: "SkalaSet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WymiarWBadaniuSet_PrzedmiotBadaniaSet_WymiarID",
                table: "WymiarWBadaniuSet",
                column: "WymiarID",
                principalTable: "PrzedmiotBadaniaSet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZestawTreningowySlownikSet_SlownikSet_SlownikNazwa",
                table: "ZestawTreningowySlownikSet",
                column: "SlownikNazwa",
                principalTable: "SlownikSet",
                principalColumn: "Nazwa",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZestawTreningowySlownikSet_JednostkaLeksykalnaSet_ZestawTreningowyIdentyfikatorWordNet",
                table: "ZestawTreningowySlownikSet",
                column: "ZestawTreningowyIdentyfikatorWordNet",
                principalTable: "JednostkaLeksykalnaSet",
                principalColumn: "IdentyfikatorWordNet",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnkietaSet_EksperymentSet_EksperymentIdentyfikator",
                table: "AnkietaSet");

            migrationBuilder.DropForeignKey(
                name: "FK_BadaneEmocjeEksperymentSet_PrzedmiotBadaniaSet_BadaneEmocjeID",
                table: "BadaneEmocjeEksperymentSet");

            migrationBuilder.DropForeignKey(
                name: "FK_BadaneEmocjeEksperymentSet_EksperymentSet_EksperymentIdentyfikator",
                table: "BadaneEmocjeEksperymentSet");

            migrationBuilder.DropForeignKey(
                name: "FK_EksperymentSet_SkalaSet_SkalaEmocjiID",
                table: "EksperymentSet");

            migrationBuilder.DropForeignKey(
                name: "FK_EksperymentSet_SlownikSet_SlownikNazwa",
                table: "EksperymentSet");

            migrationBuilder.DropForeignKey(
                name: "FK_EksperymentSet_TrybSet_TrybID",
                table: "EksperymentSet");

            migrationBuilder.DropForeignKey(
                name: "FK_EksperymentSet_WidokSet_WidokEmocjiID",
                table: "EksperymentSet");

            migrationBuilder.DropForeignKey(
                name: "FK_JednostkaWSlownikuSet_JednostkaLeksykalnaSet_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "JednostkaWSlownikuSet");

            migrationBuilder.DropForeignKey(
                name: "FK_OcenaSet_PozycjaSet_WybranaWartoscWartosc",
                table: "OcenaSet");

            migrationBuilder.DropForeignKey(
                name: "FK_OdpowiedzSet_AnkietaSet_AnkietaNumer",
                table: "OdpowiedzSet");

            migrationBuilder.DropForeignKey(
                name: "FK_OdpowiedzSet_JednostkaLeksykalnaSet_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "OdpowiedzSet");

            migrationBuilder.DropForeignKey(
                name: "FK_OdpowiedzSet_OcenaSet_PrzedmiotBadaniaOcenaID",
                table: "OdpowiedzSet");

            migrationBuilder.DropForeignKey(
                name: "FK_PozycjaSet_SkalaSet_SkalaID",
                table: "PozycjaSet");

            migrationBuilder.DropForeignKey(
                name: "FK_PrzedmiotBadaniaSet_EksperymentSet_EksperymentLokalnyIdentyfikator",
                table: "PrzedmiotBadaniaSet");

            migrationBuilder.DropForeignKey(
                name: "FK_PrzedmiotBadaniaSet_OcenaSet_OdpowiedzOcenaID",
                table: "PrzedmiotBadaniaSet");

            migrationBuilder.DropForeignKey(
                name: "FK_PrzykladUzycSet_JednostkaLeksykalnaSet_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "PrzykladUzycSet");

            migrationBuilder.DropForeignKey(
                name: "FK_SlownikSet_JednostkaWSlownikuSet_JednostkiID",
                table: "SlownikSet");

            migrationBuilder.DropForeignKey(
                name: "FK_WymiarWBadaniuSet_EksperymentSet_EksperymentIdentyfikator",
                table: "WymiarWBadaniuSet");

            migrationBuilder.DropForeignKey(
                name: "FK_WymiarWBadaniuSet_SkalaSet_SkalaID",
                table: "WymiarWBadaniuSet");

            migrationBuilder.DropForeignKey(
                name: "FK_WymiarWBadaniuSet_PrzedmiotBadaniaSet_WymiarID",
                table: "WymiarWBadaniuSet");

            migrationBuilder.DropForeignKey(
                name: "FK_ZestawTreningowySlownikSet_SlownikSet_SlownikNazwa",
                table: "ZestawTreningowySlownikSet");

            migrationBuilder.DropForeignKey(
                name: "FK_ZestawTreningowySlownikSet_JednostkaLeksykalnaSet_ZestawTreningowyIdentyfikatorWordNet",
                table: "ZestawTreningowySlownikSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZestawTreningowySlownikSet",
                table: "ZestawTreningowySlownikSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WymiarWBadaniuSet",
                table: "WymiarWBadaniuSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WidokSet",
                table: "WidokSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrybSet",
                table: "TrybSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SlownikSet",
                table: "SlownikSet");

            migrationBuilder.DropIndex(
                name: "IX_SlownikSet_JednostkiID",
                table: "SlownikSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkalaSet",
                table: "SkalaSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrzykladUzycSet",
                table: "PrzykladUzycSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrzedmiotBadaniaSet",
                table: "PrzedmiotBadaniaSet");

            migrationBuilder.DropIndex(
                name: "IX_PrzedmiotBadaniaSet_OdpowiedzOcenaID",
                table: "PrzedmiotBadaniaSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PozycjaSet",
                table: "PozycjaSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OdpowiedzSet",
                table: "OdpowiedzSet");

            migrationBuilder.DropIndex(
                name: "IX_OdpowiedzSet_PrzedmiotBadaniaOcenaID",
                table: "OdpowiedzSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OcenaSet",
                table: "OcenaSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JednostkaWSlownikuSet",
                table: "JednostkaWSlownikuSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JednostkaLeksykalnaSet",
                table: "JednostkaLeksykalnaSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EksperymentSet",
                table: "EksperymentSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BadaneEmocjeEksperymentSet",
                table: "BadaneEmocjeEksperymentSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnkietaSet",
                table: "AnkietaSet");

            migrationBuilder.DropColumn(
                name: "JednostkiID",
                table: "SlownikSet");

            migrationBuilder.DropColumn(
                name: "OdpowiedzOcenaID",
                table: "PrzedmiotBadaniaSet");

            migrationBuilder.DropColumn(
                name: "PrzedmiotBadaniaOcenaID",
                table: "OdpowiedzSet");

            migrationBuilder.RenameTable(
                name: "ZestawTreningowySlownikSet",
                newName: "ZestawTreningowySlownik");

            migrationBuilder.RenameTable(
                name: "WymiarWBadaniuSet",
                newName: "WymiarWBadaniu");

            migrationBuilder.RenameTable(
                name: "WidokSet",
                newName: "Widok");

            migrationBuilder.RenameTable(
                name: "TrybSet",
                newName: "Tryb");

            migrationBuilder.RenameTable(
                name: "SlownikSet",
                newName: "Slownik");

            migrationBuilder.RenameTable(
                name: "SkalaSet",
                newName: "Skala");

            migrationBuilder.RenameTable(
                name: "PrzykladUzycSet",
                newName: "PrzykladUzyc");

            migrationBuilder.RenameTable(
                name: "PrzedmiotBadaniaSet",
                newName: "PrzedmiotBadania");

            migrationBuilder.RenameTable(
                name: "PozycjaSet",
                newName: "Pozycja");

            migrationBuilder.RenameTable(
                name: "OdpowiedzSet",
                newName: "Odpowiedz");

            migrationBuilder.RenameTable(
                name: "OcenaSet",
                newName: "Ocena");

            migrationBuilder.RenameTable(
                name: "JednostkaWSlownikuSet",
                newName: "JednostkaWSlowniku");

            migrationBuilder.RenameTable(
                name: "JednostkaLeksykalnaSet",
                newName: "JednostkaLeksykalna");

            migrationBuilder.RenameTable(
                name: "EksperymentSet",
                newName: "Eksperyment");

            migrationBuilder.RenameTable(
                name: "BadaneEmocjeEksperymentSet",
                newName: "BadaneEmocjeEksperyment");

            migrationBuilder.RenameTable(
                name: "AnkietaSet",
                newName: "Ankieta");

            migrationBuilder.RenameIndex(
                name: "IX_ZestawTreningowySlownikSet_ZestawTreningowyIdentyfikatorWordNet",
                table: "ZestawTreningowySlownik",
                newName: "IX_ZestawTreningowySlownik_ZestawTreningowyIdentyfikatorWordNet");

            migrationBuilder.RenameIndex(
                name: "IX_ZestawTreningowySlownikSet_SlownikNazwa",
                table: "ZestawTreningowySlownik",
                newName: "IX_ZestawTreningowySlownik_SlownikNazwa");

            migrationBuilder.RenameIndex(
                name: "IX_WymiarWBadaniuSet_WymiarID",
                table: "WymiarWBadaniu",
                newName: "IX_WymiarWBadaniu_WymiarID");

            migrationBuilder.RenameIndex(
                name: "IX_WymiarWBadaniuSet_SkalaID",
                table: "WymiarWBadaniu",
                newName: "IX_WymiarWBadaniu_SkalaID");

            migrationBuilder.RenameIndex(
                name: "IX_WymiarWBadaniuSet_EksperymentIdentyfikator",
                table: "WymiarWBadaniu",
                newName: "IX_WymiarWBadaniu_EksperymentIdentyfikator");

            migrationBuilder.RenameIndex(
                name: "IX_PrzykladUzycSet_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "PrzykladUzyc",
                newName: "IX_PrzykladUzyc_JednostkaLeksykalnaIdentyfikatorWordNet");

            migrationBuilder.RenameIndex(
                name: "IX_PrzedmiotBadaniaSet_EksperymentLokalnyIdentyfikator",
                table: "PrzedmiotBadania",
                newName: "IX_PrzedmiotBadania_EksperymentLokalnyIdentyfikator");

            migrationBuilder.RenameIndex(
                name: "IX_PozycjaSet_SkalaID",
                table: "Pozycja",
                newName: "IX_Pozycja_SkalaID");

            migrationBuilder.RenameIndex(
                name: "IX_OdpowiedzSet_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "Odpowiedz",
                newName: "IX_Odpowiedz_JednostkaLeksykalnaIdentyfikatorWordNet");

            migrationBuilder.RenameIndex(
                name: "IX_OdpowiedzSet_AnkietaNumer",
                table: "Odpowiedz",
                newName: "IX_Odpowiedz_AnkietaNumer");

            migrationBuilder.RenameIndex(
                name: "IX_OcenaSet_WybranaWartoscWartosc",
                table: "Ocena",
                newName: "IX_Ocena_WybranaWartoscWartosc");

            migrationBuilder.RenameIndex(
                name: "IX_JednostkaWSlownikuSet_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "JednostkaWSlowniku",
                newName: "IX_JednostkaWSlowniku_JednostkaLeksykalnaIdentyfikatorWordNet");

            migrationBuilder.RenameIndex(
                name: "IX_EksperymentSet_WidokEmocjiID",
                table: "Eksperyment",
                newName: "IX_Eksperyment_WidokEmocjiID");

            migrationBuilder.RenameIndex(
                name: "IX_EksperymentSet_TrybID",
                table: "Eksperyment",
                newName: "IX_Eksperyment_TrybID");

            migrationBuilder.RenameIndex(
                name: "IX_EksperymentSet_SlownikNazwa",
                table: "Eksperyment",
                newName: "IX_Eksperyment_SlownikNazwa");

            migrationBuilder.RenameIndex(
                name: "IX_EksperymentSet_SkalaEmocjiID",
                table: "Eksperyment",
                newName: "IX_Eksperyment_SkalaEmocjiID");

            migrationBuilder.RenameIndex(
                name: "IX_BadaneEmocjeEksperymentSet_EksperymentIdentyfikator",
                table: "BadaneEmocjeEksperyment",
                newName: "IX_BadaneEmocjeEksperyment_EksperymentIdentyfikator");

            migrationBuilder.RenameIndex(
                name: "IX_BadaneEmocjeEksperymentSet_BadaneEmocjeID",
                table: "BadaneEmocjeEksperyment",
                newName: "IX_BadaneEmocjeEksperyment_BadaneEmocjeID");

            migrationBuilder.RenameIndex(
                name: "IX_AnkietaSet_EksperymentIdentyfikator",
                table: "Ankieta",
                newName: "IX_Ankieta_EksperymentIdentyfikator");

            migrationBuilder.AlterColumn<string>(
                name: "Opis",
                table: "PrzedmiotBadania",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "OdpowiedzID",
                table: "Ocena",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrzedmiotBadaniaID",
                table: "Ocena",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SlownikNazwa",
                table: "JednostkaWSlowniku",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "MaksymalnyCzasPojedynczejOdpowiedzi",
                table: "Eksperyment",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataZakonczenia",
                table: "Eksperyment",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataRozpoczecia",
                table: "Eksperyment",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TerminZakonczenia",
                table: "Ankieta",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZestawTreningowySlownik",
                table: "ZestawTreningowySlownik",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WymiarWBadaniu",
                table: "WymiarWBadaniu",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Widok",
                table: "Widok",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tryb",
                table: "Tryb",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slownik",
                table: "Slownik",
                column: "Nazwa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skala",
                table: "Skala",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrzykladUzyc",
                table: "PrzykladUzyc",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrzedmiotBadania",
                table: "PrzedmiotBadania",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pozycja",
                table: "Pozycja",
                column: "Wartosc");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Odpowiedz",
                table: "Odpowiedz",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ocena",
                table: "Ocena",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JednostkaWSlowniku",
                table: "JednostkaWSlowniku",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JednostkaLeksykalna",
                table: "JednostkaLeksykalna",
                column: "IdentyfikatorWordNet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Eksperyment",
                table: "Eksperyment",
                column: "Identyfikator");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BadaneEmocjeEksperyment",
                table: "BadaneEmocjeEksperyment",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ankieta",
                table: "Ankieta",
                column: "Numer");

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
                name: "IX_JednostkaWSlowniku_SlownikNazwa",
                table: "JednostkaWSlowniku",
                column: "SlownikNazwa",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ankieta_Eksperyment_EksperymentIdentyfikator",
                table: "Ankieta",
                column: "EksperymentIdentyfikator",
                principalTable: "Eksperyment",
                principalColumn: "Identyfikator",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BadaneEmocjeEksperyment_PrzedmiotBadania_BadaneEmocjeID",
                table: "BadaneEmocjeEksperyment",
                column: "BadaneEmocjeID",
                principalTable: "PrzedmiotBadania",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BadaneEmocjeEksperyment_Eksperyment_EksperymentIdentyfikator",
                table: "BadaneEmocjeEksperyment",
                column: "EksperymentIdentyfikator",
                principalTable: "Eksperyment",
                principalColumn: "Identyfikator",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Eksperyment_Skala_SkalaEmocjiID",
                table: "Eksperyment",
                column: "SkalaEmocjiID",
                principalTable: "Skala",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Eksperyment_Slownik_SlownikNazwa",
                table: "Eksperyment",
                column: "SlownikNazwa",
                principalTable: "Slownik",
                principalColumn: "Nazwa",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Eksperyment_Tryb_TrybID",
                table: "Eksperyment",
                column: "TrybID",
                principalTable: "Tryb",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Eksperyment_Widok_WidokEmocjiID",
                table: "Eksperyment",
                column: "WidokEmocjiID",
                principalTable: "Widok",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JednostkaWSlowniku_JednostkaLeksykalna_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "JednostkaWSlowniku",
                column: "JednostkaLeksykalnaIdentyfikatorWordNet",
                principalTable: "JednostkaLeksykalna",
                principalColumn: "IdentyfikatorWordNet",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JednostkaWSlowniku_Slownik_SlownikNazwa",
                table: "JednostkaWSlowniku",
                column: "SlownikNazwa",
                principalTable: "Slownik",
                principalColumn: "Nazwa",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocena_Odpowiedz_OdpowiedzID",
                table: "Ocena",
                column: "OdpowiedzID",
                principalTable: "Odpowiedz",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocena_PrzedmiotBadania_PrzedmiotBadaniaID",
                table: "Ocena",
                column: "PrzedmiotBadaniaID",
                principalTable: "PrzedmiotBadania",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocena_Pozycja_WybranaWartoscWartosc",
                table: "Ocena",
                column: "WybranaWartoscWartosc",
                principalTable: "Pozycja",
                principalColumn: "Wartosc",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Odpowiedz_Ankieta_AnkietaNumer",
                table: "Odpowiedz",
                column: "AnkietaNumer",
                principalTable: "Ankieta",
                principalColumn: "Numer",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Odpowiedz_JednostkaLeksykalna_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "Odpowiedz",
                column: "JednostkaLeksykalnaIdentyfikatorWordNet",
                principalTable: "JednostkaLeksykalna",
                principalColumn: "IdentyfikatorWordNet",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pozycja_Skala_SkalaID",
                table: "Pozycja",
                column: "SkalaID",
                principalTable: "Skala",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrzedmiotBadania_Eksperyment_EksperymentLokalnyIdentyfikator",
                table: "PrzedmiotBadania",
                column: "EksperymentLokalnyIdentyfikator",
                principalTable: "Eksperyment",
                principalColumn: "Identyfikator",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrzykladUzyc_JednostkaLeksykalna_JednostkaLeksykalnaIdentyfikatorWordNet",
                table: "PrzykladUzyc",
                column: "JednostkaLeksykalnaIdentyfikatorWordNet",
                principalTable: "JednostkaLeksykalna",
                principalColumn: "IdentyfikatorWordNet",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WymiarWBadaniu_Eksperyment_EksperymentIdentyfikator",
                table: "WymiarWBadaniu",
                column: "EksperymentIdentyfikator",
                principalTable: "Eksperyment",
                principalColumn: "Identyfikator",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WymiarWBadaniu_Skala_SkalaID",
                table: "WymiarWBadaniu",
                column: "SkalaID",
                principalTable: "Skala",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WymiarWBadaniu_PrzedmiotBadania_WymiarID",
                table: "WymiarWBadaniu",
                column: "WymiarID",
                principalTable: "PrzedmiotBadania",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZestawTreningowySlownik_Slownik_SlownikNazwa",
                table: "ZestawTreningowySlownik",
                column: "SlownikNazwa",
                principalTable: "Slownik",
                principalColumn: "Nazwa",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ZestawTreningowySlownik_JednostkaLeksykalna_ZestawTreningowyIdentyfikatorWordNet",
                table: "ZestawTreningowySlownik",
                column: "ZestawTreningowyIdentyfikatorWordNet",
                principalTable: "JednostkaLeksykalna",
                principalColumn: "IdentyfikatorWordNet",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
