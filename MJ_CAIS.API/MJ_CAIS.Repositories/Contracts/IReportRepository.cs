using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IReportRepository : IBaseAsyncRepository<AReport, string, CaisDbContext>
    {
        Task<AApplication> GetApplicationData(string applicationID);
        Task<DDocContent> GetReportContent(string reportID);

        Task<List<BulletindecisionDateDTO>> GetBulletinesPerPerson(List<string> pids);
    }
}
