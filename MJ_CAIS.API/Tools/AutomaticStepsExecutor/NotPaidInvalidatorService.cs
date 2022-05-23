using Microsoft.Extensions.Logging;
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
    public class NotPaidInvalidatorService : IAutomaticStepService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<NotPaidInvalidatorService> _logger;

        public NotPaidInvalidatorService(CaisDbContext dbContext, ILogger<NotPaidInvalidatorService> logger)
        {
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

        public async Task<AutomaticStepResult> ProcessEntitiesAsync(List<IBaseIdEntity> entities)
        {
            AutomaticStepResult result = new AutomaticStepResult();
            int numberOfProcesedEntities = 0;
            int numberOfSuccessEntities = 0;
            int numberOfFailedEntities = 0;
            if (entities.Count > 0)
            {
                numberOfProcesedEntities = entities.Count;
                var statuses = await _dbContext.AApplicationStatuses.Where(a => a.Code == ApplicationConstants.ApplicationStatuses.Canceled).ToListAsync();
                if (statuses.Count != 1)
                {
                    throw new Exception($"Application statuses do not exist. Statuses: {ApplicationConstants.ApplicationStatuses.Canceled }");

                }

                entities.ForEach(x =>
                  {
                      var application = (AApplication)x;
                      application.StatusCode = statuses.First().Code;

                  });

                _dbContext.AApplications.UpdateRange(entities.Select(x => (AApplication)x));
                var resultCount = await _dbContext.SaveChangesAsync();
                numberOfSuccessEntities = resultCount;
                //безсмислено, защото или ще минат всички, или никой
                numberOfFailedEntities = numberOfProcesedEntities - numberOfSuccessEntities;
            }

            return result;

        }

        public async Task<List<IBaseIdEntity>> SelectEntitiesAsync(int pageSize)
        {
            var systemParametrs = await _dbContext.GSystemParameters.Where(x => x.Code == SystemParametersConstants.SystemParametersNames.TERM_FOR_PAYMENT_DESK_DAYS).ToListAsync();

            if (systemParametrs.Count != 1 || systemParametrs.Any(x => x.ValueNumber == null))
            {
                throw new Exception($"System parameter {SystemParametersConstants.SystemParametersNames.TERM_FOR_PAYMENT_DESK_DAYS} and {SystemParametersConstants.SystemParametersNames.TERM_FOR_PAYMENT_WEB_DAYS} are not set.");
            }
            var systemParamValidityPeriodOnDesk = (int)systemParametrs.FirstOrDefault(x => x.Code == SystemParametersConstants.SystemParametersNames.TERM_FOR_PAYMENT_DESK_DAYS).ValueNumber;
            //var systemParamValidityPeriodWeb = (int)systemParametrs.FirstOrDefault(x => x.Code == SystemParametersConstants.SystemParametersNames.TERM_FOR_PAYMENT_WEB_DAYS).ValueNumber;
            var startDateOnDesk = DateTime.UtcNow.AddDays(-systemParamValidityPeriodOnDesk).Date;
            //var startDateWeb = DateTime.UtcNow.AddDays(-systemParamValidityPeriodWeb).Date;
            var result = await Task.FromResult(_dbContext.AApplications
                                .Where(aa => aa.StatusCode == ApplicationConstants.ApplicationStatuses.CheckPayment
                                                           && aa.CreatedOn < startDateOnDesk)//((aa.CreatedOn < startDateOnDesk && aa.WApplicationId == null)
                                                                                             //|| (aa.CreatedOn < startDateWeb && aa.WApplicationId != null))
                                                             .OrderBy(a => a.CreatedOn)
                              .Take(pageSize).ToList<IBaseIdEntity>());
            return result;
        }
    }
}
