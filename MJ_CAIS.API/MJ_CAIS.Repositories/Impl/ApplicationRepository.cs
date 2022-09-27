using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.AStatusH;
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
                .Include(x => x.StatusCodeNavigation)
                .Include(x => x.CsAuthorityBirth)
                .Include(x => x.ACertificates)
                .AsNoTracking();

            result = _userContext.FilterByAuthorityForAllRoles(result);

            return result;
        }

        public async Task<IQueryable<ACertificate>> SelectAllCertificateAsync()
        {
            var result = this._dbContext.Set<ACertificate>()
                .Include(x => x.StatusCodeNavigation)
                .Include(x => x.Application)
                    .ThenInclude(x => x.CsAuthorityBirth)
                .Where(x => x.Application.CsAuthorityId == _userContext.CsAuthorityId)
                .AsNoTracking();

            return await Task.FromResult(result);
        }

        public async Task<IQueryable<BBulletin>> SelectBulletinIdsAsync(string personId)
        {
            var result = (this._dbContext.BBulletins.Where(b =>
                        //(b.StatusId != BulletinConstants.Status.Deleted)
                        b.EgnNavigation.PersonId == personId).Select(b => new BBulletin { Id = b.Id, CreatedOn = b.CreatedOn, StatusId = b.StatusId , DecisionFinalDate = b.DecisionFinalDate, DecisionDate = b.DecisionDate, CaseYear = b.CaseYear})
                        .Union(this._dbContext.BBulletins.Where(b =>
                        // (b.StatusId != BulletinConstants.Status.Deleted)
                         b.LnchNavigation.PersonId == personId).Select(b => new BBulletin { Id = b.Id, CreatedOn = b.CreatedOn, StatusId = b.StatusId, DecisionFinalDate = b.DecisionFinalDate, DecisionDate = b.DecisionDate, CaseYear = b.CaseYear }))
                        .Union(this._dbContext.BBulletins.Where(b =>
                        // (b.StatusId != BulletinConstants.Status.Deleted)
                        b.LnNavigation.PersonId == personId).Select(b => new BBulletin { Id = b.Id, CreatedOn = b.CreatedOn, StatusId = b.StatusId, DecisionFinalDate = b.DecisionFinalDate, DecisionDate = b.DecisionDate, CaseYear = b.CaseYear }))
                        .Union(this._dbContext.BBulletins.Where(b =>
                         //  (b.StatusId != BulletinConstants.Status.Deleted)
                         b.SuidNavigation.PersonId == personId).Select(b => new BBulletin { Id = b.Id, CreatedOn = b.CreatedOn, StatusId = b.StatusId, DecisionFinalDate = b.DecisionFinalDate, DecisionDate = b.DecisionDate, CaseYear = b.CaseYear }))
                        )
                        .Where(b => b.StatusId != BulletinConstants.Status.Deleted)
                        //order_bulletins
                        .OrderBy(b => b.DecisionFinalDate)
                        .OrderBy(b => b.DecisionDate)
                        .OrderBy(b => b.CaseYear)
                        .OrderBy(b => b.CreatedOn.HasValue ? b.CreatedOn.Value.Date : DateTime.Now)
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

        public async Task<AApplication> SelectEntityAsync(string id)
        {
            var result = await this._dbContext.Set<AApplication>()
                   .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }


        public async Task<IQueryable<AStatusHGridDTO>> SelectApplicationPersStatusHAsync(string aId)
        {
            var query = from aStatusH in _dbContext.AStatusHes.AsNoTracking()
                        join aApplication in _dbContext.AApplications.AsNoTracking() on aStatusH.ApplicationId equals aApplication.Id
                        join aCertificates in _dbContext.ACertificates.AsNoTracking() on aStatusH.CertificateId equals aCertificates.Id
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
                            ApplicationRegistrationNumber = aApplication.RegistrationNumber,
                            CertificateRegistrationNumber = aCertificates.RegistrationNumber,
                        };

            return await Task.FromResult(query);

        }

        public async Task<AApplication?> GetApplicationForCertificateGeneration(string id)
        {
            return await _dbContext.AApplications.AsNoTracking()
                 .Include(a => a.EgnNavigation).AsNoTracking()
                 .Include(a => a.LnchNavigation).AsNoTracking()
                 .Include(a => a.LnNavigation).AsNoTracking()
                 .Include(a => a.SuidNavigation).AsNoTracking()
                 .Include(a => a.ApplicationType).AsNoTracking()
                 .Include(a => a.AStatusHes).AsNoTracking()
                 .FirstOrDefaultAsync(aa => aa.Id == id);
        }


    }
}
