using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DTO.Report;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System.Collections.Generic;
using MJ_CAIS.Common.Constants;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.DTO.Application;
using static MJ_CAIS.Common.Constants.PersonConstants;
using MJ_CAIS.DTO.Shared;
using MJ_CAIS.Common.Enums;

namespace MJ_CAIS.Services
{
    public class ReportService : BaseAsyncService<ReportDTO, ReportDTO, ReportGridDTO, AReport, string, CaisDbContext>, IReportService
    {
        private readonly IReportRepository _reportRepository;

        private readonly IApplicationService _applicationService;
        private readonly IRegisterTypeService _registerTypeService;
        private readonly IPersonService _personService;


        public ReportService(IMapper mapper, IReportRepository reportRepository, IApplicationService applicationService, IRegisterTypeService registerTypeService,
            IPersonService personService)
            : base(mapper, reportRepository)
        {
            _reportRepository = reportRepository;
            _applicationService = applicationService;
            _registerTypeService = registerTypeService;
            _personService = personService;
        }

        public async Task<string> InsertAsync(ApplicationInDTO aInDto)
        {

            var entity = mapper.MapToEntity<ApplicationInDTO, AApplication>(aInDto, true);
            var regNumber = await _registerTypeService.GetRegisterNumberForApplicationOnDesk(entity.CsAuthorityId);
            entity.RegistrationNumber = regNumber;

            await UpdateTransactionsAsync(aInDto, entity);
            await dbContext.SaveEntityAsync(entity, true);


            dbContext.Entry(entity).State = EntityState.Detached;
            entity.Version = 1;
            entity.EntityState = EntityStateEnum.Modified;
            entity.ModifiedProperties = new List<string> { nameof(entity.Version) };

            await UpdatePersonDataAsync(aInDto, entity); ;

            return entity.Id;
        }

        private async Task UpdatePersonDataAsync(ApplicationInDTO aInDto, AApplication entity)
        {
            var person = await _personService.CreatePersonAsync(aInDto.Person);
            foreach (var personIdObj in person.PPersonIds)
            {
                if (personIdObj.PidTypeId == PidType.Egn)
                {
                    entity.ModifiedProperties.Add(nameof(entity.Egn));
                    entity.ModifiedProperties.Add(nameof(entity.EgnId));
                    entity.EgnId = personIdObj.Id;
                    entity.EgnNavigation = personIdObj;
                }
                else if (personIdObj.PidTypeId == PidType.Lnch)
                {
                    entity.ModifiedProperties.Add(nameof(entity.Lnch));
                    entity.ModifiedProperties.Add(nameof(entity.LnchId));
                    entity.LnchId = personIdObj.Id;

                    entity.LnchNavigation = personIdObj;
                }
                else if (personIdObj.PidTypeId == PidType.Ln)
                {
                    entity.ModifiedProperties.Add(nameof(entity.Ln));
                    entity.ModifiedProperties.Add(nameof(entity.LnId));
                    entity.LnId = personIdObj.Id;

                    entity.LnNavigation = personIdObj;
                }
                else if (personIdObj.PidTypeId == PidType.Suid)
                {
                    entity.ModifiedProperties.Add(nameof(entity.SuidId));
                    entity.ModifiedProperties.Add(nameof(entity.Suid));
                    entity.Suid = personIdObj.Pid;
                    entity.SuidId = personIdObj.Id;
                    entity.SuidNavigation = personIdObj;
                }

                dbContext.ApplyChanges(personIdObj, new List<IBaseIdEntity>());
            }

            dbContext.ApplyChanges(entity, new List<IBaseIdEntity>());
            await dbContext.SaveChangesAsync();
        }


