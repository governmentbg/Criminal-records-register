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

        public async Task<string> GetWebApplicationTypeId()
        {

            var result = await _applicationWebRepository.SingleOrDefaultAsync<AApplicationType>(x => x.Code == ApplicationConstants.ApplicationTypes.WebCertificate);


                //dbContext.AApplicationTypes.AsNoTracking()
                //.FirstOrDefault(x => x.Code == ApplicationConstants.ApplicationTypes.WebCertificate);

            return result.Id;
        }
        public async Task<string> GetExternalWebApplicationTypeId()
        {
            var result = await _applicationWebRepository.SingleOrDefaultAsync<AApplicationType>(x => x.Code == ApplicationConstants.ApplicationTypes.WebExternalCertificate);

            //var result = dbContext.AApplicationTypes.AsNoTracking()
            //    .FirstOrDefault(x => x.Code == ApplicationConstants.ApplicationTypes.WebExternalCertificate);

            return result.Id;
        }

        public Task<string?> FindDuplicate(string egn, string purposeId, string applicantId)
        {
            return _applicationWebRepository.FindDuplicate(egn, purposeId, applicantId);
        }

        public async Task<string> InsertPublicAsync(PublicApplicationDTO aInDto)
        {
            var userId = _applicationWebRepository.GetCurrentUserId();
            if (!await _applicationWebRepository.HasDublicates(aInDto.Egn,aInDto.PurposeId, ApplicationConstants.ApplicationTypes.WebCertificate, userId))
            {
                var entity = mapper.MapToEntity<PublicApplicationDTO, WApplication>(aInDto, isAdded: true);
                await this.TransformDataOnInsertAsync(entity);
                entity.ApplicationTypeId = await GetWebApplicationTypeId();

                entity.UserCitizenId = userId;//_applicationWebRepository.GetCurrentUserId(); //dbContext.CurrentUserId;
                entity.RegistrationNumber = await _registerTypeService.GetRegisterNumberForApplicationWeb(entity.CsAuthorityId);

                _applicationWebRepository.ApplyChanges(entity, new List<IBaseIdEntity>());
                // await this.SaveEntityAsync(entity, true);
                return entity.Id;
            }
            else
            {
                throw new BusinessLogicException("Дублирано заявление.");
            }
        }

        public async Task<decimal?> GetPriceByApplicationType(string applicationTypeID)
        {
            //return _applicationWebRepository
            //    .GetDbContext()
            //    .AApplicationTypes
            //    .Where(at => at.Id == applicationTypeID)
            //    .Select(at => at.Price)
            //    .FirstOrDefaultAsync();
            return (await _applicationWebRepository.SingleOrDefaultAsync<AApplicationType>
                (at => at.Id == applicationTypeID))?.Price;
        }

        public async Task<string> InsertExternalAsync(ExternalApplicationDTO aInDto)
        {
            var userId = _applicationWebRepository.GetCurrentUserId();
            if (!await _applicationWebRepository.HasDublicates(aInDto.Egn, aInDto.PurposeId, ApplicationConstants.ApplicationTypes.WebExternalCertificate,userId ))
            {
                var entity = mapper.MapToEntity<ExternalApplicationDTO, WApplication>(aInDto, isAdded: true);
                await this.TransformDataOnInsertAsync(entity);
                entity.ApplicationTypeId = await GetExternalWebApplicationTypeId();

                entity.UserExtId = userId;// _applicationWebRepository.GetCurrentUserId();//dbContext.CurrentUserId;
                entity.RegistrationNumber = await _registerTypeService.GetRegisterNumberForApplicationWebExternal(entity.CsAuthorityId);

                _applicationWebRepository.ApplyChanges(entity, new List<IBaseIdEntity>());
                //await this.SaveEntityAsync(entity, true);
                return entity.Id;
            }
            else
            {
                throw new BusinessLogicException("Дублирано заявление.");
            }

        }

        protected  async Task TransformDataOnInsertAsync(WApplication entity)
        {
            base.TransformDataOnInsertAsync(entity);
            entity.EntityState = Common.Enums.EntityStateEnum.Added;
           // entity.ApplicationTypeId = GetWebApplicationTypeId(); //GetExternalWebApplicationTypeId();
            var statusNew =
                await _applicationWebRepository.SingleOrDefaultAsync<WApplicationStatus>(x => x.Code == ApplicationWebStatuses.NewWebApplication);


            //dbContext.WApplicationStatuses.FirstOrDefault(x => x.Code == ApplicationWebStatuses.NewWebApplication);
            if (statusNew == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.statusDoesNotExist, ApplicationWebStatuses.NewWebApplication));

            SetWApplicationStatus(entity, statusNew, ApplicationResources.titleNewApp, false);
         
            entity.UserId = _applicationWebRepository.GetCurrentUserId(); //dbContext.CurrentUserId; // TODO: must be nullable
            entity.WApplicationId = "-"; // TODO: remove, no such column
            entity.StatusCode = ApplicationWebStatuses.NewWebApplication;
            entity.CsAuthorityId = _userContext.CsAuthorityId ?? "660"; // TODO: constant
            _applicationWebRepository.ApplyChanges(entity, new List<IBaseIdEntity>());
        }

        

        public void SetWApplicationStatus(WApplication wapplication, WApplicationStatus newStatus, string description, bool addToContext = true)
        {
            wapplication.StatusCode = newStatus.Code;
            if(wapplication.EntityState!= Common.Enums.EntityStateEnum.Added)
            {
                wapplication.EntityState = Common.Enums.EntityStateEnum.Modified;

            }
         
            if( wapplication.ModifiedProperties== null)
            {
                wapplication.ModifiedProperties = new List<string>();
            }
            wapplication.ModifiedProperties.Add(nameof(wapplication.StatusCode));
            //wapplication.StatusCodeNavigation = newStatus;
            WStatusH wStatusH = new WStatusH();
            wStatusH.EntityState = Common.Enums.EntityStateEnum.Added;
            wStatusH.Descr = description;
            wStatusH.StatusCode = newStatus.Code;
            //wStatusH.StatusCodeNavigation = newStatus;
            if (wapplication.WStatusHes == null)
            {
                wapplication.WStatusHes = new List<WStatusH>();
            }

            wStatusH.ReportOrder = wapplication.WStatusHes.Count(x => x.StatusCode == newStatus.Code) + 1;
            wStatusH.Id = BaseEntity.GenerateNewId();
            wStatusH.ApplicationId = wapplication.Id;
            wStatusH.Application = wapplication;

            wapplication.WStatusHes.Add(wStatusH);
            _applicationWebRepository.ApplyChanges (wStatusH, new List<IBaseIdEntity>());
           // _applicationWebRepository.ApplyChanges(wapplication, new List<IBaseIdEntity>());
            //if (addToContext)
            //{
            //    dbContext.WStatusHes.Add(wStatusH);
            //    dbContext.WApplications.Update(wapplication);
            //}
        }

        public IQueryable<ExternalApplicationGridDTO> SelectExternalApplications(string userId)
        {
            return _applicationWebRepository.SelectExternalApplications(userId);
        }

        public async Task<DTO.Application.Public.ApplicationPreviewDTO> GetPublicForPreviewAsync(string id)
        {
            return await _applicationWebRepository.GetPublicForPreviewAsync(id);
        }
        public async Task CancelAsync(string id, string? userId, string? userName)
        {
            var application = await _applicationWebRepository.SelectAsync(id);
            if (application.UserCitizenId != userId)
            {
                throw new ApplicationException("You can cancel only applications created by yourself!");
            }
            var webStatusCancel =  await _applicationWebRepository.GetWebCanceledStatus();
            SetWApplicationStatus(application, webStatusCancel, "Анулирана от заявител");
            _applicationWebRepository.ApplyChanges(application, new List<IBaseIdEntity>());
            await _applicationWebRepository.SaveChangesAsync();
        }

        public async Task<DTO.Application.External.ApplicationPreviewDTO> GetExternalForPreviewAsync(string id)
        {
            return await _applicationWebRepository.GetExternalForPreviewAsync(id);
        }

        public IQueryable<PublicApplicationGridDTO> SelectPublicApplications(string userId)
        {
            return _applicationWebRepository.SelectPublicApplications(userId);
        }

       

    }
}
