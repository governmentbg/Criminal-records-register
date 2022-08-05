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
