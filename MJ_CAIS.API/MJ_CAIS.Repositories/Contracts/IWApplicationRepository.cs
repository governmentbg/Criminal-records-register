using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.WApplicaiton;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IWApplicationRepository : IBaseAsyncRepository<WApplication, string, CaisDbContext>
    {
        IQueryable<WApplicaitonGridDTO> SelectAllForCheckPayment();

        IQueryable<EPayment> GetPendingPaymentsByWAppId(string aId);
    }
}
