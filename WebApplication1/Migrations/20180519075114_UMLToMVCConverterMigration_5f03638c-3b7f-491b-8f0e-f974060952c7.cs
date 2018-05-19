using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class UMLToMVCConverterMigration_5f03638c3b7f491b8f0ef974060952c7 : Migration
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
                name: "CarRadio",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarBrand = table.Column<string>(nullable: true),
                    CarModel = table.Column<string>(nullable: true),
                    CarVersion = table.Column<string>(nullable: true),
                    Producer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRadio", x => x.ID);
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
                });

            migrationBuilder.CreateTable(
                name: "SteeringWheel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarBrand = table.Column<string>(nullable: false),
                    CarModel = table.Column<string>(nullable: false),
                    CarVersion = table.Column<string>(nullable: false),
                    Perimeter = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteeringWheel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Brand = table.Column<string>(nullable: false),
                    Model = table.Column<string>(nullable: false),
                    Version = table.Column<string>(nullable: false),
                    CarRadioID = table.Column<int>(nullable: true),
                    SteeringWheelID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => new { x.Brand, x.Model, x.Version });
                    table.ForeignKey(
                        name: "FK_Car_CarRadio_CarRadioID",
                        column: x => x.CarRadioID,
                        principalTable: "CarRadio",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Car_SteeringWheel_SteeringWheelID",
                        column: x => x.SteeringWheelID,
                        principalTable: "SteeringWheel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarRadioID",
                table: "Car",
                column: "CarRadioID");

            migrationBuilder.CreateIndex(
                name: "IX_Car_SteeringWheelID",
                table: "Car",
                column: "SteeringWheelID");

            migrationBuilder.CreateIndex(
                name: "IX_CarRadio_CarBrand_CarModel_CarVersion",
                table: "CarRadio",
                columns: new[] { "CarBrand", "CarModel", "CarVersion" },
                unique: true,
                filter: "[CarBrand] IS NOT NULL AND [CarModel] IS NOT NULL AND [CarVersion] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteNumber_WorkerID",
                table: "FavouriteNumber",
                column: "WorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_KnownWords_BabyID",
                table: "KnownWords",
                column: "BabyID");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_CarBrand_CarModel_CarVersion",
                table: "Seat",
                columns: new[] { "CarBrand", "CarModel", "CarVersion" });

            migrationBuilder.CreateIndex(
                name: "IX_SteeringWheel_CarBrand_CarModel_CarVersion",
                table: "SteeringWheel",
                columns: new[] { "CarBrand", "CarModel", "CarVersion" },
                unique: true);

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
                name: "FK_CarRadio_Car_CarBrand_CarModel_CarVersion",
                table: "CarRadio",
                columns: new[] { "CarBrand", "CarModel", "CarVersion" },
                principalTable: "Car",
                principalColumns: new[] { "Brand", "Model", "Version" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Car_CarBrand_CarModel_CarVersion",
                table: "Seat",
                columns: new[] { "CarBrand", "CarModel", "CarVersion" },
                principalTable: "Car",
                principalColumns: new[] { "Brand", "Model", "Version" },
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Car_CarRadio_CarRadioID",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_SteeringWheel_SteeringWheelID",
                table: "Car");

            migrationBuilder.DropTable(
                name: "CompanyInfo");

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
                name: "Worker");

            migrationBuilder.DropTable(
                name: "Baby");

            migrationBuilder.DropTable(
                name: "StatusWniosku");

            migrationBuilder.DropTable(
                name: "Enterprise");

            migrationBuilder.DropTable(
                name: "CarRadio");

            migrationBuilder.DropTable(
                name: "SteeringWheel");

            migrationBuilder.DropTable(
                name: "Car");
        }
    }
}
