using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ReportApplication;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class ReportApplicationRepository : BaseAsyncRepository<AReportApplication, CaisDbContext>, IReportApplicationRepository
    {
        public ReportApplicationRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<AReportApplication> SelectAsync(string id)
        {
            var result = await this._dbContext.AReportApplications
                .Include(x => x.StatusCodeNavigation)
                .Include(x => x.ARepCitizenships)
                .Include(x => x.BirthCountry)
                .Include(x => x.BirthCity)
                   .ThenInclude(x => x.Municipality)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task SaveSignersAsync(string reportId, string firstSignerId, string secondSignerId)
        {
            var report = await _dbContext.AReports.AsNoTracking().FirstAsync(x => x.Id == reportId);
            report.FirstSignerId = firstSignerId;
            report.SecondSignerId = secondSignerId;
            report.EntityState = Common.Enums.EntityStateEnum.Modified;
            report.ModifiedProperties = new List<string> { nameof(report.FirstSignerId), nameof(report.SecondSignerId), nameof(report.Version) };
            _dbContext.ApplyChanges(report);
            await SaveChangesAsync(true);
        }

        public IQueryable<ReportAppStatusHistoryDTO> SelectAllStatusHistoryData()
        {
            var query = from reportAppHis in _dbContext.AReportStatusHes.AsNoTracking()
                        join user in _dbContext.GUsers.AsNoTracking() on reportAppHis.CreatedBy equals user.Id
                            into userLeft
                        from user in userLeft.DefaultIfEmpty()
                        join status in _dbContext.AReportStatuses.AsNoTracking() on reportAppHis.StatusCode equals status.Code
                        into statusLeft
                        from status in statusLeft.DefaultIfEmpty()
                        select new ReportAppStatusHistoryDTO
                        {
                            Id = reportAppHis.Id,
                            CreatedBy = user.Firstname + " " + user.Surname + " " + user.Familyname,
                            CreatedOn = reportAppHis.CreatedOn,
                            Descr = reportAppHis.Descr,
                            Status = status.Name,
                            AReportApplId = reportAppHis.AReportApplId,
                            AReportId = reportAppHis.AReportId,
                        };

            return query;
        }

        public IQueryable<GeneratedReportGridDTO> SelectAllGeneratedReports()
        {
            var query = from reports in _dbContext.AReports.AsNoTracking()
                        join reportAppl in _dbContext.AReportApplications.AsNoTracking() on reports.ARepApplId equals reportAppl.Id
                            into reportApplLeft
                        from reportAppl in reportApplLeft.DefaultIfEmpty()

                        select new GeneratedReportGridDTO
                        {
                            Id = reports.Id,
                            StatusCode = reports.StatusCode,
                            BirthDate = reportAppl.BirthDate,
                            CreatedOn = reports.CreatedOn,
                            Familyname = reportAppl.Familyname,
                            Firstname = reportAppl.Firstname,
                            Egn = reportAppl.Egn,
                            Purpose = reportAppl.Purpose,
                            RegistrationNumber = reports.RegistrationNumber,
                            ReportApplId = reportAppl.Id,
                            ReportApplRegNumber = reportAppl.RegistrationNumber,
                            Surname = reportAppl.Surname,
                            CsAuthorityId = reportAppl.CsAuthorityId
                        };

            return query;
        }

        public IQueryable<GeneratedReportDTO> SelectAllGeneratedReportsByAppId(string appId)
        {
            var query = from reports in _dbContext.AReports.AsNoTracking()
                        join signer1 in _dbContext.GUsers.AsNoTracking() on reports.FirstSignerId equals signer1.Id
                            into signer1Left
                        from signer1 in signer1Left.DefaultIfEmpty()
                        join signer2 in _dbContext.GUsers.AsNoTracking() on reports.SecondSignerId equals signer2.Id
                        into signer2Left
                        from signer2 in signer2Left.DefaultIfEmpty()

                        join status in _dbContext.AReportStatuses.AsNoTracking() on reports.StatusCode equals status.Code
                        into statusLeft
                        from status in statusLeft.DefaultIfEmpty()
                        where reports.ARepApplId == appId
                        orderby reports.CreatedOn descending
                        select new GeneratedReportDTO
                        {
                            Id = reports.Id,
                            DocId = reports.DocId,
                            FirstSigner = signer1.Firstname + " " + signer1.Surname + " " + signer1.Familyname,
                            FirstSignerId = reports.FirstSignerId,
                            SecondSigner = signer2.Firstname + " " + signer2.Surname + " " + signer2.Familyname,
                            SecondSignerId = reports.SecondSignerId,
                            CreatedOn = reports.CreatedOn,
                            RegistrationNumber = reports.RegistrationNumber,
                            StatusCode = reports.StatusCode,
                            StatusName = status.Name,
                            ValidFrom = reports.ValidFrom,
                            ValidTo = reports.ValidTo,
                            BulletinsCount = reports.ARepBulletins.Count
                        };

            return query;
        }

        public async Task<List<ReportAppBulletinIdDTO>> GetBulletinsByPersonIdAsync(string personId)
        {
            var bulletinsByEgn = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                 join egn in _dbContext.PPersonIds.AsNoTracking() on bulletin.EgnId equals egn.Id
                                 where egn.PersonId == personId
                                 &&
                                 bulletin.StatusId != BulletinConstants.Status.Deleted                      
                                 select new BBulletin
                                 {
                                     Id = bulletin.Id,
                                     CreatedOn = bulletin.CreatedOn,
                                     DecisionDate = bulletin.DecisionDate,
                                     DecisionFinalDate = bulletin.DecisionFinalDate,
                                     CaseYear = bulletin.CaseYear,
                                     UpdatedOn = bulletin.UpdatedOn
                                 };

            var bulletinsByLnch = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                  join lnch in _dbContext.PPersonIds.AsNoTracking() on bulletin.LnchId equals lnch.Id
                                  where lnch.PersonId == personId
                                         &&
                                 bulletin.StatusId != BulletinConstants.Status.Deleted
                                  select new BBulletin
                                  {
                                      Id = bulletin.Id,
                                      CreatedOn = bulletin.CreatedOn,
                                      DecisionDate = bulletin.DecisionDate,
                                      DecisionFinalDate = bulletin.DecisionFinalDate,
                                      CaseYear = bulletin.CaseYear,
                                      UpdatedOn = bulletin.UpdatedOn
                                  };

            var bulletinsByLn = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                join ln in _dbContext.PPersonIds.AsNoTracking() on bulletin.LnId equals ln.Id
                                where ln.PersonId == personId
                                       &&
                                 bulletin.StatusId != BulletinConstants.Status.Deleted
                                select new BBulletin
                                {
                                    Id = bulletin.Id,
                                    CreatedOn = bulletin.CreatedOn,
                                    DecisionDate = bulletin.DecisionDate,
                                    DecisionFinalDate = bulletin.DecisionFinalDate,
                                    CaseYear = bulletin.CaseYear,
                                    UpdatedOn = bulletin.UpdatedOn
                                };


            var bulletinsBySuid = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                  join suid in _dbContext.PPersonIds.AsNoTracking() on bulletin.SuidId equals suid.Id
                                  where suid.PersonId == personId
                                         &&
                                 bulletin.StatusId != BulletinConstants.Status.Deleted
                                  select new BBulletin
                                  {
                                      Id = bulletin.Id,
                                      CreatedOn = bulletin.CreatedOn,
                                      DecisionDate = bulletin.DecisionDate,
                                      DecisionFinalDate = bulletin.DecisionFinalDate,
                                      CaseYear = bulletin.CaseYear,
                                      UpdatedOn = bulletin.UpdatedOn
                                  };

            var bulletins = bulletinsByEgn
                                .Union(bulletinsByLnch)
                                .Union(bulletinsByLn)
                                .Union(bulletinsBySuid);

            var allBulletins = await  bulletins.OrderBulletins().ToListAsync();
            var result = allBulletins.Select(x => new ReportAppBulletinIdDTO
            {
                Id = x.Id,
                CreatedOn = x.CreatedOn,
                DecisionDate = x.DecisionDate
            }).ToList();

            return result;
        }

        public async Task<byte[]> GetReportAppContentByIdAsync(string aId)
        {
            var doc = await _dbContext.DDocuments.AsNoTracking()
                                        .Include(x => x.DocContent).AsNoTracking()
                                        .Select(x => new
                                        {
                                            Id = x.Id,
                                            Content = x.DocContent.Content
                                        })
                                        .FirstAsync(x => x.Id == aId);

            return doc.Content;
        }

        public async Task<AReport> GetFullAppReportByIdAsync(string aId)
        {
            var result = await _dbContext.AReports.AsNoTracking()
                            .Include(x => x.ARepAppl).AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == aId);

            return result;
        }

        public async Task<string> GetPersonIdByPidIdsAsync(string egnId, string lnchId, string lnId, string suidId)
        {
            var personIds = await _dbContext.PPersonIds.AsNoTracking()
                            .Where(x => x.Id == egnId || x.Id == lnchId || x.Id == lnId || x.Id == suidId)
                            .Select(x => x.PersonId)
                            .ToListAsync();

            var distinctIds = personIds?.Where(x => !string.IsNullOrEmpty(x)).Distinct();

            if (distinctIds?.Count() > 1)
            {
                throw new BusinessLogicException(BusinessLogicExceptionResources.mgsMoreThenOnePersonWithPids);
            }

            return distinctIds.FirstOrDefault();
        }
    }
}
