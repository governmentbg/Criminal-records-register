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

        public IQueryable<DDocument> GetDocumentDataByApplicationID(string aId)
        {
            return _dbContext.DDocuments
                .AsNoTracking()
                .Include(x => x.DocType)
                .Include(x => x.DocContent)
                .Where(x => x.ApplicationId == aId)
                ;
        }
        public IQueryable<DDocument> GetDocumentDataByFbbcID(string aId)
        {
            return _dbContext.DDocuments
                .AsNoTracking()
                .Include(x => x.DocType)
                .Include(x => x.DocContent)
                .Where(x => x.FbbcId == aId)
                ;
        }

        public async Task<DDocument> GetDocumentWithContentByID(string documentId)
        {
            return await _dbContext.Set<DDocument>().AsNoTracking()
                .Include(x => x.DocContent)
                .FirstOrDefaultAsync(x => x.Id == documentId);
        }
    }
}