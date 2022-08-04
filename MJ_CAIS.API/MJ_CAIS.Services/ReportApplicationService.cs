using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ReportApplication;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
    public class ReportApplicationService : BaseAsyncService<ReportApplicationDTO, ReportApplicationDTO, ReportApplicationGridDTO, AReportApplication, string, CaisDbContext>, IReportApplicationService
    {
        private readonly IReportApplicationRepository _reportApplicationRepository;
        private readonly IUserContext _userContext;
        private readonly IRegisterTypeService _registerTypeService;

        public ReportApplicationService(IMapper mapper, IReportApplicationRepository reportApplicationRepository,
            IUserContext userContext,
           IRegisterTypeService registerTypeService)
            : base(mapper, reportApplicationRepository)
        {
            _reportApplicationRepository = reportApplicationRepository;
            _userContext = userContext;
            _registerTypeService = registerTypeService;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public virtual async Task<IgPageResult<ReportApplicationGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<ReportApplicationGridDTO> aQueryOptions, string? statusCode)
        {
            var entityQuery = GetSelectAllQueryable();
            if (!string.IsNullOrEmpty(statusCode))
            {
                entityQuery = entityQuery.Where(x => x.StatusCode == statusCode);
            }

            var baseQuery = entityQuery.ProjectTo<ReportApplicationGridDTO>(mapperConfiguration);
            var resultQuery = await ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<ReportApplicationGridDTO>();
            PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public override async Task<string> InsertAsync(ReportApplicationDTO aInDto)
        {
            var entity = mapper.MapToEntity<ReportApplicationDTO, AReportApplication>(aInDto, true);
            entity.CsAuthorityId = _userContext.CsAuthorityId;
            entity.StatusCode = ReportApplicationConstants.Status.New;
            entity.RegistrationNumber = await _registerTypeService.GetRegisterNumberForReport(entity.CsAuthorityId);

            entity.ARepCitizenships = CaisMapper.MapMultipleChooseToEntityList<ARepCitizenship, string, string>(
             aInDto.Person.Nationalities, nameof(ARepCitizenship.Id), nameof(ARepCitizenship.CountryId));
            entity.AReportStatusHes = new List<AReportStatusH> { CreateH(entity.StatusCode) };

            await _reportApplicationRepository.SaveEntityAsync(entity, true);
            return entity.Id;
        }

        public async Task<string> UpdateAsync(ReportApplicationDTO aInDto, bool isFinal)
        {
            var reportApp = await baseAsyncRepository.SingleOrDefaultAsync<AReportApplication>(a => a.Id == aInDto.Id);
            if (reportApp == null) return null;

            if (reportApp.CsAuthorityId != _userContext.CsAuthorityId)
                throw new BusinessLogicException(BusinessLogicExceptionResources.editIsUnauthorized);

            var entity = mapper.MapToEntity<ReportApplicationDTO, AReportApplication>(aInDto, false);
            entity.StatusCode = isFinal ? ReportApplicationConstants.Status.Approved : reportApp.StatusCode;
            entity.ARepCitizenships = CaisMapper.MapMultipleChooseToEntityList<ARepCitizenship, string, string>(
            aInDto.Person.Nationalities, nameof(ARepCitizenship.Id), nameof(ARepCitizenship.CountryId));

            if (isFinal)
            {     // todo
                reportApp.AReportStatusHes = new List<AReportStatusH> { CreateH(reportApp.StatusCode) };

                //await UpdatePersonDataAsync(aInDto, entity);
                //await GenerateReportFromReportApplication(applicationDb.Id);
                return entity.Id;
            }

            await _reportApplicationRepository.SaveEntityAsync(entity, true);
            return entity.Id;
        }

        public async Task<string> CancelAsync(string aId, string cancelDesc)
        {
            var reportApp = await baseAsyncRepository.SingleOrDefaultAsync<AReportApplication>(a => a.Id == aId);
            if (reportApp == null) return null;

            if (string.IsNullOrEmpty(cancelDesc))
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.fieldIsRequired, ReportApplicationResources.lblCancelDesc));

            reportApp.EntityState = EntityStateEnum.Modified;
            reportApp.ModifiedProperties = new List<string>{nameof(reportApp.StatusCode), nameof(reportApp.Version)};
            reportApp.StatusCode = ReportApplicationConstants.Status.Canceled;
            // todo: add history
            // todo: check for generated report

            reportApp.AReportStatusHes = new List<AReportStatusH> {CreateH(reportApp.StatusCode, cancelDesc)};
            await _reportApplicationRepository.SaveEntityAsync(reportApp,true);
            return reportApp.Id;
        }

        private static AReportStatusH CreateH(string statusCode, string desc = null)
        {
            return new AReportStatusH
            {
                Id = BaseEntity.GenerateNewId(),
                Descr = desc,
                EntityState = EntityStateEnum.Added,
                StatusCode = statusCode,
            };
        }
    }
}
