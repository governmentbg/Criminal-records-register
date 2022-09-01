
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.ExternalWebServices.Contracts;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;

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
       // private readonly IReportService _reportService;
       // private readonly IReportGenerationService _reportGenerationService;
        private readonly ICertificateGenerationService _certificateService;
        public CertificateGeneratorService(CaisDbContext dbContext, ILogger<CertificateGeneratorService> logger, IApplicationService applicationService, IReportService reportService, IReportGenerationService reportGenerationService,
            ICertificateGenerationService certificateService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _applicationService = applicationService;
           // _reportService = reportService;
          //_reportGenerationService = reportGenerationService;
            _certificateService = certificateService;

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
                //todo: направо всички за сертификат?!
                var statuses = await Task.FromResult( _dbContext.AApplicationStatuses.AsNoTracking().Where(a => 
                                                      a.Code == ApplicationConstants.ApplicationStatuses.BulletinsCheck
                                                   || a.Code == ApplicationConstants.ApplicationStatuses.CertificateContentReady
                                                   || a.Code == ApplicationConstants.ApplicationStatuses.ApprovedApplication
                                                   || a.Code == ApplicationConstants.ApplicationStatuses.CertificateUserSign
                                                   || a.Code == ApplicationConstants.ApplicationStatuses.CertificateForDelivery
                                                   || a.Code == ApplicationConstants.ApplicationStatuses.CertificatePaperPrint
                                                   || a.Code == ApplicationConstants.ApplicationStatuses.Delivered).ToList());
                if (statuses.Count != 7)
                {
                    throw new Exception($"Application statuses do not exist. Statuses: {ApplicationConstants.ApplicationStatuses.ApprovedApplication}, {ApplicationConstants.ApplicationStatuses.BulletinsCheck}, {ApplicationConstants.ApplicationStatuses.CertificateContentReady}," +
                        $" {ApplicationConstants.ApplicationStatuses.CertificateUserSign},{ ApplicationConstants.ApplicationStatuses.CertificateForDelivery },                                                   { ApplicationConstants.ApplicationStatuses.CertificatePaperPrint},"+
                         $"{ ApplicationConstants.ApplicationStatuses.Delivered} ");

                }
                var systemParameters = await Task.FromResult(_dbContext.GSystemParameters.AsNoTracking().Where(x => x.Code == SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS
                                                    || x.Code == SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_SUBJECT_FILENAME
                                                    || x.Code == SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_BODY_FILENAME
                                                    || x.Code == SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME).ToList());
                if (systemParameters.Count != 4)
                {
                    throw new Exception($"Parameters do not exist. Parameters: {SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS}, {SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME}" +
                        $"{SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_SUBJECT_FILENAME} , {SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME}");

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
                var statusCertificateUserSign = statuses.First(s => s.Code == ApplicationConstants.ApplicationStatuses.CertificateUserSign);
                var statusForDelivery = statuses.First(s => s.Code == ApplicationConstants.ApplicationStatuses.CertificateForDelivery);
                var statusCertificatePaperprint = statuses.First(s => s.Code == ApplicationConstants.ApplicationStatuses.CertificatePaperPrint);
                var statusCertificateDelivered = statuses.First(s => s.Code == ApplicationConstants.ApplicationStatuses.Delivered);
                string mailSubjectTemplate = AutomaticStepsHelper.GetTextFromFile(systemParameters.First(s => s.Code == SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_SUBJECT_FILENAME).ValueString);
                string mailBodyTemplate = AutomaticStepsHelper.GetTextFromFile(systemParameters.First(s => s.Code == SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_BODY_FILENAME).ValueString);
                var webPortalUrl = await _certificateService.GetWebPortalAddress();


                foreach (IBaseIdEntity entity in entities)
                {
                    numberOfProcesedEntities++;
                    try
                    {
                        var application = (AApplication)entity;
                        //не обработваме справки
                        //if (application.ApplicationType.Code == ApplicationConstants.ApplicationTypes.ConvictionRequest)
                        //{
                        //    var report = await _reportService.GenerateReportFromApplication(application, applicationStatus, (int)certificateValidityMonths);
                        //    await _dbContext.SaveChangesAsync();
                        //    var file = await _reportGenerationService.CreateReport(report, signingCertificateName);                      
                        //    await _dbContext.SaveChangesAsync();
                        //    //System.IO.File.WriteAllBytes($"hello{application.Id}.pdf", file);
                        //}
                        //else
                        //{
                        _logger.LogTrace($"{application.Id} : Before GenerateCertificateFromApplication.");
                         var cert = await _applicationService.GenerateCertificateFromApplication(application, applicationStatus, bulletinCheckStatus, certificateContentReadyStatus, (int)certificateValidityMonths);
                        _logger.LogTrace($"{application.Id} : After GenerateCertificateFromApplication.");
                        await _dbContext.SaveChangesAsync();
                        _logger.LogTrace($"{application.Id} : After SaveChanges.");
                        _dbContext.ChangeTracker.Clear();
                        if(cert.StatusCode== ApplicationConstants.ApplicationStatuses.CertificateContentReady)
                        {
                            //създаване на pdf и завършване на процеса
                            _logger.LogTrace($"{application.Id} : Before CreateCertificate.");
                            var file = await _certificateService.CreateCertificate(cert, mailSubjectTemplate, mailBodyTemplate, signingCertificateName,
                                                                         statusCertificateUserSign, statusForDelivery, statusCertificateDelivered, statusCertificatePaperprint, webPortalUrl);
                            _logger.LogTrace($"{application.Id} : Before SaveChangesAsync.");
                            await _dbContext.SaveChangesAsync();
                            _logger.LogTrace($"{application.Id} : After SaveChangesAsync.");
                            _dbContext.ChangeTracker.Clear();
                        }
                        //}

                        numberOfSuccessEntities++;
                    }
                    catch (Exception ex)
                    {
                        numberOfFailedEntities++;
                        _dbContext.ChangeTracker.Clear();
                        _logger.LogError($"ApplicationID {entity.Id}: " + ex.Message, ex.Data, ex);
                    }


                }
            }
            return result;


        }


        public async Task<List<IBaseIdEntity>> SelectEntitiesAsync(int pageSize, Microsoft.Extensions.Configuration.IConfiguration config, int numberOfPage = 0)
        {
            List<string> processedTypes = new List<string>() { ApplicationConstants.ApplicationTypes.WebCertificate, ApplicationConstants.ApplicationTypes.WebExternalCertificate };
            var result = await Task.FromResult(
                                  _dbContext.AApplications.AsNoTracking()
                                           .Include(a => a.EgnNavigation).AsNoTracking()
                                           .Include(a => a.LnchNavigation).AsNoTracking()
                                           .Include(a => a.LnNavigation).AsNoTracking()
                                           .Include(a => a.SuidNavigation).AsNoTracking()
                                           .Include(a => a.ApplicationType).AsNoTracking()
                                           .Include(a=>a.PurposeNavigation).AsNoTracking()
                                           .Include(а=>а.AStatusHes).AsNoTracking()
                                 .Where(aa => aa.StatusCode == ApplicationConstants.ApplicationStatuses.ApprovedApplication
                                 && aa.ServiceMigrationId ==null
                                 && processedTypes.Contains(aa.ApplicationType.Code)
                                 //това е краен статус, затова търсим само такива, за които няма генерирани репорти или сертификати
                                 && !aa.ACertificates.Any())
                                 .OrderByDescending(a => a.CreatedOn)
                                 .Skip(numberOfPage*pageSize)
                                 .Take(pageSize)

                                 .ToList()
                                 
            .ToList<IBaseIdEntity>());
            return result;
        }
    }
}
