using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Impl
{
    public class CertificateRepository : BaseAsyncRepository<ACertificate, CaisDbContext>, ICertificateRepository
    {
        public CertificateRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}
