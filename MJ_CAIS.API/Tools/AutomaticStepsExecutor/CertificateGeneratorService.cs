using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
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
        public CertificateGeneratorService(CaisDbContext dbContext, ILogger<CertificateGeneratorService> logger, IApplicationService applicationService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _applicationService = applicationService;


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
                var statuses = await _dbContext.AApplicationStatuses.Where(a => a.Code == ApplicationConstants.ApplicationStatuses.BulletinsCheck ||
                                                   a.Code == ApplicationConstants.ApplicationStatuses.CertificateContentReady 
                                                   || a.Code == ApplicationConstants.ApplicationStatuses.ApprovedApplication).ToListAsync();
                if (statuses.Count != 3)
                {
                    throw new Exception($"Application statuses do not exist. Statuses: {ApplicationConstants.ApplicationStatuses.ApprovedApplication}, {ApplicationConstants.ApplicationStatuses.BulletinsCheck}, {ApplicationConstants.ApplicationStatuses.CertificateContentReady}");

                }
                var certificateValidityMonths = (await _dbContext.GSystemParameters.FirstOrDefaultAsync(x => x.Code == SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS))?.ValueNumber;
                if (certificateValidityMonths == null)
                {
                    throw new Exception($"System parameter {SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS} not set.");
                }

                var applicationStatus = statuses.First(a=> a.Code == ApplicationConstants.ApplicationStatuses.ApprovedApplication);
                var certificateContentReadyStatus = statuses.First(a => a.Code == ApplicationConstants.ApplicationStatuses.CertificateContentReady);
                var  bulletinCheckStatus = statuses.First(a => a.Code == ApplicationConstants.ApplicationStatuses.BulletinsCheck);
                foreach (IBaseIdEntity entity in entities)
                {
                    numberOfProcesedEntities++;
                    try
                    {
                        var application = (AApplication)entity;
                        await _applicationService.GenerateCertificateFromApplication(application, applicationStatus, bulletinCheckStatus,certificateContentReadyStatus, (int)certificateValidityMonths);
                        await _dbContext.SaveChangesAsync();
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


        public async Task<List<IBaseIdEntity>> SelectEntitiesAsync(int pageSize)
        {
            var result = await Task.FromResult(_dbContext.AApplications
                                 .Where(aa => aa.StatusCode == ApplicationConstants.ApplicationStatuses.ApprovedApplication
                                            && !aa.ACertificates.Any())
                                 .OrderBy(a => a.CreatedOn)
                                 .Take(pageSize)
                               .ToList<IBaseIdEntity>());
            return result;
        }
    }
}
