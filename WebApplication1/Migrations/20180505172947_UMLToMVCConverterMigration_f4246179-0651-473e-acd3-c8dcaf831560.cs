using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class UMLToMVCConverterMigration_f42461790651473eacd3c8dcaf831560 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Point",
                table: "Point");

            migrationBuilder.RenameTable(
                name: "Point",
                newName: "LineSegment");

            migrationBuilder.RenameColumn(
                name: "Y",
                table: "LineSegment",
                newName: "Y_Y");

            migrationBuilder.RenameColumn(
                name: "X",
                table: "LineSegment",
                newName: "Y_X");

            migrationBuilder.AddColumn<int>(
                name: "X_X",
                table: "LineSegment",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "X_Y",
                table: "LineSegment",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LineSegment",
                table: "LineSegment",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LineSegment",
                table: "LineSegment");

            migrationBuilder.DropColumn(
                name: "X_X",
                table: "LineSegment");

            migrationBuilder.DropColumn(
                name: "X_Y",
                table: "LineSegment");

            migrationBuilder.RenameTable(
                name: "LineSegment",
                newName: "Point");

            migrationBuilder.RenameColumn(
                name: "Y_Y",
                table: "Point",
                newName: "Y");

            migrationBuilder.RenameColumn(
                name: "Y_X",
                table: "Point",
                newName: "X");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Point",
                table: "Point",
                column: "ID");
        }
    }
}
