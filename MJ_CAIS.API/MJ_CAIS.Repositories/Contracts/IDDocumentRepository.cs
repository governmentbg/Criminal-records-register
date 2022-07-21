using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IDDocumentRepository : IBaseAsyncRepository<DDocument, string, CaisDbContext>
    {
        Task<DDocument> SelectByEcrisIdAsync(string id);
    }
}
