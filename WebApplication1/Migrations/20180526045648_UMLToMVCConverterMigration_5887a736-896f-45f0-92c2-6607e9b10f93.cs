using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class UMLToMVCConverterMigration_5887a736896f45f092c26607e9b10f93 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_SteeringWheel_SteeringWheelID",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarRadio_SuperRadioID",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_SteeringWheelID",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_SuperRadioID",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "SteeringWheelID",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "SuperRadioID",
                table: "Car");

            migrationBuilder.AddColumn<string>(
                name: "CarBrand",
                table: "SteeringWheel",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CarModel",
                table: "SteeringWheel",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CarVersion",
                table: "SteeringWheel",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RadiosCarBrand",
                table: "CarRadio",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RadiosCarModel",
                table: "CarRadio",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RadiosCarVersion",
                table: "CarRadio",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SteeringWheel_CarBrand_CarModel_CarVersion",
                table: "SteeringWheel",
                columns: new[] { "CarBrand", "CarModel", "CarVersion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarRadio_RadiosCarBrand_RadiosCarModel_RadiosCarVersion",
                table: "CarRadio",
                columns: new[] { "RadiosCarBrand", "RadiosCarModel", "RadiosCarVersion" },
                unique: true,
                filter: "[RadiosCarBrand] IS NOT NULL AND [RadiosCarModel] IS NOT NULL AND [RadiosCarVersion] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRadio_Car_RadiosCarBrand_RadiosCarModel_RadiosCarVersion",
                table: "CarRadio",
                columns: new[] { "RadiosCarBrand", "RadiosCarModel", "RadiosCarVersion" },
                principalTable: "Car",
                principalColumns: new[] { "Brand", "Model", "Version" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SteeringWheel_Car_CarBrand_CarModel_CarVersion",
                table: "SteeringWheel",
                columns: new[] { "CarBrand", "CarModel", "CarVersion" },
                principalTable: "Car",
                principalColumns: new[] { "Brand", "Model", "Version" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRadio_Car_RadiosCarBrand_RadiosCarModel_RadiosCarVersion",
                table: "CarRadio");

            migrationBuilder.DropForeignKey(
                name: "FK_SteeringWheel_Car_CarBrand_CarModel_CarVersion",
                table: "SteeringWheel");

            migrationBuilder.DropIndex(
                name: "IX_SteeringWheel_CarBrand_CarModel_CarVersion",
                table: "SteeringWheel");

            migrationBuilder.DropIndex(
                name: "IX_CarRadio_RadiosCarBrand_RadiosCarModel_RadiosCarVersion",
                table: "CarRadio");

            migrationBuilder.DropColumn(
                name: "CarBrand",
                table: "SteeringWheel");

            migrationBuilder.DropColumn(
                name: "CarModel",
                table: "SteeringWheel");

            migrationBuilder.DropColumn(
                name: "CarVersion",
                table: "SteeringWheel");

            migrationBuilder.DropColumn(
                name: "RadiosCarBrand",
                table: "CarRadio");

            migrationBuilder.DropColumn(
                name: "RadiosCarModel",
                table: "CarRadio");

            migrationBuilder.DropColumn(
                name: "RadiosCarVersion",
                table: "CarRadio");

            migrationBuilder.AddColumn<int>(
                name: "SteeringWheelID",
                table: "Car",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SuperRadioID",
                table: "Car",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Car_SteeringWheelID",
                table: "Car",
                column: "SteeringWheelID",
                unique: true,
                filter: "[SteeringWheelID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Car_SuperRadioID",
                table: "Car",
                column: "SuperRadioID",
                unique: true,
                filter: "[SuperRadioID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_SteeringWheel_SteeringWheelID",
                table: "Car",
                column: "SteeringWheelID",
                principalTable: "SteeringWheel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarRadio_SuperRadioID",
                table: "Car",
                column: "SuperRadioID",
                principalTable: "CarRadio",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
