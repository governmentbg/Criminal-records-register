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
using MJ_CAIS.DTO.Person;
using MJ_CAIS.DTO.ReportApplication;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;
using static MJ_CAIS.Common.Constants.PersonConstants;
using static MJ_CAIS.Common.Constants.ReportApplicationConstants;

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

        public IQueryable<ReportAppStatusHistoryDTO> GetStatusHistoryByReportAppId(string aId)
            => _reportApplicationRepository.SelectAllStatusHistoryData()
                .Where(x => x.AReportApplId == aId)
                .OrderByDescending(x => x.CreatedOn);

        public IQueryable<GeneratedReportDTO> GetReportsByAppId(string aId)
           => _reportApplicationRepository.SelectAllGeneratedReportsByAppId(aId);

        public async Task<byte[]> GetReportAppContentByIdAsync(string aId)
            => await _reportApplicationRepository.GetReportAppContentByIdAsync(aId);

        public virtual async Task<IgPageResult<ReportApplicationGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<ReportApplicationGridDTO> aQueryOptions, string? statusCode)
        {
            var entityQuery = GetSelectAllQueryable().Where(x => x.CsAuthorityId == _userContext.CsAuthorityId);
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

        public async Task<IgPageResult<GeneratedReportGridDTO>> SelectAllGeneratedReportsWithPaginationAsync(ODataQueryOptions<GeneratedReportGridDTO> aQueryOptions)
        {
            var entityQuery = _reportApplicationRepository.SelectAllGeneratedReports()
                .Where(x => x.StatusCode == Status.ReadyReport ||
                            x.StatusCode == Status.DeliveredReport &&
                            x.CsAuthorityId == _userContext.CsAuthorityId);

            var resultQuery = await ApplyOData(entityQuery, aQueryOptions);
            var pageResult = new IgPageResult<GeneratedReportGridDTO>();
            PopulatePageResultAsync(pageResult, aQueryOptions, entityQuery, resultQuery);
            return pageResult;
        }

        public async Task<AReportApplication> CreateAppReportAsync(ReportApplicationDTO aInDto)
        {
            var entity = mapper.MapToEntity<ReportApplicationDTO, AReportApplication>(aInDto, true);
            entity.Id = BaseEntity.GenerateNewId(); // when call regix set id manually
            entity.CsAuthorityId = _userContext.CsAuthorityId;
            entity.StatusCode = Status.New;
            entity.RegistrationNumber = await _registerTypeService.GetRegisterNumberForReport(entity.CsAuthorityId);

            entity.ARepCitizenships = CaisMapper.MapMultipleChooseToEntityList<ARepCitizenship, string, string>(
             aInDto.Person.Nationalities, nameof(ARepCitizenship.Id), nameof(ARepCitizenship.CountryId));
            entity.AReportStatusHes = new List<AReportStatusH> { CreateH(entity) };

            await _reportApplicationRepository.SaveEntityAsync(entity, true, true);
            return entity;
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

            var person = await UpdatePersonDataAsync(aInDto, entity);
            var personId = person.EntityState == EntityStateEnum.Modified && person.PPersonIds.Count > 0 ? person.Id : null;

            var report = await GenerateReportAsync(aInDto, entity, personId);

            _reportApplicationRepository.ApplyChanges(entity, applyToAllLevels: true);
            _reportApplicationRepository.ApplyChanges(report, applyToAllLevels: true);
            await _reportApplicationRepository.SaveChangesAsync(clearTracker: true);

            return report.Id;
        }

        public async Task<string> CancelAsync(string aId, string cancelDesc)
        {
            var reportApp = await baseAsyncRepository.SingleOrDefaultAsync<AReportApplication>(a => a.Id == aId);
            if (reportApp == null) throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.msgAppReportDoesNotExist, aId));

            if (string.IsNullOrEmpty(cancelDesc))
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.fieldIsRequired, ReportApplicationResources.lblCancelDesc));

            if (reportApp.StatusCode == Status.Canceled)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.msg�ppReportIsCanceled, aId));

            reportApp.EntityState = EntityStateEnum.Modified;
            reportApp.ModifiedProperties = new List<string> { nameof(reportApp.StatusCode), nameof(reportApp.Version) };
            reportApp.StatusCode = Status.Canceled;

            reportApp.AReportStatusHes = new List<AReportStatusH> { CreateH(reportApp, cancelDesc) };
            await _reportApplicationRepository.SaveEntityAsync(reportApp, true);
            return reportApp.Id;
        }

        public async Task<string> CancelReportAsync(CancelReportDTO aInDto)
        {
            var report = await _reportApplicationRepository.GetFullAppReportByIdAsync(aInDto.ReportId);
            if (report == null) throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.msgReportDoesNotExist, aInDto.ReportId));

            if (report.StatusCode == Status.CanceledReport)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.msgReportIsCanceled, aInDto.ReportId));

            if (report.StatusCode == Status.DeliveredReport)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.msgReportIsDelivered, aInDto.ReportId));

            // change current report data 
            report.EntityState = EntityStateEnum.Modified;
            report.ModifiedProperties = new List<string> { nameof(report.StatusCode), nameof(report.Version) };
            report.StatusCode = Status.CanceledReport;
            report.AReportStatusHes = new List<AReportStatusH> { CreateH(report, aInDto.Description) };
            _reportApplicationRepository.ApplyChanges(report, applyToAllLevels: true);

            var newReportDTO = new ReportApplicationDTO { FirstSignerId = aInDto.FirstSignerId, SecondSignerId = aInDto.SecondSignerId };

            var personId = await _reportApplicationRepository.GetPersonIdByPidIdsAsync(report.ARepAppl.EgnId, report.ARepAppl.LnchId, report.ARepAppl.LnId, report.ARepAppl.SuidId);
            var newReport = await GenerateReportAsync(newReportDTO, report.ARepAppl, personId);

            _reportApplicationRepository.ApplyChanges(newReport, applyToAllLevels: true);

            await _reportApplicationRepository.SaveChangesAsync();
            return report.Id;
        }

        public async Task<string> DeliverAsync(string aId)
        {
            var appReport = await _reportApplicationRepository.GetFullAppReportByIdAsync(aId);
            if (appReport == null) return null;

            appReport.StatusCode = Status.DeliveredReport;
            appReport.ARepAppl.StatusCode = Status.Delivered;

            appReport.EntityState = EntityStateEnum.Modified;
            appReport.ARepAppl.EntityState = EntityStateEnum.Modified;

            appReport.ModifiedProperties = new List<string> { nameof(appReport.StatusCode), nameof(appReport.Version) };
            appReport.ARepAppl.ModifiedProperties = new List<string> { nameof(appReport.ARepAppl.StatusCode), nameof(appReport.ARepAppl.Version) };

            var reportH = CreateH(appReport, ReportApplicationResources.msgDelivered);
            var reportAppH = CreateH(appReport.ARepAppl, ReportApplicationResources.msgDeliveredReport);

            appReport.AReportStatusHes = new List<AReportStatusH> { reportH };
            appReport.ARepAppl.AReportStatusHes = new List<AReportStatusH> { reportAppH };

            await _reportApplicationRepository.SaveEntityAsync(appReport, true);

            return appReport.Id;
        }

        public async Task<ReportApplicationDTO> SelectWithPersonDataAsync(string personId)
        {
            var result = new ReportApplicationDTO
            {
                Id = BaseEntity.GenerateNewId(),
                CsAuthorityId = _userContext.CsAuthorityId
            };

            var person = await _managePersonService.SelectWithBirthInfoAsync(personId);
            result.Person = person ?? new PersonDTO();
            return result;
        }

        private async Task<AReportApplication> ApplyDataForUpdateAsync(ReportApplicationDTO aInDto, bool isFinal)
        {
            var reportApp = await baseAsyncRepository.SingleOrDefaultAsync<AReportApplication>(a => a.Id == aInDto.Id);
            if (reportApp == null) return null;

            if (reportApp.CsAuthorityId != _userContext.CsAuthorityId)
                throw new BusinessLogicException(BusinessLogicExceptionResources.editIsUnauthorized);

            var entity = mapper.MapToEntity<ReportApplicationDTO, AReportApplication>(aInDto, false);
            entity.StatusCode = isFinal ? Status.Approved : reportApp.StatusCode;
            if (isFinal)
            {
                entity.AReportStatusHes = new List<AReportStatusH> { CreateH(entity) };
            }

            entity.ARepCitizenships = CaisMapper.MapMultipleChooseToEntityList<ARepCitizenship, string, string>(
            aInDto.Person.Nationalities, nameof(ARepCitizenship.Id), nameof(ARepCitizenship.CountryId));

            return entity;
        }

        private async Task<AReport> GenerateReportAsync(ReportApplicationDTO aInDto, AReportApplication entity, string? personId)
        {
            var reportRegNumber = await _registerTypeService.GetRegisterNumberForReport(entity.CsAuthorityId);

            var report = new AReport
            {
                Id = BaseEntity.GenerateNewId(),
                ARepApplId = entity.Id,
                StatusCode = Status.DraftReport,
                EntityState = EntityStateEnum.Added,
                FirstSignerId = aInDto.FirstSignerId,
                SecondSignerId = aInDto.SecondSignerId,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddMonths(6), // todo: system parameter
                RegistrationNumber = reportRegNumber,
            };

            var reportStatusH = CreateH(report);
            report.AReportStatusHes = new List<AReportStatusH> { reportStatusH };

            if (!string.IsNullOrEmpty(personId))
            {
                var bulletins = _reportApplicationRepository.GetBulletinsByPids(personId);
                var bulletinList = await bulletins.ToListAsync();
                if (bulletinList.Any())
                {

                    var orderNumber = 0;
                    report.ARepBulletins = bulletinList
                        .OrderByDescending(b => b.CreatedOn.HasValue ? b.CreatedOn.Value.Date : DateTime.Now)
                        //.ThenByDescending(b => b.DecisionDate)
                        .Select(b =>
                        {
                            orderNumber++;
                            return new ARepBulletin
                            {
                                Id = BaseEntity.GenerateNewId(),
                                BulletinId = b.Id,
                                EntityState = EntityStateEnum.Added,
                                OrderNumber = orderNumber,
                                ReportId = report.Id
                            };
                        }).ToList();
                }
            }

            return report;
        }

        private static AReportStatusH CreateH(AReport report, string desc = null)
            => CreateH(report.StatusCode, report.ARepApplId, report.Id, desc);

        private static AReportStatusH CreateH(AReportApplication reportAppl, string desc = null)
            => CreateH(reportAppl.StatusCode, reportAppl.Id, null, desc);

        private static AReportStatusH CreateH(string statusCode, string reportApplId, string reportId, string desc)
            => new AReportStatusH
            {
                Id = BaseEntity.GenerateNewId(),
                Descr = desc,
                EntityState = EntityStateEnum.Added,
                StatusCode = statusCode,
                AReportApplId = reportApplId,
                AReportId = reportId
            };

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
                    entity.ModifiedProperties.Add(nameof(entity.Suid));
                    entity.SuidId = personIdObj.Id;
                    entity.Suid = personIdObj.Pid;
                }

                _reportApplicationRepository.ApplyChanges(personIdObj);
            }

            return person;
        }
    }
}
