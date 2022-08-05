using Microsoft.EntityFrameworkCore;
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
                        select new GeneratedReportDTO
                        {
                            Id = reports.Id,
                            DocId = reports.DocId,
                            FirstSigner = signer1.Firstname + " " + signer1.Surname + " " + signer1.Familyname,
                            SecondSigner = signer2.Firstname + " " + signer2.Surname + " " + signer2.Familyname,
                            CreatedOn = reports.CreatedOn,
                            RegistrationNumber = reports.RegistrationNumber,
                            StatusCode = reports.StatusCode,
                            StatusName = status.Name,
                            ValidFrom = reports.ValidFrom,
                            ValidTo = reports.ValidTo,
                        };

            return query;
        }

        public IQueryable<ReportAppBulletinIdDTO> GetBulletinsByPids(string egnId, string lnchId, string lnId, string suidId)
        {
            var egnBull = _dbContext.BBulletins.AsNoTracking().Where(x => x.EgnId == egnId);
            var lnchBull = _dbContext.BBulletins.AsNoTracking().Where(x => x.LnchId == lnchId);
            var lnBull = _dbContext.BBulletins.AsNoTracking().Where(x => x.LnId == lnId);
            var suidBull = _dbContext.BBulletins.AsNoTracking().Where(x => x.SuidId == suidId);

            var result = egnBull
                            .Union(lnchBull)
                            .Union(lnchBull)
                            .Union(suidBull)
                            .Select(x => new ReportAppBulletinIdDTO
                            {
                                Id = x.Id,
                                CreatedOn = x.CreatedOn,
                                DecisionDate = x.DecisionDate
                            });

            return result;
        }

    }
}
