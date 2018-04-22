using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class UMLToMVCConverterMigration_1f3b3201421d47d896ef3735a36f4c75 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WheelID",
                table: "Car",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Wheel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarBrand = table.Column<string>(nullable: true),
                    CarModel = table.Column<string>(nullable: true),
                    CarVersion = table.Column<string>(nullable: true),
                    Perimeter = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wheel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Wheel_Car_CarBrand_CarModel_CarVersion",
                        columns: x => new { x.CarBrand, x.CarModel, x.CarVersion },
                        principalTable: "Car",
                        principalColumns: new[] { "Brand", "Model", "Version" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_WheelID",
                table: "Car",
                column: "WheelID");

            migrationBuilder.CreateIndex(
                name: "IX_Wheel_CarBrand_CarModel_CarVersion",
                table: "Wheel",
                columns: new[] { "CarBrand", "CarModel", "CarVersion" },
                unique: true,
                filter: "[CarBrand] IS NOT NULL AND [CarModel] IS NOT NULL AND [CarVersion] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Wheel_WheelID",
                table: "Car",
                column: "WheelID",
                principalTable: "Wheel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Wheel_WheelID",
                table: "Car");

            migrationBuilder.DropTable(
                name: "Wheel");

            migrationBuilder.DropIndex(
                name: "IX_Car_WheelID",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "WheelID",
                table: "Car");
        }
    }
}
