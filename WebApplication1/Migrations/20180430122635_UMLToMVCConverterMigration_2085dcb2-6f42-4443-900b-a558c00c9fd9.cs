using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class UMLToMVCConverterMigration_2085dcb26f424443900ba558c00c9fd9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarBrand = table.Column<string>(nullable: false),
                    CarBrand1 = table.Column<string>(nullable: true),
                    CarModel = table.Column<string>(nullable: false),
                    CarModel1 = table.Column<string>(nullable: true),
                    CarVersion = table.Column<string>(nullable: false),
                    CarVersion1 = table.Column<string>(nullable: true),
                    LeatherMade = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Seat_Car_CarBrand_CarModel_CarVersion",
                        columns: x => new { x.CarBrand, x.CarModel, x.CarVersion },
                        principalTable: "Car",
                        principalColumns: new[] { "Brand", "Model", "Version" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seat_Car_CarBrand1_CarModel1_CarVersion1",
                        columns: x => new { x.CarBrand1, x.CarModel1, x.CarVersion1 },
                        principalTable: "Car",
                        principalColumns: new[] { "Brand", "Model", "Version" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tire",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(nullable: true),
                    CarBrand = table.Column<string>(nullable: true),
                    CarBrand1 = table.Column<string>(nullable: true),
                    CarModel = table.Column<string>(nullable: true),
                    CarModel1 = table.Column<string>(nullable: true),
                    CarVersion = table.Column<string>(nullable: true),
                    CarVersion1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tire", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tire_Car_CarBrand_CarModel_CarVersion",
                        columns: x => new { x.CarBrand, x.CarModel, x.CarVersion },
                        principalTable: "Car",
                        principalColumns: new[] { "Brand", "Model", "Version" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tire_Car_CarBrand1_CarModel1_CarVersion1",
                        columns: x => new { x.CarBrand1, x.CarModel1, x.CarVersion1 },
                        principalTable: "Car",
                        principalColumns: new[] { "Brand", "Model", "Version" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wheel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wheel", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seat_CarBrand_CarModel_CarVersion",
                table: "Seat",
                columns: new[] { "CarBrand", "CarModel", "CarVersion" });

            migrationBuilder.CreateIndex(
                name: "IX_Seat_CarBrand1_CarModel1_CarVersion1",
                table: "Seat",
                columns: new[] { "CarBrand1", "CarModel1", "CarVersion1" });

            migrationBuilder.CreateIndex(
                name: "IX_Tire_CarBrand_CarModel_CarVersion",
                table: "Tire",
                columns: new[] { "CarBrand", "CarModel", "CarVersion" });

            migrationBuilder.CreateIndex(
                name: "IX_Tire_CarBrand1_CarModel1_CarVersion1",
                table: "Tire",
                columns: new[] { "CarBrand1", "CarModel1", "CarVersion1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "Tire");

            migrationBuilder.DropTable(
                name: "Wheel");
        }
    }
}
