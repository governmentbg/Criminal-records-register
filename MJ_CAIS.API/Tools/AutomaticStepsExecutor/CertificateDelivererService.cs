using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.ExternalWebServices.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticStepsExecutor
{
    public class CertificateDelivererService : IAutomaticStepService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<CertificateDelivererService> _logger;
        private readonly ICertificateGenerationService _certificateService;

        public CertificateDelivererService(CaisDbContext dbContext, ILogger<CertificateDelivererService> logger, ICertificateGenerationService certificateService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _certificateService = certificateService;
        }

        public async Task PreSelectAsync(Microsoft.Extensions.Configuration.IConfiguration config)
        {
        
        }

        public async Task<List<IBaseIdEntity>> SelectEntitiesAsync(int pageSize, Microsoft.Extensions.Configuration.IConfiguration config)
        {
            var result = await Task.FromResult(_dbContext.ACertificates
                                    .Include(a=>a.Application)
                                    .Include(a=>a.Application.SrvcResRcptMeth)
                              //todo: дали има шанс да е в това състояние:ApplicationConstants.ApplicationStatuses.CertificateServerSign
                              .Where(aa => aa.StatusCode == ApplicationConstants.ApplicationStatuses.CertificateUserSigned
                              //todo: да гледаме ли срок на плащането, ако не е платено в срок?!                             
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
                        await _certificateService.DeliverCertificateAsync(certificate,  mailBodyTemplate, mailSubjectTemplate, webPortalUrl);
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
