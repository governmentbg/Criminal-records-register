using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.ExternalWebServices.Contracts;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MJ_CAIS.Services.Contracts;

namespace AutomaticStepsExecutor
{
    public class CertificateDelivererService : IAutomaticStepService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<CertificateDelivererService> _logger;
        private readonly ICertificateGenerationService _certificateGenerationService;
        private readonly ICertificateService _certificateService;

        public CertificateDelivererService(CaisDbContext dbContext, ILogger<CertificateDelivererService> logger, ICertificateGenerationService certificateGenerationService, ICertificateService certificateService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _certificateGenerationService = certificateGenerationService;
            _certificateService = certificateService;
        }

        public async Task PreSelectAsync(Microsoft.Extensions.Configuration.IConfiguration config)
        {
        
        }

        public async Task<List<IBaseIdEntity>> SelectEntitiesAsync(int pageSize, Microsoft.Extensions.Configuration.IConfiguration config)
        {
            List<string> processedTypes = new List<string>() { ApplicationConstants.ApplicationTypes.WebCertificate, ApplicationConstants.ApplicationTypes.WebExternalCertificate };

            var result = await Task.FromResult(_dbContext.ACertificates.AsNoTracking()
                                    .Include(a=>a.Application).AsNoTracking()
                                    .Include(a=>a.Application.SrvcResRcptMeth).AsNoTracking()
                             .Where(aa => aa.StatusCode == ApplicationConstants.ApplicationStatuses.CertificateForDelivery
                              && processedTypes.Contains(aa.Application.ApplicationType.Code)                             
                              )
                              .OrderBy(a=>a.CreatedOn)
                              .Take(pageSize)
                              .ToList<IBaseIdEntity>());
            return result;

        }

        public async Task PostSelectAsync(Microsoft.Extensions.Configuration.IConfiguration config)
        {
          
        }

        public async Task PreProcessAsync(Microsoft.Extensions.Configuration.IConfiguration config)
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
                var statuses = await Task.FromResult(_dbContext.AApplicationStatuses.Where(a => a.Code == ApplicationConstants.ApplicationStatuses.Delivered ).ToList());
                if (statuses.Count != 1)
                {
                    throw new Exception($"Application statuses do not exist. Statuses: {ApplicationConstants.ApplicationStatuses.Delivered}");

                }
                var statusCertificateDelivered = statuses.First();
                var webPortalUrl = await _certificateGenerationService.GetWebPortalAddress();
                //todo: get mail data
                var sysParamsForMail = await _dbContext.GSystemParameters.AsNoTracking().Where(s => s.Code == SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_BODY_FILENAME
                || s.Code == SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_SUBJECT_FILENAME).ToListAsync();
                if (sysParamsForMail.Count != 2 || sysParamsForMail.Any(x => string.IsNullOrEmpty(x.ValueString)))
                {
                    throw new Exception($"System parameters {SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_SUBJECT_FILENAME} and {SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_BODY_FILENAME} are not set.");
                }
                string mailSubjectTemplate = AutomaticStepsHelper.GetTextFromFile(sysParamsForMail.First(s => s.Code == SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_SUBJECT_FILENAME).ValueString);
                string mailBodyTemplate = AutomaticStepsHelper.GetTextFromFile(sysParamsForMail.First(s => s.Code == SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_BODY_FILENAME).ValueString);
             
                foreach (IBaseIdEntity entity in entities)
                {
                    numberOfProcesedEntities++;
                    try
                    {
                        var certificate = (ACertificate)entity;
                        await _certificateGenerationService.DeliverCertificateAsync(certificate,  mailBodyTemplate, mailSubjectTemplate, webPortalUrl);
                        _certificateService.SetCertificateStatus(certificate, statusCertificateDelivered, "Приключена обработка");
                        await _dbContext.SaveChangesAsync();
                        numberOfSuccessEntities++;
                    }
                    catch (Exception ex)
                    {
                        numberOfFailedEntities++;
                        _logger.LogError($"CertificateID {entity.Id}: " + ex.Message, ex.Data, ex);
                    }


                }
            }
            return result;
        }

        public async Task PostProcessAsync(Microsoft.Extensions.Configuration.IConfiguration config)
        {
          
        }
    }
}
