using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class UMLToMVCConverterMigration_dcec36445f024d0593608eb4d9718f79 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnterpriseID",
                table: "Worker",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Enterprise",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprise", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Worker_EnterpriseID",
                table: "Worker",
                column: "EnterpriseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_Enterprise_EnterpriseID",
                table: "Worker",
                column: "EnterpriseID",
                principalTable: "Enterprise",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worker_Enterprise_EnterpriseID",
                table: "Worker");

            migrationBuilder.DropTable(
                name: "Enterprise");

            migrationBuilder.DropIndex(
                name: "IX_Worker_EnterpriseID",
                table: "Worker");

            migrationBuilder.DropColumn(
                name: "EnterpriseID",
                table: "Worker");
        }
    }
}
