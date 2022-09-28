using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.Repositories.Impl
{
    public class ReportRepository : BaseAsyncRepository<AReport, CaisDbContext>, IReportRepository
    {
        public ReportRepository(CaisDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<AApplication> GetApplicationData(string applicationID)
        {//todo: change
            return await _dbContext.AApplications
            .Include(a => a.EgnNavigation)
            .Include(a => a.LnchNavigation)
            .Include(a => a.LnNavigation)
            .Include(a => a.SuidNavigation)
            .Include(a => a.ApplicationType)
            //.Include(x => x.AReports)
            .FirstOrDefaultAsync(aa => aa.Id == applicationID);
        }

        public async Task<DDocContent> GetReportContent(string reportID)
        {
            var content = await _dbContext.AReports.Where(x => x.Id == reportID && x.Doc != null).Select(x => x.Doc.DocContent).FirstOrDefaultAsync();
            //if (content == null)
            //{
            // throw new Exception("Certificate does not exist.");
            //}
            return content;
        }

        public async Task<List<BulletindecisionDateDTO>> GetBulletinesPerPerson(List<string> pids)
        {
            var bulletins = _dbContext.BBulletins
                .Where(b => b.Status.Code != BulletinConstants.Status.Deleted &&
                                                                (pids.Contains(b.EgnId) ||
                                                                 pids.Contains(b.LnchId) ||
                                                                 pids.Contains(b.LnId) ||
                                                                 pids.Contains(b.IdDocNumberId) ||
                                                                 pids.Contains(b.SuidId)));
            bulletins = bulletins.OrderBulletins();

            //&& b.PBulletinIds.Any(bulID => pids.Contains(bulID.Person.PersonId)))
            var result =
               //.Select(b => new BBulletin
               //   { 
               //       Id = b.Id,
               //       DecisionFinalDate= b.DecisionFinalDate,
               //       DecisionDate = b.DecisionDate,
               //       CaseYear = b.CaseYear,
               //       CreatedOn = b.CreatedOn
               //   }).Distinct()
               //   //order_bulletins
               //   .OrderBy(b => b.DecisionFinalDate)
               //   .OrderBy(b => b.DecisionDate)
               //   .OrderBy(b => b.CaseYear)
               //   .OrderBy(b => b.CreatedOn.HasValue ? b.CreatedOn.Value.Date : DateTime.Now)
               await bulletins.Select(b => new BulletindecisionDateDTO
               {
                   Id = b.Id,
                   DecisionDate = b.DecisionDate
               })
               .ToListAsync();

            return result;
        }

        public async Task<AReport> GetReport(string reportID)
        {
            return await _dbContext.AReports.AsNoTracking()
                 .Include(x => x.AReportStatusHes).AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Id == reportID);
        }
    }
}
