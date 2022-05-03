using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MJ_CAIS.IdentityServer.CAISAppCredentials.Entities;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MJ_CAIS.IdentityServer.CAISAppCredentials
{
    public partial class CaisDbContext : DbContext
    {
        public CaisDbContext()
        {
        }

        public CaisDbContext(DbContextOptions<CaisDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GRoles> GRoles { get; set; }
        public virtual DbSet<GUserRoles> GUserRoles { get; set; }
        public virtual DbSet<GUsers> GUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "MJ_CAIS");

            modelBuilder.Entity<GRoles>(entity =>
            {
                entity.ToTable("G_ROLES");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasColumnName("CODE")
                    .HasMaxLength(200);

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<GUserRoles>(entity =>
            {
                entity.ToTable("G_USER_ROLES");

                entity.HasIndex(e => e.RoleId)
                    .HasName("XIF2G_USER_ROLES");

                entity.HasIndex(e => e.UserId)
                    .HasName("XIF1G_USER_ROLES");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId)
                    .HasColumnName("ROLE_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("USER_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.GUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_G_USER_ROLES_G_ROLES");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_G_USER_ROLES_G_USERS");
            });

            modelBuilder.Entity<GUsers>(entity =>
            {
                entity.ToTable("G_USERS");

                entity.HasIndex(e => e.CsAuthorityId)
                    .HasName("XIF1G_USERS");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Active).HasColumnName("ACTIVE");

                entity.Property(e => e.CsAuthorityId)
                    .HasColumnName("CS_AUTHORITY_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Egn)
                    .HasColumnName("EGN")
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(200);

                entity.Property(e => e.Familyname)
                    .HasColumnName("FAMILYNAME")
                    .HasMaxLength(200);

                entity.Property(e => e.Firstname)
                    .HasColumnName("FIRSTNAME")
                    .HasMaxLength(200);

                entity.Property(e => e.Position).HasColumnName("POSITION");

                entity.Property(e => e.Surname)
                    .HasColumnName("SURNAME")
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
