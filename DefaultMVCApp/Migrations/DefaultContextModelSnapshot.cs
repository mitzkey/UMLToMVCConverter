﻿// <auto-generated />
using Default.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DefaultMVCApp.Migrations
{
    [DbContext(typeof(DefaultContext))]
    partial class DefaultContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Default.Models.Baby", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("BabySet");
                });

            modelBuilder.Entity("Default.Models.CompanyInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ID");

                    b.ToTable("CompanyInfoSet");
                });

            modelBuilder.Entity("Default.Models.FavouriteNumber", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Value");

                    b.Property<int?>("WorkerID");

                    b.HasKey("ID");

                    b.HasIndex("WorkerID");

                    b.ToTable("FavouriteNumberSet");
                });

            modelBuilder.Entity("Default.Models.KnownWords", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BabyID");

                    b.Property<string>("Value");

                    b.HasKey("ID");

                    b.HasIndex("BabyID");

                    b.ToTable("KnownWordsSet");
                });

            modelBuilder.Entity("Default.Models.Point", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("X");

                    b.Property<int?>("Y");

                    b.HasKey("ID");

                    b.ToTable("PointSet");
                });

            modelBuilder.Entity("Default.Models.Worker", b =>
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

                    b.ToTable("WorkerSet");
                });

            modelBuilder.Entity("Default.Models.FavouriteNumber", b =>
                {
                    b.HasOne("Default.Models.Worker")
                        .WithMany("FavouriteNumber")
                        .HasForeignKey("WorkerID");
                });

            modelBuilder.Entity("Default.Models.KnownWords", b =>
                {
                    b.HasOne("Default.Models.Baby")
                        .WithMany("KnownWords")
                        .HasForeignKey("BabyID");
                });

            modelBuilder.Entity("Default.Models.Worker", b =>
                {
                    b.HasOne("Default.Models.Worker")
                        .WithMany("Coworkers")
                        .HasForeignKey("WorkerID");
                });
#pragma warning restore 612, 618
        }
    }
}