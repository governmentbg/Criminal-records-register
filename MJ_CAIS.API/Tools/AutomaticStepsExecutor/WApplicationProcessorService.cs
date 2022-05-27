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
    internal class WApplicationProcessorService : IAutomaticStepService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<WApplicationProcessorService> _logger;
        private readonly IRegisterTypeService _registerTypeService;
        private readonly IApplicationService _applicationService;
        private readonly IApplicationWebService _applicationWebService;
        public const string Pending = "Pending";
        public const string Accepted = "Accepted";
        public const string Rejected = "Rejected";


        public WApplicationProcessorService(CaisDbContext dbContext, ILogger<WApplicationProcessorService> logger, IRegisterTypeService registerTypeService, IApplicationService applicationService, IApplicationWebService applicationWebService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _registerTypeService = registerTypeService;
            _applicationService = applicationService;
            _applicationWebService = applicationWebService;
        }

        public async Task PreSelectAsync()
        {

        }

        public async Task<List<IBaseIdEntity>> SelectEntitiesAsync(int pageSize)
        {
            var result = await Task.FromResult(_dbContext.WApplications
                            .Include(a => a.WWebRequests)
                            .Include(a=>a.WStatusHes)
                      //todo: дали е този статус?! 
                      .Where(aa => aa.StatusCode == ApplicationConstants.ApplicationStatuses.WebRegistersChecks)
                        .OrderBy(a => a.CreatedOn)
                        .Take(pageSize)
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

                var internalApplicationType = await _dbContext.AApplicationTypes.FirstOrDefaultAsync(x => x.Code == ApplicationConstants.ApplicationTypes.WebInternalCertificate);
                if (internalApplicationType == null)
                {
                    throw new Exception($"Code \"{ApplicationConstants.ApplicationTypes.WebInternalCertificate}\" for internal applications is not set.");

                }

                var paymentMethodFree = await _dbContext.APaymentMethods.FirstOrDefaultAsync(x => x.Code == ApplicationConstants.PaymentMethodsCodes.Free);
                if (paymentMethodFree == null)
                {
                    throw new Exception($"Code \"{ApplicationConstants.PaymentMethodsCodes.Free}\" for payment method is not set.");

                }

                var statuses = await _dbContext.AApplicationStatuses.Where(a => a.Code == ApplicationConstants.ApplicationStatuses.ApprovedApplication).ToListAsync();
                if (statuses.Count != 1)
                {
                    throw new Exception($"Application statuses do not exist. Statuses: {ApplicationConstants.ApplicationStatuses.ApprovedApplication }");

                }
                var statusApprovedApplication = statuses.First();

                var webStatuses = await _dbContext.WApplicationStatuses.Where(a => a.Code == ApplicationConstants.ApplicationStatuses.WebApprovedApplication
                                             || a.Code == ApplicationConstants.ApplicationStatuses.WebCanceled
                                             ||a.Code == ApplicationConstants.ApplicationStatuses.WebCheckTaxFree
                                             || a.Code == ApplicationConstants.ApplicationStatuses.WebCheckPayment).ToListAsync();
                if (webStatuses.Count != 4)
                {
                    throw new Exception($"Application statuses do not exist. Statuses: {ApplicationConstants.ApplicationStatuses.WebApprovedApplication} " +
                        $", { ApplicationConstants.ApplicationStatuses.WebCanceled },{ ApplicationConstants.ApplicationStatuses.WebCheckTaxFree}, {ApplicationConstants.ApplicationStatuses.WebCheckPayment}");

                }
                var statusWebApprovedApplication = webStatuses.First(a => a.Code == ApplicationConstants.ApplicationStatuses.WebApprovedApplication);
                var statusWebCancel = webStatuses.First(a => a.Code == ApplicationConstants.ApplicationStatuses.WebCanceled);
                var statusWebCheckTaxFree = webStatuses.First(a => a.Code == ApplicationConstants.ApplicationStatuses.WebCheckTaxFree);
                var statusWebCheckPayment = webStatuses.First(a => a.Code == ApplicationConstants.ApplicationStatuses.WebCheckPayment);

                var maxNumberOfAttempts = (await _dbContext.GSystemParameters.FirstOrDefaultAsync(x => x.Code == SystemParametersConstants.SystemParametersNames.REGIX_NUMBER_OF_ATTEMPTS))?.ValueNumber;
                if (maxNumberOfAttempts == null )
                {
                    throw new Exception($"System parameter \"{SystemParametersConstants.SystemParametersNames.REGIX_NUMBER_OF_ATTEMPTS}\" is not set.");
                }
                foreach (IBaseIdEntity entity in entities)
                {
                    numberOfProcesedEntities++;
                    try
                    {
                        var wapplication = (WApplication)entity;
                        var checkRegixSuccess = SuccessfullCheckInRegisters(wapplication, (int)maxNumberOfAttempts);
                        //ако още не са преминали проверките - skip
                        if (checkRegixSuccess == Pending)
                        {
                            continue;
                        }
                        //при неуспешни проверки в регистрите се анулира

                        if (checkRegixSuccess == Rejected)
                        {
                            _applicationWebService.SetWApplicationStatus(wapplication, statusWebCancel, "Неуспешни проверки е регистрите.");
                            // wapplication.StatusCode = ApplicationConstants.ApplicationStatuses.WebCanceled;
                            _dbContext.WApplications.Update(wapplication);
                            await _dbContext.SaveChangesAsync();
                            numberOfSuccessEntities++;
                            continue;
                        }

                        //ако е служебно заявление,влиза в цаис за обработка
                        if (wapplication.ApplicationTypeId == internalApplicationType.Id)
                        {
                            await AutomaticStepsHelper.ProcessWebApplicationToApplicationAsync(wapplication, _dbContext, _registerTypeService, _applicationService,_applicationWebService,statusWebApprovedApplication,statusApprovedApplication);
                            await _dbContext.SaveChangesAsync();
                            numberOfSuccessEntities++;
                            continue;
                        }
                        //ако е освободен от плащане - отива при съдия
                        if (wapplication.PaymentMethodId == paymentMethodFree.Id)
                        {
                            _applicationWebService.SetWApplicationStatus(wapplication, statusWebCheckTaxFree, " За проверка за освобождаване от плащане");
                           // wapplication.StatusCode = ApplicationConstants.ApplicationStatuses.WebCheckTaxFree;
                            _dbContext.WApplications.Update(wapplication);
                            await _dbContext.SaveChangesAsync();
                            numberOfSuccessEntities++;
                            continue;
                        }
                        //дефолтно - очаква плащане
                        //todo: дали да не се прави и проверката за плащане тук и ако има плащане да преминава към следваща стъпка
                        _applicationWebService.SetWApplicationStatus(wapplication, statusWebCheckPayment, " За проверка за плащане");
                       // wapplication.StatusCode = ApplicationConstants.ApplicationStatuses.WebCheckPayment;
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

        private string SuccessfullCheckInRegisters(WApplication wapplication, int maxNumberOfAttempts)
        {

            if (wapplication.WWebRequests.Any(rq => (rq.Status == Pending || rq.Status == Rejected) && rq.Attempts < maxNumberOfAttempts))
                return Pending;
            if (wapplication.WWebRequests.Any(rq => rq.Status == Rejected && rq.Attempts == maxNumberOfAttempts))
                return Rejected;
            if (wapplication.WWebRequests.All(rq => rq.Status == Accepted))
                return Accepted;

            return Pending;

        }

        public async Task PostProcessAsync()
        {

        }
    }
}
