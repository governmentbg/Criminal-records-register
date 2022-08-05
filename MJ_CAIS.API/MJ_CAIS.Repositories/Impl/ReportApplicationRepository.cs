using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DTO.ReportApplication;

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
                        select new ReportAppStatusHistoryDTO
                        {
                            Id = reportAppHis.Id,
                            CreatedBy = user.Firstname + " " + user.Surname + " " + user.Familyname,
                            CreatedOn = reportAppHis.CreatedOn,
                            Descr = reportAppHis.Descr,
                            Status = reportAppHis.StatusCode == ReportApplicationConstants.Status.New
                                ? ReportApplicationResources.statusNew
                                : (reportAppHis.StatusCode == ReportApplicationConstants.Status.Approved
                                    ? ReportApplicationResources.approved
                                    : (reportAppHis.StatusCode == ReportApplicationConstants.Status.Canceled
                                        ? ReportApplicationResources.canceled
                                        : (reportAppHis.StatusCode == ReportApplicationConstants.Status.Delivered
                                            ? ReportApplicationResources.delivered
                                            : string.Empty))),
                            AReportApplId = reportAppHis.AReportApplId,
                            AReportId = reportAppHis.AReportId,
                        };

            return query;
        }

    }
}
