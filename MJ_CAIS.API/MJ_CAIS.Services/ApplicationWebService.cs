using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application.External;
using MJ_CAIS.DTO.Application.Public;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using static MJ_CAIS.Common.Constants.ApplicationConstants;

namespace MJ_CAIS.Services
{
    public class ApplicationWebService : BaseAsyncService<PublicApplicationDTO, PublicApplicationDTO, PublicApplicationGridDTO, WApplication, string, CaisDbContext>, IApplicationWebService
    {
        private readonly IUserContext _userContext;
        private readonly IApplicationWebRepository _applicationWebRepository;
        private readonly IRegisterTypeService _registerTypeService;

        public ApplicationWebService(IMapper mapper,
                                     IApplicationWebRepository applicationWebRepository,
                                     IRegisterTypeService registerTypeService,
                                     IUserContext userContext)
            : base(mapper, applicationWebRepository)
        {
            _applicationWebRepository = applicationWebRepository;
            _registerTypeService = registerTypeService;
            _userContext = userContext;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public string GetWebApplicationTypeId()
        {
            
            var result = dbContext.AApplicationTypes.AsNoTracking()
                .FirstOrDefault(x => x.Code == ApplicationConstants.ApplicationTypes.WebCertificate);

            return result.Id;
        }
        public string GetExternalWebApplicationTypeId()
        {
           
            var result = dbContext.AApplicationTypes.AsNoTracking()
                .FirstOrDefault(x => x.Code == ApplicationConstants.ApplicationTypes.WebExternalCertificate);

            return result.Id;
        }

        public async Task<string> InsertPublicAsync(PublicApplicationDTO aInDto)
        {
            var entity = mapper.MapToEntity<PublicApplicationDTO, WApplication>(aInDto, isAdded: true);
            this.TransformDataOnInsert(entity);
            entity.ApplicationTypeId = GetWebApplicationTypeId();

            entity.UserCitizenId = dbContext.CurrentUserId;
            entity.RegistrationNumber = await _registerTypeService.GetRegisterNumberForApplicationWeb(entity.CsAuthorityId);

           dbContext.ApplyChanges(entity, new List<IBaseIdEntity>());
           // await this.SaveEntityAsync(entity, true);
            return entity.Id;
        }

        public Task<decimal?> GetPriceByApplicationType(string applicationTypeID)
        {
            return _applicationWebRepository
                .GetDbContext()
                .AApplicationTypes
                .Where(at => at.Id == applicationTypeID)
                .Select(at => at.Price)
                .FirstOrDefaultAsync();
        }

        public async Task<string> InsertExternalAsync(ExternalApplicationDTO aInDto)
        {
            var entity = mapper.MapToEntity<ExternalApplicationDTO, WApplication>(aInDto, isAdded: true);
            this.TransformDataOnInsert(entity);
            entity.ApplicationTypeId = GetExternalWebApplicationTypeId();

            entity.UserExtId = dbContext.CurrentUserId;
            entity.RegistrationNumber = await _registerTypeService.GetRegisterNumberForApplicationWebExternal(entity.CsAuthorityId);

            dbContext.ApplyChanges(entity, new List<IBaseIdEntity>());
            //await this.SaveEntityAsync(entity, true);
            return entity.Id;
        }

        protected override void TransformDataOnInsert(WApplication entity)
        {
            base.TransformDataOnInsert(entity);
          
           // entity.ApplicationTypeId = GetWebApplicationTypeId(); //GetExternalWebApplicationTypeId();
            var statusNew = dbContext.WApplicationStatuses.FirstOrDefault(x => x.Code == ApplicationWebStatuses.NewWebApplication);
            if (statusNew == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.statusDoesNotExist, ApplicationWebStatuses.NewWebApplication));

            SetWApplicationStatus(entity, statusNew, ApplicationResources.titleNewApp, false);
            entity.UserId = dbContext.CurrentUserId; // TODO: must be nullable
            entity.WApplicationId = "-"; // TODO: remove, no such column
            entity.StatusCode = ApplicationWebStatuses.NewWebApplication;
            entity.CsAuthorityId = _userContext.CsAuthorityId ?? "660"; // TODO: constant
        }

        public IQueryable<PublicApplicationGridDTO> SelectPublicApplications(string userId)
        {
            var result =
                from app in dbContext.WApplications.AsNoTracking()

                join status in dbContext.WApplicationStatuses.AsNoTracking()
                    on app.StatusCode equals status.Code

                join purposes in dbContext.APurposes.AsNoTracking()
                on app.PurposeId equals purposes.Id into purposesLeft
                from purposes in purposesLeft.DefaultIfEmpty()

                where app.UserCitizenId == userId
                select new PublicApplicationGridDTO
                {
                    Id = app.Id,
                    RegistrationNumber = app.RegistrationNumber,
                    Purpose = app.Purpose,
                    PurposeTypeName = purposes.Name,
                    StatusCode = app.StatusCode,
                    StatusName = status.Name,
                    CreatedOn = app.CreatedOn,
                    Email = app.Email,
                    Version = app.Version,
                };

            return result;
        }

