﻿using Microsoft.Extensions.Logging;
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
        public CertificateGeneratorService(CaisDbContext dbContext, ILogger<CertificateGeneratorService> logger, IApplicationService applicationService) {
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

        public async Task<AutomaticStepResult> ProcessEntitiesAsync(List<BaseEntity> entities)
        {
            AutomaticStepResult result = new AutomaticStepResult();
            int numberOfProcesedEntities = 0;
            int numberOfSuccessEntities = 0;
            int numberOfFailedEntities = 0;
            if (entities.Count > 0)
            {
                var statuses = await _dbContext.AApplicationStatuses.Where(a => a.Code == ApplicationConstants.ApplicationStatuses.BulletinsForPurpose ||
                                                   a.Code == ApplicationConstants.ApplicationStatuses.CertificateReady).ToListAsync();
                if (statuses.Count != 2)
                {
                    throw new Exception($"Application statuses do not exist. Statuses: {ApplicationConstants.ApplicationStatuses.BulletinsForPurpose}, {ApplicationConstants.ApplicationStatuses.CertificateReady}");

                }

                foreach (BaseEntity entity in entities)
                {
                    numberOfProcesedEntities++;
                    try
                    {
                        var application = (AApplication)entity;
                        await _applicationService.GenerateCertificateFromApplication(application, ApplicationConstants.ApplicationStatuses.CertificateReady, ApplicationConstants.ApplicationStatuses.BulletinsForPurpose);
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


        public async Task<List<BaseEntity>> SelectEntitiesAsync()
        {
            var result = await Task.FromResult( _dbContext.AApplications
                               .Where(aa=>aa.StatusCode==ApplicationConstants.ApplicationStatuses.HasBulletins)
                               .ToList<BaseEntity>());
            return result;
        }
    }
}
