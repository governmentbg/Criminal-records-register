using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class CertificateRepository : BaseAsyncRepository<ACertificate, CaisDbContext>, ICertificateRepository
    {
        public CertificateRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ACertificate> GetByApplicationIdAsync(string appId)
        {
            // todo: last cert ?
            var certificate = await _dbContext.ACertificates.AsNoTracking()
                                .Where(x => x.ApplicationId == appId)
                                .OrderByDescending(x => x.CreatedOn)
                                .FirstOrDefaultAsync();

            return certificate;
        }
    }
}
