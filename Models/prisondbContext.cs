using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EgzaminoProjektas.Models
{
    public partial class prisondbContext : DbContext
    {
        public prisondbContext()
        {
        }
        public prisondbContext(DbContextOptions<prisondbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Crime> Crimes { get; set; } = null!;
        public virtual DbSet<Prisoner> Prisoners { get; set; } = null!;
        public virtual DbSet<PrisonerCrime> Prisonercrimes { get; set; } = null!;
        public virtual DbSet<PrisonerVisitor> Prisonervisitors { get; set; } = null!;
        public virtual DbSet<Visitor> Visitors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = WebApplication.CreateBuilder();
                optionsBuilder.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.HasIndex(e => e.Name, "UK_l61tawv0e2a93es77jkyvi7qa")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Crime>(entity =>
            {
                entity.ToTable("crime");

                entity.HasIndex(e => e.Name, "UK_7nqvhquxytsdygvnmflj0rus4")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Prisoner>(entity =>
            {
                entity.ToTable("prisoner");

                entity.HasIndex(e => e.CityId, "FK89sijll1b9th01bas7w3set65");

                entity.HasIndex(e => e.StatusId, "FKauwfagsiahym1bypwcn2jvdkv");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BirthDate).HasColumnName("birth_date");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.FileName)
                    .HasMaxLength(255)
                    .HasColumnName("file_name");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Surname)
                    .HasMaxLength(20)
                    .HasColumnName("surname");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Prisoners)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK89sijll1b9th01bas7w3set65");
            });

            modelBuilder.Entity<PrisonerCrime>(entity =>
            {
                entity.HasKey(e => new { e.CrimeId, e.Date, e.PrisonerId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("prisonercrime");

                entity.HasIndex(e => e.PrisonerId, "FK4iqdil681wxqpbheignlojjw8");

                entity.Property(e => e.CrimeId).HasColumnName("crime_id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.PrisonerId).HasColumnName("prisoner_id");

                entity.HasOne(d => d.Crime)
                    .WithMany(p => p.Prisonercrimes)
                    .HasForeignKey(d => d.CrimeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK7fml36v3yemlv5gaytucfm409");

                entity.HasOne(d => d.Prisoner)
                    .WithMany(p => p.Prisonercrimes)
                    .HasForeignKey(d => d.PrisonerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK4iqdil681wxqpbheignlojjw8");
            });

            modelBuilder.Entity<PrisonerVisitor>(entity =>
            {
                entity.HasKey(e => new { e.PrisonerId, e.StartDate, e.VisitorId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("prisonervisitor");

                entity.HasIndex(e => e.VisitorId, "FKdwf8yxhodvr4hk5tgbnwescfg");

                entity.Property(e => e.PrisonerId).HasColumnName("prisoner_id");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.VisitorId).HasColumnName("visitor_id");

                entity.Property(e => e.FinishDate)
                    .HasMaxLength(255)
                    .HasColumnName("finish_date");

                entity.HasOne(d => d.Prisoner)
                    .WithMany(p => p.Prisonervisitors)
                    .HasForeignKey(d => d.PrisonerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK5jbud1gdlhfoumfdcmx9tulsw");

                entity.HasOne(d => d.Visitor)
                    .WithMany(p => p.Prisonervisitors)
                    .HasForeignKey(d => d.VisitorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKdwf8yxhodvr4hk5tgbnwescfg");
            });

            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.ToTable("visitor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BirthDate).HasColumnName("birth_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");

                entity.Property(e => e.Surname)
                    .HasMaxLength(20)
                    .HasColumnName("surname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
