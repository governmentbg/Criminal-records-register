using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DTO.Report;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System.Collections.Generic;
using MJ_CAIS.Common.Constants;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Services
{
    public class ReportService : BaseAsyncService<ReportDTO, ReportDTO, ReportGridDTO, AReport, string, CaisDbContext>, IReportService
    {
        private readonly IReportRepository _reportRepository;

        private readonly IApplicationService _applicationService;
        private readonly IRegisterTypeService _registerTypeService;


        public ReportService(IMapper mapper, IReportRepository reportRepository, IApplicationService applicationService, IRegisterTypeService registerTypeService)
            : base(mapper, reportRepository)
        {
            _reportRepository = reportRepository;
            _applicationService = applicationService;
            _registerTypeService = registerTypeService;

        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        public async Task<AReport> GenerateReportFromApplication(AApplication application, AApplicationStatus applicationStatus,int validityMonths = 6)            //string certificateWithoutBulletinStatusID = ApplicationConstants.ApplicationStatuses.CertificateContentReady, string certificateWithBulletinStatusID = ApplicationConstants.ApplicationStatuses.BulletinsCheck)
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
                                 && b.PBulletinIds.Any(bulID => pids.Contains(bulID.PersonId))).Distinct().ToListAsync();
                if (bulletins.Count > 0)
                {
                    rep.ARepBulletins = bulletins.OrderByDescending(b => b.DecisionDate).Select(b =>
                    {

                        return new ARepBulletin()
                        {
                            Id = BaseEntity.GenerateNewId(),
                            BulletinId = b.Id,
                            Bulletin = b,
                            ReportId = rep.Id,
                            Report = rep

                        };
                    }).ToList();

                }
            }
            _applicationService.SetApplicationStatus(application, applicationStatus, "Създаване на справка");
       
            application.AReports.Add(rep);
            dbContext.AReports.Add(rep);
            dbContext.ARepBulletins.AddRange(rep.ARepBulletins);
            dbContext.AApplications.Update(application);

            return rep;
        }

      }
}
