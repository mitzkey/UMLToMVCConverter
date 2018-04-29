using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class UMLToMVCConverterMigration_65498f6564db4c5ea47886e0f5de4e2c : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Wheel_WheelID",
                table: "Car");

            migrationBuilder.DropTable(
                name: "Wheel");

            migrationBuilder.RenameColumn(
                name: "WheelID",
                table: "Car",
                newName: "SteeringWheelID");

            migrationBuilder.RenameIndex(
                name: "IX_Car_WheelID",
                table: "Car",
                newName: "IX_Car_SteeringWheelID");

            migrationBuilder.CreateTable(
                name: "SteeringWheel",
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
                    table.PrimaryKey("PK_SteeringWheel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SteeringWheel_Car_CarBrand_CarModel_CarVersion",
                        columns: x => new { x.CarBrand, x.CarModel, x.CarVersion },
                        principalTable: "Car",
                        principalColumns: new[] { "Brand", "Model", "Version" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SteeringWheel_CarBrand_CarModel_CarVersion",
                table: "SteeringWheel",
                columns: new[] { "CarBrand", "CarModel", "CarVersion" },
                unique: true,
                filter: "[CarBrand] IS NOT NULL AND [CarModel] IS NOT NULL AND [CarVersion] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_SteeringWheel_SteeringWheelID",
                table: "Car",
                column: "SteeringWheelID",
                principalTable: "SteeringWheel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_SteeringWheel_SteeringWheelID",
                table: "Car");

            migrationBuilder.DropTable(
                name: "SteeringWheel");

            migrationBuilder.RenameColumn(
                name: "SteeringWheelID",
                table: "Car",
                newName: "WheelID");

            migrationBuilder.RenameIndex(
                name: "IX_Car_SteeringWheelID",
                table: "Car",
                newName: "IX_Car_WheelID");

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
    }
}
