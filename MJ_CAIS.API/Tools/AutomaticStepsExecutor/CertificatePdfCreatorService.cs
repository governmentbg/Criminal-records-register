using Microsoft.Extensions.Logging;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MJ_CAIS.ExternalWebServices.Contracts;
using MJ_CAIS.Common.Constants;
using System.Data.Entity;
using MJ_CAIS.DataAccess.Entities;

namespace AutomaticStepsExecutor
{
    public class CertificatePdfCreatorService : IAutomaticStepService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<CertificatePdfCreatorService> _logger;
        private readonly ICertificateGenerationService _certificateService;
        public CertificatePdfCreatorService(CaisDbContext dbContext, ILogger<CertificatePdfCreatorService> logger, ICertificateGenerationService certificateService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _certificateService = certificateService;


        }
        public async Task PostProcessAsync()
        {
            
        }

        public async Task PostSelectAsync()
        {
          
        }

        public async Task PreProcessAsync()
        {
           
        }

        public async Task PreSelectAsync()
        {
            
        }

        public async Task<AutomaticStepResult> ProcessEntitiesAsync(List<IBaseIdEntity> entities)
        {
            AutomaticStepResult result = new AutomaticStepResult();
            int numberOfProcesedEntities = 0;
            int numberOfSuccessEntities = 0;
            int numberOfFailedEntities = 0;
            if (entities.Count > 0)
            {
                ////todo: като се добави ID някой ден в таблицата да се връща ИД
                //var statuses = await _dbContext.AApplicationStatuses.Where(a => a.Code == ApplicationConstants.ApplicationStatuses.CertificateServerSign).ToListAsync();
                //if (statuses.Count() != 1)
                //{
                //    throw new Exception($"Application statuses do not exist. Statuses: {ApplicationConstants.ApplicationStatuses.CertificateServerSign}");

                //}

                var webPortalUrl = await _certificateService.GetWebPortalAddress();
                //todo: get mail data
                var sysParamsForMail = await _dbContext.GSystemParameters.Where(s => s.Code == SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_BODY_FILENAME
                || s.Code == SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_SUBJECT_FILENAME).ToListAsync();
                if (sysParamsForMail.Count != 2 || sysParamsForMail.Any(x=>string.IsNullOrEmpty(x.ValueString)))
                {
                    throw new Exception($"System parameters {SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_SUBJECT_FILENAME} and {SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_BODY_FILENAME} are not set.");
                }
                string mailSubjectTemplate = AutomaticStepsHelper.GetTextFromFile(sysParamsForMail.First(s=>s.Code== SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_SUBJECT_FILENAME).ValueString);
                string mailBodyTemplate = AutomaticStepsHelper.GetTextFromFile(sysParamsForMail.First(s => s.Code == SystemParametersConstants.SystemParametersNames.DELIVERY_MAIL_BODY_FILENAME).ValueString);

                foreach (IBaseIdEntity entity in entities)
                {
                    numberOfProcesedEntities++;
                    try
                    {
                        var certificate = (ACertificate)entity;
                        await _certificateService.CreateCertificate(certificate, mailSubjectTemplate, mailBodyTemplate, webPortalUrl);
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

        public async Task<List<IBaseIdEntity>> SelectEntitiesAsync(int pageSize)
        {
            var result = await Task.FromResult(_dbContext.ACertificates
                              .Include(c=>c.AAppBulletins)
                              .Include(c=>c.Application)
                              .Include(c=>c.Application.Purpose)
                              .Include(c => c.Application.SrvcResRcptMeth)
                              .Where(aa => aa.StatusCode == ApplicationConstants.ApplicationStatuses.CertificateContentReady)
                              .OrderBy(a => a.CreatedOn)
                              .Take(pageSize)
                              .ToList<IBaseIdEntity>());
            return result;

        }
    }
}
