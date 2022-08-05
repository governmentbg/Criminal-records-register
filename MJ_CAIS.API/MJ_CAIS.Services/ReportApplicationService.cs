using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
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
using static MJ_CAIS.Common.Constants.PersonConstants;

namespace MJ_CAIS.Services
{
    public class ReportApplicationService : BaseAsyncService<ReportApplicationDTO, ReportApplicationDTO, ReportApplicationGridDTO, AReportApplication, string, CaisDbContext>, IReportApplicationService
    {
        private readonly IReportApplicationRepository _reportApplicationRepository;
        private readonly IUserContext _userContext;
        private readonly IRegisterTypeService _registerTypeService;
        private readonly IManagePersonService _managePersonService;

        public ReportApplicationService(IMapper mapper, IReportApplicationRepository reportApplicationRepository,
            IUserContext userContext,
           IRegisterTypeService registerTypeService,
           IManagePersonService managePersonService)
            : base(mapper, reportApplicationRepository)
        {
            _reportApplicationRepository = reportApplicationRepository;
            _userContext = userContext;
            _registerTypeService = registerTypeService;
            _managePersonService = managePersonService;
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

        public async Task<string> UpdateAsync(ReportApplicationDTO aInDto)
        {
            var entity = await ApplyDataForUpdateAsync(aInDto, false);
            await _reportApplicationRepository.SaveEntityAsync(entity, true);
            return entity.Id;
        }

        public async Task<string> FinalUpdateAsync(ReportApplicationDTO aInDto)
        {
            var entity = await ApplyDataForUpdateAsync(aInDto, true);
            var report = await GenerateReportAsync(aInDto, entity);

            _reportApplicationRepository.ApplyChanges(entity, applyToAllLevels: true);
            _reportApplicationRepository.ApplyChanges(report, applyToAllLevels: true);
            await _reportApplicationRepository.SaveChangesAsync(clearTracker: true);

            return report.Id;
        }

        public async Task<string> CancelAsync(string aId, string cancelDesc)
        {
            var reportApp = await baseAsyncRepository.SingleOrDefaultAsync<AReportApplication>(a => a.Id == aId);
            if (reportApp == null) return null;

            if (string.IsNullOrEmpty(cancelDesc))
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.fieldIsRequired, ReportApplicationResources.lblCancelDesc));

            reportApp.EntityState = EntityStateEnum.Modified;
            reportApp.ModifiedProperties = new List<string> { nameof(reportApp.StatusCode), nameof(reportApp.Version) };
            reportApp.StatusCode = ReportApplicationConstants.Status.Canceled;
            // todo: add history
            // todo: check for generated report

            reportApp.AReportStatusHes = new List<AReportStatusH> { CreateH(reportApp.StatusCode, cancelDesc) };
            await _reportApplicationRepository.SaveEntityAsync(reportApp, true);
            return reportApp.Id;
        }

        public IQueryable<ReportAppStatusHistoryDTO> GetStatusHistoryByReportAppId(string aId)
        {
            var statues = _reportApplicationRepository.SelectAllStatusHistoryData();
            var filteredStatuses = statues.Where(x => x.AReportApplId == aId);
            return filteredStatuses;
        }

        public IQueryable<GeneratedReportDTO> GetReportsByAppId(string aId)
        {
            return _reportApplicationRepository.SelectAllGeneratedReportsByAppId(aId);
        }

