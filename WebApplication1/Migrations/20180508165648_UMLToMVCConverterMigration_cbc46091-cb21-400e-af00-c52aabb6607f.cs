using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class UMLToMVCConverterMigration_cbc46091cb21400eaf00c52aabb6607f : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusID",
                table: "WithSingleIDProperty",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WithSingleIDProperty_StatusID",
                table: "WithSingleIDProperty",
                column: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_WithSingleIDProperty_StatusWniosku_StatusID",
                table: "WithSingleIDProperty",
                column: "StatusID",
                principalTable: "StatusWniosku",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WithSingleIDProperty_StatusWniosku_StatusID",
                table: "WithSingleIDProperty");

            migrationBuilder.DropIndex(
                name: "IX_WithSingleIDProperty_StatusID",
                table: "WithSingleIDProperty");

            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "WithSingleIDProperty");
        }
    }
}
