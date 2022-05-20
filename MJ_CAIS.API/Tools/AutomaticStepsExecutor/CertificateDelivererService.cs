using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticStepsExecutor
{
    public class CertificateDelivererService : IAutomaticStepService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<CertificateDelivererService> _logger;
    
        public CertificateDelivererService(CaisDbContext dbContext, ILogger<CertificateDelivererService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task PreSelectAsync()
        {
        
        }

        public async Task<List<IBaseIdEntity>> SelectEntitiesAsync()
        {
            var result = await Task.FromResult(_dbContext.AApplications
                              .Where(aa => aa.StatusCode == ApplicationConstants.ApplicationStatuses.CertificateServerSign
                              || aa.StatusCode == ApplicationConstants.ApplicationStatuses.CertificateUserSigned
                              //todo: да гледаме ли срок на плащането, ако не е платено в срок?!                             
                              )
                              .ToList<IBaseIdEntity>());
            return result;

        }

        public async Task PostSelectAsync()
        {
          
        }

        public async Task PreProcessAsync()
        {
           
        }

        public async Task<AutomaticStepResult> ProcessEntitiesAsync(List<IBaseIdEntity> entities)
        {
            return null;
        }

        public async Task PostProcessAsync()
        {
          
        }
    }
}
