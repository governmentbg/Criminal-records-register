using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MJ_CAIS.Common.Enums;

namespace AutomaticStepsExecutor
{
    public class PaymentCheckerService : IAutomaticStepService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<PaymentCheckerService> _logger;
        //private readonly IRegisterTypeService _registerTypeService;
        //private readonly IApplicationService _applicationService;
        //private readonly IApplicationWebService _applicationWebService;
        //private readonly IPersonService _personService;
        private readonly IWApplicationService _wApplicationService;
      
        public PaymentCheckerService(CaisDbContext dbContext, 
            ILogger<PaymentCheckerService> logger,
            //IRegisterTypeService registerTypeService,
            //IApplicationService applicationService, 
            //IApplicationWebService applicationWebService,
            //IPersonService personService,
            IWApplicationService wApplicationService)
        {
            _dbContext = dbContext;
            _logger = logger;
            //_registerTypeService = registerTypeService;
            //_applicationService = applicationService;
            //_applicationWebService = applicationWebService;
            //_personService = personService;
            _wApplicationService = wApplicationService;
            
        }

        public async Task PreSelectAsync(Microsoft.Extensions.Configuration.IConfiguration config)
        {
       
        }

        public async Task<List<IBaseIdEntity>> SelectEntitiesAsync(int pageSize, Microsoft.Extensions.Configuration.IConfiguration config)
        {
            var result = await Task.FromResult(_dbContext.WApplications.AsNoTracking()
                                            .Include(a => a.APayments)
                                            .ThenInclude(p => p.EPayment).AsNoTracking()
                                            .Include(a => a.ApplicationType).AsNoTracking()
                                            .Include(a=>a.WAppCitizenships).AsNoTracking()
                                            .Include(a=>a.WAppPersAliases).AsNoTracking()
                               .Where(aa => aa.StatusCode == ApplicationConstants.ApplicationStatuses.WebCheckPayment)
                                 .OrderBy(a => a.CreatedOn)
                              .Take(pageSize)
                               .ToList<IBaseIdEntity>());
            return result;

        }

        public async Task PostSelectAsync(Microsoft.Extensions.Configuration.IConfiguration config)
        {

        }

        public async Task PreProcessAsync(Microsoft.Extensions.Configuration.IConfiguration config)
        {
       
        }

        public async Task<AutomaticStepResult> ProcessEntitiesAsync(List<IBaseIdEntity> entities, Microsoft.Extensions.Configuration.IConfiguration config)
        {
            AutomaticStepResult result = new AutomaticStepResult();
            int numberOfProcesedEntities = 0;
            int numberOfSuccessEntities = 0;
            int numberOfFailedEntities = 0;
            if (entities.Count > 0)
            {
                numberOfProcesedEntities = entities.Count;
                var statuses = await _dbContext.AApplicationStatuses.AsNoTracking().Where(a => a.Code == ApplicationConstants.ApplicationStatuses.ApprovedApplication).ToListAsync();
                if (statuses.Count != 1)
                {
                    throw new Exception($"Application statuses do not exist. Statuses: {ApplicationConstants.ApplicationStatuses.ApprovedApplication }");

                }
                var statusApprovedApplication = statuses.First();

                var webStatuses= await _dbContext.WApplicationStatuses.AsNoTracking().Where(a => a.Code == ApplicationConstants.ApplicationStatuses.WebApprovedApplication
                                            || a.Code == ApplicationConstants.ApplicationStatuses.WebCanceled).ToListAsync();
                if (webStatuses.Count != 2)
                {
                    throw new Exception($"Application statuses do not exist. Statuses: {ApplicationConstants.ApplicationStatuses.WebApprovedApplication} , {ApplicationConstants.ApplicationStatuses.WebCanceled}");

                }
                var statusWebApprovedApplication = webStatuses.First(a => a.Code == ApplicationConstants.ApplicationStatuses.WebApprovedApplication);
                var statusWebCancel = webStatuses.First(a => a.Code == ApplicationConstants.ApplicationStatuses.WebCanceled);

                var systemParamValidityPeriodWeb = (int?)_dbContext.GSystemParameters.FirstOrDefault(x => x.Code == SystemParametersConstants.SystemParametersNames.TERM_FOR_PAYMENT_WEB_DAYS)?.ValueNumber;
                if (systemParamValidityPeriodWeb == null)
                {
                    throw new Exception($"System parameter {SystemParametersConstants.SystemParametersNames.TERM_FOR_PAYMENT_WEB_DAYS} is not set.");
                }
                var startDateWeb = DateTime.Now.AddDays(-systemParamValidityPeriodWeb.Value).Date;

            

                foreach (IBaseIdEntity entity in entities)
                {
                    numberOfProcesedEntities++;
                    try
                    {
                        var wapplication = (WApplication)entity;
                        await _wApplicationService.ProcessWApplicationCheckPayment(statusApprovedApplication, statusWebApprovedApplication, statusWebCancel, startDateWeb, wapplication);
                        await _dbContext.SaveChangesAsync();
                        _dbContext.ChangeTracker.Clear();
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

        

       

        public async Task PostProcessAsync(Microsoft.Extensions.Configuration.IConfiguration config)
        {
           
        }
    }
}
