using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.ExternalWebServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticStepsExecutor
{
    public class EDeliverySenderService : IAutomaticStepService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<EDeliverySenderService> _logger;
        private readonly ICAISEDeliveryService _senderService;

        public EDeliverySenderService(CaisDbContext dbContext, ILogger<EDeliverySenderService> logger, ICAISEDeliveryService senderService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _senderService = senderService;
        }
        public async Task PostProcessAsync(IConfiguration config)
        {
           
        }

        public  async Task PostSelectAsync(IConfiguration config)
        {
           
        }

        public async Task PreProcessAsync(IConfiguration config)
        {
            
        }

        public async Task PreSelectAsync(IConfiguration config)
        {
            
        }

        public async Task<AutomaticStepResult> ProcessEntitiesAsync(List<IBaseIdEntity> entities, IConfiguration config)
        {

            AutomaticStepResult result = new AutomaticStepResult();
            int numberOfProcesedEntities = 0;
            int numberOfSuccessEntities = 0;
            int numberOfFailedEntities = 0;
            if (entities.Count > 0)
            {
                foreach (IBaseIdEntity entity in entities)
                {
                    numberOfProcesedEntities++;
                    try
                    {
                        var msg = (EEdeliveryMsg)entity;
                        var returnValue =  await _senderService.SendCertificateToEDeliveryAsync(msg);
                        if (returnValue == -1)
                        {
                            _logger.LogError($"Delivery message ID: {entity.Id} - not delivered. ");
                        }
                        numberOfSuccessEntities++;
                    }
                    catch (Exception ex)
                    {
                        numberOfFailedEntities++;
                        _logger.LogError($"Delivery message ID: {entity.Id}: " + ex.Message, ex.Data, ex);
                    }


                }
            }
            return result;

        }

        public async Task<List<IBaseIdEntity>> SelectEntitiesAsync(int pageSize, IConfiguration config)
        {
            var sysParam = await _dbContext.GSystemParameters.Where(s => s.Code == SystemParametersConstants.SystemParametersNames.EDELIVERY_NUMBER_OF_ATTEMPTS).FirstOrDefaultAsync();
            if(sysParam==null || sysParam.ValueNumber == null)
            {
                throw new Exception($"System parameter {SystemParametersConstants.SystemParametersNames.EDELIVERY_NUMBER_OF_ATTEMPTS} not set.");
            }

            var result = await Task.FromResult(
                                  _dbContext.EEdeliveryMsgs
                                 .Where(msg=>msg.Status== EdeliveryConstants.EdeliveryStatuses.Pending ||
                                 (msg.Status==EdeliveryConstants.EdeliveryStatuses.Failed && msg.Attempts<sysParam.ValueNumber))
                                 .OrderBy(a => a.CreatedOn)
                                 .Take(pageSize)
                                 .ToList()

            .ToList<IBaseIdEntity>());
            return result;
        }
    }
}
