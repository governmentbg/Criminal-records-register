using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DTO.WApplicaiton;
using static MJ_CAIS.Common.Constants.ApplicationConstants;
using MJ_CAIS.Common.Constants;

namespace MJ_CAIS.Repositories.Impl
{
    public class WApplicationRepository : BaseAsyncRepository<WApplication, CaisDbContext>, IWApplicationRepository
    {
        public WApplicationRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<WApplication> SelectAll()
        {
            return _dbContext.WApplications
                .Include(x => x.PurposeNavigation)
                .Include(x => x.PaymentMethod)
                .AsNoTracking();
        }

        public override Task<WApplication> SelectAsync(string id)
        {
            return _dbContext.WApplications
                .Include(x => x.StatusCodeNavigation)
                .Include(x => x.ApplicationType)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<WApplicaitonGridDTO> SelectAllForCheckPayment()
        {
            var query = _dbContext.WApplications
                .Include(x => x.APayments)
                .ThenInclude(x => x.EPayment)
                .Include(x => x.PaymentMethod)
                .Include(x => x.PurposeNavigation)
                .AsNoTracking()
                .Where(x => x.StatusCode == ApplicationWebStatuses.WebCheckPayment && x.APayments.Any(x =>
                    x.EPayment.PaymentStatus != PaymentConstants.PaymentStatuses.Payed))
                .Select(x => new WApplicaitonGridDTO
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    Egn = x.Egn,
                    PaymentMethodName = x.PaymentMethod.Name,
                    Purpose = x.PurposeNavigation.Name,
                    PurposeDesc = x.PurposeNavigation.Description,
                    RegistrationNumber = x.RegistrationNumber,
                    Version = x.Version,
                });

            return query;
        }

        public IQueryable<EPayment> GetPendingPaymentsByWAppId(string aId)
        {
            var query = _dbContext.APayments
                .Include(x => x.EPayment)
                .AsNoTracking()
                .Where(x => x.WApplicationId == aId && x.EPayment.PaymentStatus == PaymentConstants.PaymentStatuses.Pending)
                .Select(x => x.EPayment);

            return query;
        }
    }
}
