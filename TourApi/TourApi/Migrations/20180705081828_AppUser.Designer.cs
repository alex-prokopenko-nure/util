﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TourApi.Models;

namespace TourApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180705081828_AppUser")]
    partial class AppUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TourApi.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TourApi.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("TourApi.Models.Excursion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Excursions");
                });

            modelBuilder.Entity("TourApi.Models.ExcursionSight", b =>
                {
                    b.Property<Guid>("ExcursionId");

                    b.Property<Guid>("SightId");

                    b.HasKey("ExcursionId", "SightId");

                    b.HasIndex("SightId");

                    b.ToTable("ExcursionSights");
                });

            modelBuilder.Entity("TourApi.Models.Sight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Sights");
                });

            modelBuilder.Entity("TourApi.Models.Tour", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClientId");

                    b.Property<DateTimeOffset>("Date");

                    b.Property<Guid>("ExcursionId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ExcursionId");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("TourApi.Models.ExcursionSight", b =>
                {
                    b.HasOne("TourApi.Models.Excursion")
                        .WithMany("ExcursionSights")
                        .HasForeignKey("ExcursionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TourApi.Models.Sight")
                        .WithMany("ExcursionSights")
                        .HasForeignKey("SightId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TourApi.Models.Tour", b =>
                {
                    b.HasOne("TourApi.Models.Client")
                        .WithMany("Tours")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TourApi.Models.Excursion")
                        .WithMany("Tours")
                        .HasForeignKey("ExcursionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
