﻿// <auto-generated />
using System;
using EgzaminoProjektas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EgzaminoProjektas.Migrations
{
    [DbContext(typeof(prisondbContext))]
    [Migration("20221203181511_typeChange")]
    partial class typeChange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("EgzaminoProjektas.Models.City", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint unsigned")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "UK_l61tawv0e2a93es77jkyvi7qa")
                        .IsUnique();

                    b.ToTable("city", (string)null);
                });

            modelBuilder.Entity("EgzaminoProjektas.Models.Crime", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "UK_7nqvhquxytsdygvnmflj0rus4")
                        .IsUnique();

                    b.ToTable("crime", (string)null);
                });

            modelBuilder.Entity("EgzaminoProjektas.Models.Prisoner", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateOnly?>("BirthDate")
                        .IsRequired()
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<byte?>("CityId")
                        .IsRequired()
                        .HasColumnType("tinyint unsigned")
                        .HasColumnName("city_id");

                    b.Property<string>("FileName")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("file_name");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("phone");

                    b.Property<int?>("StatusId")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("status_id");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("surname");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CityId" }, "FK89sijll1b9th01bas7w3set65");

                    b.HasIndex(new[] { "StatusId" }, "FKauwfagsiahym1bypwcn2jvdkv");

                    b.ToTable("prisoner", (string)null);
                });

            modelBuilder.Entity("EgzaminoProjektas.Models.PrisonerCrime", b =>
                {
                    b.Property<long>("CrimeId")
                        .HasColumnType("bigint")
                        .HasColumnName("crime_id");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.Property<long>("PrisonerId")
                        .HasColumnType("bigint")
                        .HasColumnName("prisoner_id");

                    b.HasKey("CrimeId", "Date", "PrisonerId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                    b.HasIndex(new[] { "PrisonerId" }, "FK4iqdil681wxqpbheignlojjw8");

                    b.ToTable("prisonercrime", (string)null);
                });

            modelBuilder.Entity("EgzaminoProjektas.Models.PrisonerVisitor", b =>
                {
                    b.Property<long>("PrisonerId")
                        .HasColumnType("bigint")
                        .HasColumnName("prisoner_id");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("start_date");

                    b.Property<long>("VisitorId")
                        .HasColumnType("bigint")
                        .HasColumnName("visitor_id");

                    b.Property<DateTime>("FinishDate")
                        .HasMaxLength(255)
                        .HasColumnType("datetime(6)")
                        .HasColumnName("finish_date");

                    b.HasKey("PrisonerId", "StartDate", "VisitorId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                    b.HasIndex(new[] { "VisitorId" }, "FKdwf8yxhodvr4hk5tgbnwescfg");

                    b.ToTable("prisonervisitor", (string)null);
                });

            modelBuilder.Entity("EgzaminoProjektas.Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("longtext");

                    b.Property<string>("Role1")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Role1" }, "UK_g50w4r0ru3g9uf6i6fr4kpro8")
                        .IsUnique();

                    b.ToTable("role", (string)null);
                });

            modelBuilder.Entity("EgzaminoProjektas.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Status1")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("status", (string)null);
                });

            modelBuilder.Entity("EgzaminoProjektas.Models.Visitor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("name");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("surname");

                    b.HasKey("Id");

                    b.ToTable("visitor", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("IdentityUser");
                });

            modelBuilder.Entity("EgzaminoProjektas.Models.Prisoner", b =>
                {
                    b.HasOne("EgzaminoProjektas.Models.City", "City")
                        .WithMany("Prisoners")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK89sijll1b9th01bas7w3set65");

                    b.HasOne("EgzaminoProjektas.Models.Status", "Status")
                        .WithMany("Prisoners")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FKauwfagsiahym1bypwcn2jvdkv");

                    b.Navigation("City");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("EgzaminoProjektas.Models.PrisonerCrime", b =>
                {
                    b.HasOne("EgzaminoProjektas.Models.Crime", "Crime")
                        .WithMany("Prisonercrimes")
                        .HasForeignKey("CrimeId")
                        .IsRequired()
                        .HasConstraintName("FK7fml36v3yemlv5gaytucfm409");

                    b.HasOne("EgzaminoProjektas.Models.Prisoner", "Prisoner")
                        .WithMany("Prisonercrimes")
                        .HasForeignKey("PrisonerId")
                        .IsRequired()
                        .HasConstraintName("FK4iqdil681wxqpbheignlojjw8");

                    b.Navigation("Crime");

                    b.Navigation("Prisoner");
                });

            modelBuilder.Entity("EgzaminoProjektas.Models.PrisonerVisitor", b =>
                {
                    b.HasOne("EgzaminoProjektas.Models.Prisoner", "Prisoner")
                        .WithMany("Prisonervisitors")
                        .HasForeignKey("PrisonerId")
                        .IsRequired()
                        .HasConstraintName("FK5jbud1gdlhfoumfdcmx9tulsw");

                    b.HasOne("EgzaminoProjektas.Models.Visitor", "Visitor")
                        .WithMany("Prisonervisitors")
                        .HasForeignKey("VisitorId")
                        .IsRequired()
                        .HasConstraintName("FKdwf8yxhodvr4hk5tgbnwescfg");

                    b.Navigation("Prisoner");

                    b.Navigation("Visitor");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.HasOne("EgzaminoProjektas.Models.Role", null)
                        .WithMany("Users")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("EgzaminoProjektas.Models.City", b =>
                {
                    b.Navigation("Prisoners");
                });

            modelBuilder.Entity("EgzaminoProjektas.Models.Crime", b =>
                {
                    b.Navigation("Prisonercrimes");
                });

            modelBuilder.Entity("EgzaminoProjektas.Models.Prisoner", b =>
                {
                    b.Navigation("Prisonercrimes");

                    b.Navigation("Prisonervisitors");
                });

            modelBuilder.Entity("EgzaminoProjektas.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("EgzaminoProjektas.Models.Status", b =>
                {
                    b.Navigation("Prisoners");
                });

            modelBuilder.Entity("EgzaminoProjektas.Models.Visitor", b =>
                {
                    b.Navigation("Prisonervisitors");
                });
#pragma warning restore 612, 618
        }
    }
}