        private async Task UpdateTransactionsAsync(ApplicationInDTO aInDto, AApplication entity)
        {
            entity.AAppPersAliases = mapper.MapTransactions<PersonAliasDTO, AAppPersAlias>(aInDto.Person.PersonAliasTransactions);
            entity.AAppCitizenships = CaisMapper.MapMultipleChooseToEntityList<AAppCitizenship, string, string>(aInDto.Person.Nationalities, nameof(AAppCitizenship.Id), nameof(AAppCitizenship.CountryId));
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        public async Task<AReport> GenerateReportFromApplication(string applicationID)
        {


            var statuses = await Task.FromResult(dbContext.AApplicationStatuses.Where(a => a.Code == ApplicationConstants.ApplicationStatuses.ApprovedApplication).ToList());
            if (statuses.Count != 1)
            {
                throw new Exception($"Application statuses do not exist. Statuses: {ApplicationConstants.ApplicationStatuses.ApprovedApplication}, {ApplicationConstants.ApplicationStatuses.BulletinsCheck}, {ApplicationConstants.ApplicationStatuses.CertificateContentReady}");



            }
            var systemParameters = await Task.FromResult(dbContext.GSystemParameters.Where(x => x.Code == SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS
            ).ToList());
            if (systemParameters.Count != 1)
            {
                throw new Exception($"Application statuses do not exist. Statuses: {SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS}, {SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME}");



            }
            var certificateValidityMonths = systemParameters.First(x => x.Code == SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS).ValueNumber;
            if (certificateValidityMonths == null)
            {
                throw new Exception($"System parameter {SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS} not set.");
            }

            var applicationStatus = statuses.First(a => a.Code == ApplicationConstants.ApplicationStatuses.ApprovedApplication);




            var application = await dbContext.AApplications
            .Include(a => a.EgnNavigation)
            .Include(a => a.LnchNavigation)
            .Include(a => a.LnNavigation)
            .Include(a => a.SuidNavigation)
            .Include(a => a.ApplicationType)
            .Include(x => x.AReports)
            .FirstOrDefaultAsync(aa => aa.Id == applicationID);

            var reportDb = application.AReports.FirstOrDefault();
            if (reportDb != null) return reportDb;

            var report = await GenerateReportFromApplication(application, applicationStatus, (int)certificateValidityMonths);
            await dbContext.SaveChangesAsync();



            return report;

        }


        public async Task<DDocContent> GetReportContent(string reportID)
        {
            var content = await dbContext.AReports.Where(x => x.Id == reportID && x.Doc != null).Select(x => x.Doc.DocContent).FirstOrDefaultAsync();
            //if (content == null)
            //{
            // throw new Exception("Certificate does not exist.");
            //}
            return content;
        }



        public async Task<AReport> GenerateReportFromApplication(AApplication application, AApplicationStatus applicationStatus, int validityMonths = 6)            //string certificateWithoutBulletinStatusID = ApplicationConstants.ApplicationStatuses.CertificateContentReady, string certificateWithBulletinStatusID = ApplicationConstants.ApplicationStatuses.BulletinsCheck)
        {   //трябва да са попълнени следните стойности:
            //       .Include(a => a.EgnNavigation)
            //       .Include(a => a.LnchNavigation)
            //       .Include(a => a.LnNavigation)
            //       .Include(a => a.SuidNavigation)
            var pids = new List<string>();
            if (application.EgnId != null && application.EgnNavigation != null)
            {
                pids.Add(application.EgnNavigation.PersonId);

            }
            if (application.LnchId != null && application.LnchNavigation != null)
            {
                pids.Add(application.LnchNavigation.PersonId);

            }
            if (application.LnId != null && application.LnNavigation != null)
            {
                pids.Add(application.LnNavigation.PersonId);

            }
            if (application.SuidId != null && application.SuidNavigation != null)
            {
                pids.Add(application.SuidNavigation.PersonId);

            }

            AReport rep = new AReport();
            rep.Id = BaseEntity.GenerateNewId();
            rep.ApplicationId = application.Id;
            rep.RegistrationNumber = await _registerTypeService.GetRegisterNumberForReport(application.CsAuthorityId);
            rep.ValidFrom = DateTime.UtcNow;
            rep.ValidTo = DateTime.UtcNow.AddMonths(validityMonths);

            if (pids.Count > 0)
            {
                var bulletins = await dbContext.BBulletins.Where(b => b.Status.Code != BulletinConstants.Status.Deleted
                                 && b.PBulletinIds.Any(bulID => pids.Contains(bulID.Person.PersonId))).Select(b => new { b.Id, b.DecisionDate }).Distinct().ToListAsync();
                if (bulletins.Count > 0)
                {
                    rep.ARepBulletins = bulletins.OrderByDescending(b => b.DecisionDate).Select(b =>
                    {

                        return new ARepBulletin()
                        {
                            Id = BaseEntity.GenerateNewId(),
                            BulletinId = b.Id,
                            //Bulletin = b,
                            ReportId = rep.Id,
                            Report = rep

                        };
                    }).ToList();

                }
            }
            //_applicationService.SetApplicationStatus(application, applicationStatus, "Създаване на справка");

            application.AReports.Add(rep);
            dbContext.AReports.Add(rep);
            dbContext.ARepBulletins.AddRange(rep.ARepBulletins);
            dbContext.AApplications.Update(application);

            return rep;
        }

    }
}
