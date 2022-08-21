using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MJ_CAIS.WebPortal.External.LocalContext
{
    public partial class ExtUserDbContext : DbContext
    {
        public ExtUserDbContext()
        {
        }

        public ExtUserDbContext(DbContextOptions<ExtUserDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<LocalGUsersExt> GUsersExts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocalGUsersExt>(entity =>
            {
                entity.ToTable("G_USERS_EXT");

                entity.HasIndex(e => e.AdministrationId, "XIF1G_USERS_EXT");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Active)
                    .HasPrecision(1)
                    .IsRequired(false)
                    .HasColumnName("ACTIVE");

                entity.Property(e => e.AdministrationId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired(false)
                    .HasColumnName("ADMINISTRATION_ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsRequired(false)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .IsRequired(false)
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Egn)
                    .HasMaxLength(100)
                    .IsRequired(false)
                    .HasColumnName("EGN");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.IsAdmin)
                    .HasPrecision(1)
                    .IsRequired(false)
                    .HasColumnName("IS_ADMIN");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsRequired(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.UserName)
                    .HasMaxLength(200)
                    .IsRequired(false)
                    .HasColumnName("USER_NAME");

                entity.Property(e => e.NormalizedUserName)
                    .HasMaxLength(200)
                    .IsRequired(false)
                    .HasColumnName("NORMALIZED_USER_NAME");

                entity.Property(e => e.NormalizedEmail)
                    .HasMaxLength(200)
                    .IsRequired(false)
                    .HasColumnName("NORMALIZED_EMAIL");

                entity.Property(e => e.Position)
                    .IsRequired(false)
                    .HasColumnName("POSITION");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .IsRequired(false)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .IsRequired(false)
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .IsRequired(false)
                    .HasColumnName("VERSION");

                entity.Property(e => e.ConcurrencyStamp)
                    .IsConcurrencyToken()
                    .HasMaxLength(256)
                    .IsRequired(false)
                    .HasColumnName("CONCURRENCY_STAMP");

                entity.Property(e => e.RegCertSubject)
                    .HasMaxLength(200)
                    .IsRequired(false)
                    .HasColumnName("REG_CERT_SUBJECT");

                entity.Property(e => e.EmailConfirmed)
                    .HasPrecision(1)
                    .HasColumnName("EMAIL_CONFIRMED");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(256)
                    .IsRequired(false)
                    .HasColumnName("PASSWORD_HASH");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(256)
                    .IsRequired(false)
                    .HasColumnName("PHONE_NUMBER");

                entity.Property(e => e.PhoneNumberConfirmed)
                    .HasPrecision(1)
                    .HasColumnName("PHONE_NUMBER_CONFIRMED");

                entity.Property(e => e.TwoFactorEnabled)
                    .HasPrecision(1)
                    .HasColumnName("TWO_FACTOR_ENABLED");

                entity.Property(e => e.LockoutEnd)
                    .IsRequired(false)
                    .HasColumnName("LOCKOUT_END_DATE_UTC");

                entity.Property(e => e.LockoutEnabled)
                    .HasPrecision(1)
                    .HasColumnName("LOCKOUT_ENABLED");

                entity.Property(e => e.AccessFailedCount)
                    .HasPrecision(10)
                    .HasColumnName("ACCESS_FAILED_COUNT");

                entity.Property(e => e.SecurityStamp)
                    .HasMaxLength(256)
                    .IsRequired(false)
                    .HasColumnName("SECURITY_STAMP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
