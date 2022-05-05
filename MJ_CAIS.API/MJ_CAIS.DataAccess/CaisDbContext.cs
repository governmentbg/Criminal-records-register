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

        public virtual DbSet<AAppCitizenship> AAppCitizenships { get; set; } = null!;
        public virtual DbSet<AAppPersAlias> AAppPersAliases { get; set; } = null!;
        public virtual DbSet<AApplication> AApplications { get; set; } = null!;
        public virtual DbSet<AApplicationStatus> AApplicationStatuses { get; set; } = null!;
        public virtual DbSet<AApplicationType> AApplicationTypes { get; set; } = null!;
        public virtual DbSet<APaymentMethod> APaymentMethods { get; set; } = null!;
        public virtual DbSet<APurpose> APurposes { get; set; } = null!;
        public virtual DbSet<ASrvcResRcptMeth> ASrvcResRcptMeths { get; set; } = null!;
        public virtual DbSet<AStatusH> AStatusHes { get; set; } = null!;
        public virtual DbSet<BBullPersAlias> BBullPersAliases { get; set; } = null!;
        public virtual DbSet<BBulletin> BBulletins { get; set; } = null!;
        public virtual DbSet<BBulletinStatus> BBulletinStatuses { get; set; } = null!;
        public virtual DbSet<BBulletinStatusH> BBulletinStatusHes { get; set; } = null!;
        public virtual DbSet<BCaseType> BCaseTypes { get; set; } = null!;
        public virtual DbSet<BDecision> BDecisions { get; set; } = null!;
        public virtual DbSet<BDecisionChType> BDecisionChTypes { get; set; } = null!;
        public virtual DbSet<BDecisionType> BDecisionTypes { get; set; } = null!;
        public virtual DbSet<BEcrisOffCategory> BEcrisOffCategories { get; set; } = null!;
        public virtual DbSet<BEcrisStanctCateg> BEcrisStanctCategs { get; set; } = null!;
        public virtual DbSet<BFormOfGuilt> BFormOfGuilts { get; set; } = null!;
        public virtual DbSet<BIdDocCategory> BIdDocCategories { get; set; } = null!;
        public virtual DbSet<BInternalRequest> BInternalRequests { get; set; } = null!;
        public virtual DbSet<BOffence> BOffences { get; set; } = null!;
        public virtual DbSet<BOffenceCategory> BOffenceCategories { get; set; } = null!;
        public virtual DbSet<BPersNationality> BPersNationalities { get; set; } = null!;
        public virtual DbSet<BProbation> BProbations { get; set; } = null!;
        public virtual DbSet<BReqStatus> BReqStatuses { get; set; } = null!;
        public virtual DbSet<BSanctProbCategory> BSanctProbCategories { get; set; } = null!;
        public virtual DbSet<BSanctProbMeasure> BSanctProbMeasures { get; set; } = null!;
        public virtual DbSet<BSanction> BSanctions { get; set; } = null!;
        public virtual DbSet<BSanctionActivity> BSanctionActivities { get; set; } = null!;
        public virtual DbSet<BSanctionCategory> BSanctionCategories { get; set; } = null!;
        public virtual DbSet<DCsDocRegister> DCsDocRegisters { get; set; } = null!;
        public virtual DbSet<DDocContent> DDocContents { get; set; } = null!;
        public virtual DbSet<DDocRegister> DDocRegisters { get; set; } = null!;
        public virtual DbSet<DDocType> DDocTypes { get; set; } = null!;
        public virtual DbSet<DDocument> DDocuments { get; set; } = null!;
        public virtual DbSet<DRegisterType> DRegisterTypes { get; set; } = null!;
        public virtual DbSet<EEcrisAuthority> EEcrisAuthorities { get; set; } = null!;
        public virtual DbSet<EEcrisIdentification> EEcrisIdentifications { get; set; } = null!;
        public virtual DbSet<EEcrisInbox> EEcrisInboxes { get; set; } = null!;
        public virtual DbSet<EEcrisMessage> EEcrisMessages { get; set; } = null!;
        public virtual DbSet<EEcrisMsgStatus> EEcrisMsgStatuses { get; set; } = null!;
        public virtual DbSet<EEcrisOutbox> EEcrisOutboxes { get; set; } = null!;
        public virtual DbSet<EIsinDatum> EIsinData { get; set; } = null!;
        public virtual DbSet<ERegixCache> ERegixCaches { get; set; } = null!;
        public virtual DbSet<ESynchronizationParameter> ESynchronizationParameters { get; set; } = null!;
        public virtual DbSet<EWebRequest> EWebRequests { get; set; } = null!;
        public virtual DbSet<EWebService> EWebServices { get; set; } = null!;
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
        public virtual DbSet<GExtAdministration> GExtAdministrations { get; set; } = null!;
        public virtual DbSet<GNomenclature> GNomenclatures { get; set; } = null!;
        public virtual DbSet<GRole> GRoles { get; set; } = null!;
        public virtual DbSet<GUser> GUsers { get; set; } = null!;
        public virtual DbSet<GUserRole> GUserRoles { get; set; } = null!;
        public virtual DbSet<GUsersCitizen> GUsersCitizens { get; set; } = null!;
        public virtual DbSet<GUsersExt> GUsersExts { get; set; } = null!;
        public virtual DbSet<GraoPerson> GraoPeople { get; set; } = null!;
        public virtual DbSet<PAppId> PAppIds { get; set; } = null!;
        public virtual DbSet<PBulletinId> PBulletinIds { get; set; } = null!;
        public virtual DbSet<PPersionIdsH> PPersionIdsHes { get; set; } = null!;
        public virtual DbSet<PPerson> PPeople { get; set; } = null!;
        public virtual DbSet<PPersonH> PPersonHs { get; set; } = null!;
        public virtual DbSet<PPersonId> PPersonIds { get; set; } = null!;
        public virtual DbSet<PPersonIdType> PPersonIdTypes { get; set; } = null!;
        public virtual DbSet<PPersonOld> PPersonOlds { get; set; } = null!;
        public virtual DbSet<ZImportFbbc> ZImportFbbcs { get; set; } = null!;
        public virtual DbSet<ZImportFbbcTest> ZImportFbbcTests { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("MJ_CAIS");

            modelBuilder.Entity<AAppCitizenship>(entity =>
            {
                entity.ToTable("A_APP_CITIZENSHIP");

                entity.HasIndex(e => e.ApplicationId, "XIF1A_APP_CITIZENSHIP");

                entity.HasIndex(e => e.CountryId, "XIF2A_APP_CITIZENSHIP");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.ApplicationId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("APPLICATION_ID");

                entity.Property(e => e.CountryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AAppCitizenships)
                    .HasForeignKey(d => d.ApplicationId)
                    .HasConstraintName("FK_A_APP_CITIZENSHIP_A_APPLICA");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.AAppCitizenships)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_A_APP_CITIZENSHIP_G_COUNTRI");
            });

            modelBuilder.Entity<AAppPersAlias>(entity =>
            {
                entity.ToTable("A_APP_PERS_ALIAS");

                entity.HasIndex(e => e.ApplicationId, "XIF1A_APP_PERS_ALIAS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.ApplicationId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("APPLICATION_ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AAppPersAliases)
                    .HasForeignKey(d => d.ApplicationId)
                    .HasConstraintName("FK_A_APP_PERS_ALIAS_A_APPLICAT");
            });

            modelBuilder.Entity<AApplication>(entity =>
            {
                entity.ToTable("A_APPLICATIONS");

                entity.HasIndex(e => e.UserCitizenId, "XIF11A_APPLICATIONS");

                entity.HasIndex(e => e.UserId, "XIF12A_APPLICATIONS");

                entity.HasIndex(e => e.UserExtId, "XIF13A_APPLICATIONS");

                entity.HasIndex(e => e.SrvcResRcptMethId, "XIF2A_APPLICATIONS");

                entity.HasIndex(e => e.ApplicationTypeId, "XIF3A_APPLICATIONS");

                entity.HasIndex(e => e.CsAuthorityId, "XIF4A_APPLICATIONS");

                entity.HasIndex(e => e.PurposeId, "XIF5A_APPLICATIONS");

                entity.HasIndex(e => e.PaymentMethodId, "XIF6A_APPLICATIONS");

                entity.HasIndex(e => e.DocContentId, "XIF7A_APPLICATIONS");

                entity.HasIndex(e => e.StatusCode, "XIF8A_APPLICATIONS");

                entity.HasIndex(e => e.BirthCountryId, "XIF9A_APPLICATIONS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.AddrDistrict).HasColumnName("ADDR_DISTRICT");

                entity.Property(e => e.AddrEmail).HasColumnName("ADDR_EMAIL");

                entity.Property(e => e.AddrName).HasColumnName("ADDR_NAME");

                entity.Property(e => e.AddrPhone).HasColumnName("ADDR_PHONE");

                entity.Property(e => e.AddrState).HasColumnName("ADDR_STATE");

                entity.Property(e => e.AddrStr).HasColumnName("ADDR_STR");

                entity.Property(e => e.AddrTown).HasColumnName("ADDR_TOWN");

                entity.Property(e => e.Address).HasColumnName("ADDRESS");

                entity.Property(e => e.ApplicantName)
                    .HasMaxLength(200)
                    .HasColumnName("APPLICANT_NAME");

                entity.Property(e => e.ApplicationTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("APPLICATION_TYPE_ID");

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

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.CsAuthorityId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CS_AUTHORITY_ID");

                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

                entity.Property(e => e.DocContentId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DOC_CONTENT_ID");

                entity.Property(e => e.Egn)
                    .HasMaxLength(100)
                    .HasColumnName("EGN");

                entity.Property(e => e.Email).HasColumnName("EMAIL");

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

                entity.Property(e => e.FromCosul)
                    .HasPrecision(1)
                    .HasColumnName("FROM_COSUL");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(200)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.IsLocal)
                    .HasPrecision(1)
                    .HasColumnName("IS_LOCAL");

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

                entity.Property(e => e.PaymentMethodId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PAYMENT_METHOD_ID");

                entity.Property(e => e.Purpose).HasColumnName("PURPOSE");

                entity.Property(e => e.PurposeCountry).HasColumnName("PURPOSE_COUNTRY");

                entity.Property(e => e.PurposeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PURPOSE_ID");

                entity.Property(e => e.PurposePosition).HasColumnName("PURPOSE_POSITION");

                entity.Property(e => e.RegistrationNumber)
                    .HasMaxLength(100)
                    .HasColumnName("REGISTRATION_NUMBER");

                entity.Property(e => e.Sex)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SEX");

                entity.Property(e => e.SrvcResRcptMethId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SRVC_RES_RCPT_METH_ID");

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS_CODE");

                entity.Property(e => e.Surname)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME");

                entity.Property(e => e.SurnameLat)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME_LAT");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.UserCitizenId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USER_CITIZEN_ID");

                entity.Property(e => e.UserExtId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USER_EXT_ID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.ApplicationType)
                    .WithMany(p => p.AApplications)
                    .HasForeignKey(d => d.ApplicationTypeId)
                    .HasConstraintName("FK_A_APPLICATIONS_A_APPLICATIO");

                entity.HasOne(d => d.BirthCity)
                    .WithMany(p => p.AApplications)
                    .HasForeignKey(d => d.BirthCityId)
                    .HasConstraintName("FK_A_APPLICATIONS_G_CITIES");

                entity.HasOne(d => d.BirthCountry)
                    .WithMany(p => p.AApplications)
                    .HasForeignKey(d => d.BirthCountryId)
                    .HasConstraintName("FK_A_APPLICATIONS_G_COUNTRIES");

                entity.HasOne(d => d.CsAuthority)
                    .WithMany(p => p.AApplications)
                    .HasForeignKey(d => d.CsAuthorityId)
                    .HasConstraintName("FK_A_APPLICATIONS_G_CS_AUTHORI");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.AApplications)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("FK_A_APPLICATIONS_A_PAYMENT_ME");

                entity.HasOne(d => d.PurposeNavigation)
                    .WithMany(p => p.AApplications)
                    .HasForeignKey(d => d.PurposeId)
                    .HasConstraintName("FK_A_APPLICATIONS_A_PURPOSES");

                entity.HasOne(d => d.SrvcResRcptMeth)
                    .WithMany(p => p.AApplications)
                    .HasForeignKey(d => d.SrvcResRcptMethId)
                    .HasConstraintName("FK_A_APPLICATIONS_A_SRVC_RES_R");
            });

            modelBuilder.Entity<AApplicationStatus>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("XPKA_APPLICATION_STATUSES");

                entity.ToTable("A_APPLICATION_STATUSES");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<AApplicationType>(entity =>
            {
                entity.ToTable("A_APPLICATION_TYPES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .HasColumnName("CODE");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

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

            modelBuilder.Entity<APaymentMethod>(entity =>
            {
                entity.ToTable("A_PAYMENT_METHODS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

                entity.Property(e => e.IsForWeb)
                    .HasPrecision(1)
                    .HasColumnName("IS_FOR_WEB");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
            });

            modelBuilder.Entity<APurpose>(entity =>
            {
                entity.ToTable("A_PURPOSES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

                entity.Property(e => e.InstructionsForFiles).HasColumnName("INSTRUCTIONS_FOR_FILES");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.RequestInfo)
                    .HasPrecision(1)
                    .HasColumnName("REQUEST_INFO");

                entity.Property(e => e.TaxFree)
                    .HasPrecision(1)
                    .HasColumnName("TAX_FREE");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
            });

            modelBuilder.Entity<ASrvcResRcptMeth>(entity =>
            {
                entity.ToTable("A_SRVC_RES_RCPT_METH");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .HasColumnName("CODE");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
            });

            modelBuilder.Entity<AStatusH>(entity =>
            {
                entity.ToTable("A_STATUS_H");

                entity.HasIndex(e => new { e.StatusCode, e.ApplicationId, e.ReportOrder }, "XAK1A_STATUS_H")
                    .IsUnique();

                entity.HasIndex(e => e.StatusCode, "XIF1A_STATUS_H");

                entity.HasIndex(e => e.ApplicationId, "XIF2A_STATUS_H");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.ApplicationId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("APPLICATION_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Descr).HasColumnName("DESCR");

                entity.Property(e => e.ReportOrder)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("REPORT_ORDER");

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS_CODE");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AStatusHes)
                    .HasForeignKey(d => d.ApplicationId)
                    .HasConstraintName("FK_A_STATUS_H_A_APPLICATIONS");

                entity.HasOne(d => d.StatusCodeNavigation)
                    .WithMany(p => p.AStatusHes)
                    .HasForeignKey(d => d.StatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_A_STATUS_H_A_APPLICATION_ST");
            });

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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

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

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

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

                entity.HasIndex(e => e.CaseAuthId, "XIF19B_BULLETINS");

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

                entity.Property(e => e.CaseAuthId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CASE_AUTH_ID");

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

                entity.Property(e => e.ConvRemarks)
                    .HasColumnType("CLOB")
                    .HasColumnName("CONV_REMARKS");

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

                entity.Property(e => e.Locked)
                    .HasPrecision(1)
                    .HasColumnName("LOCKED");

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

                entity.Property(e => e.NoSanction)
                    .HasPrecision(1)
                    .HasColumnName("NO_SANCTION");

                entity.Property(e => e.PrevSuspSent)
                    .HasPrecision(1)
                    .HasColumnName("PREV_SUSP_SENT");

                entity.Property(e => e.PrevSuspSentDescr).HasColumnName("PREV_SUSP_SENT_DESCR");

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

                entity.HasOne(d => d.CaseAuth)
                    .WithMany(p => p.BBulletinCaseAuths)
                    .HasForeignKey(d => d.CaseAuthId)
                    .HasConstraintName("FK_B_BULLETINS_G_DEC_AUTH_C");

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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

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

            modelBuilder.Entity<BBulletinStatusH>(entity =>
            {
                entity.ToTable("B_BULLETIN_STATUS_H");

                entity.HasIndex(e => e.BulletinId, "XIF1B_BULLETIN_STATUS_H");

                entity.HasIndex(e => e.NewStatusCode, "XIF2B_BULLETIN_STATUS_H");

                entity.HasIndex(e => e.OldStatusCode, "XIF3B_BULLETIN_STATUS_H");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.BulletinId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BULLETIN_ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Descr).HasColumnName("DESCR");

                entity.Property(e => e.Locked)
                    .HasPrecision(1)
                    .HasColumnName("LOCKED");

                entity.Property(e => e.NewStatusCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NEW_STATUS_CODE");

                entity.Property(e => e.OldStatusCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OLD_STATUS_CODE");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.Bulletin)
                    .WithMany(p => p.BBulletinStatusHes)
                    .HasForeignKey(d => d.BulletinId)
                    .HasConstraintName("FK_B_BULLETIN_STATUS_H_B_BULLE");

                entity.HasOne(d => d.NewStatusCodeNavigation)
                    .WithMany(p => p.BBulletinStatusHNewStatusCodeNavigations)
                    .HasForeignKey(d => d.NewStatusCode)
                    .HasConstraintName("FK_B_BULLETIN_STATUS_H_B_NEW");

                entity.HasOne(d => d.OldStatusCodeNavigation)
                    .WithMany(p => p.BBulletinStatusHOldStatusCodeNavigations)
                    .HasForeignKey(d => d.OldStatusCode)
                    .HasConstraintName("FK_B_BULLETIN_STATUS_H_B_OLD");
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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EXTERNAL_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
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

                entity.Property(e => e.ChangeDate)
                    .HasColumnType("DATE")
                    .HasColumnName("CHANGE_DATE");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

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

                entity.Property(e => e.Descr)
                    .HasColumnType("CLOB")
                    .HasColumnName("DESCR");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.EcrisTechnId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_TECHN_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(500)
                    .HasColumnName("NAME_EN");

                entity.Property(e => e.OrderNumber)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ORDER_NUMBER");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EXTERNAL_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
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
                    .HasPrecision(1)
                    .HasColumnName("CATEGORY_IS_OPEN");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.EcrisTechnId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_TECHN_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(500)
                    .HasColumnName("NAME_EN");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.EcrisTechnId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_TECHN_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(500)
                    .HasColumnName("NAME_EN");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
            });

            modelBuilder.Entity<BFormOfGuilt>(entity =>
            {
                entity.ToTable("B_FORM_OF_GUILT");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
            });

            modelBuilder.Entity<BIdDocCategory>(entity =>
            {
                entity.ToTable("B_ID_DOC_CATEGORIES");

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

                entity.Property(e => e.EcrisTechnId)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_TECHN_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(500)
                    .HasColumnName("NAME_EN");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
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

                entity.Property(e => e.AAppBulletinId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("A_APP_BULLETIN_ID");

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

                entity.HasIndex(e => e.OffPlaceCityId, "XIF6B_OFFENCES");

                entity.HasIndex(e => e.FormOfGuiltId, "XIF7B_OFFENCES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.BulletinId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BULLETIN_ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.EcrisOffCatId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_OFF_CAT_ID");

                entity.Property(e => e.FormOfGuiltId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FORM_OF_GUILT_ID");

                entity.Property(e => e.LegalProvisions).HasColumnName("LEGAL_PROVISIONS");

                entity.Property(e => e.OffEndDate)
                    .HasColumnType("DATE")
                    .HasColumnName("OFF_END_DATE");

                entity.Property(e => e.OffEndDatePrec)
                    .HasMaxLength(200)
                    .HasColumnName("OFF_END_DATE_PREC");

                entity.Property(e => e.OffPlaceCityId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OFF_PLACE_CITY_ID");

                entity.Property(e => e.OffPlaceCountryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OFF_PLACE_COUNTRY_ID");

                entity.Property(e => e.OffPlaceDescr).HasColumnName("OFF_PLACE_DESCR");

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

                entity.Property(e => e.Remarks).HasColumnName("REMARKS");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.Bulletin)
                    .WithMany(p => p.BOffences)
                    .HasForeignKey(d => d.BulletinId)
                    .HasConstraintName("FK_B_OFFENCES_B_BULLETINS");

                entity.HasOne(d => d.EcrisOffCat)
                    .WithMany(p => p.BOffences)
                    .HasForeignKey(d => d.EcrisOffCatId)
                    .HasConstraintName("FK_B_OFFENCES_B_ECRIS_OFF_CATE");

                entity.HasOne(d => d.OffPlaceCity)
                    .WithMany(p => p.BOffences)
                    .HasForeignKey(d => d.OffPlaceCityId)
                    .HasConstraintName("FK_B_OFFENCES_G_CITIES");

                entity.HasOne(d => d.OffPlaceCountry)
                    .WithMany(p => p.BOffences)
                    .HasForeignKey(d => d.OffPlaceCountryId)
                    .HasConstraintName("FK_B_OFFENCES_G_COUNTRIES");

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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.OffLevel)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("OFF_LEVEL");

                entity.Property(e => e.OrderNumber)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ORDER_NUMBER");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.Bulletin)
                    .WithMany(p => p.BPersNationalities)
                    .HasForeignKey(d => d.BulletinId)
                    .HasConstraintName("FK_B_PERS_NATIONALITIES_B_BULL");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.BPersNationalities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_B_PERS_NATIONALITIES_G_COUN");
            });

            modelBuilder.Entity<BProbation>(entity =>
            {
                entity.ToTable("B_PROBATIONS");

                entity.HasIndex(e => e.SanctProbCategId, "XIF1B_PROBATIONS");

                entity.HasIndex(e => e.SanctProbMeasureId, "XIF2B_PROBATIONS");

                entity.HasIndex(e => e.SanctionId, "XIF3B_PROBATIONS");

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

                entity.Property(e => e.DecisionDurationDays)
                    .HasPrecision(4)
                    .HasColumnName("DECISION_DURATION_DAYS");

                entity.Property(e => e.DecisionDurationHours)
                    .HasPrecision(4)
                    .HasColumnName("DECISION_DURATION_HOURS");

                entity.Property(e => e.DecisionDurationMonths)
                    .HasPrecision(4)
                    .HasColumnName("DECISION_DURATION_MONTHS");

                entity.Property(e => e.DecisionDurationYears)
                    .HasPrecision(4)
                    .HasColumnName("DECISION_DURATION_YEARS");

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

                entity.Property(e => e.SanctionId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SANCTION_ID");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.SanctProbCateg)
                    .WithMany(p => p.BProbations)
                    .HasForeignKey(d => d.SanctProbCategId)
                    .HasConstraintName("FK_B_PROBATIONS_B_SANCT_PROB_C");

                entity.HasOne(d => d.SanctProbMeasure)
                    .WithMany(p => p.BProbations)
                    .HasForeignKey(d => d.SanctProbMeasureId)
                    .HasConstraintName("FK_B_PROBATIONS_B_SANCT_PROB_M");

                entity.HasOne(d => d.Sanction)
                    .WithMany(p => p.BProbations)
                    .HasForeignKey(d => d.SanctionId)
                    .HasConstraintName("FK_B_PROBATIONS_B_SANCTIONS");
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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EXTERNAL_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EXTERNAL_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
            });

            modelBuilder.Entity<BSanction>(entity =>
            {
                entity.ToTable("B_SANCTIONS");

                entity.HasIndex(e => e.BulletinId, "XIF1B_SANCTIONS");

                entity.HasIndex(e => e.SanctCategoryId, "XIF2B_SANCTIONS");

                entity.HasIndex(e => e.EcrisSanctCategId, "XIF4B_SANCTIONS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.BulletinId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BULLETIN_ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.DecisionDurationDays)
                    .HasPrecision(4)
                    .HasColumnName("DECISION_DURATION_DAYS");

                entity.Property(e => e.DecisionDurationHours)
                    .HasPrecision(4)
                    .HasColumnName("DECISION_DURATION_HOURS");

                entity.Property(e => e.DecisionDurationMonths)
                    .HasPrecision(4)
                    .HasColumnName("DECISION_DURATION_MONTHS");

                entity.Property(e => e.DecisionDurationYears)
                    .HasPrecision(4)
                    .HasColumnName("DECISION_DURATION_YEARS");

                entity.Property(e => e.Descr).HasColumnName("DESCR");

                entity.Property(e => e.DetenctionDescr).HasColumnName("DETENCTION_DESCR");

                entity.Property(e => e.EcrisSanctCategId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_SANCT_CATEG_ID");

                entity.Property(e => e.FineAmount)
                    .HasColumnType("NUMBER(18,2)")
                    .HasColumnName("FINE_AMOUNT");

                entity.Property(e => e.SanctCategoryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SANCT_CATEGORY_ID");

                entity.Property(e => e.SuspentionDurationDays)
                    .HasPrecision(4)
                    .HasColumnName("SUSPENTION_DURATION_DAYS");

                entity.Property(e => e.SuspentionDurationHours)
                    .HasPrecision(4)
                    .HasColumnName("SUSPENTION_DURATION_HOURS");

                entity.Property(e => e.SuspentionDurationMonths)
                    .HasPrecision(4)
                    .HasColumnName("SUSPENTION_DURATION_MONTHS");

                entity.Property(e => e.SuspentionDurationYears)
                    .HasPrecision(4)
                    .HasColumnName("SUSPENTION_DURATION_YEARS");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.Bulletin)
                    .WithMany(p => p.BSanctions)
                    .HasForeignKey(d => d.BulletinId)
                    .HasConstraintName("FK_B_SANCTIONS_B_BULLETINS");

                entity.HasOne(d => d.EcrisSanctCateg)
                    .WithMany(p => p.BSanctions)
                    .HasForeignKey(d => d.EcrisSanctCategId)
                    .HasConstraintName("FK_B_SANCTIONS_B_ECRIS_STANCT_");

                entity.HasOne(d => d.SanctCategory)
                    .WithMany(p => p.BSanctions)
                    .HasForeignKey(d => d.SanctCategoryId)
                    .HasConstraintName("FK_B_SANCTIONS_B_SANCTION_CATE");
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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EXTERNAL_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EXTERNAL_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
            });

            modelBuilder.Entity<DCsDocRegister>(entity =>
            {
                entity.ToTable("D_CS_DOC_REGISTERS");

                entity.HasIndex(e => e.CsAuthorityId, "XIF1D_CS_DOC_REGISTERS");

                entity.HasIndex(e => e.DocRegisterId, "XIF2D_CS_DOC_REGISTERS");

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

                entity.Property(e => e.CsAuthorityId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CS_AUTHORITY_ID");

                entity.Property(e => e.DocRegisterId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DOC_REGISTER_ID");

                entity.Property(e => e.RegisterIndex)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("REGISTER_INDEX");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.CsAuthority)
                    .WithMany(p => p.DCsDocRegisters)
                    .HasForeignKey(d => d.CsAuthorityId)
                    .HasConstraintName("FK_D_CS_DOC_REGISTERS_G_CS_AUT");

                entity.HasOne(d => d.DocRegister)
                    .WithMany(p => p.DCsDocRegisters)
                    .HasForeignKey(d => d.DocRegisterId)
                    .HasConstraintName("FK_D_CS_DOC_REGISTERS_D_DOC_RE");
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

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Md5Hash).HasColumnName("MD5_HASH");

                entity.Property(e => e.MimeType)
                    .HasMaxLength(200)
                    .HasColumnName("MIME_TYPE");

                entity.Property(e => e.Sha1Hash)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("SHA1_HASH")
                    .IsFixedLength();

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
            });

            modelBuilder.Entity<DDocRegister>(entity =>
            {
                entity.ToTable("D_DOC_REGISTERS");

                entity.HasIndex(e => e.AppTypeId, "XIF1D_DOC_REGISTERS");

                entity.HasIndex(e => e.RegisterTypeId, "XIF3D_DOC_REGISTERS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.AppTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("APP_TYPE_ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.IsLocal)
                    .HasPrecision(1)
                    .HasColumnName("IS_LOCAL");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.RegisterIndex)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("REGISTER_INDEX");

                entity.Property(e => e.RegisterTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("REGISTER_TYPE_ID");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.Property(e => e.Year)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("YEAR");

                entity.HasOne(d => d.AppType)
                    .WithMany(p => p.DDocRegisters)
                    .HasForeignKey(d => d.AppTypeId)
                    .HasConstraintName("FK_D_DOC_REGISTERS_A_APPLICATI");

                entity.HasOne(d => d.RegisterType)
                    .WithMany(p => p.DDocRegisters)
                    .HasForeignKey(d => d.RegisterTypeId)
                    .HasConstraintName("FK_D_DOC_REGISTERS_D_REGISTER_");
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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.SystemCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SYSTEM_CODE");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

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

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

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

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

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
            });

            modelBuilder.Entity<DRegisterType>(entity =>
            {
                entity.ToTable("D_REGISTER_TYPES");

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

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

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

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

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

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
            });

            modelBuilder.Entity<EEcrisIdentification>(entity =>
            {
                entity.ToTable("E_ECRIS_IDENTIFICATION");

                entity.HasIndex(e => e.EcrisMsgId, "XIF1E_ECRIS_IDENTIFICATION");

                entity.HasIndex(e => e.GraoPersonId, "XIF3E_ECRIS_IDENTIFICATION");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Approved)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("APPROVED");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.EcrisMsgId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_MSG_ID");

                entity.Property(e => e.GraoPersonId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("GRAO_PERSON_ID");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.EcrisMsg)
                    .WithMany(p => p.EEcrisIdentifications)
                    .HasForeignKey(d => d.EcrisMsgId)
                    .HasConstraintName("FK_E_ECRIS_IDENTIFICATION_E_EC");

                entity.HasOne(d => d.GraoPerson)
                    .WithMany(p => p.EEcrisIdentifications)
                    .HasForeignKey(d => d.GraoPersonId)
                    .HasConstraintName("FK_E_ECRIS_IDENTIFICATION_GRAO");
            });

            modelBuilder.Entity<EEcrisInbox>(entity =>
            {
                entity.ToTable("E_ECRIS_INBOX");

                entity.HasIndex(e => e.EcrisMsgId, "XIF1E_ECRIS_INBOX");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.EcrisMsgId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_MSG_ID");

                entity.Property(e => e.ImportedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("IMPORTED_ON");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.Property(e => e.XmlMessage)
                    .HasColumnType("CLOB")
                    .HasColumnName("XML_MESSAGE");

                entity.Property(e => e.XmlMessageTraits)
                    .HasColumnType("CLOB")
                    .HasColumnName("XML_MESSAGE_TRAITS");

                entity.HasOne(d => d.EcrisMsg)
                    .WithMany(p => p.EEcrisInboxes)
                    .HasForeignKey(d => d.EcrisMsgId)
                    .HasConstraintName("FK_E_ECRIS_INBOX_E_ECRIS_MESSA");
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

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Deadline)
                    .HasColumnType("DATE")
                    .HasColumnName("DEADLINE");

                entity.Property(e => e.EcrisIdentifier)
                    .HasMaxLength(100)
                    .HasColumnName("ECRIS_IDENTIFIER");

                entity.Property(e => e.EcrisMsgConvictionId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_MSG_CONVICTION_ID");

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

                entity.Property(e => e.Pin)
                    .HasMaxLength(100)
                    .HasColumnName("PIN");

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

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Urgent)
                    .HasPrecision(1)
                    .HasColumnName("URGENT");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

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

            modelBuilder.Entity<EEcrisOutbox>(entity =>
            {
                entity.ToTable("E_ECRIS_OUTBOX");

                entity.HasIndex(e => e.EcrisMsgId, "XIF1E_ECRIS_OUTBOX");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Attempts)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ATTEMPTS");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.EcrisMsgId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_MSG_ID");

                entity.Property(e => e.Error).HasColumnName("ERROR");

                entity.Property(e => e.ExecutionDate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXECUTION_DATE");

                entity.Property(e => e.HasError)
                    .HasPrecision(1)
                    .HasColumnName("HAS_ERROR");

                entity.Property(e => e.Operation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OPERATION");

                entity.Property(e => e.StackTrace)
                    .HasColumnType("CLOB")
                    .HasColumnName("STACK_TRACE");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.Property(e => e.XmlObject)
                    .HasColumnType("CLOB")
                    .HasColumnName("XML_OBJECT");

                entity.HasOne(d => d.EcrisMsg)
                    .WithMany(p => p.EEcrisOutboxes)
                    .HasForeignKey(d => d.EcrisMsgId)
                    .HasConstraintName("FK_E_ECRIS_OUTBOX_E_ECRIS_MESS");
            });

            modelBuilder.Entity<EIsinDatum>(entity =>
            {
                entity.ToTable("E_ISIN_DATA");

                entity.HasIndex(e => e.BulletinId, "XIF2E_ISIN_DATA");

                entity.HasIndex(e => e.WebRequestId, "XIF3E_ISIN_DATA");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.BirthPlace).HasColumnName("BIRTH_PLACE");

                entity.Property(e => e.BirthcountryCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BIRTHCOUNTRY_CODE");

                entity.Property(e => e.BirthcountryName)
                    .HasMaxLength(200)
                    .HasColumnName("BIRTHCOUNTRY_NAME");

                entity.Property(e => e.Birthdate)
                    .HasColumnType("DATE")
                    .HasColumnName("BIRTHDATE");

                entity.Property(e => e.BulletinId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BULLETIN_ID");

                entity.Property(e => e.CaseAuthCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CASE_AUTH_CODE");

                entity.Property(e => e.CaseAuthName)
                    .HasMaxLength(200)
                    .HasColumnName("CASE_AUTH_NAME");

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

                entity.Property(e => e.Country1Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY1_CODE");

                entity.Property(e => e.Country1Name)
                    .HasMaxLength(200)
                    .HasColumnName("COUNTRY1_NAME");

                entity.Property(e => e.Country2Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY2_CODE");

                entity.Property(e => e.Country2Name)
                    .HasMaxLength(200)
                    .HasColumnName("COUNTRY2_NAME");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.DecisionAuthCode)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("DECISION_AUTH_CODE")
                    .IsFixedLength();

                entity.Property(e => e.DecisionAuthName)
                    .HasMaxLength(200)
                    .HasColumnName("DECISION_AUTH_NAME");

                entity.Property(e => e.DecisionDate)
                    .HasColumnType("DATE")
                    .HasColumnName("DECISION_DATE");

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

                entity.Property(e => e.Familyname)
                    .HasMaxLength(200)
                    .HasColumnName("FAMILYNAME");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(200)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Identifier)
                    .HasMaxLength(100)
                    .HasColumnName("IDENTIFIER");

                entity.Property(e => e.IdentifierType)
                    .HasMaxLength(200)
                    .HasColumnName("IDENTIFIER_TYPE");

                entity.Property(e => e.SanctionEndDate)
                    .HasColumnType("DATE")
                    .HasColumnName("SANCTION_END_DATE");

                entity.Property(e => e.SanctionStartDate)
                    .HasColumnType("DATE")
                    .HasColumnName("SANCTION_START_DATE");

                entity.Property(e => e.Sex)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SEX");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

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

                entity.Property(e => e.WebRequestId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("WEB_REQUEST_ID");

                entity.HasOne(d => d.Bulletin)
                    .WithMany(p => p.EIsinData)
                    .HasForeignKey(d => d.BulletinId)
                    .HasConstraintName("FK_E_ISIN_DATA_B_BULLETINS");

                entity.HasOne(d => d.WebRequest)
                    .WithMany(p => p.EIsinData)
                    .HasForeignKey(d => d.WebRequestId)
                    .HasConstraintName("FK_E_ISIN_DATA_E_WEB_REQUESTS");
            });

            modelBuilder.Entity<ERegixCache>(entity =>
            {
                entity.ToTable("E_REGIX_CACHE");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Alias).HasColumnName("ALIAS");

                entity.Property(e => e.BirthCityName).HasColumnName("BIRTH_CITY_NAME");

                entity.Property(e => e.BirthCountryCode).HasColumnName("BIRTH_COUNTRY_CODE");

                entity.Property(e => e.BirthCountryName).HasColumnName("BIRTH_COUNTRY_NAME");

                entity.Property(e => e.BirthCountryNameLat).HasColumnName("BIRTH_COUNTRY_NAME_LAT");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("DATE")
                    .HasColumnName("BIRTH_DATE");

                entity.Property(e => e.BirthDistrictName).HasColumnName("BIRTH_DISTRICT_NAME");

                entity.Property(e => e.BirthMunName).HasColumnName("BIRTH_MUN_NAME");

                entity.Property(e => e.BirthPlace).HasColumnName("BIRTH_PLACE");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Egn)
                    .HasMaxLength(100)
                    .HasColumnName("EGN");

                entity.Property(e => e.ExecutionDate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXECUTION_DATE");

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

                entity.Property(e => e.Lnch)
                    .HasMaxLength(100)
                    .HasColumnName("LNCH");

                entity.Property(e => e.RequestXml)
                    .HasColumnType("CLOB")
                    .HasColumnName("REQUEST_XML");

                entity.Property(e => e.ResponseXml)
                    .HasColumnType("CLOB")
                    .HasColumnName("RESPONSE_XML");

                entity.Property(e => e.Surname)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME");

                entity.Property(e => e.SurnameLat)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME_LAT");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.Property(e => e.WebServiceName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("WEB_SERVICE_NAME");
            });

            modelBuilder.Entity<ESynchronizationParameter>(entity =>
            {
                entity.ToTable("E_SYNCHRONIZATION_PARAMETERS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.LastDate)
                    .HasPrecision(6)
                    .HasColumnName("LAST_DATE");

                entity.Property(e => e.LastId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("LAST_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
            });

            modelBuilder.Entity<EWebRequest>(entity =>
            {
                entity.ToTable("E_WEB_REQUESTS");

                entity.HasIndex(e => e.BulletinId, "XIF1E_WEB_REQUESTS");

                entity.HasIndex(e => e.ApplicationId, "XIF2E_WEB_REQUESTS");

                entity.HasIndex(e => e.EcrisMsgId, "XIF3E_WEB_REQUESTS");

                entity.HasIndex(e => e.WebServiceId, "XIF4E_WEB_REQUESTS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.ApiServiceCallId)
                    .HasMaxLength(100)
                    .HasColumnName("API_SERVICE_CALL_ID");

                entity.Property(e => e.ApplicationId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("APPLICATION_ID");

                entity.Property(e => e.Attempts)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ATTEMPTS");

                entity.Property(e => e.BulletinId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BULLETIN_ID");

                entity.Property(e => e.CallContext)
                    .HasColumnType("CLOB")
                    .HasColumnName("CALL_CONTEXT");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.EcrisMsgId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_MSG_ID");

                entity.Property(e => e.Error).HasColumnName("ERROR");

                entity.Property(e => e.ExecutionDate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXECUTION_DATE");

                entity.Property(e => e.HasError)
                    .HasPrecision(1)
                    .HasColumnName("HAS_ERROR");

                entity.Property(e => e.IsFromCache)
                    .HasPrecision(1)
                    .HasColumnName("IS_FROM_CACHE");

                entity.Property(e => e.RemoteAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("REMOTE_ADDRESS");

                entity.Property(e => e.RequestXml)
                    .HasColumnType("CLOB")
                    .HasColumnName("REQUEST_XML");

                entity.Property(e => e.ResponseXml)
                    .HasColumnType("CLOB")
                    .HasColumnName("RESPONSE_XML");

                entity.Property(e => e.StackTrace)
                    .HasColumnType("CLOB")
                    .HasColumnName("STACK_TRACE");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.Property(e => e.WebServiceId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("WEB_SERVICE_ID");

                entity.HasOne(d => d.Bulletin)
                    .WithMany(p => p.EWebRequests)
                    .HasForeignKey(d => d.BulletinId)
                    .HasConstraintName("FK_E_WEB_REQUESTS_B_BULLETINS");

                entity.HasOne(d => d.EcrisMsg)
                    .WithMany(p => p.EWebRequests)
                    .HasForeignKey(d => d.EcrisMsgId)
                    .HasConstraintName("FK_E_WEB_REQUESTS_E_ECRIS_MESS");

                entity.HasOne(d => d.WebService)
                    .WithMany(p => p.EWebRequests)
                    .HasForeignKey(d => d.WebServiceId)
                    .HasConstraintName("FK_E_WEB_REQUESTS_E_WEB_SERVIC");
            });

            modelBuilder.Entity<EWebService>(entity =>
            {
                entity.ToTable("E_WEB_SERVICES");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.TypeCode)
                    .HasMaxLength(200)
                    .HasColumnName("TYPE_CODE");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.Property(e => e.WebServiceName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("WEB_SERVICE_NAME");
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

                entity.HasIndex(e => e.EcrisConvId, "XUKFBBC")
                    .IsUnique();

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
                    .HasPrecision(1)
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
                    .HasConstraintName("FK_FBBC_P_PERSON_IDS");

                entity.HasOne(d => d.SanctionType)
                    .WithMany(p => p.Fbbcs)
                    .HasForeignKey(d => d.SanctionTypeId)
                    .HasConstraintName("FK_FBBC_FBBC_SANCT_TYPES");

                entity.HasOne(d => d.StatusCodeNavigation)
                    .WithMany(p => p.Fbbcs)
                    .HasForeignKey(d => d.StatusCode)
                    .HasConstraintName("FK_FBBC_FBBC_STATUSES");
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

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
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

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

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

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

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

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

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

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

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

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.UsedForNationality)
                    .HasPrecision(1)
                    .HasColumnName("USED_FOR_NATIONALITY");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.DecidingAuthId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DECIDING_AUTH_ID");

                entity.Property(e => e.IsCentral)
                    .HasPrecision(1)
                    .HasColumnName("IS_CENTRAL");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.OldId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OLD_ID");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

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
                    .HasPrecision(1)
                    .HasColumnName("ACTIVE_FOR_BULLETINS");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .HasColumnName("CODE");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

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
                    .HasPrecision(1)
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

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("DATE")
                    .HasColumnName("VALID_TO");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.Property(e => e.Visible)
                    .HasPrecision(1)
                    .HasColumnName("VISIBLE");
            });

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

            modelBuilder.Entity<GNomenclature>(entity =>
            {
                entity.ToTable("G_NOMENCLATURES");

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

                entity.Property(e => e.TableName)
                    .HasMaxLength(200)
                    .HasColumnName("TABLE_NAME");

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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

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

            modelBuilder.Entity<GUser>(entity =>
            {
                entity.ToTable("G_USERS");

                entity.HasIndex(e => e.CsAuthorityId, "XIF1G_USERS");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Active)
                    .HasPrecision(1)
                    .HasColumnName("ACTIVE");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

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

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

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

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(200)
                    .HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.GUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_G_USER_ROLES_G_ROLES");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_G_USER_ROLES_G_USERS");
            });

            modelBuilder.Entity<GUsersCitizen>(entity =>
            {
                entity.ToTable("G_USERS_CITIZEN");

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

                entity.Property(e => e.Egn)
                    .HasMaxLength(100)
                    .HasColumnName("EGN");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

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

                entity.HasOne(d => d.Administration)
                    .WithMany(p => p.GUsersExts)
                    .HasForeignKey(d => d.AdministrationId)
                    .HasConstraintName("FK_G_USERS_EXT_G_EXT_ADMINISTR");
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

            modelBuilder.Entity<PAppId>(entity =>
            {
                entity.ToTable("P_APP_IDS");

                entity.HasIndex(e => e.ApplicationId, "XIF1P_APP_IDS");

                entity.HasIndex(e => e.PersonId, "XIF2P_APP_IDS");

                entity.HasIndex(e => new { e.ApplicationId, e.PersonId }, "XUKP_APP_IDS")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.ApplicationId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("APPLICATION_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.PersonId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PERSON_ID");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.PAppIds)
                    .HasForeignKey(d => d.ApplicationId)
                    .HasConstraintName("FK_P_APP_IDS_A_APPLICATIONS");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PAppIds)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_P_APP_IDS_P_PERSON_IDS");
            });

            modelBuilder.Entity<PBulletinId>(entity =>
            {
                entity.ToTable("P_BULLETIN_IDS");

                entity.HasIndex(e => e.PersonId, "XIF1P_BULLETIN_IDS");

                entity.HasIndex(e => e.BulletinId, "XIF2P_BULLETIN_IDS");

                entity.HasIndex(e => new { e.PersonId, e.BulletinId }, "XUKP_BULLETIN_IDS")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.BulletinId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BULLETIN_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.PersonId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PERSON_ID");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.Bulletin)
                    .WithMany(p => p.PBulletinIds)
                    .HasForeignKey(d => d.BulletinId)
                    .HasConstraintName("FK_P_BULLETIN_IDS_B_BULLETINS");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PBulletinIds)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_P_BULLETIN_IDS_P_PERSON_IDS");
            });

            modelBuilder.Entity<PPersionIdsH>(entity =>
            {
                entity.ToTable("P_PERSION_IDS_H");

                entity.HasIndex(e => e.PersonHId, "XIF1P_PERSION_IDS_H");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.CountryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Issuer)
                    .HasMaxLength(200)
                    .HasColumnName("ISSUER");

                entity.Property(e => e.PersonHId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PERSON_H_ID");

                entity.Property(e => e.PersonId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PERSON_ID");

                entity.Property(e => e.Pid)
                    .HasMaxLength(500)
                    .HasColumnName("PID");

                entity.Property(e => e.PidTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PID_TYPE_ID");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.PersonH)
                    .WithMany(p => p.PPersionIdsHes)
                    .HasForeignKey(d => d.PersonHId)
                    .HasConstraintName("FK_P_PERSION_IDS_H_P_PERSON_H");
            });

            modelBuilder.Entity<PPerson>(entity =>
            {
                entity.ToTable("P_PERSON");

                entity.HasIndex(e => e.BirthCityId, "XIF2P_PERSON");

                entity.HasIndex(e => e.BirthCountryId, "XIF3P_PERSON");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

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

                entity.Property(e => e.BirthPlaceOther).HasColumnName("BIRTH_PLACE_OTHER");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

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

                entity.Property(e => e.Sex)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SEX");

                entity.Property(e => e.Surname)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME");

                entity.Property(e => e.SurnameLat)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME_LAT");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.BirthCity)
                    .WithMany(p => p.PPeople)
                    .HasForeignKey(d => d.BirthCityId)
                    .HasConstraintName("FK_P_PERSON_G_CITIES");

                entity.HasOne(d => d.BirthCountry)
                    .WithMany(p => p.PPeople)
                    .HasForeignKey(d => d.BirthCountryId)
                    .HasConstraintName("FK_P_PERSON_G_COUNTRIES");
            });

            modelBuilder.Entity<PPersonH>(entity =>
            {
                entity.ToTable("P_PERSON_H");

                entity.HasIndex(e => e.BirthCountryId, "XIF2P_PERSON_H");

                entity.HasIndex(e => e.BirthCityId, "XIF3P_PERSON_H");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

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

                entity.Property(e => e.BirthPlaceOther).HasColumnName("BIRTH_PLACE_OTHER");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

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

                entity.Property(e => e.Sex)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SEX");

                entity.Property(e => e.Surname)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME");

                entity.Property(e => e.SurnameLat)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME_LAT");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.BirthCity)
                    .WithMany(p => p.PPersonHs)
                    .HasForeignKey(d => d.BirthCityId)
                    .HasConstraintName("FK_P_PERSON_H_G_CITIES");

                entity.HasOne(d => d.BirthCountry)
                    .WithMany(p => p.PPersonHs)
                    .HasForeignKey(d => d.BirthCountryId)
                    .HasConstraintName("FK_P_PERSON_H_G_COUNTRIES");
            });

            modelBuilder.Entity<PPersonId>(entity =>
            {
                entity.ToTable("P_PERSON_IDS");

                entity.HasIndex(e => e.PidTypeId, "XIF3P_PERSON_IDS");

                entity.HasIndex(e => e.CountryId, "XIF4P_PERSON_IDS");

                entity.HasIndex(e => e.PersonId, "XIF5P_PERSON_IDS");

                entity.HasIndex(e => new { e.Pid, e.PidTypeId, e.Issuer, e.CountryId }, "XUKP_PERSON_IDS")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.CountryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Issuer)
                    .HasMaxLength(200)
                    .HasColumnName("ISSUER");

                entity.Property(e => e.PersonId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PERSON_ID");

                entity.Property(e => e.Pid)
                    .HasMaxLength(500)
                    .HasColumnName("PID");

                entity.Property(e => e.PidTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PID_TYPE_ID");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.PPersonIds)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_P_PERSON_IDS_G_COUNTRIES");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PPersonIds)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_P_PERSON_IDS_P_PERSON");

                entity.HasOne(d => d.PidType)
                    .WithMany(p => p.PPersonIds)
                    .HasForeignKey(d => d.PidTypeId)
                    .HasConstraintName("FK_P_PERSON_IDS_P_PERSON_ID_TY");
            });

            modelBuilder.Entity<PPersonIdType>(entity =>
            {
                entity.ToTable("P_PERSON_ID_TYPE");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("NAME");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
            });

            modelBuilder.Entity<PPersonOld>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("P_PERSON_OLD");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("DATE")
                    .HasColumnName("UPDATED_ON");

                entity.Property(e => e.Version)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("VERSION");
            });

            modelBuilder.Entity<ZImportFbbc>(entity =>
            {
                entity.ToTable("Z_IMPORT_FBBC");

                entity.HasIndex(e => e.RegdocType, "Z_IMP_FBBC_TYPE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID");

                entity.Property(e => e.Anotacia)
                    .HasColumnType("CLOB")
                    .HasColumnName("ANOTACIA");

                entity.Property(e => e.BirthCity)
                    .HasMaxLength(200)
                    .HasColumnName("BIRTH_CITY");

                entity.Property(e => e.BirthCountry1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BIRTH_COUNTRY");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("DATE")
                    .HasColumnName("BIRTH_DATE");

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

                entity.Property(e => e.EcrisIdentifier)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_IDENTIFIER");

                entity.Property(e => e.EcrisMsgConvictionId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_MSG_CONVICTION_ID");

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

                entity.Property(e => e.Familyname)
                    .HasMaxLength(200)
                    .HasColumnName("FAMILYNAME");

                entity.Property(e => e.FatherFamily)
                    .HasMaxLength(200)
                    .HasColumnName("FATHER_FAMILY");

                entity.Property(e => e.FatherFname)
                    .HasMaxLength(200)
                    .HasColumnName("FATHER_FNAME");

                entity.Property(e => e.FatherMname)
                    .HasMaxLength(200)
                    .HasColumnName("FATHER_MNAME");

                entity.Property(e => e.FbbcId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FBBC_ID");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(200)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.FlagAdmPenality)
                    .HasMaxLength(1)
                    .HasColumnName("FLAG_ADM_PENALITY");

                entity.Property(e => e.Fname)
                    .HasMaxLength(200)
                    .HasColumnName("FNAME");

                entity.Property(e => e.FromAuthId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FROM_AUTH_ID");

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

                entity.Property(e => e.Identifier)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IDENTIFIER");

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

                entity.Property(e => e.MsgTimestamp)
                    .HasColumnType("DATE")
                    .HasColumnName("MSG_TIMESTAMP");

                entity.Property(e => e.Nationality1Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NATIONALITY1_CODE");

                entity.Property(e => e.Nationality2Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NATIONALITY2_CODE");

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

            modelBuilder.Entity<ZImportFbbcTest>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Z_IMPORT_FBBC_TEST");

                entity.Property(e => e.Anotacia)
                    .HasColumnType("CLOB")
                    .HasColumnName("ANOTACIA");

                entity.Property(e => e.BirthCity)
                    .HasMaxLength(200)
                    .HasColumnName("BIRTH_CITY");

                entity.Property(e => e.BirthCountry1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BIRTH_COUNTRY");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("DATE")
                    .HasColumnName("BIRTH_DATE");

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

                entity.Property(e => e.EcrisIdentifier)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_IDENTIFIER");

                entity.Property(e => e.EcrisMsgConvictionId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ECRIS_MSG_CONVICTION_ID");

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

                entity.Property(e => e.Familyname)
                    .HasMaxLength(200)
                    .HasColumnName("FAMILYNAME");

                entity.Property(e => e.FatherFamily)
                    .HasMaxLength(200)
                    .HasColumnName("FATHER_FAMILY");

                entity.Property(e => e.FatherFname)
                    .HasMaxLength(200)
                    .HasColumnName("FATHER_FNAME");

                entity.Property(e => e.FatherMname)
                    .HasMaxLength(200)
                    .HasColumnName("FATHER_MNAME");

                entity.Property(e => e.FbbcId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FBBC_ID");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(200)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.FlagAdmPenality)
                    .HasMaxLength(1)
                    .HasColumnName("FLAG_ADM_PENALITY");

                entity.Property(e => e.Fname)
                    .HasMaxLength(200)
                    .HasColumnName("FNAME");

                entity.Property(e => e.FromAuthId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FROM_AUTH_ID");

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

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID");

                entity.Property(e => e.Identifier)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IDENTIFIER");

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

                entity.Property(e => e.MsgTimestamp)
                    .HasColumnType("DATE")
                    .HasColumnName("MSG_TIMESTAMP");

                entity.Property(e => e.Nationality1Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NATIONALITY1_CODE");

                entity.Property(e => e.Nationality2Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NATIONALITY2_CODE");

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

                entity.Property(e => e.Sex)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SEX");

                entity.Property(e => e.Surname)
                    .HasMaxLength(200)
                    .HasColumnName("SURNAME");

                entity.Property(e => e.ToAuthId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TO_AUTH_ID");

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
