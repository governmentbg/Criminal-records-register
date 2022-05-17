using Microsoft.Extensions.Logging;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
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
        public CertificateGeneratorService(CaisDbContext dbContext, ILogger<CertificateGeneratorService> logger) {
            _dbContext = dbContext;
            _logger = logger;
        
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
            foreach (BaseEntity entity in entities)
            {
                var application = (AApplication)entity;

                _logger.LogInformation(application.RegistrationNumber);
            }

            return result;


        }

        public async Task<List<BaseEntity>> SelectEntitiesAsync()
        {
            var result = await Task.FromResult( _dbContext.AApplications
                                //todo: add filter
                                .ToList<BaseEntity>());
            return result;
        }
    }
}
