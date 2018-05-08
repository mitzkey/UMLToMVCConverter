using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class UMLToMVCConverterMigration_4512228666fc4ed9b0bd81b8de7f5aeb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WithSingleIDProperty_StatusWniosku_StatusID",
                table: "WithSingleIDProperty");

            migrationBuilder.AlterColumn<int>(
                name: "StatusID",
                table: "WithSingleIDProperty",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WithSingleIDProperty_StatusWniosku_StatusID",
                table: "WithSingleIDProperty",
                column: "StatusID",
                principalTable: "StatusWniosku",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WithSingleIDProperty_StatusWniosku_StatusID",
                table: "WithSingleIDProperty");

            migrationBuilder.AlterColumn<int>(
                name: "StatusID",
                table: "WithSingleIDProperty",
                nullable: true,
                oldClrType: typeof(int),
                oldDefaultValueSql: "1");

            migrationBuilder.AddForeignKey(
                name: "FK_WithSingleIDProperty_StatusWniosku_StatusID",
                table: "WithSingleIDProperty",
                column: "StatusID",
                principalTable: "StatusWniosku",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