        private async Task<AReportApplication> ApplyDataForUpdateAsync(ReportApplicationDTO aInDto, bool isFinal)
        {
            var reportApp = await baseAsyncRepository.SingleOrDefaultAsync<AReportApplication>(a => a.Id == aInDto.Id);
            if (reportApp == null) return null;

            if (reportApp.CsAuthorityId != _userContext.CsAuthorityId)
                throw new BusinessLogicException(BusinessLogicExceptionResources.editIsUnauthorized);

            var entity = mapper.MapToEntity<ReportApplicationDTO, AReportApplication>(aInDto, false);
            entity.StatusCode = isFinal ? ReportApplicationConstants.Status.Approved : reportApp.StatusCode;
            if (isFinal)
            {
                entity.AReportStatusHes = new List<AReportStatusH> { CreateH(entity.StatusCode) };
            }

            entity.ARepCitizenships = CaisMapper.MapMultipleChooseToEntityList<ARepCitizenship, string, string>(
            aInDto.Person.Nationalities, nameof(ARepCitizenship.Id), nameof(ARepCitizenship.CountryId));

            return entity;
        }

        private async Task<AReport> GenerateReportAsync(ReportApplicationDTO aInDto, AReportApplication entity)
        {
            var reportStatusH = CreateH(ReportApplicationConstants.Status.DraftReport);
            reportStatusH.AReportApplId = entity.Id;
            if (entity.AReportStatusHes == null)
                entity.AReportStatusHes = new List<AReportStatusH>();

            var person = await UpdatePersonDataAsync(aInDto, entity);
            var reportRegNumber = await _registerTypeService.GetRegisterNumberForReport(entity.CsAuthorityId);
            var report = new AReport
            {
                Id = BaseEntity.GenerateNewId(),
                ARepApplId = entity.Id,
                StatusCode = ReportApplicationConstants.Status.DraftReport,
                AReportStatusHes = new List<AReportStatusH> { reportStatusH },
                EntityState = EntityStateEnum.Added,
                FirstSignerId = aInDto.FirstSignerId,
                SecondSignerId = aInDto.SecondSignerId,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddMonths(6), // todo: system parameter
                RegistrationNumber = reportRegNumber,
            };

            // if person exist then check for bulletins
            if (person.EntityState == EntityStateEnum.Modified && person.PPersonIds.Count > 0)
            {
                var bulletins = _reportApplicationRepository.GetBulletinsByPids(entity.EgnId, entity.LnchId, entity.LnId, entity.SuidId);

                if (bulletins.Any())
                {
                    var bulletinList = await bulletins.ToListAsync();

                    var orderNumber = 0;
                    report.ARepBulletins = bulletinList
                        .OrderByDescending(b => b.CreatedOn.HasValue ? b.CreatedOn.Value.Date : DateTime.Now)
                        .ThenByDescending(b => b.DecisionDate).Select(b =>
                        {
                            orderNumber++;
                            return new ARepBulletin
                            {
                                Id = BaseEntity.GenerateNewId(),
                                BulletinId = b.Id,
                                EntityState = EntityStateEnum.Added,
                                OrderNumber = orderNumber,
                            };
                        }).ToList();
                }
            }

            return report;
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

        private async Task<PPerson> UpdatePersonDataAsync(ReportApplicationDTO aInDto, AReportApplication entity)
        {
            var person = await _managePersonService.CreatePersonAsync(aInDto.Person);

            foreach (var personIdObj in person.PPersonIds)
            {
                if (personIdObj.PidTypeId == PidType.Egn)
                {
                    entity.ModifiedProperties.Add(nameof(entity.EgnId));
                    entity.EgnId = personIdObj.Id;
                }
                else if (personIdObj.PidTypeId == PidType.Lnch)
                {
                    entity.ModifiedProperties.Add(nameof(entity.LnchId));
                    entity.LnchId = personIdObj.Id;

                }
                else if (personIdObj.PidTypeId == PidType.Ln)
                {
                    entity.ModifiedProperties.Add(nameof(entity.LnId));
                    entity.LnId = personIdObj.Id;

                }
                else if (personIdObj.PidTypeId == PidType.Suid)
                {
                    entity.ModifiedProperties.Add(nameof(entity.SuidId));
                    entity.SuidId = personIdObj.Id;
                }

                _reportApplicationRepository.ApplyChanges(personIdObj);
            }

            return person;
        }

    }
}
