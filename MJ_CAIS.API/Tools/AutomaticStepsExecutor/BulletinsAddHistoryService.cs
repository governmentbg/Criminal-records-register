using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MJ_CAIS.Common.Constants.BulletinConstants;

namespace AutomaticStepsExecutor
{
    public class BulletinsAddHistoryService : IAutomaticStepService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<BulletinsAddHistoryService> _logger;
        private readonly IBulletinService _bulletinService;
        private readonly IMapper _mapper;
        bool _writeHistory = false;
        bool _addDateofDestruction = false;
        bool _calcEcrisConvictionId = false;
        bool _replaceValues = false;
        int currentPageNumber;

        public BulletinsAddHistoryService(CaisDbContext dbContext, ILogger<BulletinsAddHistoryService> logger, IBulletinService bulletinService, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _bulletinService = bulletinService;
            currentPageNumber = 0;
            _mapper = mapper;

        }

        public async Task PreSelectAsync(IConfiguration config)
        {

            _writeHistory = config.GetValue<bool>("AutomaticStepsExecutor:BulletinsService:WriteHistoryOfBulletins");
            _addDateofDestruction = config.GetValue<bool>("AutomaticStepsExecutor:BulletinsService:AddDateOfDestruction");
            _calcEcrisConvictionId = config.GetValue<bool>("AutomaticStepsExecutor:BulletinsService:AddEcrisConvictionId");
            _replaceValues = config.GetValue<bool>("AutomaticStepsExecutor:BulletinsService:ReplaceValuesForBulletins");


        }

        public async Task<List<IBaseIdEntity>> SelectEntitiesAsync(int pageSize, IConfiguration config, int numberOfPage = 0)
        {

            var result = await Task.FromResult(
                                  _dbContext.BBulletins.AsNoTracking()
                                  .Include(b => b.CaseType).AsNoTracking()
                                  .Include(b => b.BBulletinStatusHes).AsNoTracking()
                                  .Include(x => x.BSanctions).AsNoTracking()
                                  .Include(x => x.BOffences).AsNoTracking()
                                  .Include(x => x.BDecisions).AsNoTracking()
                                  .Include(x => x.BBullPersAliases).AsNoTracking()
                                  .Include(x => x.BPersNationalities).AsNoTracking()
                                 .Where(b => b.MigrationBulletinId != null)
                                 .OrderByDescending(a => a.CreatedOn)
                                 .Skip(pageSize * currentPageNumber)
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
                        bool isUpdatedStatus = false;
                        bool isEcrisChanged = false;
                        var bulletin = (BBulletin)entity;
                        var oldStatus = bulletin.StatusId;

                        if (_addDateofDestruction)
                        {

                            _bulletinService.UpdateDeleteDateData(bulletin);
                            if (oldStatus != bulletin.StatusId)
                            {
                                isUpdatedStatus = true;
                            }

                        }
                        if (_calcEcrisConvictionId)
                        {
                            if (_replaceValues || (_replaceValues && string.IsNullOrEmpty(bulletin.EcrisConvictionId)))
                            {
                                try

                                {
                                    string oldValue = bulletin.EcrisConvictionId;
                                    _bulletinService.SetEcrisConvId(bulletin);

                                    if (oldValue != bulletin.EcrisConvictionId)
                                    {
                                        isEcrisChanged = true;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError(ex, $"BulletinID: {bulletin.Id} Error in Ecris Conv Id setting:{ex.Message}", ex.Data);
                                    isEcrisChanged = false;
                                }

                            }
                        }

                        if (_writeHistory || isEcrisChanged || isUpdatedStatus)
                        {
                            string oldStatusForDel = null;
                            if (bulletin.BBulletinStatusHes != null && bulletin.BBulletinStatusHes.Count > 0)
                            {
                                oldStatusForDel = oldStatus;


                            }
                            var statusHistory = new BBulletinStatusH
                            {
                                Id = Guid.NewGuid().ToString(),
                                BulletinId = bulletin.Id,
                                NewStatusCode = bulletin.StatusId,
                                OldStatusCode = oldStatusForDel,
                                EntityState = EntityStateEnum.Added,
                                Locked = bulletin.StatusId != Status.NewOffice,
                                Descr = "Автоматична обработка",
                                Version = 1
                            };



                            bulletin.BBulletinStatusHes ??= new List<BBulletinStatusH>();
                            bulletin.BBulletinStatusHes.Add(statusHistory);



                            var bulletinXmlModel = _mapper.Map<BBulletin, BulletinType>(bulletin);
                            var xml = XmlUtils.SerializeToXml(bulletinXmlModel);
                            var statusH = bulletin.BBulletinStatusHes.FirstOrDefault(x => x.EntityState == MJ_CAIS.Common.Enums.EntityStateEnum.Added);
                            if (statusH != null)
                            {
                                statusH.Content = xml;
                                statusH.HasContent = true;

                            }


                        }
                        _dbContext.ApplyChanges(bulletin);
                        _dbContext.ApplyChanges(bulletin.BBulletinStatusHes);
                        await _dbContext.SaveChangesAsync();
                        _dbContext.ChangeTracker.Clear();

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
