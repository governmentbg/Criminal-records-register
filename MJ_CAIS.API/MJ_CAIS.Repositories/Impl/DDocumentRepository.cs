using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class DDocumentRepository : BaseAsyncRepository<DDocument, CaisDbContext>, IDDocumentRepository
    {
        public DDocumentRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<DDocument> SelectByEcrisIdAsync(string id)
        {
            var result = await _dbContext.Set<DDocument>().Include(x => x.DocContent).AsNoTracking()
                .FirstOrDefaultAsync(x => x.EcrisMsgId == id);
            return result;
        }
    }
}