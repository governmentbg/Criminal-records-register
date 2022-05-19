using Microsoft.Extensions.Logging;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MJ_CAIS.ExternalWebServices.Contracts;

namespace AutomaticStepsExecutor
{
    public class CertificateCreatorService : IAutomaticStepService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<CertificateCreatorService> _logger;
        private readonly ICertificateGenerationService _certificateService;
        public CertificateCreatorService(CaisDbContext dbContext, ILogger<CertificateCreatorService> logger, ICertificateGenerationService certificateService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _certificateService = certificateService;


        }
        public Task PostProcessAsync()
        {
            throw new NotImplementedException();
        }

        public Task PostSelectAsync()
        {
            throw new NotImplementedException();
        }

        public Task PreProcessAsync()
        {
            throw new NotImplementedException();
        }

        public Task PreSelectAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AutomaticStepResult> ProcessEntitiesAsync(List<BaseEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<List<BaseEntity>> SelectEntitiesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
