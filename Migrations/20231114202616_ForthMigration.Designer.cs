﻿// <auto-generated />
using System;
using GoTravnikApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GoTravnikApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231114202616_ForthMigration")]
    partial class ForthMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GoTravnikApi.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("XCoordinate")
                        .HasColumnType("float");

                    b.Property<double>("YCoordinate")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Location", (string)null);
                });

            modelBuilder.Entity("GoTravnikApi.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TextComment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TouristContentId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TouristContentId");

                    b.ToTable("Rating", (string)null);
                });

            modelBuilder.Entity("GoTravnikApi.Models.Subcategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subcategory", (string)null);
                });

            modelBuilder.Entity("GoTravnikApi.Models.TouristContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("TouristContent");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SubcategoryTouristContent", b =>
                {
                    b.Property<int>("SubcategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("TouristContentsId")
                        .HasColumnType("int");

                    b.HasKey("SubcategoriesId", "TouristContentsId");

                    b.HasIndex("TouristContentsId");

                    b.ToTable("SubcategoryTouristContent");
                });

            modelBuilder.Entity("GoTravnikApi.Models.Accommodation", b =>
                {
                    b.HasBaseType("GoTravnikApi.Models.TouristContent");

                    b.Property<string>("TelephoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Accommodation", (string)null);
                });

            modelBuilder.Entity("GoTravnikApi.Models.Activity", b =>
                {
                    b.HasBaseType("GoTravnikApi.Models.TouristContent");

                    b.ToTable("Activity", (string)null);
                });

            modelBuilder.Entity("GoTravnikApi.Models.Attraction", b =>
                {
                    b.HasBaseType("GoTravnikApi.Models.TouristContent");

                    b.ToTable("Attraction", (string)null);
                });

            modelBuilder.Entity("GoTravnikApi.Models.Event", b =>
                {
                    b.HasBaseType("GoTravnikApi.Models.TouristContent");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("datetime2");

                    b.ToTable("Event", (string)null);
                });

            modelBuilder.Entity("GoTravnikApi.Models.FoodAndDrink", b =>
                {
                    b.HasBaseType("GoTravnikApi.Models.TouristContent");

                    b.Property<string>("TelephoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("FoodAndDrink", (string)null);
                });

            modelBuilder.Entity("GoTravnikApi.Models.Post", b =>
                {
                    b.HasBaseType("GoTravnikApi.Models.TouristContent");

                    b.Property<DateTime>("PostDate")
                        .HasColumnType("datetime2");

                    b.ToTable("Post", (string)null);
                });

            modelBuilder.Entity("GoTravnikApi.Models.Rating", b =>
                {
                    b.HasOne("GoTravnikApi.Models.TouristContent", null)
                        .WithMany("Ratings")
                        .HasForeignKey("TouristContentId");
                });

            modelBuilder.Entity("GoTravnikApi.Models.TouristContent", b =>
                {
                    b.HasOne("GoTravnikApi.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("SubcategoryTouristContent", b =>
                {
                    b.HasOne("GoTravnikApi.Models.Subcategory", null)
                        .WithMany()
                        .HasForeignKey("SubcategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoTravnikApi.Models.TouristContent", null)
                        .WithMany()
                        .HasForeignKey("TouristContentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GoTravnikApi.Models.Accommodation", b =>
                {
                    b.HasOne("GoTravnikApi.Models.TouristContent", null)
                        .WithOne()
                        .HasForeignKey("GoTravnikApi.Models.Accommodation", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GoTravnikApi.Models.Activity", b =>
                {
                    b.HasOne("GoTravnikApi.Models.TouristContent", null)
                        .WithOne()
                        .HasForeignKey("GoTravnikApi.Models.Activity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GoTravnikApi.Models.Attraction", b =>
                {
                    b.HasOne("GoTravnikApi.Models.TouristContent", null)
                        .WithOne()
                        .HasForeignKey("GoTravnikApi.Models.Attraction", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GoTravnikApi.Models.Event", b =>
                {
                    b.HasOne("GoTravnikApi.Models.TouristContent", null)
                        .WithOne()
                        .HasForeignKey("GoTravnikApi.Models.Event", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GoTravnikApi.Models.FoodAndDrink", b =>
                {
                    b.HasOne("GoTravnikApi.Models.TouristContent", null)
                        .WithOne()
                        .HasForeignKey("GoTravnikApi.Models.FoodAndDrink", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GoTravnikApi.Models.Post", b =>
                {
                    b.HasOne("GoTravnikApi.Models.TouristContent", null)
                        .WithOne()
                        .HasForeignKey("GoTravnikApi.Models.Post", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GoTravnikApi.Models.TouristContent", b =>
                {
                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}