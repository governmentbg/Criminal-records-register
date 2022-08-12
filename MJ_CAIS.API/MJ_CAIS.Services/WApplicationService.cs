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
                var entityQuery = GetSelectAllQueryable();
                entityQuery = entityQuery.Where(x => x.StatusCode == statusId);
                baseQuery = entityQuery.ProjectTo<WApplicaitonGridDTO>(mapperConfiguration);
            }
            else
            {
                var entityQuery = GetSelectAllQueryable();
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

            await _wApplicationRepository.SaveEntityListAsync(ePayments);
        }

        public async Task ProcessTaxFreeAsync(string aId, bool approved)
        {
            // tracked entities
            var wApp = await _wApplicationRepository.SelectAsync(aId);

            if (approved)
            {
                var statusWebApprovedApplication = await _wApplicationRepository.SingleOrDefaultAsync<WApplicationStatus>(a => a.Code == ApplicationStatuses.WebApprovedApplication);
                //    await dbContext.WApplicationStatuses.FirstOrDefaultAsync(a => a.Code == ApplicationStatuses.WebApprovedApplication);
                var statusApprovedApplication = await _wApplicationRepository.SingleOrDefaultAsync<AApplicationStatus>(a => a.Code == ApplicationStatuses.ApprovedApplication);
                //await dbContext.AApplicationStatuses.FirstOrDefaultAsync(a => a.Code == ApplicationStatuses.ApprovedApplication);
                await ProcessWebApplicationToApplicationAsync(wApp, statusWebApprovedApplication, statusApprovedApplication);              
                await _wApplicationRepository.SaveChangesAsync();
                return;
            }

            wApp.StatusCode = ApplicationStatuses.WebCheckPayment;
            wApp.EntityState = EntityStateEnum.Modified;
            if (wApp.ModifiedProperties == null)
            {
                wApp.ModifiedProperties = new List<string>();
            }
            wApp.ModifiedProperties.Add(nameof(wApp.StatusCode));
      
            //dbContext.WApplications.Update(wApp);

            var ePayment = new EPayment()
            {
                Id = BaseEntity.GenerateNewId(),
                EntityState = EntityStateEnum.Added,
                Amount = wApp.ApplicationType.Price,
                PaymentStatus = PaymentConstants.PaymentStatuses.Pending,
                InvoiceNumber = wApp.RegistrationNumber
            };

            var aPayment = new APayment()
            {
                Id = BaseEntity.GenerateNewId(),
                WApplicationId = aId,
                EntityState = EntityStateEnum.Added,
                EPaymentId = ePayment.Id,
                EPayment = ePayment
            };

            //dbContext.APayments.Add(aPayment);
            //dbContext.EPayments.Add(ePayment);
            _wApplicationRepository.ApplyChanges(wApp, new List<IBaseIdEntity>());
            _wApplicationRepository.ApplyChanges(aPayment, new List<IBaseIdEntity>());
            _wApplicationRepository.ApplyChanges(ePayment, new List<IBaseIdEntity>());
            await _wApplicationRepository.SaveChangesAsync();      
        }

        public async Task<PPerson> ProcessWebApplicationToApplicationAsync(WApplication wapplication, WApplicationStatus wapplicationStatus, AApplicationStatus applicationStatus)
        {
  
            _webApplicationService.SetWApplicationStatus(wapplication, wapplicationStatus, ApplicationResources.descAprovedApp);
     
            //string regNumber = "";

            //if (wapplication.ApplicationType.Code == ApplicationConstants.ApplicationTypes.WebCertificate)
            //{
            //    regNumber = await _registerTypeService.GetRegisterNumberForCertificateWeb(wapplication.CsAuthorityId);
            //}
            //if (wapplication.ApplicationType.Code == ApplicationConstants.ApplicationTypes.WebExternalCertificate)
            //{
            //    regNumber = await _registerTypeService.GetRegisterNumberForCertificateWebExternal(wapplication.CsAuthorityId);
            //}

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
                RegistrationNumber = wapplication.RegistrationNumber,
                //ApplicationType = wapplication.ApplicationType,
                ApplicationTypeId = wapplication.ApplicationTypeId,
                EntityState = EntityStateEnum.Added,
                
            
                
            };

            appl.AAppPersAliases = wapplication.WAppPersAliases.Select(w => new AAppPersAlias
            {
                EntityState = EntityStateEnum.Added,
                ApplicationId = appl.Id,
                Familyname = w.Familyname,
                Firstname = w.Firstname,
                Fullname = w.Fullname,
                Id = BaseEntity.GenerateNewId(),
                 Surname=w.Surname
                 
            }).ToList();
            appl.AAppCitizenships = wapplication.WAppCitizenships.Select(w => new AAppCitizenship
            {
                EntityState = EntityStateEnum.Added,
                ApplicationId = appl.Id,               
                Id = BaseEntity.GenerateNewId(),
               CountryId = w.CountryId,
               
            }).ToList();
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

            appl.EgnId = person.PPersonIds.First(x => x.PidTypeId == PersonConstants.PidType.Egn).Id;
            var documents = await baseAsyncRepository.FindAsync<WDocument>(d => d.WApplId == wapplication.Id);
            foreach(var document in documents)
            {
                DDocContent dDocContent = new DDocContent();
                dDocContent.EntityState = EntityStateEnum.Added;
                dDocContent.Id = BaseEntity.GenerateNewId();
                dDocContent.Bytes = document.Bytes;
                dDocContent.Content = document.Content;
                dDocContent.MimeType = document.MimeType;
                dDocContent.Md5Hash = document.Md5Hash;
                dDocContent.Sha1Hash = document.Sha1Hash;
                
                DDocument d= new DDocument();
                d.EntityState = EntityStateEnum.Added;
                d.Id = BaseEntity.GenerateNewId();
                d.ApplicationId = appl.Id;
                d.DocContentId = dDocContent.Id;
                d.DocContent = dDocContent; ;
                d.Name= document.Name;
                d.Descr = document.Descr;
                _wApplicationRepository.ApplyChanges(d, new List<IBaseIdEntity>());
            }

            _wApplicationRepository.ApplyChanges(appl, new List<IBaseIdEntity>(), true);      
           

            _wApplicationRepository.ApplyChanges(wapplication, new List<IBaseIdEntity>(), true);

            return person;
        }
        public async Task ProcessWApplicationCheckPayment(AApplicationStatus statusApprovedApplication, WApplicationStatus statusWebApprovedApplication, WApplicationStatus statusWebCancel, DateTime startDateWeb, WApplication wapplication)
        {
            //todo: check payment
            //todo: да гледаме ли срок на плащането, ако не е платено в срок, но все пак е платено какво става?!  

            bool isPaid = CheckPayment(wapplication);
            if (isPaid)
            {
                //await  AutomaticStepsHelper.ProcessWebApplicationToApplicationAsync(wapplication, _dbContext, _registerTypeService, _applicationService,_applicationWebService, _personService, statusWebApprovedApplication,  statusApprovedApplication );
                //todo: must add some FK for payment?!
                var person = await ProcessWebApplicationToApplicationAsync(wapplication, wapplicationStatus: statusWebApprovedApplication, applicationStatus: statusApprovedApplication);
               
            }
            else
            {
                if (wapplication.CreatedOn.Value.Date < startDateWeb)
                {
                    _webApplicationService.SetWApplicationStatus(wapplication, statusWebCancel, "Служебно анулиране - услугата не е платена в срок.");
                    _wApplicationRepository.ApplyChanges(wapplication, new List<IBaseIdEntity>());

                }

            }
        }

        private bool CheckPayment(WApplication wapplication)
        {
            if (wapplication.APayments == null || wapplication.APayments.Count == 0)
            {
                return false;
            }
            if (wapplication.APayments.Select(x=>x.EPayment).Count() == 0)
            {
                return false;
            }
            return wapplication.APayments.All(x => x.EPayment.PaymentStatus == PaymentConstants.PaymentStatuses.Payed);
        }
    }
}
