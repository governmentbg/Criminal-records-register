using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Home;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DTO.AStatusH;

namespace MJ_CAIS.Repositories.Impl
{
    public class ApplicationRepository : BaseAsyncRepository<AApplication, CaisDbContext>, IApplicationRepository
    {
        public ApplicationRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<AApplication> SelectAllAsync()
        {
            var result = this._dbContext
                .Set<AApplication>()
                .Include(x => x.CsAuthorityBirth)
                .AsNoTracking();

            return result;
        }

        public async Task<IQueryable<ACertificate>> SelectAllCertificateAsync()
        {
            var result = this._dbContext.ACertificates
                .Include(x => x.Application)
                    .ThenInclude(x => x.CsAuthorityBirth)
                .AsNoTracking();

            return await Task.FromResult(result);
        }

        public override async Task<AApplication> SelectAsync(string id)
        {
            var result = await this._dbContext.Set<AApplication>()
                .Include(x => x.ACertificates)
                .Include(x => x.AAppCitizenships).AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }


        public async Task<IQueryable<AAppPersAlias>> SelectApplicationPersAliasByApplicationIdAsync(string aId)
        {
            return await Task.FromResult(_dbContext.AAppPersAliases.AsNoTracking()
                .Where(x => x.ApplicationId == aId));
        }

        public async Task<IQueryable<AStatusHGridDTO>> SelectApplicationPersStatusHAsync(string aId)
        {
            var query = from aStatusH in _dbContext.AStatusHes.AsNoTracking()
                        join status in _dbContext.AApplicationStatuses.AsNoTracking() on aStatusH.StatusCode equals status.Code
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
                        };

            return await Task.FromResult(query);

        }

        public async Task<IQueryable<ACertificate>> SelectApplicationCertificateByApplicationIdAsync(string aId)
        {
            return await Task.FromResult(_dbContext.ACertificates.AsNoTracking()
                .Where(x => x.ApplicationId == aId));
        }


        public async Task<IQueryable<ObjectStatusCountDTO>> GetStatusCountAsync()
        {
            var query = _dbContext.AApplications.AsNoTracking()
                .Where(x => x.StatusCode == ApplicationConstants.ApplicationStatuses.NewId ||
                            x.StatusCode == ApplicationConstants.ApplicationStatuses.CheckPayment ||
                            x.StatusCode == ApplicationConstants.ApplicationStatuses.CheckTaxFree ||
                            x.StatusCode == ApplicationConstants.ApplicationStatuses.BulletinsCheck)
                .GroupBy(x => x.StatusCode)
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count()
                });

            return await Task.FromResult(query);
        }
    }
}
