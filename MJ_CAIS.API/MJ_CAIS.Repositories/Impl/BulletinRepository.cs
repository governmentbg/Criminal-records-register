using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.Home;
using MJ_CAIS.DTO.Statistics;
using MJ_CAIS.Repositories.Contracts;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MJ_CAIS.Repositories.Impl
{
    public class BulletinRepository : BaseAsyncRepository<BBulletin, CaisDbContext>, IBulletinRepository
    {
        private readonly IUserContext _userContext;
        private const string STATISTICS_PACKAGE_NAME = "STATISTICS";
        private const string STATISTICS_PROCEDURE_BULLETIN_NAME = "bulletins_statistics";
        private const string STATISTICS_PROCEDURE_APPLICATION_NAME = "applications_statistics";

        public BulletinRepository(CaisDbContext dbContext, IUserContext userContext)
            : base(dbContext)
        {
            this._userContext = userContext;
        }

        public override IQueryable<BBulletin> SelectAll()
        {
            var query = this._dbContext.BBulletins
                                .Include(x => x.Status)
                                .Include(x => x.BulletinAuthority)
                                .Where(x => x.CsAuthorityId == _userContext.CsAuthorityId)
                                .AsNoTracking();
            return query;
        }

        public override async Task<BBulletin> SelectAsync(string aId)
        {
            var bulletin = await _dbContext.BBulletins
               .Include(x => x.BPersNationalities)
               .Include(x => x.CsAuthority)
               .Include(x => x.BirthCountry)
               .Include(x => x.BirthCity)
                   .ThenInclude(x => x.Municipality)
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == aId);

            return bulletin;
        }

        public async Task<string> GetBulletinAuthIdAsync(string aId)
        {
            var bulletin = await _dbContext.BBulletins.AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == aId);

            return bulletin?.CsAuthorityId;
        }

        public async Task<DDocument> SelectDocumentAsync(string documentId)
        {
            return await _dbContext.DDocuments.AsNoTracking()
               .Include(x => x.DocContent)
               .FirstOrDefaultAsync(x => x.Id == documentId);
        }

        public async Task<IQueryable<DDocument>> SelectAllDocumentsAsync()
        {
            var query = _dbContext.DDocuments.AsNoTracking()
               .Include(x => x.DocContent);

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<BBullPersAlias>> SelectBullPersAliasByBulletinIdAsync(string aId)
        {
            return await Task.FromResult(_dbContext.BBullPersAliases.AsNoTracking()
                .Where(x => x.BulletinId == aId));
        }

        public async Task<IQueryable<BOffence>> SelectAllOffencesAsync()
        {
            var query = _dbContext.BOffences
                 .AsNoTracking()
                 .Include(x => x.OffenceCat)
                 .Include(x => x.EcrisOffCat)
                 .Include(x => x.OffPlaceCountry)
                 .Include(x => x.OffPlaceCity)
                     .ThenInclude(x => x.Municipality);

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<BSanction>> SelectAllSanctionsAsync()
        {
            var query = _dbContext.BSanctions
                 .AsNoTracking()
                 .Include(x => x.EcrisSanctCateg)
                 .Include(x => x.SanctCategory)
                 .Include(x => x.BProbations);

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<BDecision>> SelectAllDecisionsAsync()
        {
            var query = _dbContext.BDecisions
                .AsNoTracking()
                .Include(x => x.DecisionAuth)
                .Include(x => x.DecisionChType)
                .Include(x => x.DecisionType);

            return await Task.FromResult(query);
        }

        public IQueryable<BulletinStatusHistoryDTO> SelectAllStatusHistoryData()
        {
            var query = from bulletinHis in _dbContext.BBulletinStatusHes.AsNoTracking()
                        join newStatus in _dbContext.BBulletinStatuses.AsNoTracking() on bulletinHis.NewStatusCode equals newStatus.Code
                            into newStatusLeft
                        from newStatus in newStatusLeft.DefaultIfEmpty()
                        join oldStatus in _dbContext.BBulletinStatuses.AsNoTracking() on bulletinHis.OldStatusCode equals oldStatus.Code
                           into oldStatusLeft
                        from oldStatus in oldStatusLeft.DefaultIfEmpty()
                        join user in _dbContext.GUsers.AsNoTracking() on bulletinHis.CreatedBy equals user.Id
                          into userLeft
                        from user in userLeft.DefaultIfEmpty()
                        select new BulletinStatusHistoryDTO
                        {
                            Id = bulletinHis.Id,
                            CreatedBy = user.Firstname + " " + user.Surname + " " + user.Familyname,
                            CreatedOn = bulletinHis.CreatedOn,
                            Descr = bulletinHis.Descr,
                            Locked = bulletinHis.Locked,
                            NewStatus = newStatus.Name,
                            OldStatus = oldStatus.Name,
                            Version = bulletinHis.Version,
                            BulletinId = bulletinHis.BulletinId
                        };

            return query;
        }

        public async Task<BBulletin> SelectBulletinPersonInfoAsync(string bulletinId)
        {
            var bulletin = await _dbContext.BBulletins.AsNoTracking()
                    .Include(x => x.BirthCountry)
                    .Include(x => x.CsAuthority)
                    .Include(x => x.BirthCity)
                        .ThenInclude(x => x.Municipality)
                            .ThenInclude(x => x.District)
                    .Include(x => x.DecidingAuth)
                    .Include(x => x.DecisionType)
                    .Include(x => x.BPersNationalities)
                        .ThenInclude(x => x.Country)
                    .Include(x => x.BBullPersAliases)
                    .Include(x => x.EgnNavigation)
                    .Include(x => x.LnchNavigation)
                    .Include(x => x.LnNavigation)
                    .Include(x => x.IdDocNumberNavigation)
                    .Include(x => x.SuidNavigation)
               .FirstOrDefaultAsync(x => x.Id == bulletinId);

            return bulletin;
        }

        public IQueryable<ObjectStatusCountDTO> GetStatusCountByCurrentAuthority()
        {
            var query = _dbContext.BBulletins.AsNoTracking()
                .Where(x => x.CsAuthorityId == _userContext.CsAuthorityId && (x.StatusId == BulletinConstants.Status.NewOffice ||
                            x.StatusId == BulletinConstants.Status.NewEISS ||
                            x.StatusId == BulletinConstants.Status.ForRehabilitation ||
                            x.StatusId == BulletinConstants.Status.ForDestruction))
                .GroupBy(x => x.StatusId)
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count()
                });

            return query;
        }

        public void CreateEcrisTcn(string bulletinId, string action)
        {
            var ecrisTcn = new EEcrisTcn
            {
                Id = BaseEntity.GenerateNewId(),
                BulletinId = bulletinId,
                Status = BulletinEventConstants.Status.New,
                Action = action
            };

            _dbContext.Add(ecrisTcn);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

      

        public async Task<PPerson> GetPersonIdByPidAsync(string pid, string pidType)
        {
            var person = await _dbContext.PPersonIds.AsNoTracking()
                .Include(x => x.PidType)
                .Include(x => x.Person)
                     .ThenInclude(x => x.BirthCity)
                .Include(x => x.Person)
                     .ThenInclude(x => x.BirthCountry)
                .Where(p => p.Pid == pid && p.PidType.Code == pidType)
                .Select(x => x.Person)
                .FirstOrDefaultAsync();

            return person;
        }

        public async Task<IQueryable<BBulletin>> GetBulletinsByPidIdAsync(string pidId)
        {
            var result = _dbContext.BBulletins.AsNoTracking()
                    .Include(x => x.BirthCity)
                    .Include(x => x.BirthCountry)
                    .Include(x => x.DecidingAuth)
                    .Include(x => x.DecisionType)
                    .Include(x => x.CaseType)
                    .Include(x => x.CaseAuth)

                    .Include(x => x.BOffences).ThenInclude(x => x.OffenceCat)
                    .Include(x => x.BOffences).ThenInclude(x => x.EcrisOffCat)
                    .Include(x => x.BOffences).ThenInclude(x => x.OffPlaceCountry)
                    .Include(x => x.BOffences).ThenInclude(x => x.OffPlaceCity)

                    .Include(x => x.BSanctions).ThenInclude(x => x.SanctCategory)
                    .Include(x => x.BSanctions).ThenInclude(x => x.EcrisSanctCateg)
                    .Include(x => x.BSanctions).ThenInclude(x => x.BProbations).ThenInclude(x => x.SanctProbCateg)
                    .Include(x => x.BSanctions).ThenInclude(x => x.BProbations).ThenInclude(x => x.SanctProbMeasure)

                    .Include(x => x.BDecisions).ThenInclude(x => x.DecisionAuth)
                    .Include(x => x.BulletinAuthority)
                    .Include(x => x.CsAuthority)
                    .Include(x => x.BPersNationalities)
                        .ThenInclude(x => x.Country)
                    .Where(x => x.EgnId == pidId ||
                          x.LnchId == pidId ||
                          x.LnId == pidId ||
                          x.IdDocNumberId == pidId ||
                          x.SuidId == pidId);

            return await Task.FromResult(result);
        }

        public async Task SaveBulletinsAsync(List<BBulletin> bulletins)
        {
            _dbContext.BBulletins.AddRange(bulletins);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Dictionary<string, string>> GetAuthIdByEkatteAsync(List<string> ekatteCodes)
        {
            var result = await _dbContext.GCities.AsNoTracking()
                .Where(x => ekatteCodes.Contains(x.EkatteCode))
                .Select(x => new KeyValuePair<string, string>(x.EkatteCode, x.CsAuthorityId))
                .ToDictionaryAsync(x => x.Key, x => x.Value);

            return result;
        }

        public async Task<IQueryable<BBulletin>> GetBulletinsForPeriodAsync(DateTime dateFrom, DateTime dateTo)
        {
            var result = _dbContext.BBulletins.AsNoTracking()
                    .Include(x => x.BirthCity)
                    .Include(x => x.BirthCountry)
                    .Include(x => x.DecidingAuth)
                    .Include(x => x.DecisionType)
                    .Include(x => x.CaseType)
                    .Include(x => x.CaseAuth)

                    .Include(x => x.BOffences).ThenInclude(x => x.OffenceCat)
                    .Include(x => x.BOffences).ThenInclude(x => x.EcrisOffCat)
                    .Include(x => x.BOffences).ThenInclude(x => x.OffPlaceCountry)
                    .Include(x => x.BOffences).ThenInclude(x => x.OffPlaceCity)

                    .Include(x => x.BSanctions).ThenInclude(x => x.SanctCategory)
                    .Include(x => x.BSanctions).ThenInclude(x => x.EcrisSanctCateg)
                    .Include(x => x.BSanctions).ThenInclude(x => x.BProbations).ThenInclude(x => x.SanctProbCateg)
                    .Include(x => x.BSanctions).ThenInclude(x => x.BProbations).ThenInclude(x => x.SanctProbMeasure)

                    .Include(x => x.BDecisions).ThenInclude(x => x.DecisionAuth)
                    .Include(x => x.BulletinAuthority)
                    .Include(x => x.CsAuthority)
                    .Include(x => x.BPersNationalities)
                        .ThenInclude(x => x.Country)
                    .Where(x => x.CreatedOn >= dateFrom && x.CreatedOn <= dateTo);

            return await Task.FromResult(result);
        }

        public async Task<List<StatisticsCountDTO>> GetStatisticsForBulletinsAsync(StatisticsSearchDTO searchParams)
        {
            return await GetStatisticsAsync(searchParams, STATISTICS_PROCEDURE_BULLETIN_NAME);
        }

        public async Task<List<StatisticsCountDTO>> GetStatisticsForApplicationsAsync(StatisticsSearchDTO searchParams)
        {
            return await GetStatisticsAsync(searchParams, STATISTICS_PROCEDURE_APPLICATION_NAME);
        }

        private async Task<List<StatisticsCountDTO>> GetStatisticsAsync(StatisticsSearchDTO searchParams, string procedureName)
        {
            var ds = new DataSet();
            var result = new List<StatisticsCountDTO>();

            try
            {
                using (OracleConnection oracleConnection = new OracleConnection(_dbContext.Database.GetConnectionString()))
                {
                    // Create command
                    OracleCommand cmd = new OracleCommand($"{STATISTICS_PACKAGE_NAME}.{procedureName}", oracleConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set parameters

                    var fromDate = searchParams.FromDate.HasValue ? searchParams.FromDate.Value.Date : (DateTime?)null;
                    var toDate = searchParams.ToDate.HasValue ? searchParams.ToDate.Value.Date : (DateTime?)null;
                    cmd.Parameters.Add(new OracleParameter("p_date_from", OracleDbType.Date, fromDate, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_date_to", OracleDbType.Date, toDate, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_cs_authority", OracleDbType.Varchar2, searchParams.Authority, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_out", OracleDbType.RefCursor, null, ParameterDirection.Output));

                    OracleDataAdapter resultDataSet = new OracleDataAdapter(cmd);
                    try
                    {
                        await oracleConnection.OpenAsync();
                        resultDataSet.Fill(ds);

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            result.Add(new StatisticsCountDTO
                            {
                                Count = int.Parse(row["cnt"].ToString()),
                                ObjectType = row["text"].ToString(),
                                OrderNumber = int.Parse(row["order_number"].ToString()),
                            });
                        }
                    }
                    catch (Exception exception)
                    {
                        // todo: log
                        throw;
                    }
                    finally
                    {
                        oracleConnection.Close();
                        oracleConnection.Dispose();
                    }
                }
            }

            catch (Exception ex)
            {
                // todo: log error
                // add message
                throw;
            }

            return result.OrderBy(x => x.OrderNumber).ToList();
        }
        public async Task<BBulletin> GetBulletinData(string bulletinId)
        {
            return await _dbContext.BBulletins
                .Include(x => x.BPersNationalities)
                    .ThenInclude(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == bulletinId);
        }

        public async Task<string> GetDataForSendFinesDataAsync(string egn, string? decisionTypeId, DateTime? decisionDate, string decisionNumber, string? decidingAuthId, string caseNumber, bool caseYearParsed, decimal caseYear, string? caseTypeId, string? caseAuthId)
        {
            return await _dbContext.BBulletins.AsNoTracking()
                .Include(x => x.EgnNavigation).AsNoTracking()
                .Where(bulletin => bulletin.EgnNavigation.Pid == egn &&
                                    (bulletin.DecisionTypeId == decisionTypeId || string.IsNullOrEmpty(decisionTypeId)) &&
                                    (bulletin.DecisionDate == decisionDate || !decisionDate.HasValue) &&
                                    bulletin.DecisionNumber == decisionNumber &&
                                    bulletin.DecidingAuthId == decidingAuthId &&
                                    bulletin.CaseNumber == caseNumber &&
                                    (bulletin.CaseYear == caseYear || !caseYearParsed) &&
                                    (bulletin.CaseTypeId == caseTypeId || string.IsNullOrEmpty(caseTypeId)) &&
                                    bulletin.CaseAuthId == caseAuthId)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<BSanction>> GetDeletedSanctionsAsync(List<string> deletedSanctionIds)
        {
            if (deletedSanctionIds.Count == 0) return new List<BSanction>();

            var deletedSanctionAndItsProbations = await _dbContext.BSanctions.AsNoTracking()
                      .Where(x => deletedSanctionIds.Contains(x.Id))
                      .Include(x => x.BProbations)
                      .Select(x => new BSanction
                      {
                          Id = x.Id,
                          EntityState = EntityStateEnum.Deleted,
                          BProbations = x.BProbations.Select(x => new BProbation
                          {
                              Id = x.Id,
                              EntityState = EntityStateEnum.Deleted,
                              Version = x.Version
                          }).ToArray(),
                          Version = x.Version
                      }).ToListAsync();

            return deletedSanctionAndItsProbations;
        }
        public  async Task<bool> IsEuCitizen(IEnumerable<string> personNationalities)
        {
            return await _dbContext.EEcrisAuthorities.AsNoTracking().AnyAsync(x => personNationalities.Contains(x.CountryId));
        }
    }
}
