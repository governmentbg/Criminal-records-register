using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.AStatusH;
using MJ_CAIS.DTO.Home;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class ApplicationRepository : BaseAsyncRepository<AApplication, CaisDbContext>, IApplicationRepository
    {
        private readonly IUserContext _userContext;

        public ApplicationRepository(CaisDbContext dbContext, IUserContext userContext) : base(dbContext)
        {
            _userContext = userContext;
        }

        public override IQueryable<AApplication> SelectAll()
        {
            var result = this._dbContext
                .Set<AApplication>()
                .Include(x => x.CsAuthorityBirth)
                .Include(x => x.ACertificates)
                .AsNoTracking();

            result = _userContext.FilterByAuthorityForAllRoles(result);

            return result;
        }

        public async Task<IQueryable<ACertificate>> SelectAllCertificateAsync()
        {
            var result = this._dbContext.ACertificates
                .Include(x => x.StatusCodeNavigation)
                .Include(x => x.Application)
                    .ThenInclude(x => x.CsAuthorityBirth)
                .AsNoTracking();

            return await Task.FromResult(result);
        }

        public override async Task<AApplication> SelectAsync(string id)
        {
            var result = await this._dbContext.Set<AApplication>()
                .Include(x => x.ACertificates)
                .Include(x => x.ApplicationType)
                .Include(x => x.StatusCodeNavigation)
                .Include(x => x.AAppCitizenships)
                .Include(x => x.BirthCountry)
                .Include(x => x.BirthCity)
                .Include(x => x.BirthCity.Municipality)
                //.Include(x => x.BirthCity.Country)
                .AsNoTracking()
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
                            Version = aStatusH.Version,
                        };

            return await Task.FromResult(query);

        }

        public async Task<IQueryable<ACertificate>> SelectApplicationCertificateByApplicationIdAsync(string aId)
        {
            return await Task.FromResult(_dbContext.ACertificates.AsNoTracking()
                .Where(x => x.ApplicationId == aId));
        }

        public IQueryable<ObjectStatusCountDTO> GetStatusCountByCurrentAuthority()
        {
            var query = _dbContext.AApplications.AsNoTracking()
                .Where(x => x.CsAuthorityId == _userContext.CsAuthorityId && (x.StatusCode == ApplicationConstants.ApplicationStatuses.NewId ||
                                                            x.StatusCode == ApplicationConstants.ApplicationStatuses.CheckPayment ||
                                                            x.StatusCode == ApplicationConstants.ApplicationStatuses.CheckTaxFree ||
                                                            x.StatusCode == ApplicationConstants.ApplicationStatuses.BulletinsCheck))
                .GroupBy(x => x.StatusCode)
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count()
                });

            return query;
        }

        public IQueryable<ObjectStatusCountDTO> GetForJudgeCountByCurrentAuthority()
        {
            var query = _dbContext.AApplications.AsNoTracking()
                .Where(x => x.CsAuthorityId == _userContext.CsAuthorityId && (
                                                                              x.StatusCode == ApplicationConstants.ApplicationStatuses.CheckPayment ||
                                                                              x.StatusCode == ApplicationConstants.ApplicationStatuses.CertificateContentReady ||
                                                                              x.StatusCode == ApplicationConstants.ApplicationStatuses.CertificatePaperPrint ||
                                                                              x.StatusCode == ApplicationConstants.ApplicationStatuses.BulletinsSelection))
                .GroupBy(x => x.StatusCode)
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count()
                });

            return query;
        }
    }
}
