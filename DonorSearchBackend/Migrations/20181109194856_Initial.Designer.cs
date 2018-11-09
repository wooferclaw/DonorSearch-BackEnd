﻿// <auto-generated />
using System;
using DonorSearchBackend.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DonorSearchBackend.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20181109194856_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("DonorSearchBackend.DAL.Donation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DonationId");

                    b.Property<DateTime>("DonationTimestamp");

                    b.Property<int>("DonationType");

                    b.Property<DateTime>("RecomendationTimestamp");

                    b.Property<int>("StationId");

                    b.Property<int>("StatusId");

                    b.Property<bool>("Succeed");

                    b.Property<int>("VkId");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("DonorSearchBackend.DAL.User", b =>
                {
                    b.Property<int>("VkId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DSId");

                    b.HasKey("VkId");

                    b.HasIndex("DSId")
                        .IsUnique();

                    b.HasIndex("VkId")
                        .IsUnique();

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