        public IQueryable<ExternalApplicationGridDTO> SelectExternalApplications(string userId)
        {
            var result =
                from app in dbContext.WApplications.AsNoTracking()

                join status in dbContext.WApplicationStatuses.AsNoTracking()
                    on app.StatusCode equals status.Code

                join purposes in dbContext.APurposes.AsNoTracking()
                on app.PurposeId equals purposes.Id into purposesLeft
                from purposes in purposesLeft.DefaultIfEmpty()

                join application in dbContext.AApplications.AsNoTracking()
              on app.WApplicationId equals application.Id into applicationLeft
                from application in applicationLeft.DefaultIfEmpty()

                where app.UserExtId == userId
                select new ExternalApplicationGridDTO
                {
                    Id = app.Id,
                    RegistrationNumber = app.RegistrationNumber,
                    ApplicantName = app.ApplicantName,
                    Purpose = app.Purpose,
                    PurposeName = purposes.Name,
                    PurposeId = app.PurposeId,
                    StatusCode = app.StatusCode,
                    StatusName = status.Name,
                    CreatedOn = app.CreatedOn,
                    Egn = app.Egn,
                    Name = application.Firstname + " " + application.Surname + " " + application.Familyname
                };

            return result;
        }

        public async Task<DTO.Application.Public.ApplicationPreviewDTO> GetPublicForPreviewAsync(string id)
        {
            var result = await (from app in dbContext.WApplications.AsNoTracking()

                                join status in dbContext.WApplicationStatuses.AsNoTracking()
                                    on app.StatusCode equals status.Code

                                join purposes in dbContext.APurposes.AsNoTracking()
                                    on app.PurposeId equals purposes.Id into purposesLeft
                                from purposes in purposesLeft.DefaultIfEmpty()

                                join paymentMethods in dbContext.APaymentMethods.AsNoTracking()
                                    on app.PaymentMethodId equals paymentMethods.Id into paymentMethodsLeft
                                from paymentMethods in paymentMethodsLeft.DefaultIfEmpty()

                                select new DTO.Application.Public.ApplicationPreviewDTO
                                {
                                    Id = app.Id,
                                    CreatedOn = app.CreatedOn,
                                    Egn = app.Egn,
                                    Email = app.Email,
                                    PaymentMethodName = paymentMethods.Name,
                                    PurposeName = purposes.Name,
                                    Purpose = app.Purpose,
                                    RegistrationNumber = app.RegistrationNumber,
                                    Status = status.Name,
                                    StatusCode = status.Code,
                                    // IsPaid  ?? todo
                                }).FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<DTO.Application.External.ApplicationPreviewDTO> GetExternalForPreviewAsync(string id)
        {
            var result = await (from app in dbContext.WApplications.AsNoTracking()

                                join status in dbContext.WApplicationStatuses.AsNoTracking()
                                    on app.StatusCode equals status.Code

                                join purposes in dbContext.APurposes.AsNoTracking()
                                    on app.PurposeId equals purposes.Id into purposesLeft
                                from purposes in purposesLeft.DefaultIfEmpty()

                                join application in dbContext.AApplications.AsNoTracking()
                                    on app.WApplicationId equals application.Id into applicationLeft
                                from application in applicationLeft.DefaultIfEmpty()
                                select new DTO.Application.External.ApplicationPreviewDTO
                                {
                                    Id = app.Id,
                                    CreatedOn = app.CreatedOn,
                                    Egn = app.Egn,
                                    Email = app.Email,
                                    PurposeName = purposes.Name,
                                    Purpose = app.Purpose,
                                    RegistrationNumber = app.RegistrationNumber,
                                    Status = status.Name,
                                    StatusCode = status.Code,
                                    Name = application.Firstname + " " + application.Surname + " " + application.Familyname
                                }).FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public void SetWApplicationStatus(WApplication wapplication, WApplicationStatus newStatus, string description, bool addToContext = true)
        {
            wapplication.StatusCode = newStatus.Code;
            wapplication.StatusCodeNavigation = newStatus;
            WStatusH wStatusH = new WStatusH();
            wStatusH.EntityState = Common.Enums.EntityStateEnum.Added;
            wStatusH.Descr = description;
            wStatusH.StatusCode = newStatus.Code;
            wStatusH.StatusCodeNavigation = newStatus;
            if (wapplication.WStatusHes == null)
            {
                wapplication.WStatusHes = new List<WStatusH>();
            }

            wStatusH.ReportOrder = wapplication.WStatusHes.Count(x => x.StatusCode == newStatus.Code) + 1;
            wStatusH.Id = BaseEntity.GenerateNewId();
            wStatusH.ApplicationId = wapplication.Id;
            wStatusH.Application = wapplication;

            wapplication.WStatusHes.Add(wStatusH);
            if (addToContext)
            {
                dbContext.WStatusHes.Add(wStatusH);
                dbContext.WApplications.Update(wapplication);
            }
        }     
    }
}
