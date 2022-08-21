using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class WCertificateRepository : BaseAsyncRepository<WCertificate, CaisDbContext>, IWCertificateRepository
    {
        public WCertificateRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<WCertificate> GetByApplicationIdAsync(string appId)
        {
            // todo: last cert ?
            var certificate = await _dbContext.WCertificates
                .AsNoTracking()
                .Where(x => x.WApplId == appId)
                .FirstOrDefaultAsync();

            return certificate;
        }
    }
}
