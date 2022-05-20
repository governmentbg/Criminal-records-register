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
    internal class WApplicationProcessorService : IAutomaticStepService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<WApplicationProcessorService> _logger;

        public WApplicationProcessorService(CaisDbContext dbContext, ILogger<WApplicationProcessorService> logger)
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
                      //todo: дали е този статус?! include WWebRequests
                      .Where(aa => aa.StatusCode == ApplicationConstants.ApplicationStatuses.WebRegistersChecks)

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

                var internalApplicationType = await _dbContext.AApplicationTypes.FirstOrDefaultAsync(x => x.Code == ApplicationConstants.ApplicationTypes.InternalCode5);
                if (internalApplicationType == null)
                {
                    throw new Exception($"Code \"{ApplicationConstants.ApplicationTypes.InternalCode5}\" for internal applications is not set.");

                }

                var paymentMethodFree = await _dbContext.APaymentMethods.FirstOrDefaultAsync(x => x.Code == ApplicationConstants.PaymentMethodsCodes.Free);
                if (paymentMethodFree == null)
                {
                    throw new Exception($"Code \"{ApplicationConstants.PaymentMethodsCodes.Free}\" for payment method is not set.");

                }

                foreach (IBaseIdEntity entity in entities)
                {
                    numberOfProcesedEntities++;
                    try
                    {
                        var wapplication = (WApplication)entity;
                        //при неуспешни проверки в регистрите се анулира
                        if (!(await SuccessfullCheckInRegisters(wapplication)))
                        {
                            wapplication.StatusCode = ApplicationConstants.ApplicationStatuses.WebCanceled;
                            _dbContext.WApplications.Update(wapplication);
                            await _dbContext.SaveChangesAsync();
                            numberOfSuccessEntities++;
                            continue;
                        }
                        //ако е служебно заявление,влиза в цаис за обработка
                        if (wapplication.ApplicationTypeId == internalApplicationType.Id)
                        {
                            await AutomaticStepsHelper.ProcessWebApplicationToApplicationAsync(wapplication, _dbContext);
                            await _dbContext.SaveChangesAsync();
                            numberOfSuccessEntities++;
                            continue;
                        }
                        //ако е освободен от плащане - отива при съдия
                        if (wapplication.PaymentMethodId == paymentMethodFree.Id)
                        {
                            wapplication.StatusCode = ApplicationConstants.ApplicationStatuses.WebCheckTaxFree;
                            _dbContext.WApplications.Update(wapplication);
                            await _dbContext.SaveChangesAsync();
                            numberOfSuccessEntities++;
                            continue;
                        }
                        //дефолтно - очаква плащане
                        //todo: дали да не се прави и проверката за плащане тук и ако има плащане да преминава към следваща стъпка
                        wapplication.StatusCode = ApplicationConstants.ApplicationStatuses.WebCheckPayment;
                        _dbContext.WApplications.Update(wapplication);
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

        private async Task<bool> SuccessfullCheckInRegisters(WApplication wapplication)
        {
            throw new NotImplementedException();
        }

        public async Task PostProcessAsync()
        {

        }
    }
}
