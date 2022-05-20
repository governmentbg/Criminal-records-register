﻿using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
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
    public class PaymentCheckerService : IAutomaticStepService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<PaymentCheckerService> _logger;

        public PaymentCheckerService(CaisDbContext dbContext, ILogger<PaymentCheckerService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;



        }

        public async Task PreSelectAsync()
        {
       
        }

        public async Task<List<IBaseIdEntity>> SelectEntitiesAsync()
        {
            var result = await Task.FromResult(_dbContext.WApplications
                               .Where(aa => aa.StatusCode == ApplicationConstants.ApplicationStatuses.WebCheckPayment
                                                      
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
            AutomaticStepResult result = new AutomaticStepResult();
            int numberOfProcesedEntities = 0;
            int numberOfSuccessEntities = 0;
            int numberOfFailedEntities = 0;
            if (entities.Count > 0)
            {
                numberOfProcesedEntities = entities.Count;
                var statuses = await _dbContext.AApplicationStatuses.Where(a => a.Code == ApplicationConstants.ApplicationStatuses.ApprovedApplication).ToListAsync();
                if (statuses.Count != 1)
                {
                    throw new Exception($"Application statuses do not exist. Statuses: {ApplicationConstants.ApplicationStatuses.ApprovedApplication }");

                }
                var systemParamValidityPeriodWeb = (int?)_dbContext.GSystemParameters.FirstOrDefault(x => x.Code == SystemParametersConstants.SystemParametersNames.TERM_FOR_PAYMENT_WEB_DAYS)?.ValueNumber;
                if (systemParamValidityPeriodWeb == null)
                {
                    throw new Exception($"System parameter {SystemParametersConstants.SystemParametersNames.TERM_FOR_PAYMENT_WEB_DAYS} is not set.");
                }
                var startDateWeb = DateTime.UtcNow.AddDays(-systemParamValidityPeriodWeb.Value).Date;

                foreach (IBaseIdEntity entity in entities)
                {
                    numberOfProcesedEntities++;
                    try
                    {
                        var wapplication = (WApplication)entity;
                        //todo: check payment
                        //todo: да гледаме ли срок на плащането, ако не е платено в срок, но все пак е платено какво става?!  
                        bool isPaid = false;
                        if (isPaid)
                        {
                           await  AutomaticStepsHelper.ProcessWebApplicationToApplicationAsync(wapplication, _dbContext);
                            //todo: must add some FK for payment?!
                        }
                        else
                        {
                            if(wapplication.CreatedOn.Value.Date< startDateWeb)
                            {
                                wapplication.StatusCode = ApplicationConstants.ApplicationStatuses.WebCanceled;
                            }
                            _dbContext.WApplications.Update(wapplication);
                        }

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

        public async Task PostProcessAsync()
        {
           
        }
    }
}