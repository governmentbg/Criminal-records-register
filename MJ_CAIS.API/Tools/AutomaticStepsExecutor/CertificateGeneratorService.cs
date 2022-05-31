using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.ExternalWebServices.Contracts;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticStepsExecutor
{
    public class CertificateGeneratorService : IAutomaticStepService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<CertificateGeneratorService> _logger;
        private readonly IApplicationService _applicationService;
        private readonly IReportService _reportService;
        private readonly IReportGenerationService _reportGenerationService;
        public CertificateGeneratorService(CaisDbContext dbContext, ILogger<CertificateGeneratorService> logger, IApplicationService applicationService, IReportService reportService, IReportGenerationService reportGenerationService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _applicationService = applicationService;
            _reportService = reportService;
            _reportGenerationService = reportGenerationService;

        }
        public async Task PostProcessAsync(Microsoft.Extensions.Configuration.IConfiguration config)
        {

        }

        public async Task PostSelectAsync(Microsoft.Extensions.Configuration.IConfiguration config)
        {

        }

        public async Task PreProcessAsync(Microsoft.Extensions.Configuration.IConfiguration config)
        {

        }

        public async Task PreSelectAsync(Microsoft.Extensions.Configuration.IConfiguration config)
        {

        }

        public async Task<AutomaticStepResult> ProcessEntitiesAsync(List<IBaseIdEntity> entities, Microsoft.Extensions.Configuration.IConfiguration config)
        {
            AutomaticStepResult result = new AutomaticStepResult();
            int numberOfProcesedEntities = 0;
            int numberOfSuccessEntities = 0;
            int numberOfFailedEntities = 0;
            if (entities.Count > 0)
            {
                var statuses = await _dbContext.AApplicationStatuses.Where(a => a.Code == ApplicationConstants.ApplicationStatuses.BulletinsCheck ||
                                                   a.Code == ApplicationConstants.ApplicationStatuses.CertificateContentReady
                                                   || a.Code == ApplicationConstants.ApplicationStatuses.ApprovedApplication).ToListAsync();
                if (statuses.Count != 3)
                {
                    throw new Exception($"Application statuses do not exist. Statuses: {ApplicationConstants.ApplicationStatuses.ApprovedApplication}, {ApplicationConstants.ApplicationStatuses.BulletinsCheck}, {ApplicationConstants.ApplicationStatuses.CertificateContentReady}");

                }
                var systemParameters = await _dbContext.GSystemParameters.Where(x => x.Code == SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS
                                                    || x.Code == SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME).ToListAsync();
                if (systemParameters.Count != 2)
                {
                    throw new Exception($"Application statuses do not exist. Statuses: {SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS}, {SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME}");

                }
                var certificateValidityMonths = systemParameters.First(x => x.Code == SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS).ValueNumber;
                if (certificateValidityMonths == null)
                {
                    throw new Exception($"System parameter {SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS} not set.");
                }
                var signingCertificateName = systemParameters.First(x => x.Code == SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME).ValueString;
                if (signingCertificateName == null)
                {
                    throw new Exception($"System parameter {SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME} not set.");
                }
                var applicationStatus = statuses.First(a => a.Code == ApplicationConstants.ApplicationStatuses.ApprovedApplication);
                var certificateContentReadyStatus = statuses.First(a => a.Code == ApplicationConstants.ApplicationStatuses.CertificateContentReady);
                var bulletinCheckStatus = statuses.First(a => a.Code == ApplicationConstants.ApplicationStatuses.BulletinsCheck);

                foreach (IBaseIdEntity entity in entities)
                {
                    numberOfProcesedEntities++;
                    try
                    {
                        var application = (AApplication)entity;
                        if (application.ApplicationType.Code == ApplicationConstants.ApplicationTypes.ConvictionRequest)
                        {
                            var report = await _reportService.GenerateReportFromApplication(application, applicationStatus, (int)certificateValidityMonths);
                            await _dbContext.SaveChangesAsync();
                            await _reportGenerationService.CreateReport(report, signingCertificateName);
                            await _dbContext.SaveChangesAsync();
                        }
                        else
                        {
                            await _applicationService.GenerateCertificateFromApplication(application, applicationStatus, bulletinCheckStatus, certificateContentReadyStatus, (int)certificateValidityMonths);
                            await _dbContext.SaveChangesAsync();
                        }

                        numberOfSuccessEntities++;
                    }
                    catch (Exception ex)
                    {
                        numberOfFailedEntities++;
                        _logger.LogError($"ApplicationID {entity.Id}: " + ex.Message, ex.Data, ex);
                    }


                }
            }
            return result;


        }


        public async Task<List<IBaseIdEntity>> SelectEntitiesAsync(int pageSize, Microsoft.Extensions.Configuration.IConfiguration config)
        {
            var result = await Task.FromResult(_dbContext.AApplications
                                             .Include(a => a.EgnNavigation)
                                             .Include(a => a.LnchNavigation)
                                             .Include(a => a.LnNavigation)
                                             .Include(a => a.SuidNavigation)
                                             .Include(a => a.ApplicationType)
                                 .Where(aa => aa.StatusCode == ApplicationConstants.ApplicationStatuses.ApprovedApplication
                                 //това е краен статус, затова търсим само такива, за които няма генерирани репорти или сертификати
                                            && ((aa.ApplicationType.Code == ApplicationConstants.ApplicationTypes.ConvictionRequest && !aa.AReports.Any())
                                               || (aa.ApplicationType.Code != ApplicationConstants.ApplicationTypes.ConvictionRequest && !aa.ACertificates.Any())))
                                 .OrderBy(a => a.CreatedOn)
                                 .Take(pageSize)
                               .ToList<IBaseIdEntity>());
            return result;
        }
    }
}
