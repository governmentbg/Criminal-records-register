using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.DTO.WApplicaiton;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;
using static MJ_CAIS.Common.Constants.ApplicationConstants;

namespace MJ_CAIS.Services
{
    public class WApplicationService : BaseAsyncService<WApplicaitonDTO, WApplicaitonDTO, WApplicaitonGridDTO, WApplication, string, CaisDbContext>, IWApplicationService
    {
        private readonly IWApplicationRepository _wApplicationRepository;
        private readonly IApplicationWebService _webApplicationService;
        private readonly IRegisterTypeService _registerTypeService;
        private readonly IApplicationService _applicationService;
        private readonly IManagePersonService _managePersonService;

        public WApplicationService(IMapper mapper,
            IWApplicationRepository wApplicationRepository,
            IApplicationWebService webApplicationService,
            IRegisterTypeService registerTypeService,
            IApplicationService applicationService,
            IManagePersonService managePersonService)
            : base(mapper, wApplicationRepository)
        {
            _wApplicationRepository = wApplicationRepository;
            _webApplicationService = webApplicationService;
            _registerTypeService = registerTypeService;
            _applicationService = applicationService;
            _managePersonService = managePersonService;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public async Task<IgPageResult<WApplicaitonGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<WApplicaitonGridDTO> aQueryOptions, string? statusId)
        {
            var baseQuery = new List<WApplicaitonGridDTO>().AsQueryable();

            if (statusId is ApplicationWebStatuses.WebCheckPayment)
            {
                baseQuery = _wApplicationRepository.SelectAllForCheckPayment();
            }
            else if (statusId is ApplicationWebStatuses.WebCheckTaxFree)
            {
                var entityQuery = GetSelectAllQueriable();
                entityQuery = entityQuery.Where(x => x.StatusCode == statusId);
                baseQuery = entityQuery.ProjectTo<WApplicaitonGridDTO>(mapperConfiguration);
            }
            else
            {
                var entityQuery = GetSelectAllQueriable();
                baseQuery = entityQuery.ProjectTo<WApplicaitonGridDTO>(mapperConfiguration);
            }

            var resultQuery = await ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<WApplicaitonGridDTO>();
            PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public async Task ConfirmPaymentAsync(string aId)
        {
            var ePaymentsQuery = _wApplicationRepository.GetPendingPaymentsByWAppId(aId);
            var ePayments = await ePaymentsQuery.ToListAsync();
            foreach (var ePayment in ePayments)
            {
                ePayment.PaymentStatus = PaymentConstants.PaymentStatuses.Payed;
                ePayment.EntityState = EntityStateEnum.Modified;
                ePayment.ModifiedProperties = new List<string> { nameof(ePayment.PaymentStatus), nameof(ePayment.Version) };
            }

            await dbContext.SaveEntityListAsync(ePayments);
        }

        public async Task ProcessTaxFreeAsync(string aId, bool approved)
        {
            // tracked entities
            var wApp = await _wApplicationRepository.SelectAsync(aId);

            if (approved)
            {
                var statusWebApprovedApplication = await dbContext.WApplicationStatuses.FirstOrDefaultAsync(a => a.Code == ApplicationStatuses.WebApprovedApplication);
                var statusApprovedApplication = await dbContext.AApplicationStatuses.FirstOrDefaultAsync(a => a.Code == ApplicationStatuses.ApprovedApplication);
                await ProcessWebApplicationToApplicationAsync(wApp, statusWebApprovedApplication, statusApprovedApplication);              
                await dbContext.SaveChangesAsync();
                return;
            }

            wApp.StatusCode = ApplicationStatuses.WebCheckPayment;
            dbContext.WApplications.Update(wApp);

            var ePayment = new EPayment()
            {
                Id = BaseEntity.GenerateNewId(),
                Amount = wApp.ApplicationType.Price,
                PaymentStatus = PaymentConstants.PaymentStatuses.Pending,
                InvoiceNumber = wApp.RegistrationNumber
            };

            var aPayment = new APayment()
            {
                Id = BaseEntity.GenerateNewId(),
                WApplicationId = aId,
                EPaymentId = ePayment.Id,
                EPayment = ePayment
            };

            dbContext.APayments.Add(aPayment);
            dbContext.EPayments.Add(ePayment);
            await dbContext.SaveChangesAsync();      
        }

        public async Task<PPerson> ProcessWebApplicationToApplicationAsync(WApplication wapplication, WApplicationStatus wapplicationStatus, AApplicationStatus applicationStatus)
        {
            //wapplication.StatusCode = ApplicationConstants.ApplicationStatuses.WebApprovedApplication;
            _webApplicationService.SetWApplicationStatus(wapplication, wapplicationStatus, ApplicationResources.descAprovedApp);
            string regNumber = "";

            if (wapplication.ApplicationType.Code == ApplicationConstants.ApplicationTypes.WebCertificate)
            {
                regNumber = await _registerTypeService.GetRegisterNumberForCertificateWeb(wapplication.CsAuthorityId);
            }
            if (wapplication.ApplicationType.Code == ApplicationConstants.ApplicationTypes.WebExternalCertificate)
            {
                regNumber = await _registerTypeService.GetRegisterNumberForCertificateWebExternal(wapplication.CsAuthorityId);
            }

            AApplication appl = new AApplication()
            {
                Id = BaseEntity.GenerateNewId(),
                AddrDistrict = wapplication.AddrDistrict,
                AddrEmail = wapplication.AddrEmail,
                AddrPhone = wapplication.AddrPhone,
                Address = wapplication.Address,
                AddrName = wapplication.AddrName,
                AddrState = wapplication.AddrState,
                AddrStr = wapplication.AddrStr,
                AddrTown = wapplication.AddrTown,
                ApplicantName = wapplication.ApplicantName,
                BirthCityId = wapplication.BirthCityId,
                BirthCountryId = wapplication.BirthCountryId,
                BirthDate = wapplication.BirthDate,
                BirthDatePrecision = wapplication.BirthDatePrecision,
                BirthPlaceOther = wapplication.BirthPlaceOther,
                CsAuthorityBirthId = wapplication.CsAuthorityBirthId,
                CsAuthorityId = wapplication.CsAuthorityId,
                Description = wapplication.Description,
                Egn = wapplication.Egn,
                Email = wapplication.Email,
                Familyname = wapplication.Familyname,
                FatherFamilyname = wapplication.FatherFamilyname,
                FamilynameLat = wapplication.FamilynameLat,
                FatherFirstname = wapplication.FatherFirstname,
                FatherSurname = wapplication.FatherSurname,
                FatherFullname = wapplication.FatherFullname,
                Firstname = wapplication.Firstname,
                FirstnameLat = wapplication.FirstnameLat,
                FromCosul = wapplication.FromCosul,
                Fullname = wapplication.Fullname,
                FullnameLat = wapplication.FullnameLat,
                IsLocal = wapplication.IsLocal,
                Ln = wapplication.Ln,
                Lnch = wapplication.Lnch,
                MotherFamilyname = wapplication.MotherFamilyname,
                MotherFirstname = wapplication.MotherFirstname,
                MotherSurname = wapplication.MotherSurname,
                MotherFullname = wapplication.MotherFullname,
                PaymentMethodId = wapplication.PaymentMethodId,
                PurposeId = wapplication.PurposeId,
                Sex = wapplication.Sex,
                SrvcResRcptMethId = wapplication.SrvcResRcptMethId,
                // StatusCode = ApplicationConstants.ApplicationStatuses.ApprovedApplication,
                Surname = wapplication.Surname,
                SurnameLat = wapplication.SurnameLat,
                UserCitizenId = wapplication.UserCitizenId,
                UserExtId = wapplication.UserExtId,
                UserId = wapplication.UserId,
                WApplicationId = wapplication.Id,
                RegistrationNumber = regNumber,
                ApplicationType = wapplication.ApplicationType,
                ApplicationTypeId = wapplication.ApplicationTypeId,
            };

            _applicationService.SetApplicationStatus(appl, applicationStatus, ApplicationResources.descApplicationFromWeb);

            PersonDTO personDto = new PersonDTO();

            personDto.Egn = wapplication.Egn;
            personDto.Firstname = wapplication.Firstname;
            personDto.Surname = wapplication.Surname;
            personDto.Familyname = wapplication.Familyname;
            personDto.BirthDate = wapplication.BirthDate;
            personDto.FirstnameLat = wapplication.FirstnameLat;
            personDto.SurnameLat = wapplication.SurnameLat;
            personDto.FamilynameLat = wapplication.FamilynameLat;
            personDto.FatherFamilyname = wapplication.FatherFamilyname;
            personDto.FatherFirstname = wapplication.FatherFirstname;
            personDto.FatherFullname = wapplication.FatherFullname;
            personDto.FatherSurname = wapplication.FatherSurname;
            personDto.MotherFirstname = wapplication.MotherFirstname;
            personDto.MotherSurname = wapplication.MotherSurname;
            personDto.MotherFamilyname = wapplication.MotherFamilyname;
            personDto.MotherFullname = wapplication.MotherFullname;
            personDto.Sex = wapplication.Sex;

            var person = await _managePersonService.CreatePersonAsync(personDto);

            // var idpid = await dbContext.PPersonIds.FirstOrDefaultAsync(x => x.Issuer == PersonConstants.IssuerType.GRAO && x.PidTypeId == PersonConstants.PidType.Egn 
            //                                             && x.CountryId == PersonConstants.BG && x.Pid == appl.Egn);

            appl.EgnId = person.PPersonIds.First(x => x.PidTypeId == PersonConstants.PidType.Egn).Id;
            appl.EgnNavigation = person.PPersonIds.First(x => x.PidTypeId == PersonConstants.PidType.Egn);

            //foreach (var v in wapplication.AAppCitizenships)
            //{
            //    var newObj = new AAppCitizenship()
            //    {
            //        Id = BaseEntity.GenerateNewId(),
            //        ApplicationId = appl.Id,
            //        CountryId = v.CountryId
            //    };
            //    appl.AAppCitizenships.Add(newObj);

            //}
            //todo: какво е това
            // appl.AAppPersAliases

            //appl.PAppIds = await dbContext.PPersonIds.Where(x => (x.PidTypeId == PersonConstants.PidType.Egn && x.Issuer == PersonConstants.IssuerType.GRAO && x.Pid == appl.Egn)
            //                                || (x.PidTypeId == PersonConstants.PidType.Lnch && x.Issuer == PersonConstants.IssuerType.MVR && x.Pid == appl.Lnch)
            //                                || (x.PidTypeId == PersonConstants.PidType.Ln && x.Issuer == PersonConstants.IssuerType.CAIS && x.Pid == appl.Ln))
            //                     .Select(x => new PAppId
            //                     {
            //                         ApplicationId = appl.Id,
            //                         Id = BaseEntity.GenerateNewId(),
            //                         PersonId = x.Id
            //                     }).ToListAsync();

            //TODO: при неколкократно извикване на метода за един и същ човек
            //се променя статуса на Added за всички траквани обекти.
            //причината е различния начин на ползване на дб контекста
            //в personService на ред 349 dbContext.ApplyChanges(personToUpdate, new List<IBaseIdEntity>(), true); 
            //предизвиква проблема
            //не зная защо само това ентити, а не и aapplication...?!
            foreach (var entity in dbContext.ChangeTracker.Entries<AStatusH>())
            {
                if (entity.Entity.ApplicationId != appl.Id)
                {
                    if (entity.Entity != null && entity.Entity.EntityState != EntityStateEnum.Detached)
                    {
                        dbContext.Entry(entity.Entity).State = EntityState.Detached;
                    }
                }
            }
            
            dbContext.AApplications.Add(appl);
            //dbContext.AStatusHes.AddRange(appl.AStatusHes);
            // dbContext.PAppIds.AddRange(appl.PAppIds);
            dbContext.WApplications.Update(wapplication);

            return person;
        }
    }
}
