using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class UMLToMVCConverterMigration_873cff2a9f72410bbb2776dc1b357fbd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Car_CarBrand1_CarModel1_CarVersion1",
                table: "Seat");

            migrationBuilder.DropForeignKey(
                name: "FK_Tire_Car_CarBrand1_CarModel1_CarVersion1",
                table: "Tire");

            migrationBuilder.DropIndex(
                name: "IX_Tire_CarBrand1_CarModel1_CarVersion1",
                table: "Tire");

            migrationBuilder.DropIndex(
                name: "IX_Seat_CarBrand1_CarModel1_CarVersion1",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "CarBrand1",
                table: "Tire");

            migrationBuilder.DropColumn(
                name: "CarModel1",
                table: "Tire");

            migrationBuilder.DropColumn(
                name: "CarVersion1",
                table: "Tire");

            migrationBuilder.DropColumn(
                name: "CarBrand1",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "CarModel1",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "CarVersion1",
                table: "Seat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarBrand1",
                table: "Tire",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarModel1",
                table: "Tire",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarVersion1",
                table: "Tire",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarBrand1",
                table: "Seat",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarModel1",
                table: "Seat",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarVersion1",
                table: "Seat",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tire_CarBrand1_CarModel1_CarVersion1",
                table: "Tire",
                columns: new[] { "CarBrand1", "CarModel1", "CarVersion1" });

            migrationBuilder.CreateIndex(
                name: "IX_Seat_CarBrand1_CarModel1_CarVersion1",
                table: "Seat",
                columns: new[] { "CarBrand1", "CarModel1", "CarVersion1" });

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Car_CarBrand1_CarModel1_CarVersion1",
                table: "Seat",
                columns: new[] { "CarBrand1", "CarModel1", "CarVersion1" },
                principalTable: "Car",
                principalColumns: new[] { "Brand", "Model", "Version" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tire_Car_CarBrand1_CarModel1_CarVersion1",
                table: "Tire",
                columns: new[] { "CarBrand1", "CarModel1", "CarVersion1" },
                principalTable: "Car",
                principalColumns: new[] { "Brand", "Model", "Version" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
