using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class UMLToMVCConverterMigration_59d0e7a82dfb4aab8e5b51d9a07b810f : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Baby",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baby", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CarRadio",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Producer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRadio", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CompanyInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LineSegment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    X_X = table.Column<int>(nullable: true),
                    X_Y = table.Column<int>(nullable: true),
                    Y_X = table.Column<int>(nullable: true),
                    Y_Y = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineSegment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StatusWniosku",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusWniosku", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SteeringWheel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Perimeter = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteeringWheel", x => x.ID);
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

            migrationBuilder.CreateTable(
                name: "Writer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Writer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KnownWords",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BabyID = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnownWords", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KnownWords_Baby_BabyID",
                        column: x => x.BabyID,
                        principalTable: "Baby",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enterprise",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyInfoID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprise", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Enterprise_CompanyInfo_CompanyInfoID",
                        column: x => x.CompanyInfoID,
                        principalTable: "CompanyInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WithSingleIDProperty",
                columns: table => new
                {
                    MyIdentifier = table.Column<string>(nullable: false),
                    Another = table.Column<int>(nullable: true),
                    StatusID = table.Column<int>(nullable: false, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithSingleIDProperty", x => x.MyIdentifier);
                    table.ForeignKey(
                        name: "FK_WithSingleIDProperty_StatusWniosku_StatusID",
                        column: x => x.StatusID,
                        principalTable: "StatusWniosku",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Brand = table.Column<string>(nullable: false),
                    Model = table.Column<string>(nullable: false),
                    Version = table.Column<string>(nullable: false),
                    SteeringWheelID = table.Column<int>(nullable: true),
                    SuperRadioID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => new { x.Brand, x.Model, x.Version });
                    table.ForeignKey(
                        name: "FK_Car_SteeringWheel_SteeringWheelID",
                        column: x => x.SteeringWheelID,
                        principalTable: "SteeringWheel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Car_CarRadio_SuperRadioID",
                        column: x => x.SuperRadioID,
                        principalTable: "CarRadio",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    EnterpriseID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Wage = table.Column<double>(nullable: true),
                    WorkerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Worker_Enterprise_EnterpriseID",
                        column: x => x.EnterpriseID,
                        principalTable: "Enterprise",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Worker_Worker_WorkerID",
                        column: x => x.WorkerID,
                        principalTable: "Worker",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarBrand = table.Column<string>(nullable: false),
                    CarModel = table.Column<string>(nullable: false),
                    CarVersion = table.Column<string>(nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "Tire",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(nullable: true),
                    CarBrand = table.Column<string>(nullable: true),
                    CarModel = table.Column<string>(nullable: true),
                    CarVersion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tire", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tire_Car_CarBrand_CarModel_CarVersion",
                        columns: x => new { x.CarBrand, x.CarModel, x.CarVersion },
                        principalTable: "Car",
                        principalColumns: new[] { "Brand", "Model", "Version" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteNumber",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<int>(nullable: false),
                    WorkerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteNumber", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FavouriteNumber_Worker_WorkerID",
                        column: x => x.WorkerID,
                        principalTable: "Worker",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookWriter",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookID = table.Column<int>(nullable: true),
                    WriterID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookWriter", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookWriter_Writer_WriterID",
                        column: x => x.WriterID,
                        principalTable: "Writer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FavouriteBookID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Book_Professor_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Professor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorID",
                table: "Book",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_BookWriter_BookID",
                table: "BookWriter",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookWriter_WriterID",
                table: "BookWriter",
                column: "WriterID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Enterprise_CompanyInfoID",
                table: "Enterprise",
                column: "CompanyInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteNumber_WorkerID",
                table: "FavouriteNumber",
                column: "WorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_KnownWords_BabyID",
                table: "KnownWords",
                column: "BabyID");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_FavouriteBookID",
                table: "Professor",
                column: "FavouriteBookID");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_CarBrand_CarModel_CarVersion",
                table: "Seat",
                columns: new[] { "CarBrand", "CarModel", "CarVersion" });

            migrationBuilder.CreateIndex(
                name: "IX_Tire_CarBrand_CarModel_CarVersion",
                table: "Tire",
                columns: new[] { "CarBrand", "CarModel", "CarVersion" });

            migrationBuilder.CreateIndex(
                name: "IX_WithSingleIDProperty_StatusID",
                table: "WithSingleIDProperty",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_EnterpriseID",
                table: "Worker",
                column: "EnterpriseID");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_WorkerID",
                table: "Worker",
                column: "WorkerID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookWriter_Book_BookID",
                table: "BookWriter",
                column: "BookID",
                principalTable: "Book",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Book_FavouriteBookID",
                table: "Professor",
                column: "FavouriteBookID",
                principalTable: "Book",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Professor_AuthorID",
                table: "Book");

            migrationBuilder.DropTable(
                name: "BookWriter");

            migrationBuilder.DropTable(
                name: "FavouriteNumber");

            migrationBuilder.DropTable(
                name: "KnownWords");

            migrationBuilder.DropTable(
                name: "LineSegment");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "Tire");

            migrationBuilder.DropTable(
                name: "Wheel");

            migrationBuilder.DropTable(
                name: "WithSingleIDProperty");

            migrationBuilder.DropTable(
                name: "Writer");

            migrationBuilder.DropTable(
                name: "Worker");

            migrationBuilder.DropTable(
                name: "Baby");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "StatusWniosku");

            migrationBuilder.DropTable(
                name: "Enterprise");

            migrationBuilder.DropTable(
                name: "SteeringWheel");

            migrationBuilder.DropTable(
                name: "CarRadio");

            migrationBuilder.DropTable(
                name: "CompanyInfo");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
