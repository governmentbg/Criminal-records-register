using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.AStatusH;
using MJ_CAIS.DTO.WApplicaiton;
using MJ_CAIS.Repositories.Contracts;
using static MJ_CAIS.Common.Constants.ApplicationConstants;

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
            var result = _dbContext.WApplications.AsNoTracking()
                .Include(x => x.BirthCity).AsNoTracking()
                .Include(x => x.BirthCity.Municipality).AsNoTracking()
                .Include(x => x.StatusCodeNavigation).AsNoTracking()
                .Include(x => x.ApplicationType).AsNoTracking()
                .Include(x => x.WAppCitizenships).AsNoTracking()
                .Include(x => x.WAppPersAliases).AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
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
                    Firstname = x.Firstname,
                    Surname = x.Surname,
                    Familyname = x.Familyname,
                    Fullname = x.Fullname,
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

        public async Task<IQueryable<AStatusHGridDTO>> SelectApplicationPersStatusHAsync(string aId)
        {
            var query = from aStatusH in _dbContext.WStatusHes.AsNoTracking()
                        join aApplication in _dbContext.WApplications.AsNoTracking() on aStatusH.ApplicationId equals aApplication.Id
                        join status in _dbContext.WApplicationStatuses.AsNoTracking() on aStatusH.StatusCode equals status.Code
                        join users in _dbContext.GUsers.AsNoTracking() on aStatusH.CreatedBy equals users.Id
                            into astatusHLeft
                        from user in astatusHLeft.DefaultIfEmpty()
                        where (string.IsNullOrEmpty(aId) || aStatusH.ApplicationId == aId)
                        select new AStatusHGridDTO()
                        {
                            Id = aStatusH.Id,
                            Descr = aStatusH.Descr,
                            UpdatedBy = user.Firstname + " " + user.Familyname,
                            CreatedOn = aStatusH.CreatedOn,
                            StatusCode = status.Name,
                            Version = aStatusH.Version,
                            ApplicationRegistrationNumber = aApplication.RegistrationNumber,
                        };

            return await Task.FromResult(query);

        }
    }
}
