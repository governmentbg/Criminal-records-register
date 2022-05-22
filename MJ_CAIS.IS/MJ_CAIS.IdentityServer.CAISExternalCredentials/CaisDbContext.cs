using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MJ_CAIS.IdentityServer.CAISExternalCredentials.Entities;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MJ_CAIS.IdentityServer.CAISExternalCredentials
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

        public virtual DbSet<GUsersExt> GUsersExt { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "MJ_CAIS");

            modelBuilder.Entity<GUsersExt>(entity =>
            {
                entity.ToTable("G_USERS_EXT");

                entity.HasIndex(e => e.AdministrationId)
                    .HasName("XIF1G_USERS_EXT");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Active).HasColumnName("ACTIVE");

                entity.Property(e => e.AdministrationId)
                    .HasColumnName("ADMINISTRATION_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("CREATED_BY")
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("CREATED_ON")
                    .HasColumnType("DATE");

                entity.Property(e => e.Egn)
                    .HasColumnName("EGN")
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(200);

                entity.Property(e => e.IsAdmin).HasColumnName("IS_ADMIN");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.Position).HasColumnName("POSITION");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("UPDATED_BY")
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("UPDATED_ON")
                    .HasColumnType("DATE");

                entity.Property(e => e.Version)
                    .HasColumnName("VERSION")
                    .HasColumnType("NUMBER(38)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
