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
    [Migration("20181109235814_Initial")]
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
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("blood_class_ids");

                    b.Property<int?>("donation_Id");

                    b.Property<DateTime>("donation_timestamp");

                    b.Property<DateTime>("recomendation_timestamp");

                    b.Property<int>("station_id");

                    b.Property<int>("status_id");

                    b.Property<bool?>("succeed");

                    b.Property<int>("vk_id");

                    b.HasKey("id");

                    b.HasIndex("id")
                        .IsUnique();

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("DonorSearchBackend.DAL.User", b =>
                {
                    b.Property<int?>("vk_id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("about_self");

                    b.Property<DateTime?>("bdate");

                    b.Property<int>("blood_class_ids");

                    b.Property<int?>("blood_type_id");

                    b.Property<bool?>("bone_marrow");

                    b.Property<bool?>("cant_to_be_donor");

                    b.Property<int?>("city_id");

                    b.Property<int?>("donor_pause_to");

                    b.Property<int>("ds_id");

                    b.Property<string>("first_name")
                        .IsRequired();

                    b.Property<int?>("gender");

                    b.Property<bool?>("has_registration");

                    b.Property<string>("last_name")
                        .IsRequired();

                    b.Property<string>("maiden_name");

                    b.Property<string>("second_name");

                    b.HasKey("vk_id");

                    b.HasIndex("ds_id")
                        .IsUnique();

                    b.HasIndex("vk_id")
                        .IsUnique();

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
