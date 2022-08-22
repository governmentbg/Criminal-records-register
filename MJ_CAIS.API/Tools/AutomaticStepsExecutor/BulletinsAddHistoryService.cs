using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticStepsExecutor
{
    public class BulletinsAddHistoryService : IAutomaticStepService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<BulletinsAddHistoryService> _logger;
        private readonly IBulletinService _bulletinService;
        bool _writeHistory = false;
        bool _addDateofDestruction = false;
        bool _calcEcrisConvictionId = false;
        bool _replaceValues = false;
        int currentPageNumber;

        public BulletinsAddHistoryService(CaisDbContext dbContext, ILogger<BulletinsAddHistoryService> logger, IBulletinService bulletinService)
        {
            _dbContext = dbContext;
            _logger = logger;  
            _bulletinService = bulletinService;
            currentPageNumber = 0;

        }

        public async Task PreSelectAsync(IConfiguration config)
        {
       
                _writeHistory = config.GetValue<bool>("AutomaticStepsExecutor:BulletinsService:WriteHistoryOfBulletins");
                _addDateofDestruction = config.GetValue<bool>("AutomaticStepsExecutor:BulletinsService:AddDateOfDestruction");
                _calcEcrisConvictionId = config.GetValue<bool>("AutomaticStepsExecutor:BulletinsService:AddEcrisConvictionId");
                _replaceValues = config.GetValue<bool>("AutomaticStepsExecutor:BulletinsService:ReplaceValuesForBulletins");
            

        }

        public async Task<List<IBaseIdEntity>> SelectEntitiesAsync(int pageSize, IConfiguration config)
        {
          
            var result = await Task.FromResult(
                                  _dbContext.BBulletins.AsNoTracking()
                                  .Include(b=>b.CaseType).AsNoTracking()  
                                  .Include(b=>b.BBulletinStatusHes). AsNoTracking()
                                 .Where(b => b.MigrationBulletinId != null)
                                 .OrderByDescending(a => a.CreatedOn)
                                 .Skip(pageSize*currentPageNumber)
                                 .Take(pageSize)
                                 .ToList()

            .ToList<IBaseIdEntity>());
            currentPageNumber++;
            return result;
        }

        public async Task PostSelectAsync(IConfiguration config)
        {
      
        }

        public async Task PreProcessAsync(IConfiguration config)
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
                        var bulletin = (BBulletin)entity;

                        if (_writeHistory)
                        {
                            if (bulletin.BBulletinStatusHes == null || bulletin.BBulletinStatusHes.Count == 0)
                            {
                                _bulletinService.AddBulletinStatusH(bulletin, null, BulletinConstants.Status.Active);
                                await _dbContext.SaveChangesAsync();
                                _dbContext.ChangeTracker.Clear();
                            }
                            
                        }
                        if (_addDateofDestruction)
                        {
                            var oldStatus = bulletin.StatusId;
                            _bulletinService.UpdateDeleteDateData(bulletin);
                            if(bulletin.StatusId!= oldStatus)
                            {
                                _bulletinService.AddBulletinStatusH(bulletin, oldStatus, bulletin.StatusId);
                            }
                            await _dbContext.SaveChangesAsync();
                            _dbContext.ChangeTracker.Clear();
                        }
                        if (_calcEcrisConvictionId)
                        {
                            if(_replaceValues || (_replaceValues && string.IsNullOrEmpty(bulletin.EcrisConvictionId)))
                            {
                                _bulletinService.SetEcrisConvID(bulletin);
                                bulletin.EntityState = MJ_CAIS.Common.Enums.EntityStateEnum.Modified;
                                bulletin.ModifiedProperties = new List<string> { nameof(bulletin.EcrisConvictionId) };
                                _dbContext.ApplyChanges(bulletin);
                                await _dbContext.SaveChangesAsync();
                                _dbContext.ChangeTracker.Clear();
                            }
                        }
                       
                        numberOfSuccessEntities++;
                    }
                    catch (Exception ex)
                    {
                        numberOfFailedEntities++;
                        _dbContext.ChangeTracker.Clear();
                        _logger.LogError($"Bulletin {entity.Id}: " + ex.Message, ex.Data, ex);
                    }


                }
            }
            return result;
        }

        public async Task PostProcessAsync(IConfiguration config)
        {
            
        }
    }
}
