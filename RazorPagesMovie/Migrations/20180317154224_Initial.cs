using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RazorPagesMovie.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BabySet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabySet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CompanyInfoSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInfoSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PointSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    X = table.Column<int>(nullable: true),
                    Y = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WorkerSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Wage = table.Column<double>(nullable: true),
                    WorkerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkerSet_WorkerSet_WorkerID",
                        column: x => x.WorkerID,
                        principalTable: "WorkerSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KnownWordsSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BabyID = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnownWordsSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KnownWordsSet_BabySet_BabyID",
                        column: x => x.BabyID,
                        principalTable: "BabySet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteNumberSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<int>(nullable: false),
                    WorkerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteNumberSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FavouriteNumberSet_WorkerSet_WorkerID",
                        column: x => x.WorkerID,
                        principalTable: "WorkerSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteNumberSet_WorkerID",
                table: "FavouriteNumberSet",
                column: "WorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_KnownWordsSet_BabyID",
                table: "KnownWordsSet",
                column: "BabyID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerSet_WorkerID",
                table: "WorkerSet",
                column: "WorkerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyInfoSet");

            migrationBuilder.DropTable(
                name: "FavouriteNumberSet");

            migrationBuilder.DropTable(
                name: "KnownWordsSet");

            migrationBuilder.DropTable(
                name: "PointSet");

            migrationBuilder.DropTable(
                name: "WorkerSet");

            migrationBuilder.DropTable(
                name: "BabySet");
        }
    }
}
