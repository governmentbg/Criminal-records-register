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
        public virtual DbSet<BInternalRequest> BInternalRequests { get; set; } = null!;
        public virtual DbSet<BOffence> BOffences { get; set; } = null!;
        public virtual DbSet<BOffenceCategory> BOffenceCategories { get; set; } = null!;
        public virtual DbSet<BOffenceLvlCompletion> BOffenceLvlCompletions { get; set; } = null!;
        public virtual DbSet<BPersNationality> BPersNationalities { get; set; } = null!;
        public virtual DbSet<BReqStatus> BReqStatuses { get; set; } = null!;
        public virtual DbSet<BSanctProbCategory> BSanctProbCategories { get; set; } = null!;
        public virtual DbSet<BSanctProbMeasure> BSanctProbMeasures { get; set; } = null!;
        public virtual DbSet<BSanction> BSanctions { get; set; } = null!;
        public virtual DbSet<BSanctionActivity> BSanctionActivities { get; set; } = null!;
        public virtual DbSet<BSanctionCategory> BSanctionCategories { get; set; } = null!;
        public virtual DbSet<DDocContent> DDocContents { get; set; } = null!;
        public virtual DbSet<DDocType> DDocTypes { get; set; } = null!;
        public virtual DbSet<DDocument> DDocuments { get; set; } = null!;
        public virtual DbSet<EEcrisAuthority> EEcrisAuthorities { get; set; } = null!;
        public virtual DbSet<EEcrisIdentification> EEcrisIdentifications { get; set; } = null!;
        public virtual DbSet<EEcrisMessage> EEcrisMessages { get; set; } = null!;
        public virtual DbSet<EEcrisMsgStatus> EEcrisMsgStatuses { get; set; } = null!;
        public virtual DbSet<Fbbc> Fbbcs { get; set; } = null!;
        public virtual DbSet<FbbcDocType> FbbcDocTypes { get; set; } = null!;
        public virtual DbSet<FbbcSanctType> FbbcSanctTypes { get; set; } = null!;
        public virtual DbSet<FbbcStatus> FbbcStatuses { get; set; } = null!;
        public virtual DbSet<GBgDistrict> GBgDistricts { get; set; } = null!;
        public virtual DbSet<GBgMunicipality> GBgMunicipalities { get; set; } = null!;
        public virtual DbSet<GCity> GCities { get; set; } = null!;
        public virtual DbSet<GCountry> GCountries { get; set; } = null!;
        public virtual DbSet<GCountrySubdivision> GCountrySubdivisions { get; set; } = null!;
        public virtual DbSet<GCsAuthority> GCsAuthorities { get; set; } = null!;
        public virtual DbSet<GDecidingAuthority> GDecidingAuthorities { get; set; } = null!;
        public virtual DbSet<GNomenclature> GNomenclatures { get; set; } = null!;
        public virtual DbSet<GRole> GRoles { get; set; } = null!;
        public virtual DbSet<GUser> GUsers { get; set; } = null!;
        public virtual DbSet<GUserRole> GUserRoles { get; set; } = null!;
        public virtual DbSet<GraoPerson> GraoPeople { get; set; } = null!;
        public virtual DbSet<PPersGroup> PPersGroups { get; set; } = null!;
        public virtual DbSet<PPerson> PPeople { get; set; } = null!;
        public virtual DbSet<ZImportFbbc> ZImportFbbcs { get; set; } = null!;

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

                entity.Property(e => e.DeleteDate)
                    .HasColumnType("DATE")
                    .HasColumnName("DELETE_DATE");

                entity.Property(e => e.EcrisConvictionId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_CONVICTION_ID");

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

                entity.Property(e => e.RehabilitationDate)
                    .HasColumnType("DATE")
                    .HasColumnName("REHABILITATION_DATE");

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

            modelBuilder.Entity<BInternalRequest>(entity =>
            {
                entity.ToTable("B_INTERNAL_REQUESTS");

                entity.HasIndex(e => e.BulletinId, "XIF1N_INTERNAL_REQUESTS");

                entity.HasIndex(e => e.ReqStatusCode, "XIF2N_INTERNAL_REQUESTS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.BulletinId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BULLETIN_ID");

                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

                entity.Property(e => e.RegNumber)
                    .HasMaxLength(100)
                    .HasColumnName("REG_NUMBER");

                entity.Property(e => e.ReqStatusCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("REQ_STATUS_CODE");

                entity.Property(e => e.RequestDate)
                    .HasColumnType("DATE")
                    .HasColumnName("REQUEST_DATE");

                entity.Property(e => e.ResponseDescr).HasColumnName("RESPONSE_DESCR");

                entity.HasOne(d => d.Bulletin)
                    .WithMany(p => p.BInternalRequests)
                    .HasForeignKey(d => d.BulletinId)
                    .HasConstraintName("FK_N_INTERNAL_REQUESTS_B_BULLE");

                entity.HasOne(d => d.ReqStatusCodeNavigation)
                    .WithMany(p => p.BInternalRequests)
                    .HasForeignKey(d => d.ReqStatusCode)
                    .HasConstraintName("FK_N_INTERNAL_REQUESTS_N_REQ_S");
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

                entity.Property(e => e.OrderNumber)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ORDER_NUMBER");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");
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

            modelBuilder.Entity<BReqStatus>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("XPKN_REQ_STATUSES");

                entity.ToTable("B_REQ_STATUSES");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");
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

            modelBuilder.Entity<DDocContent>(entity =>
            {
                entity.ToTable("D_DOC_CONTENTS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Bytes)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("BYTES");

                entity.Property(e => e.Content)
                    .HasColumnType("BLOB")
                    .HasColumnName("CONTENT");

                entity.Property(e => e.Md5Hash).HasColumnName("MD5_HASH");

                entity.Property(e => e.MimeType)
                    .HasMaxLength(200)
                    .HasColumnName("MIME_TYPE");

                entity.Property(e => e.Sha1Hash)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("SHA1_HASH")
                    .IsFixedLength();
            });

            modelBuilder.Entity<DDocType>(entity =>
            {
                entity.ToTable("D_DOC_TYPES");

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

                entity.Property(e => e.SystemCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SYSTEM_CODE");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Visible)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VISIBLE");

                entity.Property(e => e.Xslt)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("XSLT")
                    .IsFixedLength();
            });

            modelBuilder.Entity<DDocument>(entity =>
            {
                entity.ToTable("D_DOCUMENTS");

                entity.HasIndex(e => e.FbbcId, "XIF10D_DOCUMENTS");

                entity.HasIndex(e => e.EisppId, "XIF11D_DOCUMENTS");

                entity.HasIndex(e => e.DocContentId, "XIF12D_DOCUMENTS");

                entity.HasIndex(e => e.BulletinId, "XIF13D_DOCUMENTS");

                entity.HasIndex(e => e.DocTypeId, "XIF1D_DOCUMENTS");

                entity.HasIndex(e => e.ApplicationId, "XIF2D_DOCUMENTS");

                entity.HasIndex(e => e.PersonId, "XIF3D_DOCUMENTS");

                entity.HasIndex(e => e.EcrisMsgId, "XIF6D_DOCUMENTS");

                entity.HasIndex(e => e.IsinMsgId, "XIF7D_DOCUMENTS");

                entity.HasIndex(e => e.EissMsgId, "XIF8D_DOCUMENTS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.ApplicationId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("APPLICATION_ID");

                entity.Property(e => e.BulletinId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BULLETIN_ID");

                entity.Property(e => e.Descr).HasColumnName("DESCR");

                entity.Property(e => e.DocContentId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DOC_CONTENT_ID");

                entity.Property(e => e.DocTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DOC_TYPE_ID");

                entity.Property(e => e.EcrisMsgId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_MSG_ID");

                entity.Property(e => e.EisppId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EISPP_ID");

                entity.Property(e => e.EissMsgId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EISS_MSG_ID");

                entity.Property(e => e.FbbcId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FBBC_ID");

                entity.Property(e => e.IsinMsgId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ISIN_MSG_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.PersonId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PERSON_ID");

                entity.HasOne(d => d.Bulletin)
                    .WithMany(p => p.DDocuments)
                    .HasForeignKey(d => d.BulletinId)
                    .HasConstraintName("FK_D_DOCUMENTS_B_BULLETINS");

                entity.HasOne(d => d.DocContent)
                    .WithMany(p => p.DDocuments)
                    .HasForeignKey(d => d.DocContentId)
                    .HasConstraintName("FK_D_DOCUMENTS_D_DOC_CONTENTS");

                entity.HasOne(d => d.DocType)
                    .WithMany(p => p.DDocuments)
                    .HasForeignKey(d => d.DocTypeId)
                    .HasConstraintName("FK_D_DOCUMENTS_D_DOC_TYPES");

                entity.HasOne(d => d.EcrisMsg)
                    .WithMany(p => p.DDocuments)
                    .HasForeignKey(d => d.EcrisMsgId)
                    .HasConstraintName("FK_D_DOCUMENTS_E_ECRIS_MESSAGE");

                entity.HasOne(d => d.Fbbc)
                    .WithMany(p => p.DDocuments)
                    .HasForeignKey(d => d.FbbcId)
                    .HasConstraintName("FK_D_DOCUMENTS_FBBC");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.DDocuments)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_D_DOCUMENTS_P_PERSON");
            });

            modelBuilder.Entity<EEcrisAuthority>(entity =>
            {
                entity.ToTable("E_ECRIS_AUTHORITIES");

                entity.HasIndex(e => e.CountryId, "XIF1E_ECRIS_AUTHORITIES");

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

                entity.Property(e => e.Iso31662Number)
                    .HasMaxLength(100)
                    .HasColumnName("ISO_31662_NUMBER");

                entity.Property(e => e.MemberStateCode)
                    .HasMaxLength(100)
                    .HasColumnName("MEMBER_STATE_CODE");

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

            modelBuilder.Entity<EEcrisIdentification>(entity =>
            {
                entity.ToTable("E_ECRIS_IDENTIFICATION");

                entity.HasIndex(e => e.EcrisMsgId, "XIF1E_ECRIS_IDENTIFICATION");

                entity.HasIndex(e => e.PersonId, "XIF2E_ECRIS_IDENTIFICATION");

                entity.HasIndex(e => e.GraoPersonId, "XIF3E_ECRIS_IDENTIFICATION");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Approved)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("APPROVED");

                entity.Property(e => e.EcrisMsgId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_MSG_ID");

                entity.Property(e => e.GraoPersonId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("GRAO_PERSON_ID");

                entity.Property(e => e.PersonId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PERSON_ID");

                entity.HasOne(d => d.EcrisMsg)
                    .WithMany(p => p.EEcrisIdentifications)
                    .HasForeignKey(d => d.EcrisMsgId)
                    .HasConstraintName("FK_E_ECRIS_IDENTIFICATION_E_EC");

                entity.HasOne(d => d.GraoPerson)
                    .WithMany(p => p.EEcrisIdentifications)
                    .HasForeignKey(d => d.GraoPersonId)
                    .HasConstraintName("FK_E_ECRIS_IDENTIFICATION_GRAO");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.EEcrisIdentifications)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_E_ECRIS_IDENTIFICATION_P_PE");
            });

            modelBuilder.Entity<EEcrisMessage>(entity =>
            {
                entity.ToTable("E_ECRIS_MESSAGES");

                entity.HasIndex(e => e.FromAuthId, "XIF1E_ECRIS_MESSAGES");

                entity.HasIndex(e => e.ToAuthId, "XIF2E_ECRIS_MESSAGES");

                entity.HasIndex(e => e.ResponseTypeId, "XIF4E_ECRIS_MESSAGES");

                entity.HasIndex(e => e.RequestMsgId, "XIF5E_ECRIS_MESSAGES");

                entity.HasIndex(e => e.EcrisMsgStatus, "XIF6E_ECRIS_MESSAGES");

                entity.HasIndex(e => e.FbbcId, "XIF7E_ECRIS_MESSAGES");

                entity.HasIndex(e => e.MsgTypeId, "XIF8E_ECRIS_MESSAGES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.BirthCity).HasColumnName("BIRTH_CITY");

                entity.Property(e => e.BirthCountry).HasColumnName("BIRTH_COUNTRY");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("DATE")
                    .HasColumnName("BIRTH_DATE");

                entity.Property(e => e.EcrisIdentifier)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_IDENTIFIER");

                entity.Property(e => e.EcrisMsgStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_MSG_STATUS");

                entity.Property(e => e.Familyname)
                    .HasMaxLength(200)
                    .HasColumnName("FAMILYNAME");

                entity.Property(e => e.FbbcId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FBBC_ID");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(200)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.FromAuthId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FROM_AUTH_ID");

                entity.Property(e => e.Identifier)
                    .HasMaxLength(100)
                    .HasColumnName("IDENTIFIER");

                entity.Property(e => e.MsgTimestamp)
                    .HasColumnType("DATE")
                    .HasColumnName("MSG_TIMESTAMP");

                entity.Property(e => e.MsgTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MSG_TYPE_ID");

                entity.Property(e => e.Nationality1Code)
                    .HasMaxLength(200)
                    .HasColumnName("NATIONALITY1_CODE");

                entity.Property(e => e.Nationality2Code)
                    .HasMaxLength(200)
                    .HasColumnName("NATIONALITY2_CODE");

                entity.Property(e => e.RequestMsgId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("REQUEST_MSG_ID");

                entity.Property(e => e.ResponseTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RESPONSE_TYPE_ID");

                entity.Property(e => e.Sex)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SEX");

                entity.Property(e => e.Surname)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME");

                entity.Property(e => e.ToAuthId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TO_AUTH_ID");

                entity.HasOne(d => d.EcrisMsgStatusNavigation)
                    .WithMany(p => p.EEcrisMessages)
                    .HasForeignKey(d => d.EcrisMsgStatus)
                    .HasConstraintName("FK_E_ECRIS_MESSAGES_E_MSG_STAT");

                entity.HasOne(d => d.FromAuth)
                    .WithMany(p => p.EEcrisMessageFromAuths)
                    .HasForeignKey(d => d.FromAuthId)
                    .HasConstraintName("FK_E_ECRIS_MESSAGES_AUTH_FROM");

                entity.HasOne(d => d.RequestMsg)
                    .WithMany(p => p.InverseRequestMsg)
                    .HasForeignKey(d => d.RequestMsgId)
                    .HasConstraintName("FK_E_ECRIS_MESSAGES_E_REQ_MS");

                entity.HasOne(d => d.ToAuth)
                    .WithMany(p => p.EEcrisMessageToAuths)
                    .HasForeignKey(d => d.ToAuthId)
                    .HasConstraintName("FK_E_ECRIS_MESSAGES_AUTH_TO");
            });

            modelBuilder.Entity<EEcrisMsgStatus>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("XPKE_ECRIS_MSG_STATUSES");

                entity.ToTable("E_ECRIS_MSG_STATUSES");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Fbbc>(entity =>
            {
                entity.ToTable("FBBC");

                entity.HasIndex(e => e.CountryId, "XIF1FBBC");

                entity.HasIndex(e => e.DocTypeId, "XIF2FBBC");

                entity.HasIndex(e => e.SanctionTypeId, "XIF3FBBC");

                entity.HasIndex(e => e.BirthCityId, "XIF4FBBC");

                entity.HasIndex(e => e.BirthCountryId, "XIF5FBBC");

                entity.HasIndex(e => e.PersonId, "XIF7FBBC");

                entity.HasIndex(e => e.StatusCode, "XIF8FBBC");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Annotation)
                    .HasColumnType("CLOB")
                    .HasColumnName("ANNOTATION");

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

                entity.Property(e => e.BirthDatePrec)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BIRTH_DATE_PREC");

                entity.Property(e => e.BirthPlace)
                    .HasMaxLength(200)
                    .HasColumnName("BIRTH_PLACE");

                entity.Property(e => e.BirtyCountryDescr).HasColumnName("BIRTY_COUNTRY_DESCR");

                entity.Property(e => e.ConvDecFinalDate)
                    .HasColumnType("DATE")
                    .HasColumnName("CONV_DEC_FINAL_DATE");

                entity.Property(e => e.ConvDecisionDate)
                    .HasColumnType("DATE")
                    .HasColumnName("CONV_DECISION_DATE");

                entity.Property(e => e.CountryDescr).HasColumnName("COUNTRY_DESCR");

                entity.Property(e => e.CountryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.DestroyedDate)
                    .HasColumnType("DATE")
                    .HasColumnName("DESTROYED_DATE");

                entity.Property(e => e.DocTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DOC_TYPE_ID");

                entity.Property(e => e.EcrisConvId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_CONV_ID");

                entity.Property(e => e.EcrisUpdConvId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_UPD_CONV_ID");

                entity.Property(e => e.EcrisUpdConvTypeId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_UPD_CONV_TYPE_ID");

                entity.Property(e => e.Egn)
                    .HasMaxLength(100)
                    .HasColumnName("EGN");

                entity.Property(e => e.Familyname)
                    .HasMaxLength(200)
                    .HasColumnName("FAMILYNAME");

                entity.Property(e => e.FatherFamilyname)
                    .HasMaxLength(200)
                    .HasColumnName("FATHER_FAMILYNAME");

                entity.Property(e => e.FatherFirstname)
                    .HasMaxLength(200)
                    .HasColumnName("FATHER_FIRSTNAME");

                entity.Property(e => e.FatherSurname)
                    .HasMaxLength(200)
                    .HasColumnName("FATHER_SURNAME");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(200)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.GdkpCaseNumber)
                    .HasMaxLength(100)
                    .HasColumnName("GDKP_CASE_NUMBER");

                entity.Property(e => e.GdkpDate)
                    .HasMaxLength(100)
                    .HasColumnName("GDKP_DATE");

                entity.Property(e => e.GdkpNumber)
                    .HasMaxLength(100)
                    .HasColumnName("GDKP_NUMBER");

                entity.Property(e => e.GdkpStr)
                    .HasMaxLength(100)
                    .HasColumnName("GDKP_STR");

                entity.Property(e => e.GdkpTom)
                    .HasMaxLength(100)
                    .HasColumnName("GDKP_TOM");

                entity.Property(e => e.IsAdministrative)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("IS_ADMINISTRATIVE");

                entity.Property(e => e.IssueDate)
                    .HasColumnType("DATE")
                    .HasColumnName("ISSUE_DATE");

                entity.Property(e => e.MotherFamilyname)
                    .HasMaxLength(200)
                    .HasColumnName("MOTHER_FAMILYNAME");

                entity.Property(e => e.MotherFirstname)
                    .HasMaxLength(200)
                    .HasColumnName("MOTHER_FIRSTNAME");

                entity.Property(e => e.MotherSurname)
                    .HasMaxLength(200)
                    .HasColumnName("MOTHER_SURNAME");

                entity.Property(e => e.OffenceEndDate)
                    .HasColumnType("DATE")
                    .HasColumnName("OFFENCE_END_DATE");

                entity.Property(e => e.OffenceStartDate)
                    .HasColumnType("DATE")
                    .HasColumnName("OFFENCE_START_DATE");

                entity.Property(e => e.OldId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("OLD_ID");

                entity.Property(e => e.PersonId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PERSON_ID");

                entity.Property(e => e.ReceiveDate)
                    .HasColumnType("DATE")
                    .HasColumnName("RECEIVE_DATE");

                entity.Property(e => e.SanctionTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SANCTION_TYPE_ID");

                entity.Property(e => e.SequentialIndex)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SEQUENTIAL_INDEX");

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS_CODE");

                entity.Property(e => e.Surname)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.BirthCity)
                    .WithMany(p => p.Fbbcs)
                    .HasForeignKey(d => d.BirthCityId)
                    .HasConstraintName("FK_FBBC_G_CITIES");

                entity.HasOne(d => d.BirthCountry)
                    .WithMany(p => p.FbbcBirthCountries)
                    .HasForeignKey(d => d.BirthCountryId)
                    .HasConstraintName("FK_FBBC_G_COUNTRIES_BIRTH");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.FbbcCountries)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_FBBC_G_COUNTRIES");

                entity.HasOne(d => d.DocType)
                    .WithMany(p => p.Fbbcs)
                    .HasForeignKey(d => d.DocTypeId)
                    .HasConstraintName("FK_FBBC_FBBC_DOC_TYPES");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Fbbcs)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_FBBC_P_PERSON");

                entity.HasOne(d => d.SanctionType)
                    .WithMany(p => p.Fbbcs)
                    .HasForeignKey(d => d.SanctionTypeId)
                    .HasConstraintName("FK_FBBC_FBBC_SANCT_TYPES");
            });

            modelBuilder.Entity<FbbcDocType>(entity =>
            {
                entity.ToTable("FBBC_DOC_TYPES");

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

            modelBuilder.Entity<FbbcSanctType>(entity =>
            {
                entity.ToTable("FBBC_SANCT_TYPES");

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

            modelBuilder.Entity<FbbcStatus>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("XPKFBBC_STATUSES");

                entity.ToTable("FBBC_STATUSES");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<GBgDistrict>(entity =>
            {
                entity.ToTable("G_BG_DISTRICTS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .HasColumnName("CODE");

                entity.Property(e => e.EcrisTechnId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_TECHN_ID");

                entity.Property(e => e.EkatteCode)
                    .HasMaxLength(200)
                    .HasColumnName("EKATTE_CODE");

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

            modelBuilder.Entity<GBgMunicipality>(entity =>
            {
                entity.ToTable("G_BG_MUNICIPALITIES");

                entity.HasIndex(e => e.DistrictId, "XIF1G_BG_MUNICIPALITIES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .HasColumnName("CODE");

                entity.Property(e => e.DistrictId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DISTRICT_ID");

                entity.Property(e => e.EkatteCode)
                    .HasMaxLength(200)
                    .HasColumnName("EKATTE_CODE");

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

                entity.HasOne(d => d.District)
                    .WithMany(p => p.GBgMunicipalities)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_G_BG_MUNICIPALITIES_G_BG_DI");
            });

            modelBuilder.Entity<GCity>(entity =>
            {
                entity.ToTable("G_CITIES");

                entity.HasIndex(e => e.CountryId, "XIF1G_CITIES");

                entity.HasIndex(e => e.MunicipalityId, "XIF2G_CITIES");

                entity.HasIndex(e => e.CsAuthorityId, "XIF3G_CITIES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.CodeRel)
                    .HasMaxLength(200)
                    .HasColumnName("CODE_REL");

                entity.Property(e => e.CountryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_ID");

                entity.Property(e => e.CsAuthorityId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CS_AUTHORITY_ID");

                entity.Property(e => e.EcrisTechnId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_TECHN_ID");

                entity.Property(e => e.EkatteCode)
                    .HasMaxLength(200)
                    .HasColumnName("EKATTE_CODE");

                entity.Property(e => e.MunicipalityId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MUNICIPALITY_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(200)
                    .HasColumnName("NAME_EN");

                entity.Property(e => e.OrderNumber)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ORDER_NUMBER");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.GCities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_G_CITIES_G_COUNTRIES");

                entity.HasOne(d => d.CsAuthority)
                    .WithMany(p => p.GCities)
                    .HasForeignKey(d => d.CsAuthorityId)
                    .HasConstraintName("FK_G_CITIES_G_CS_AUTHORITIES");

                entity.HasOne(d => d.Municipality)
                    .WithMany(p => p.GCities)
                    .HasForeignKey(d => d.MunicipalityId)
                    .HasConstraintName("FK_G_CITIES_G_BG_MUNICIPALITIE");
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

                entity.Property(e => e.Iso3166Alpha2)
                    .HasMaxLength(100)
                    .HasColumnName("ISO_3166_ALPHA2");

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

                entity.HasIndex(e => e.DecidingAuthId, "XIF1G_CS_AUTHORITIES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.DecidingAuthId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DECIDING_AUTH_ID");

                entity.Property(e => e.IsCentral)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("IS_CENTRAL");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.OldId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OLD_ID");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.HasOne(d => d.DecidingAuth)
                    .WithMany(p => p.GCsAuthorities)
                    .HasForeignKey(d => d.DecidingAuthId)
                    .HasConstraintName("FK_G_CS_AUTHORITIES_G_DECIDING");
            });

            modelBuilder.Entity<GDecidingAuthority>(entity =>
            {
                entity.ToTable("G_DECIDING_AUTHORITIES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.ActiveForBulletins)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ACTIVE_FOR_BULLETINS");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .HasColumnName("CODE");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(500)
                    .HasColumnName("DISPLAY_NAME");

                entity.Property(e => e.EisppCode)
                    .HasMaxLength(200)
                    .HasColumnName("EISPP_CODE");

                entity.Property(e => e.EisppId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("EISPP_ID");

                entity.Property(e => e.IsGroup)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("IS_GROUP");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(200)
                    .HasColumnName("NAME_EN");

                entity.Property(e => e.OldId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OLD_ID");

                entity.Property(e => e.OrderNumber)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ORDER_NUMBER");

                entity.Property(e => e.ParentId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PARENT_ID");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Visible)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VISIBLE");
            });

            modelBuilder.Entity<GNomenclature>(entity =>
            {
                entity.ToTable("G_NOMENCLATURES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Descr).HasColumnName("DESCR");

                entity.Property(e => e.TableName)
                    .HasMaxLength(200)
                    .HasColumnName("TABLE_NAME");
            });

            modelBuilder.Entity<GRole>(entity =>
            {
                entity.ToTable("G_ROLES");

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

            modelBuilder.Entity<GUser>(entity =>
            {
                entity.ToTable("G_USERS");

                entity.HasIndex(e => e.CsAuthorityId, "XIF1G_USERS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Active)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ACTIVE");

                entity.Property(e => e.CsAuthorityId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CS_AUTHORITY_ID");

                entity.Property(e => e.Egn)
                    .HasMaxLength(100)
                    .HasColumnName("EGN");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Familyname)
                    .HasMaxLength(200)
                    .HasColumnName("FAMILYNAME");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(200)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Position).HasColumnName("POSITION");

                entity.Property(e => e.Surname)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME");

                entity.HasOne(d => d.CsAuthority)
                    .WithMany(p => p.GUsers)
                    .HasForeignKey(d => d.CsAuthorityId)
                    .HasConstraintName("FK_G_USERS_G_CS_AUTHORITIES");
            });

            modelBuilder.Entity<GUserRole>(entity =>
            {
                entity.ToTable("G_USER_ROLES");

                entity.HasIndex(e => e.UserId, "XIF1G_USER_ROLES");

                entity.HasIndex(e => e.RoleId, "XIF2G_USER_ROLES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.GUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_G_USER_ROLES_G_ROLES");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_G_USER_ROLES_G_USERS");
            });

            modelBuilder.Entity<GraoPerson>(entity =>
            {
                entity.ToTable("GRAO_PERSON");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("DATE")
                    .HasColumnName("BIRTH_DATE");

                entity.Property(e => e.BirthplaceCode)
                    .HasMaxLength(100)
                    .HasColumnName("BIRTHPLACE_CODE");

                entity.Property(e => e.BirthplaceText)
                    .HasMaxLength(200)
                    .HasColumnName("BIRTHPLACE_TEXT");

                entity.Property(e => e.Egn)
                    .HasMaxLength(100)
                    .HasColumnName("EGN");

                entity.Property(e => e.Familyname)
                    .HasMaxLength(200)
                    .HasColumnName("FAMILYNAME");

                entity.Property(e => e.FathersNames)
                    .HasMaxLength(200)
                    .HasColumnName("FATHERS_NAMES");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(200)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.MothersNames)
                    .HasMaxLength(200)
                    .HasColumnName("MOTHERS_NAMES");

                entity.Property(e => e.Sex)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SEX");

                entity.Property(e => e.Surname)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME");
            });

            modelBuilder.Entity<PPersGroup>(entity =>
            {
                entity.ToTable("P_PERS_GROUP");

                entity.HasIndex(e => e.FirstPersId, "XIF1P_PERS_GROUP");

                entity.HasIndex(e => e.RelPersId, "XIF2P_PERS_GROUP");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.FirstPersId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_PERS_ID");

                entity.Property(e => e.RelPersId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("REL_PERS_ID");

                entity.HasOne(d => d.FirstPers)
                    .WithMany(p => p.PPersGroupFirstPers)
                    .HasForeignKey(d => d.FirstPersId)
                    .HasConstraintName("FK_P_PERS_GROUP_FIRST_PERSON");

                entity.HasOne(d => d.RelPers)
                    .WithMany(p => p.PPersGroupRelPers)
                    .HasForeignKey(d => d.RelPersId)
                    .HasConstraintName("FK_P_PERS_GROUP_REL_PERSON");
            });

            modelBuilder.Entity<PPerson>(entity =>
            {
                entity.ToTable("P_PERSON");

                entity.HasIndex(e => e.BirthCityId, "XIF1P_PERSON");

                entity.HasIndex(e => e.BirthCountryId, "XIF2P_PERSON");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.AfisNumber)
                    .HasMaxLength(200)
                    .HasColumnName("AFIS_NUMBER");

                entity.Property(e => e.BirthCityId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BIRTH_CITY_ID");

                entity.Property(e => e.BirthCountryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BIRTH_COUNTRY_ID");

                entity.Property(e => e.BirthDay)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("BIRTH_DAY");

                entity.Property(e => e.BirthMonth)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("BIRTH_MONTH");

                entity.Property(e => e.BirthPlaceOther).HasColumnName("BIRTH_PLACE_OTHER");

                entity.Property(e => e.BirthYear)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("BIRTH_YEAR");

                entity.Property(e => e.Egn)
                    .HasMaxLength(100)
                    .HasColumnName("EGN");

                entity.Property(e => e.Familyname)
                    .HasMaxLength(200)
                    .HasColumnName("FAMILYNAME");

                entity.Property(e => e.FamilynameLat)
                    .HasMaxLength(200)
                    .HasColumnName("FAMILYNAME_LAT");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(200)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.FirstnameLat)
                    .HasMaxLength(200)
                    .HasColumnName("FIRSTNAME_LAT");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(200)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.Ln)
                    .HasMaxLength(100)
                    .HasColumnName("LN");

                entity.Property(e => e.Lnch)
                    .HasMaxLength(100)
                    .HasColumnName("LNCH");

                entity.Property(e => e.Sex)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SEX");

                entity.Property(e => e.Surname)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME");

                entity.Property(e => e.SurnameLat)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME_LAT");

                entity.HasOne(d => d.BirthCity)
                    .WithMany(p => p.PPeople)
                    .HasForeignKey(d => d.BirthCityId)
                    .HasConstraintName("FK_P_PERSON_G_CITIES");

                entity.HasOne(d => d.BirthCountry)
                    .WithMany(p => p.PPeople)
                    .HasForeignKey(d => d.BirthCountryId)
                    .HasConstraintName("FK_P_PERSON_G_COUNTRIES");
            });

            modelBuilder.Entity<ZImportFbbc>(entity =>
            {
                entity.ToTable("Z_IMPORT_FBBC");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID");

                entity.Property(e => e.Anotacia)
                    .HasColumnType("CLOB")
                    .HasColumnName("ANOTACIA");

                entity.Property(e => e.Birthcountry)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("BIRTHCOUNTRY");

                entity.Property(e => e.Birthday)
                    .HasMaxLength(8)
                    .HasColumnName("BIRTHDAY");

                entity.Property(e => e.Birthplace)
                    .HasMaxLength(200)
                    .HasColumnName("BIRTHPLACE");

                entity.Property(e => e.Country)
                    .HasMaxLength(32)
                    .HasColumnName("COUNTRY")
                    .IsFixedLength();

                entity.Property(e => e.CountryId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("COUNTRY_ID");

                entity.Property(e => e.DateBegin)
                    .HasMaxLength(10)
                    .HasColumnName("DATE_BEGIN");

                entity.Property(e => e.DateDecision)
                    .HasMaxLength(8)
                    .HasColumnName("DATE_DECISION");

                entity.Property(e => e.DateDecisionFinal)
                    .HasMaxLength(8)
                    .HasColumnName("DATE_DECISION_FINAL");

                entity.Property(e => e.DateDestroyed)
                    .HasMaxLength(8)
                    .HasColumnName("DATE_DESTROYED");

                entity.Property(e => e.DateEnd)
                    .HasMaxLength(10)
                    .HasColumnName("DATE_END");

                entity.Property(e => e.DateIns)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_INS");

                entity.Property(e => e.DateIssue)
                    .HasMaxLength(8)
                    .HasColumnName("DATE_ISSUE");

                entity.Property(e => e.DateLastUpd)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_LAST_UPD");

                entity.Property(e => e.DateReceived)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("DATE_RECEIVED")
                    .IsFixedLength();

                entity.Property(e => e.EcrisConvId)
                    .HasMaxLength(20)
                    .HasColumnName("ECRIS_CONV_ID");

                entity.Property(e => e.EcrisCountry)
                    .HasMaxLength(2)
                    .HasColumnName("ECRIS_COUNTRY");

                entity.Property(e => e.EcrisId)
                    .HasMaxLength(25)
                    .HasColumnName("ECRIS_ID");

                entity.Property(e => e.EcrisRelatedConvId)
                    .HasMaxLength(128)
                    .HasColumnName("ECRIS_RELATED_CONV_ID");

                entity.Property(e => e.EcrisUpdatedConvId)
                    .HasMaxLength(20)
                    .HasColumnName("ECRIS_UPDATED_CONV_ID");

                entity.Property(e => e.EcrisUpdatedConvType)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ECRIS_UPDATED_CONV_TYPE");

                entity.Property(e => e.Egn)
                    .HasMaxLength(10)
                    .HasColumnName("EGN");

                entity.Property(e => e.Family)
                    .HasMaxLength(200)
                    .HasColumnName("FAMILY");

                entity.Property(e => e.FatherFamily)
                    .HasMaxLength(200)
                    .HasColumnName("FATHER_FAMILY");

                entity.Property(e => e.FatherFname)
                    .HasMaxLength(200)
                    .HasColumnName("FATHER_FNAME");

                entity.Property(e => e.FatherMname)
                    .HasMaxLength(200)
                    .HasColumnName("FATHER_MNAME");

                entity.Property(e => e.FlagAdmPenality)
                    .HasMaxLength(1)
                    .HasColumnName("FLAG_ADM_PENALITY");

                entity.Property(e => e.Fname)
                    .HasMaxLength(200)
                    .HasColumnName("FNAME");

                entity.Property(e => e.GdkpDate)
                    .HasMaxLength(10)
                    .HasColumnName("GDKP_DATE");

                entity.Property(e => e.GdkpDelo)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("GDKP_DELO");

                entity.Property(e => e.GdkpNumber)
                    .HasMaxLength(8)
                    .HasColumnName("GDKP_NUMBER");

                entity.Property(e => e.GdkpStr)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("GDKP_STR");

                entity.Property(e => e.GdkpTom)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("GDKP_TOM");

                entity.Property(e => e.ImageFileName)
                    .HasMaxLength(1028)
                    .HasColumnName("IMAGE_FILE_NAME");

                entity.Property(e => e.Judge)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("JUDGE");

                entity.Property(e => e.Mname)
                    .HasMaxLength(200)
                    .HasColumnName("MNAME");

                entity.Property(e => e.MotherFamily)
                    .HasMaxLength(200)
                    .HasColumnName("MOTHER_FAMILY");

                entity.Property(e => e.MotherFname)
                    .HasMaxLength(200)
                    .HasColumnName("MOTHER_FNAME");

                entity.Property(e => e.MotherMname)
                    .HasMaxLength(200)
                    .HasColumnName("MOTHER_MNAME");

                entity.Property(e => e.NjrCountry)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("NJR_COUNTRY")
                    .IsFixedLength();

                entity.Property(e => e.NjrId)
                    .HasMaxLength(20)
                    .HasColumnName("NJR_ID");

                entity.Property(e => e.NjrIdFirst)
                    .HasMaxLength(20)
                    .HasColumnName("NJR_ID_FIRST");

                entity.Property(e => e.RegdocType)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("REGDOC_TYPE");

                entity.Property(e => e.SequentialNumber)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SEQUENTIAL_NUMBER");

                entity.Property(e => e.UserEntered)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ENTERED");

                entity.Property(e => e.UserName)
                    .HasMaxLength(200)
                    .HasColumnName("USER_NAME");

                entity.Property(e => e.UserUpdated)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_UPDATED");

                entity.Property(e => e.XmlData)
                    .HasColumnType("CLOB")
                    .HasColumnName("XML_DATA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
