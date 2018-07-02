﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ng.Models;

namespace Ng.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180627091149_Refresh")]
    partial class Refresh
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ng.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Ng.Models.Excursion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Excursions");
                });

            modelBuilder.Entity("Ng.Models.ExcursionSight", b =>
                {
                    b.Property<int>("ExcursionId");

                    b.Property<int>("SightId");

                    b.HasKey("ExcursionId", "SightId");

                    b.HasIndex("SightId");

                    b.ToTable("ExcursionSights");
                });

            modelBuilder.Entity("Ng.Models.Sight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Sights");
                });

            modelBuilder.Entity("Ng.Models.Tour", b =>
                {
                    b.Property<int>("ExcursionId");

                    b.Property<int>("ClientId");

                    b.Property<string>("DateFormatted");

                    b.Property<string>("Name");

                    b.HasKey("ExcursionId", "ClientId");

                    b.HasIndex("ClientId");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("Ng.Models.ExcursionSight", b =>
                {
                    b.HasOne("Ng.Models.Excursion", "Excursion")
                        .WithMany("ExcursionSights")
                        .HasForeignKey("ExcursionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ng.Models.Sight", "Sight")
                        .WithMany("ExcursionSights")
                        .HasForeignKey("SightId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ng.Models.Tour", b =>
                {
                    b.HasOne("Ng.Models.Client")
                        .WithMany("Tours")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ng.Models.Excursion")
                        .WithMany("Tours")
                        .HasForeignKey("ExcursionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
