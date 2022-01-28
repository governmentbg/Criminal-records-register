using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.DataAccess
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

        public virtual DbSet<BBullPersAlias> BBullPersAliases { get; set; } = null!;
        public virtual DbSet<BBulletin> BBulletins { get; set; } = null!;
        public virtual DbSet<BBulletinStatus> BBulletinStatuses { get; set; } = null!;
        public virtual DbSet<BCaseType> BCaseTypes { get; set; } = null!;
        public virtual DbSet<BDecision> BDecisions { get; set; } = null!;
        public virtual DbSet<BDecisionChType> BDecisionChTypes { get; set; } = null!;
        public virtual DbSet<BDecisionType> BDecisionTypes { get; set; } = null!;
        public virtual DbSet<BEcrisOffCategory> BEcrisOffCategories { get; set; } = null!;
        public virtual DbSet<BEcrisOffLvlPart> BEcrisOffLvlParts { get; set; } = null!;
        public virtual DbSet<BEcrisStanctCateg> BEcrisStanctCategs { get; set; } = null!;
        public virtual DbSet<BIdDocCategory> BIdDocCategories { get; set; } = null!;
        public virtual DbSet<BOffence> BOffences { get; set; } = null!;
        public virtual DbSet<BOffenceCategory> BOffenceCategories { get; set; } = null!;
        public virtual DbSet<BOffenceLvlCompletion> BOffenceLvlCompletions { get; set; } = null!;
        public virtual DbSet<BPersNationality> BPersNationalities { get; set; } = null!;
        public virtual DbSet<BSanctProbCategory> BSanctProbCategories { get; set; } = null!;
        public virtual DbSet<BSanctProbMeasure> BSanctProbMeasures { get; set; } = null!;
        public virtual DbSet<BSanction> BSanctions { get; set; } = null!;
        public virtual DbSet<BSanctionActivity> BSanctionActivities { get; set; } = null!;
        public virtual DbSet<BSanctionCategory> BSanctionCategories { get; set; } = null!;
        public virtual DbSet<GCity> GCities { get; set; } = null!;
        public virtual DbSet<GCountry> GCountries { get; set; } = null!;
        public virtual DbSet<GCountrySubdivision> GCountrySubdivisions { get; set; } = null!;
        public virtual DbSet<GCsAuthority> GCsAuthorities { get; set; } = null!;
        public virtual DbSet<GDecidingAuthority> GDecidingAuthorities { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("MJ_CAIS");

            modelBuilder.Entity<BBullPersAlias>(entity =>
            {
                entity.ToTable("B_BULL_PERS_ALIAS");

                entity.HasIndex(e => e.BulletinId, "XIF1B_BULL_PERS_ALIAS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.BulletinId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BULLETIN_ID");

                entity.Property(e => e.Familyname)
                    .HasMaxLength(200)
                    .HasColumnName("FAMILYNAME");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(200)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(200)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.Surname)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME");

                entity.Property(e => e.Type)
                    .HasMaxLength(200)
                    .HasColumnName("TYPE");

                entity.HasOne(d => d.Bulletin)
                    .WithMany(p => p.BBullPersAliases)
                    .HasForeignKey(d => d.BulletinId)
                    .HasConstraintName("FK_B_BULL_PERS_ALIAS_B_BULLETI");
            });

            modelBuilder.Entity<BBulletin>(entity =>
            {
                entity.ToTable("B_BULLETINS");

                entity.HasIndex(e => e.BirthCountryId, "XIF10B_BULLETINS");

                entity.HasIndex(e => e.IdDocCategoryId, "XIF15B_BULLETINS");

                entity.HasIndex(e => e.CsAuthorityId, "XIF17B_BULLETINS");

                entity.HasIndex(e => e.DecidingAuthId, "XIF2B_BULLETINS");

                entity.HasIndex(e => e.DecisionTypeId, "XIF3B_BULLETINS");

                entity.HasIndex(e => e.CaseTypeId, "XIF4B_BULLETINS");

                entity.HasIndex(e => e.BulletinAuthorityId, "XIF6B_BULLETINS");

                entity.HasIndex(e => e.StatusId, "XIF8B_BULLETINS");

                entity.HasIndex(e => e.BirthCityId, "XIF9B_BULLETINS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.AfisNumber)
                    .HasMaxLength(100)
                    .HasColumnName("AFIS_NUMBER");

                entity.Property(e => e.AlphabeticalIndex)
                    .HasMaxLength(100)
                    .HasColumnName("ALPHABETICAL_INDEX");

                entity.Property(e => e.ApprovedByNames).HasColumnName("APPROVED_BY_NAMES");

                entity.Property(e => e.ApprovedByPosition).HasColumnName("APPROVED_BY_POSITION");

                entity.Property(e => e.BirthCityId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BIRTH_CITY_ID");

                entity.Property(e => e.BirthCountryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BIRTH_COUNTRY_ID");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("DATE")
                    .HasColumnName("BIRTH_DATE");

                entity.Property(e => e.BirthDatePrecision)
                    .HasMaxLength(200)
                    .HasColumnName("BIRTH_DATE_PRECISION");

                entity.Property(e => e.BirthPlaceOther).HasColumnName("BIRTH_PLACE_OTHER");

                entity.Property(e => e.BulletinAuthorityId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BULLETIN_AUTHORITY_ID");

                entity.Property(e => e.BulletinCreateDate)
                    .HasColumnType("DATE")
                    .HasColumnName("BULLETIN_CREATE_DATE");

                entity.Property(e => e.BulletinReceivedDate)
                    .HasColumnType("DATE")
                    .HasColumnName("BULLETIN_RECEIVED_DATE");

                entity.Property(e => e.BulletinType)
                    .HasMaxLength(200)
                    .HasColumnName("BULLETIN_TYPE");

                entity.Property(e => e.CaseNumber)
                    .HasMaxLength(100)
                    .HasColumnName("CASE_NUMBER");

                entity.Property(e => e.CaseTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CASE_TYPE_ID");

                entity.Property(e => e.CaseYear)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CASE_YEAR");

                entity.Property(e => e.ConvIsTransmittable)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CONV_IS_TRANSMITTABLE");

                entity.Property(e => e.ConvRemarks).HasColumnName("CONV_REMARKS");

                entity.Property(e => e.ConvRetPeriodEndDate)
                    .HasColumnType("DATE")
                    .HasColumnName("CONV_RET_PERIOD_END_DATE");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedByNames).HasColumnName("CREATED_BY_NAMES");

                entity.Property(e => e.CreatedByPosition)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY_POSITION");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.CsAuthorityId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CS_AUTHORITY_ID");

                entity.Property(e => e.DecidingAuthId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DECIDING_AUTH_ID");

                entity.Property(e => e.DecisionDate)
                    .HasColumnType("DATE")
                    .HasColumnName("DECISION_DATE");

                entity.Property(e => e.DecisionEcli)
                    .HasMaxLength(100)
                    .HasColumnName("DECISION_ECLI");

                entity.Property(e => e.DecisionFinalDate)
                    .HasColumnType("DATE")
                    .HasColumnName("DECISION_FINAL_DATE");

                entity.Property(e => e.DecisionNumber)
                    .HasMaxLength(100)
                    .HasColumnName("DECISION_NUMBER");

                entity.Property(e => e.DecisionTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DECISION_TYPE_ID");

                entity.Property(e => e.Egn)
                    .HasMaxLength(100)
                    .HasColumnName("EGN");

                entity.Property(e => e.Familyname)
                    .HasMaxLength(200)
                    .HasColumnName("FAMILYNAME");

                entity.Property(e => e.FamilynameLat)
                    .HasMaxLength(200)
                    .HasColumnName("FAMILYNAME_LAT");

                entity.Property(e => e.FatherFamilyname)
                    .HasMaxLength(200)
                    .HasColumnName("FATHER_FAMILYNAME");

                entity.Property(e => e.FatherFirstname)
                    .HasMaxLength(200)
                    .HasColumnName("FATHER_FIRSTNAME");

                entity.Property(e => e.FatherFullname)
                    .HasMaxLength(200)
                    .HasColumnName("FATHER_FULLNAME");

                entity.Property(e => e.FatherSurname)
                    .HasMaxLength(200)
                    .HasColumnName("FATHER_SURNAME");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(200)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.FirstnameLat)
                    .HasMaxLength(200)
                    .HasColumnName("FIRSTNAME_LAT");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(200)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.FullnameLat)
                    .HasMaxLength(200)
                    .HasColumnName("FULLNAME_LAT");

                entity.Property(e => e.IdDocCategoryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_DOC_CATEGORY_ID");

                entity.Property(e => e.IdDocIssuingAuthority).HasColumnName("ID_DOC_ISSUING_AUTHORITY");

                entity.Property(e => e.IdDocIssuingDate)
                    .HasColumnType("DATE")
                    .HasColumnName("ID_DOC_ISSUING_DATE");

                entity.Property(e => e.IdDocIssuingDatePrec)
                    .HasMaxLength(200)
                    .HasColumnName("ID_DOC_ISSUING_DATE_PREC");

                entity.Property(e => e.IdDocNumber)
                    .HasMaxLength(100)
                    .HasColumnName("ID_DOC_NUMBER");

                entity.Property(e => e.IdDocTypeDescr).HasColumnName("ID_DOC_TYPE_DESCR");

                entity.Property(e => e.IdDocValidDate)
                    .HasColumnType("DATE")
                    .HasColumnName("ID_DOC_VALID_DATE");

                entity.Property(e => e.IdDocValidDatePrec)
                    .HasMaxLength(200)
                    .HasColumnName("ID_DOC_VALID_DATE_PREC");

                entity.Property(e => e.Ln)
                    .HasMaxLength(100)
                    .HasColumnName("LN");

                entity.Property(e => e.Lnch)
                    .HasMaxLength(100)
                    .HasColumnName("LNCH");

                entity.Property(e => e.MotherFamilyname)
                    .HasMaxLength(200)
                    .HasColumnName("MOTHER_FAMILYNAME");

                entity.Property(e => e.MotherFirstname)
                    .HasMaxLength(200)
                    .HasColumnName("MOTHER_FIRSTNAME");

                entity.Property(e => e.MotherFullname)
                    .HasMaxLength(200)
                    .HasColumnName("MOTHER_FULLNAME");

                entity.Property(e => e.MotherSurname)
                    .HasMaxLength(200)
                    .HasColumnName("MOTHER_SURNAME");

                entity.Property(e => e.RegistrationNumber)
                    .HasMaxLength(100)
                    .HasColumnName("REGISTRATION_NUMBER");

                entity.Property(e => e.SequentialIndex)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SEQUENTIAL_INDEX");

                entity.Property(e => e.Sex)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SEX");

                entity.Property(e => e.StatusId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS_ID");

                entity.Property(e => e.Surname)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME");

                entity.Property(e => e.SurnameLat)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME_LAT");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.BirthCity)
                    .WithMany(p => p.BBulletins)
                    .HasForeignKey(d => d.BirthCityId)
                    .HasConstraintName("FK_B_BULLETINS_G_CITIES");

                entity.HasOne(d => d.BirthCountry)
                    .WithMany(p => p.BBulletins)
                    .HasForeignKey(d => d.BirthCountryId)
                    .HasConstraintName("FK_B_BULLETINS_G_COUNTRIES");

                entity.HasOne(d => d.BulletinAuthority)
                    .WithMany(p => p.BBulletinBulletinAuthorities)
                    .HasForeignKey(d => d.BulletinAuthorityId)
                    .HasConstraintName("FK_B_BULLETINS_G_DEC_AUTH_BULL");

                entity.HasOne(d => d.CaseType)
                    .WithMany(p => p.BBulletins)
                    .HasForeignKey(d => d.CaseTypeId)
                    .HasConstraintName("FK_B_BULLETINS_B_CASE_TYPES");

                entity.HasOne(d => d.CsAuthority)
                    .WithMany(p => p.BBulletins)
                    .HasForeignKey(d => d.CsAuthorityId)
                    .HasConstraintName("FK_B_BULLETINS_G_CS_AUTHORITIE");

                entity.HasOne(d => d.DecidingAuth)
                    .WithMany(p => p.BBulletinDecidingAuths)
                    .HasForeignKey(d => d.DecidingAuthId)
                    .HasConstraintName("FK_B_BULLETINS_G_DECIDING_AUTH");

                entity.HasOne(d => d.DecisionType)
                    .WithMany(p => p.BBulletins)
                    .HasForeignKey(d => d.DecisionTypeId)
                    .HasConstraintName("FK_B_BULLETINS_B_DECISION_TYPE");

                entity.HasOne(d => d.IdDocCategory)
                    .WithMany(p => p.BBulletins)
                    .HasForeignKey(d => d.IdDocCategoryId)
                    .HasConstraintName("FK_B_BULLETINS_B_ID_DOC_CATEGO");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.BBulletins)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_B_BULLETINS_B_BULLETIN_STAT");
            });

            modelBuilder.Entity<BBulletinStatus>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("XPKB_BULLETIN_STATUSES");

                entity.ToTable("B_BULLETIN_STATUSES");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<BCaseType>(entity =>
            {
                entity.ToTable("B_CASE_TYPES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .HasColumnName("CODE");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EXTERNAL_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");
            });

            modelBuilder.Entity<BDecision>(entity =>
            {
                entity.ToTable("B_DECISIONS");

                entity.HasIndex(e => e.BulletinId, "XIF1B_DECISIONS");

                entity.HasIndex(e => e.DecisionChTypeId, "XIF2B_DECISIONS");

                entity.HasIndex(e => e.DecisionAuthId, "XIF3B_DECISIONS");

                entity.HasIndex(e => e.DecisionTypeId, "XIF4B_DECISIONS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.BulletinId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BULLETIN_ID");

                entity.Property(e => e.DecisionAuthId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DECISION_AUTH_ID");

                entity.Property(e => e.DecisionChTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DECISION_CH_TYPE_ID");

                entity.Property(e => e.DecisionDate)
                    .HasColumnType("DATE")
                    .HasColumnName("DECISION_DATE");

                entity.Property(e => e.DecisionEcli)
                    .HasMaxLength(100)
                    .HasColumnName("DECISION_ECLI");

                entity.Property(e => e.DecisionFinalDate)
                    .HasColumnType("DATE")
                    .HasColumnName("DECISION_FINAL_DATE");

                entity.Property(e => e.DecisionNumber)
                    .HasMaxLength(100)
                    .HasColumnName("DECISION_NUMBER");

                entity.Property(e => e.DecisionTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DECISION_TYPE_ID");

                entity.Property(e => e.Descr).HasColumnName("DESCR");

                entity.HasOne(d => d.Bulletin)
                    .WithMany(p => p.BDecisions)
                    .HasForeignKey(d => d.BulletinId)
                    .HasConstraintName("FK_B_DECISIONS_B_BULLETINS");

                entity.HasOne(d => d.DecisionAuth)
                    .WithMany(p => p.BDecisions)
                    .HasForeignKey(d => d.DecisionAuthId)
                    .HasConstraintName("FK_B_DECISIONS_G_DECIDING_AUTH");

                entity.HasOne(d => d.DecisionChType)
                    .WithMany(p => p.BDecisions)
                    .HasForeignKey(d => d.DecisionChTypeId)
                    .HasConstraintName("FK_B_DECISIONS_B_DECISION_CH_T");

                entity.HasOne(d => d.DecisionType)
                    .WithMany(p => p.BDecisions)
                    .HasForeignKey(d => d.DecisionTypeId)
                    .HasConstraintName("FK_B_DECISIONS_B_DECISION_TYPE");
            });

            modelBuilder.Entity<BDecisionChType>(entity =>
            {
                entity.ToTable("B_DECISION_CH_TYPES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Category)
                    .HasMaxLength(200)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.EcrisTechnId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_TECHN_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(500)
                    .HasColumnName("NAME_EN");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");
            });

            modelBuilder.Entity<BDecisionType>(entity =>
            {
                entity.ToTable("B_DECISION_TYPES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .HasColumnName("CODE");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EXTERNAL_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");
            });

            modelBuilder.Entity<BEcrisOffCategory>(entity =>
            {
                entity.ToTable("B_ECRIS_OFF_CATEGORIES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.CategoryIsOpen)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CATEGORY_IS_OPEN");

                entity.Property(e => e.EcrisTechnId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_TECHN_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(500)
                    .HasColumnName("NAME_EN");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");
            });

            modelBuilder.Entity<BEcrisOffLvlPart>(entity =>
            {
                entity.ToTable("B_ECRIS_OFF_LVL_PART");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Category)
                    .HasMaxLength(200)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.EcrisTechnId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_TECHN_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(500)
                    .HasColumnName("NAME_EN");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");
            });

            modelBuilder.Entity<BEcrisStanctCateg>(entity =>
            {
                entity.ToTable("B_ECRIS_STANCT_CATEG");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Category)
                    .HasMaxLength(200)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.EcrisTechnId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_TECHN_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(500)
                    .HasColumnName("NAME_EN");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");
            });

            modelBuilder.Entity<BIdDocCategory>(entity =>
            {
                entity.ToTable("B_ID_DOC_CATEGORIES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.EcrisTechnId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_TECHN_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(500)
                    .HasColumnName("NAME_EN");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");
            });

            modelBuilder.Entity<BOffence>(entity =>
            {
                entity.ToTable("B_OFFENCES");

                entity.HasIndex(e => e.BulletinId, "XIF1B_OFFENCES");

                entity.HasIndex(e => e.OffenceCatId, "XIF2B_OFFENCES");

                entity.HasIndex(e => e.EcrisOffCatId, "XIF3B_OFFENCES");

                entity.HasIndex(e => e.OffPlaceCountryId, "XIF4B_OFFENCES");

                entity.HasIndex(e => e.OffPlaceSubdivId, "XIF5B_OFFENCES");

                entity.HasIndex(e => e.OffPlaceCityId, "XIF6B_OFFENCES");

                entity.HasIndex(e => e.OffLvlComplId, "XIF7B_OFFENCES");

                entity.HasIndex(e => e.OffLvlPartId, "XIF8B_OFFENCES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.BulletinId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BULLETIN_ID");

                entity.Property(e => e.EcrisOffCatId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_OFF_CAT_ID");

                entity.Property(e => e.FormOfGuilt).HasColumnName("FORM_OF_GUILT");

                entity.Property(e => e.IsContiniuous)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("IS_CONTINIUOUS");

                entity.Property(e => e.LegalProvisions).HasColumnName("LEGAL_PROVISIONS");

                entity.Property(e => e.Occurrences)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("OCCURRENCES");

                entity.Property(e => e.OffEndDate)
                    .HasColumnType("DATE")
                    .HasColumnName("OFF_END_DATE");

                entity.Property(e => e.OffEndDatePrec)
                    .HasMaxLength(200)
                    .HasColumnName("OFF_END_DATE_PREC");

                entity.Property(e => e.OffLvlComplId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OFF_LVL_COMPL_ID");

                entity.Property(e => e.OffLvlPartId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OFF_LVL_PART_ID");

                entity.Property(e => e.OffPlaceCityId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OFF_PLACE_CITY_ID");

                entity.Property(e => e.OffPlaceCountryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OFF_PLACE_COUNTRY_ID");

                entity.Property(e => e.OffPlaceDescr).HasColumnName("OFF_PLACE_DESCR");

                entity.Property(e => e.OffPlaceSubdivId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OFF_PLACE_SUBDIV_ID");

                entity.Property(e => e.OffStartDate)
                    .HasColumnType("DATE")
                    .HasColumnName("OFF_START_DATE");

                entity.Property(e => e.OffStartDatePrec)
                    .HasMaxLength(200)
                    .HasColumnName("OFF_START_DATE_PREC");

                entity.Property(e => e.OffenceCatId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OFFENCE_CAT_ID");

                entity.Property(e => e.Recidivism)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("RECIDIVISM");

                entity.Property(e => e.Remarks).HasColumnName("REMARKS");

                entity.Property(e => e.RespExemption)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("RESP_EXEMPTION");

                entity.HasOne(d => d.Bulletin)
                    .WithMany(p => p.BOffences)
                    .HasForeignKey(d => d.BulletinId)
                    .HasConstraintName("FK_B_OFFENCES_B_BULLETINS");

                entity.HasOne(d => d.EcrisOffCat)
                    .WithMany(p => p.BOffences)
                    .HasForeignKey(d => d.EcrisOffCatId)
                    .HasConstraintName("FK_B_OFFENCES_B_ECRIS_OFF_CATE");

                entity.HasOne(d => d.OffLvlCompl)
                    .WithMany(p => p.BOffences)
                    .HasForeignKey(d => d.OffLvlComplId)
                    .HasConstraintName("FK_B_OFFENCES_B_OFFENCE_LVL_CO");

                entity.HasOne(d => d.OffLvlPart)
                    .WithMany(p => p.BOffences)
                    .HasForeignKey(d => d.OffLvlPartId)
                    .HasConstraintName("FK_B_OFFENCES_B_ECRIS_OFF_LVL_");

                entity.HasOne(d => d.OffPlaceCity)
                    .WithMany(p => p.BOffences)
                    .HasForeignKey(d => d.OffPlaceCityId)
                    .HasConstraintName("FK_B_OFFENCES_G_CITIES");

                entity.HasOne(d => d.OffPlaceCountry)
                    .WithMany(p => p.BOffences)
                    .HasForeignKey(d => d.OffPlaceCountryId)
                    .HasConstraintName("FK_B_OFFENCES_G_COUNTRIES");

                entity.HasOne(d => d.OffPlaceSubdiv)
                    .WithMany(p => p.BOffences)
                    .HasForeignKey(d => d.OffPlaceSubdivId)
                    .HasConstraintName("FK_B_OFFENCES_G_COUNTRY_SUBDIV");

                entity.HasOne(d => d.OffenceCat)
                    .WithMany(p => p.BOffences)
                    .HasForeignKey(d => d.OffenceCatId)
                    .HasConstraintName("FK_B_OFFENCES_B_OFFENCE_CATEGO");
            });

            modelBuilder.Entity<BOffenceCategory>(entity =>
            {
                entity.ToTable("B_OFFENCE_CATEGORIES");

                entity.HasIndex(e => e.ParentCatGroupId, "XIF1B_OFFENCE_CATEGORIES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .HasColumnName("CODE");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.OffLevel)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("OFF_LEVEL");

                entity.Property(e => e.ParentCatGroupId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PARENT_CAT_GROUP_ID");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.HasOne(d => d.ParentCatGroup)
                    .WithMany(p => p.InverseParentCatGroup)
                    .HasForeignKey(d => d.ParentCatGroupId)
                    .HasConstraintName("FK_B_OFFENCE_CATEGORIES_B_OFFE");
            });

            modelBuilder.Entity<BOffenceLvlCompletion>(entity =>
            {
                entity.ToTable("B_OFFENCE_LVL_COMPLETIONS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Category)
                    .HasMaxLength(200)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.EcrisTechnId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_TECHN_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(500)
                    .HasColumnName("NAME_EN");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");
            });

            modelBuilder.Entity<BPersNationality>(entity =>
            {
                entity.ToTable("B_PERS_NATIONALITIES");

                entity.HasIndex(e => e.CountryId, "XIF1B_PERS_NATIONALITIES");

                entity.HasIndex(e => e.BulletinId, "XIF2B_PERS_NATIONALITIES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.BulletinId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BULLETIN_ID");

                entity.Property(e => e.CountryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_ID");

                entity.HasOne(d => d.Bulletin)
                    .WithMany(p => p.BPersNationalities)
                    .HasForeignKey(d => d.BulletinId)
                    .HasConstraintName("FK_B_PERS_NATIONALITIES_B_BULL");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.BPersNationalities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_B_PERS_NATIONALITIES_G_COUN");
            });

            modelBuilder.Entity<BSanctProbCategory>(entity =>
            {
                entity.ToTable("B_SANCT_PROB_CATEGORIES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .HasColumnName("CODE");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EXTERNAL_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");
            });

            modelBuilder.Entity<BSanctProbMeasure>(entity =>
            {
                entity.ToTable("B_SANCT_PROB_MEASURES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .HasColumnName("CODE");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EXTERNAL_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");
            });

            modelBuilder.Entity<BSanction>(entity =>
            {
                entity.ToTable("B_SANCTIONS");

                entity.HasIndex(e => e.BulletinId, "XIF1B_SANCTIONS");

                entity.HasIndex(e => e.SanctCategoryId, "XIF2B_SANCTIONS");

                entity.HasIndex(e => e.EcrisSanctCategId, "XIF4B_SANCTIONS");

                entity.HasIndex(e => e.SanctProbCategId, "XIF6B_SANCTIONS");

                entity.HasIndex(e => e.SanctProbMeasureId, "XIF7B_SANCTIONS");

                entity.HasIndex(e => e.SanctActivityId, "XIF8B_SANCTIONS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.BulletinId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BULLETIN_ID");

                entity.Property(e => e.DecisionDurationDays)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DECISION_DURATION_DAYS");

                entity.Property(e => e.DecisionDurationHours)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DECISION_DURATION_HOURS");

                entity.Property(e => e.DecisionDurationMonths)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DECISION_DURATION_MONTHS");

                entity.Property(e => e.DecisionDurationYears)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DECISION_DURATION_YEARS");

                entity.Property(e => e.DecisionEndDate)
                    .HasColumnType("DATE")
                    .HasColumnName("DECISION_END_DATE");

                entity.Property(e => e.DecisionStartDate)
                    .HasColumnType("DATE")
                    .HasColumnName("DECISION_START_DATE");

                entity.Property(e => e.Descr).HasColumnName("DESCR");

                entity.Property(e => e.DetenctionDescr).HasColumnName("DETENCTION_DESCR");

                entity.Property(e => e.EcrisSanctCategId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_SANCT_CATEG_ID");

                entity.Property(e => e.ExecutionDurationDays)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("EXECUTION_DURATION_DAYS");

                entity.Property(e => e.ExecutionDurationHours)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("EXECUTION_DURATION_HOURS");

                entity.Property(e => e.ExecutionDurationMonths)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("EXECUTION_DURATION_MONTHS");

                entity.Property(e => e.ExecutionDurationYears)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("EXECUTION_DURATION_YEARS");

                entity.Property(e => e.ExecutionEndDate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXECUTION_END_DATE");

                entity.Property(e => e.ExecutionStartDate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXECUTION_START_DATE");

                entity.Property(e => e.FineAmount)
                    .HasColumnType("NUMBER(18,2)")
                    .HasColumnName("FINE_AMOUNT");

                entity.Property(e => e.ProbationDescr).HasColumnName("PROBATION_DESCR");

                entity.Property(e => e.SanctActivityDescr).HasColumnName("SANCT_ACTIVITY_DESCR");

                entity.Property(e => e.SanctActivityId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SANCT_ACTIVITY_ID");

                entity.Property(e => e.SanctCategoryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SANCT_CATEGORY_ID");

                entity.Property(e => e.SanctProbCategId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SANCT_PROB_CATEG_ID");

                entity.Property(e => e.SanctProbMeasureId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SANCT_PROB_MEASURE_ID");

                entity.Property(e => e.SanctProbValue)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SANCT_PROB_VALUE");

                entity.Property(e => e.SpecificToMinor)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SPECIFIC_TO_MINOR");

                entity.Property(e => e.SuspentionDurationDays)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SUSPENTION_DURATION_DAYS");

                entity.Property(e => e.SuspentionDurationHours)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SUSPENTION_DURATION_HOURS");

                entity.Property(e => e.SuspentionDurationMonths)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SUSPENTION_DURATION_MONTHS");

                entity.Property(e => e.SuspentionDurationYears)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SUSPENTION_DURATION_YEARS");

                entity.HasOne(d => d.Bulletin)
                    .WithMany(p => p.BSanctions)
                    .HasForeignKey(d => d.BulletinId)
                    .HasConstraintName("FK_B_SANCTIONS_B_BULLETINS");

                entity.HasOne(d => d.EcrisSanctCateg)
                    .WithMany(p => p.BSanctions)
                    .HasForeignKey(d => d.EcrisSanctCategId)
                    .HasConstraintName("FK_B_SANCTIONS_B_ECRIS_STANCT_");

                entity.HasOne(d => d.SanctActivity)
                    .WithMany(p => p.BSanctions)
                    .HasForeignKey(d => d.SanctActivityId)
                    .HasConstraintName("FK_B_SANCTIONS_B_SANCTION_ACTI");

                entity.HasOne(d => d.SanctCategory)
                    .WithMany(p => p.BSanctions)
                    .HasForeignKey(d => d.SanctCategoryId)
                    .HasConstraintName("FK_B_SANCTIONS_B_SANCTION_CATE");

                entity.HasOne(d => d.SanctProbCateg)
                    .WithMany(p => p.BSanctions)
                    .HasForeignKey(d => d.SanctProbCategId)
                    .HasConstraintName("FK_B_SANCTIONS_B_SANCT_PROB_CA");

                entity.HasOne(d => d.SanctProbMeasure)
                    .WithMany(p => p.BSanctions)
                    .HasForeignKey(d => d.SanctProbMeasureId)
                    .HasConstraintName("FK_B_SANCTIONS_B_SANCT_PROB_ME");
            });

            modelBuilder.Entity<BSanctionActivity>(entity =>
            {
                entity.ToTable("B_SANCTION_ACTIVITIES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .HasColumnName("CODE");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EXTERNAL_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");
            });

            modelBuilder.Entity<BSanctionCategory>(entity =>
            {
                entity.ToTable("B_SANCTION_CATEGORIES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .HasColumnName("CODE");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EXTERNAL_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");
            });

            modelBuilder.Entity<GCity>(entity =>
            {
                entity.ToTable("G_CITIES");

                entity.HasIndex(e => e.CountryId, "XIF1G_CITIES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.CountryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_ID");

                entity.Property(e => e.EcrisTechnId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_TECHN_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(200)
                    .HasColumnName("NAME_EN");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.GCities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_G_CITIES_G_COUNTRIES");
            });

            modelBuilder.Entity<GCountry>(entity =>
            {
                entity.ToTable("G_COUNTRIES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.EcrisTechnId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_TECHN_ID");

                entity.Property(e => e.Iso31662Code)
                    .HasMaxLength(100)
                    .HasColumnName("ISO_31662_CODE");

                entity.Property(e => e.Iso31662Number)
                    .HasMaxLength(100)
                    .HasColumnName("ISO_31662_NUMBER");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(200)
                    .HasColumnName("NAME_EN");

                entity.Property(e => e.Remark).HasColumnName("REMARK");

                entity.Property(e => e.UsedForNationality)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USED_FOR_NATIONALITY");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");
            });

            modelBuilder.Entity<GCountrySubdivision>(entity =>
            {
                entity.ToTable("G_COUNTRY_SUBDIVISIONS");

                entity.HasIndex(e => e.CountryId, "XIF1G_COUNTRY_SUBDIVISIONS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .HasColumnName("CODE");

                entity.Property(e => e.CountryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_ID");

                entity.Property(e => e.EcrisTechnId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_TECHN_ID");

                entity.Property(e => e.MemberState)
                    .HasMaxLength(200)
                    .HasColumnName("MEMBER_STATE");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(200)
                    .HasColumnName("NAME_EN");

                entity.Property(e => e.Type)
                    .HasMaxLength(200)
                    .HasColumnName("TYPE");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.GCountrySubdivisions)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_G_COUNTRY_SUBDIVISIONS_G_CO");
            });

            modelBuilder.Entity<GCsAuthority>(entity =>
            {
                entity.ToTable("G_CS_AUTHORITIES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .HasColumnName("CODE");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<GDecidingAuthority>(entity =>
            {
                entity.ToTable("G_DECIDING_AUTHORITIES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Active)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ACTIVE");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .HasColumnName("CODE");

                entity.Property(e => e.EisppCode)
                    .HasMaxLength(200)
                    .HasColumnName("EISPP_CODE");

                entity.Property(e => e.EisppId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("EISPP_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(200)
                    .HasColumnName("NAME_EN");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
