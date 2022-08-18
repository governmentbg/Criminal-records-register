using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MJ_CAIS.IdentityServer.CAISExternalCredentials.Entities;

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

        public virtual DbSet<GExtAdministration> GExtAdministrations { get; set; }
        public virtual DbSet<GExtAdministrationUic> GExtAdministrationUics { get; set; }
        public virtual DbSet<GUsersExt> GUsersExts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GExtAdministration>(entity =>
            {
                entity.ToTable("G_EXT_ADMINISTRATIONS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Descr).HasColumnName("DESCR");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.Role)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ROLE");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
            });

            modelBuilder.Entity<GExtAdministrationUic>(entity =>
            {
                entity.ToTable("G_EXT_ADMINISTRATION_UICS");

                entity.HasIndex(e => new { e.ExtAdmId, e.Value }, "UK_EXT_ADM_UIC_EXT_ADM")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.ExtAdmId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EXT_ADM_ID");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("VALUE");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.ExtAdm)
                    .WithMany(p => p.GExtAdministrationUics)
                    .HasForeignKey(d => d.ExtAdmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_G_EXT_ADM_UICS_G_EXT_ADM");
            });

            modelBuilder.Entity<GUsersExt>(entity =>
            {
                entity.ToTable("G_USERS_EXT");

                entity.HasIndex(e => e.AdministrationId, "XIF1G_USERS_EXT");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Active)
                    .HasPrecision(1)
                    .HasColumnName("ACTIVE");

                entity.Property(e => e.AdministrationId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ADMINISTRATION_ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Egn)
                    .HasMaxLength(100)
                    .HasColumnName("EGN");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.IsAdmin)
                    .HasPrecision(1)
                    .HasColumnName("IS_ADMIN");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.Position).HasColumnName("POSITION");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.Property(e => e.RegCertSubject)
                    .HasMaxLength(200)
                    .HasColumnName("REG_CERT_SUBJECT");

                entity.HasOne(d => d.Administration)
                    .WithMany(p => p.GUsersExts)
                    .HasForeignKey(d => d.AdministrationId)
                    .HasConstraintName("FK_G_USERS_EXT_G_EXT_ADMINISTR");
            });

            modelBuilder.HasSequence("DOC_REG_COMMON_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
