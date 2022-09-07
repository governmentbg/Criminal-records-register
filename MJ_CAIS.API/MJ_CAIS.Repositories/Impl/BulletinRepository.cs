using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.Resources;
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

        public IQueryable<BulletinGridDTO> SearchBulletins(BulletinSearchParamDTO searchParams)
        {
            var bulletinsQuery = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                 where bulletin.CsAuthorityId == _userContext.CsAuthorityId
                                 select bulletin;

            var filteredBlletins = ApplyFormFilter(bulletinsQuery, searchParams);

            var query = from bulletin in filteredBlletins
                        join auth in _dbContext.GDecidingAuthorities.AsNoTracking() on bulletin.BulletinAuthorityId equals auth.Id
                            into authLeft
                        from auth in authLeft.DefaultIfEmpty()

                        join status in _dbContext.BBulletinStatuses.AsNoTracking() on bulletin.StatusId equals status.Code
                           into statusLeft
                        from status in statusLeft.DefaultIfEmpty()

                        select new BulletinGridDTO
                        {
                            Id = bulletin.Id,
                            BulletinType = bulletin.BulletinType == BulletinConstants.Type.Bulletin78A ? BulletinResources.Bulletin78A :
                                                        bulletin.BulletinType == BulletinConstants.Type.ConvictionBulletin ? BulletinResources.ConvictionBulletin :
                                                             BulletinResources.Unspecified,
                            RegistrationNumber = bulletin.RegistrationNumber,
                            BulletinAuthorityName = auth.Name,
                            Identifier = bulletin.Egn + "/" + bulletin.Lnch,
                            FullName = !string.IsNullOrEmpty(bulletin.Fullname) ? bulletin.Fullname :
                             bulletin.Firstname + " " + bulletin.Surname + " " + bulletin.Familyname,
                            BirthDate = bulletin.BirthDate,
                            CreatedOn = bulletin.CreatedOn,
                            StatusId = bulletin.StatusId,
                            StatusName = status.Name,
                            CaseData = bulletin.CaseNumber + "/" + bulletin.CaseYear
                        };

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
                            BulletinId = bulletinHis.BulletinId,
                            HasContent = bulletinHis.HasContent
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

        public async Task<PPerson> GetPersonIdByPidAsync(string pid, string pidType)
        {
            var person = await _dbContext.PPersonIds.AsNoTracking()
                .Include(x => x.PidType)
                .Include(x => x.Person)
                     .ThenInclude(x => x.BirthCity)
                .Include(x => x.Person)
                     .ThenInclude(x => x.PPersonIds)
                     .ThenInclude(x => x.PidType)
                .Include(x => x.Person)
                     .ThenInclude(x => x.BirthCountry)
                .Where(p => p.Pid == pid && p.PidType.Code == pidType)
                .Select(x => x.Person)
                .FirstOrDefaultAsync();

            return person;
        }

        public async Task<IQueryable<BBulletin>> GetBulletinsByPidIdAsync(string pidId)
        {
            //намира лицето по идентификатора
            var personId = await this._dbContext.PPersonIds.AsNoTracking().Where(x => x.Id == pidId).Select(x => x.PersonId).FirstOrDefaultAsync();

            //намира всички бюлетеини на това лице
            var bulletinsList =  this._dbContext.BBulletins.Where(b =>
                        b.EgnNavigation.PersonId == personId).Select(b => new BBulletin { Id = b.Id,  StatusId = b.StatusId })
                        .Union(this._dbContext.BBulletins.Where(b =>
                         b.LnchNavigation.PersonId == personId).Select(b => new BBulletin { Id = b.Id,  StatusId = b.StatusId }))
                        .Union(this._dbContext.BBulletins.Where(b =>
                        b.LnNavigation.PersonId == personId).Select(b => new BBulletin { Id = b.Id,  StatusId = b.StatusId }))
                        .Union(this._dbContext.BBulletins.Where(b =>
                         b.SuidNavigation.PersonId == personId).Select(b => new BBulletin { Id = b.Id, StatusId = b.StatusId })).AsNoTracking();

            //взима всички подробности на бюлетините на това лице
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
                    .Where(x => bulletinsList.Where(b => b.StatusId != BulletinConstants.Status.Deleted).Select(b => b.Id).Contains(x.Id))
                    .OrderBy(x => x.CreatedOn);

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
            return await _dbContext.BBulletins.AsNoTracking()
                .Include(x => x.BPersNationalities)
                    .ThenInclude(x => x.Country).AsNoTracking()
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

        public async Task<List<BSanction>> GetBulletinSanctionsAsync(string bulletinId)
        {
            if (string.IsNullOrEmpty(bulletinId)) return new List<BSanction>();

            var sanctions = await _dbContext.BSanctions.AsNoTracking()
                        .Where(x => x.BulletinId == bulletinId)
                        .ToListAsync();

            return sanctions;
        }

        public async Task<bool> IsEuCitizen(IEnumerable<string> personNationalities)
        {
            return await _dbContext.EEcrisAuthorities.AsNoTracking()
                .AnyAsync(x => personNationalities.Contains(x.CountryId));
        }

        public async Task<List<BulletinForRehabilitationAndEventDTO>> GetBulletinsByPidsIdExcludeCurrentAsync(List<string> pidsId, string currentBullId)
        {
            //намира лицето по идентификатора
            var personId = await this._dbContext.PPersonIds.AsNoTracking()
                .Where(x => pidsId.Contains(x.Id))
                .Select(x => x.PersonId)
                .FirstOrDefaultAsync();

            // this is new person, not saved in db
            if (personId is null) return new List<BulletinForRehabilitationAndEventDTO>();

            //намира всички бюлетеини на това лице

            var egnBullId = this._dbContext.BBulletins
                .Where(b => b.EgnNavigation.PersonId == personId).Select(b => b.Id);

            var lnchBullId = this._dbContext.BBulletins
                .Where(b => b.LnchNavigation.PersonId == personId).Select(b => b.Id);

            var lnBullId = this._dbContext.BBulletins
                .Where(b => b.LnNavigation.PersonId == personId).Select(b => b.Id);

            var suidBullId = this._dbContext.BBulletins
                .Where(b => b.SuidNavigation.PersonId == personId).Select(b => b.Id);

            var docNumBullId = this._dbContext.BBulletins
                .Where(b => b.IdDocNumberNavigation.PersonId == personId).Select(b => b.Id);

            var bulletinsList = await egnBullId
                .Union(lnchBullId)
                .Union(lnBullId)
                .Union(suidBullId)
                .Union(docNumBullId)
                .Where(x=> x != currentBullId)
                .ToListAsync();

            var result = await _dbContext.BBulletins             
                .Where(x => bulletinsList.Contains(x.Id))
                .Select(bulletin => new BulletinForRehabilitationAndEventDTO
                {
                    Id = bulletin.Id,
                    DecisionDate = bulletin.DecisionDate,
                    DecisionFinalDate = bulletin.DecisionFinalDate,
                    Status = bulletin.StatusId,
                    RehabilitationDate = bulletin.RehabilitationDate,
                    Version = bulletin.Version,
                    CaseType = bulletin.CaseTypeId,
                    BirthDate = bulletin.BirthDate,
                    BulletinType = bulletin.BulletinType,
                    StatusId = bulletin.StatusId,
                    Sanctions = bulletin.BSanctions.Select(x => new SanctionForRehabilitationDTO
                    {
                        SuspensionDuration = new DurationDTO
                        {
                            Years = x.SuspentionDurationYears,
                            Months = x.SuspentionDurationMonths,
                            Days = x.SuspentionDurationDays,
                        },
                        Type = x.SanctCategoryId,
                        ProbationDurations = x.BProbations.Select(p => new DurationDTO
                        {
                            Years = p.DecisionDurationYears,
                            Months = p.DecisionDurationMonths,
                            Days = p.DecisionDurationDays,
                            Hours = p.DecisionDurationHours,
                        })
                    }),
                    Decisions = bulletin.BDecisions.Select(x => new DecisionForRehabilitationDTO
                    {
                        Type = x.DecisionChTypeId,
                        ChangeDate = x.ChangeDate,
                    }),
                    OffencesEndDates = bulletin.BOffences.Select(o => o.OffEndDate)
                }).ToListAsync();

            return result;
        }

        private static IQueryable<BBulletin> ApplyFormFilter(IQueryable<BBulletin> query, BulletinSearchParamDTO searchParams)
        {
            if (searchParams == null) return query;

            if (!string.IsNullOrEmpty(searchParams.RegistrationNumber))
                query = query.Where(x => x.RegistrationNumber == searchParams.RegistrationNumber);

            if (!string.IsNullOrEmpty(searchParams.BulletinType))
                query = query.Where(x => x.BulletinType == searchParams.BulletinType);

            if (!string.IsNullOrEmpty(searchParams.StatusId))
                query = query.Where(x => x.StatusId == searchParams.StatusId);

            if (!string.IsNullOrEmpty(searchParams.CaseNumber))
                query = query.Where(x => x.CaseNumber == searchParams.CaseNumber);

            if (!string.IsNullOrEmpty(searchParams.Firstname))
                query = query.Where(x => x.Firstname == searchParams.Firstname);

            if (!string.IsNullOrEmpty(searchParams.Surname))
                query = query.Where(x => x.Surname == searchParams.Surname);

            if (!string.IsNullOrEmpty(searchParams.Familyname))
                query = query.Where(x => x.Familyname == searchParams.Familyname);

            if (!string.IsNullOrEmpty(searchParams.Egn))
                query = query.Where(x => x.Egn == searchParams.Egn);

            if (!string.IsNullOrEmpty(searchParams.Lnch))
                query = query.Where(x => x.Lnch == searchParams.Lnch);

            if (searchParams.BirthDate.HasValue)
                query = query.Where(x => x.BirthDate == searchParams.BirthDate.Value.Date);

            if (searchParams.FromDate.HasValue)
                query = query.Where(x => x.CreatedOn >= searchParams.FromDate.Value.Date.AddDays(-1).Date);

            if (searchParams.ToDate.HasValue)
                query = query.Where(x => x.CreatedOn <= searchParams.ToDate.Value.Date);

            return query;
        }
    }
}
