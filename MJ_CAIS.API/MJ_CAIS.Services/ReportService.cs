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
        private readonly IManagePersonService _managePersonService;


        public ReportService(IMapper mapper, 
            IReportRepository reportRepository, 
            IApplicationService applicationService, 
            IRegisterTypeService registerTypeService,
            IManagePersonService managePersonService)
            : base(mapper, reportRepository)
        {
            _reportRepository = reportRepository;
            _applicationService = applicationService;
            _registerTypeService = registerTypeService;
            _managePersonService = managePersonService;
        }

        public async Task<string> InsertAsync(ApplicationInDTO aInDto)
        {

            var entity = mapper.MapToEntity<ApplicationInDTO, AApplication>(aInDto, true);
            var regNumber = await _registerTypeService.GetRegisterNumberForApplicationOnDesk(entity.CsAuthorityId);
            entity.RegistrationNumber = regNumber;

            await UpdateTransactionsAsync(aInDto, entity);
            await _reportRepository.SaveEntityAsync(entity, true);


            //dbContext.Entry(entity).State = EntityState.Detached;
            //todo: защо се прави това тук, не се ли оправя версията в бейс обект
            entity.Version = 1;
            entity.EntityState = EntityStateEnum.Modified;
            entity.ModifiedProperties = new List<string> { nameof(entity.Version) };

            await UpdatePersonDataAsync(aInDto, entity); ;

            return entity.Id;
        }

        private async Task UpdatePersonDataAsync(ApplicationInDTO aInDto, AApplication entity)
        {
            aInDto.Person.TableName = ContextTable.Report;
            aInDto.Person.TableId = entity.Id;

            var person = await _managePersonService.CreatePersonAsync(aInDto.Person);
            foreach (var personIdObj in person.PPersonIds)
            {
                if (entity.ModifiedProperties == null)
                {
                    entity.ModifiedProperties = new List<string>();
                }
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

                _reportRepository.ApplyChanges(personIdObj, new List<IBaseIdEntity>());
            }

            _reportRepository.ApplyChanges(entity, new List<IBaseIdEntity>());
            await _reportRepository.SaveChangesAsync();
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


            var statuses = await (await _reportRepository.FindAsync<AApplicationStatus>(a =>
            a.Code == ApplicationConstants.ApplicationStatuses.ApprovedApplication)).ToListAsync();
            //await Task.FromResult(dbContext.AApplicationStatuses.Where(a => a.Code == ApplicationConstants.ApplicationStatuses.ApprovedApplication).ToList());
            if (statuses.Count != 1)
            {
                throw new Exception($"Application statuses do not exist. Statuses: {ApplicationConstants.ApplicationStatuses.ApprovedApplication}, {ApplicationConstants.ApplicationStatuses.BulletinsCheck}, {ApplicationConstants.ApplicationStatuses.CertificateContentReady}");



            }
            var systemParameters = await (await _reportRepository.FindAsync<GSystemParameter>(x => x.Code == SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS)).ToListAsync();

            // await Task.FromResult(dbContext.GSystemParameters.Where(x => x.Code == SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS
            //).ToList());
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

            AApplication? application = await _reportRepository.GetApplicationData(applicationID);
            //todo: change
            var reportDb = new AReport();//application.AReports.FirstOrDefault();
            if (reportDb != null) return reportDb;

            var report = await GenerateReportFromApplication(application, applicationStatus, (int)certificateValidityMonths);
            await _reportRepository.SaveChangesAsync();



            return report;

        }

   

        public async Task<DDocContent> GetReportContent(string reportID)
        {
           return await _reportRepository.GetReportContent(reportID);
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
            rep.EntityState = EntityStateEnum.Added;
            //todo: change
            //rep.ApplicationId = application.Id;
            rep.RegistrationNumber = await _registerTypeService.GetRegisterNumberForReport(application.CsAuthorityId);
            rep.ValidFrom = DateTime.Now;
            rep.ValidTo = DateTime.Now.AddMonths(validityMonths);

            if (pids.Count > 0)
            {
                var bulletins = await _reportRepository.GetBulletinesPerPerson(pids);

                if (bulletins.Count() > 0)
                {
                    var orderNumber = 0;
                    rep.ARepBulletins = bulletins.Select(b =>
                    {
                        orderNumber++;
                        return new ARepBulletin()
                        {
                            Id = BaseEntity.GenerateNewId(),
                            BulletinId = b.Id,
                            //Bulletin = b,
                            ReportId = rep.Id,
                            Report = rep,
                            EntityState = EntityStateEnum.Added,
                            OrderNumber = orderNumber

                        };
                    }).ToList();

                }
            }
            //_applicationService.SetApplicationStatus(application, applicationStatus, "Създаване на справка");
            //todo: change
            //application.AReports.Add(rep);
            // dbContext.AReports.Add(rep);
            //dbContext.ARepBulletins.AddRange(rep.ARepBulletins);
            _reportRepository.ApplyChanges(rep, rep.ARepBulletins.ToList<IBaseIdEntity>() , true);

            //dbContext.AApplications.Update(application);

            return rep;
        }

 
    }
}
