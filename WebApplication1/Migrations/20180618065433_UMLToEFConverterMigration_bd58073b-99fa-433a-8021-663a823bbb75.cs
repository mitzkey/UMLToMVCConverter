using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class UMLToEFConverterMigration_bd58073b99fa433a8021663a823bbb75 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BabySet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabySet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CarSet",
                columns: table => new
                {
                    Brand = table.Column<string>(nullable: false),
                    Model = table.Column<string>(nullable: false),
                    Version = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarSet", x => new { x.Brand, x.Model, x.Version });
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
                name: "LineSegmentSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    A_X = table.Column<int>(nullable: false),
                    A_Y = table.Column<int>(nullable: false),
                    B_X = table.Column<int>(nullable: false),
                    B_Y = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineSegmentSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StatusWnioskuSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusWnioskuSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SteeringWheelSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Perimeter = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteeringWheelSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WheelSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WheelSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WriterSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WriterSet", x => x.ID);
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
                name: "CarRadioSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Producer = table.Column<string>(nullable: false),
                    RadiosCarBrand = table.Column<string>(nullable: false),
                    RadiosCarModel = table.Column<string>(nullable: false),
                    RadiosCarVersion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRadioSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CarRadioSet_CarSet_RadiosCarBrand_RadiosCarModel_RadiosCarVersion",
                        columns: x => new { x.RadiosCarBrand, x.RadiosCarModel, x.RadiosCarVersion },
                        principalTable: "CarSet",
                        principalColumns: new[] { "Brand", "Model", "Version" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarBrand = table.Column<string>(nullable: false),
                    CarModel = table.Column<string>(nullable: false),
                    CarVersion = table.Column<string>(nullable: false),
                    LeatherMade = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SeatSet_CarSet_CarBrand_CarModel_CarVersion",
                        columns: x => new { x.CarBrand, x.CarModel, x.CarVersion },
                        principalTable: "CarSet",
                        principalColumns: new[] { "Brand", "Model", "Version" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TireSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(nullable: false),
                    CarBrand = table.Column<string>(nullable: false),
                    CarModel = table.Column<string>(nullable: false),
                    CarVersion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TireSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TireSet_CarSet_CarBrand_CarModel_CarVersion",
                        columns: x => new { x.CarBrand, x.CarModel, x.CarVersion },
                        principalTable: "CarSet",
                        principalColumns: new[] { "Brand", "Model", "Version" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnterpriseSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyInfoID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterpriseSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EnterpriseSet_CompanyInfoSet_CompanyInfoID",
                        column: x => x.CompanyInfoID,
                        principalTable: "CompanyInfoSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WithSingleIDPropertySet",
                columns: table => new
                {
                    MyIdentifier = table.Column<string>(nullable: false),
                    Another = table.Column<int>(nullable: false),
                    StatusID = table.Column<int>(nullable: false, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithSingleIDPropertySet", x => x.MyIdentifier);
                    table.ForeignKey(
                        name: "FK_WithSingleIDPropertySet_StatusWnioskuSet_StatusID",
                        column: x => x.StatusID,
                        principalTable: "StatusWnioskuSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    EnterpriseID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Wage = table.Column<double>(nullable: false),
                    WorkerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkerSet_EnterpriseSet_EnterpriseID",
                        column: x => x.EnterpriseID,
                        principalTable: "EnterpriseSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkerSet_WorkerSet_WorkerID",
                        column: x => x.WorkerID,
                        principalTable: "WorkerSet",
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

            migrationBuilder.CreateTable(
                name: "BookWriterSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookID = table.Column<int>(nullable: true),
                    WriterID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookWriterSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookWriterSet_WriterSet_WriterID",
                        column: x => x.WriterID,
                        principalTable: "WriterSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FavouriteBookID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BookSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookSet_ProfessorSet_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "ProfessorSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookSet_AuthorID",
                table: "BookSet",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_BookWriterSet_BookID",
                table: "BookWriterSet",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookWriterSet_WriterID",
                table: "BookWriterSet",
                column: "WriterID");

            migrationBuilder.CreateIndex(
                name: "IX_CarRadioSet_RadiosCarBrand_RadiosCarModel_RadiosCarVersion",
                table: "CarRadioSet",
                columns: new[] { "RadiosCarBrand", "RadiosCarModel", "RadiosCarVersion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseSet_CompanyInfoID",
                table: "EnterpriseSet",
                column: "CompanyInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteNumberSet_WorkerID",
                table: "FavouriteNumberSet",
                column: "WorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_KnownWordsSet_BabyID",
                table: "KnownWordsSet",
                column: "BabyID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSet_FavouriteBookID",
                table: "ProfessorSet",
                column: "FavouriteBookID");

            migrationBuilder.CreateIndex(
                name: "IX_SeatSet_CarBrand_CarModel_CarVersion",
                table: "SeatSet",
                columns: new[] { "CarBrand", "CarModel", "CarVersion" });

            migrationBuilder.CreateIndex(
                name: "IX_TireSet_CarBrand_CarModel_CarVersion",
                table: "TireSet",
                columns: new[] { "CarBrand", "CarModel", "CarVersion" });

            migrationBuilder.CreateIndex(
                name: "IX_WithSingleIDPropertySet_StatusID",
                table: "WithSingleIDPropertySet",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerSet_EnterpriseID",
                table: "WorkerSet",
                column: "EnterpriseID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerSet_WorkerID",
                table: "WorkerSet",
                column: "WorkerID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookWriterSet_BookSet_BookID",
                table: "BookWriterSet",
                column: "BookID",
                principalTable: "BookSet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorSet_BookSet_FavouriteBookID",
                table: "ProfessorSet",
                column: "FavouriteBookID",
                principalTable: "BookSet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookSet_ProfessorSet_AuthorID",
                table: "BookSet");

            migrationBuilder.DropTable(
                name: "BookWriterSet");

            migrationBuilder.DropTable(
                name: "CarRadioSet");

            migrationBuilder.DropTable(
                name: "FavouriteNumberSet");

            migrationBuilder.DropTable(
                name: "KnownWordsSet");

            migrationBuilder.DropTable(
                name: "LineSegmentSet");

            migrationBuilder.DropTable(
                name: "SeatSet");

            migrationBuilder.DropTable(
                name: "SteeringWheelSet");

            migrationBuilder.DropTable(
                name: "TireSet");

            migrationBuilder.DropTable(
                name: "WheelSet");

            migrationBuilder.DropTable(
                name: "WithSingleIDPropertySet");

            migrationBuilder.DropTable(
                name: "WriterSet");

            migrationBuilder.DropTable(
                name: "WorkerSet");

            migrationBuilder.DropTable(
                name: "BabySet");

            migrationBuilder.DropTable(
                name: "CarSet");

            migrationBuilder.DropTable(
                name: "StatusWnioskuSet");

            migrationBuilder.DropTable(
                name: "EnterpriseSet");

            migrationBuilder.DropTable(
                name: "CompanyInfoSet");

            migrationBuilder.DropTable(
                name: "ProfessorSet");

            migrationBuilder.DropTable(
                name: "BookSet");
        }
    }
}
