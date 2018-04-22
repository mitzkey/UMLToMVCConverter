// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WebApplication1.Models;

namespace WebApplication1.Migrations
{
    [DbContext(typeof(DefaultContext))]
    [Migration("20180422083252_UMLToMVCConverterMigration_eba09bad-9a34-422a-ba89-d4687aa39269")]
    partial class UMLToMVCConverterMigration_eba09bad9a34422aba89d4687aa39269
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication1.Models.Baby", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Baby");
                });

            modelBuilder.Entity("WebApplication1.Models.Car", b =>
                {
                    b.Property<string>("Brand");

                    b.Property<string>("Model");

                    b.Property<string>("Version");

                    b.HasKey("Brand", "Model", "Version");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("WebApplication1.Models.CompanyInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ID");

                    b.ToTable("CompanyInfo");
                });

            modelBuilder.Entity("WebApplication1.Models.FavouriteNumber", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("WorkerID");

                    b.HasKey("ID");

                    b.HasIndex("WorkerID");

                    b.ToTable("FavouriteNumber");
                });

            modelBuilder.Entity("WebApplication1.Models.KnownWords", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BabyID");

                    b.HasKey("ID");

                    b.HasIndex("BabyID");

                    b.ToTable("KnownWords");
                });

            modelBuilder.Entity("WebApplication1.Models.Point", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("X");

                    b.Property<int?>("Y");

                    b.HasKey("ID");

                    b.ToTable("Point");
                });

            modelBuilder.Entity("WebApplication1.Models.WithSingleIDProperty", b =>
                {
                    b.Property<string>("MyIdentifier")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Another");

                    b.HasKey("MyIdentifier");

                    b.ToTable("WithSingleIDProperty");
                });

            modelBuilder.Entity("WebApplication1.Models.Worker", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Company");

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Name");

                    b.Property<double?>("Wage");

                    b.Property<int?>("WorkerID");

                    b.HasKey("ID");

                    b.HasIndex("WorkerID");

                    b.ToTable("Worker");
                });

            modelBuilder.Entity("WebApplication1.Models.FavouriteNumber", b =>
                {
                    b.HasOne("WebApplication1.Models.Worker")
                        .WithMany("FavouriteNumber")
                        .HasForeignKey("WorkerID");
                });

            modelBuilder.Entity("WebApplication1.Models.KnownWords", b =>
                {
                    b.HasOne("WebApplication1.Models.Baby")
                        .WithMany("KnownWords")
                        .HasForeignKey("BabyID");
                });

            modelBuilder.Entity("WebApplication1.Models.Worker", b =>
                {
                    b.HasOne("WebApplication1.Models.Worker")
                        .WithMany("Coworkers")
                        .HasForeignKey("WorkerID");
                });
#pragma warning restore 612, 618
        }
    }
}