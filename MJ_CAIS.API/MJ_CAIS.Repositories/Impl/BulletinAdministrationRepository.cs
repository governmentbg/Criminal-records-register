using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Nomenclature;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DTO.BulletinAdministration;
using MJ_CAIS.Common.Resources;

namespace MJ_CAIS.Repositories.Impl
{
    public class BulletinAdministrationRepository : BaseAsyncRepository<BBulletin, CaisDbContext>, IBulletinAdministrationRepository
    {
        private readonly IUserContext _userContext;

        public BulletinAdministrationRepository(CaisDbContext dbContext, IUserContext userContext)
            : base(dbContext)
        {
            _userContext = userContext;
        }

        public IQueryable<BulletinAdministrationGridDTO> SelectAllNotDeletedAndLockedBulletins(BulletinAdministrationSearchParamDTO searchParams)
        {
            var bulletinsQuery = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                 where bulletin.StatusId != BulletinConstants.Status.Deleted &&
                                       bulletin.Locked == true &&
                                       bulletin.CsAuthorityId == _userContext.CsAuthorityId
                                 select bulletin;

            var filteredBlletins = ApplyFormFilter(bulletinsQuery, searchParams);

            var query = from bulletin in filteredBlletins
                        join auth in _dbContext.GDecidingAuthorities.AsNoTracking() on bulletin.BulletinAuthorityId equals auth.Id
                            into authLeft
                        from auth in authLeft.DefaultIfEmpty()

                        join status in _dbContext.BBulletinStatuses.AsNoTracking() on bulletin.StatusId equals status.Code
                           into statusLeft
                        from status in statusLeft.DefaultIfEmpty()

                        select new BulletinAdministrationGridDTO
                        {
                            Id = bulletin.Id,
                            BulletinType = bulletin.BulletinType == BulletinConstants.Type.Bulletin78A ? BulletinResources.Bulletin78A :
                                                        bulletin.BulletinType == BulletinConstants.Type.ConvictionBulletin ? BulletinResources.ConvictionBulletin :
                                                             BulletinResources.Unspecified,
                            RegistrationNumber = bulletin.RegistrationNumber,
                            BulletinAuthorityName = auth.Name,
                            Identifier = bulletin.Egn + "/" + bulletin.Lnch,
                            FullName = !string.IsNullOrEmpty(bulletin.Fullname) ? bulletin.Fullname :
                             bulletin.Firstname + " " + bulletin.Surname + " " + bulletin.Familyname,
                            BirthDate = bulletin.BirthDate,
                            CreatedOn = bulletin.CreatedOn,
                            StatusId = bulletin.StatusId,
                            StatusName = status.Name,
                        };

            return query;
        }

        public override async Task<BBulletin> SelectAsync(string id)
        {
            var query = await _dbContext.BBulletins
                .AsNoTracking()
                .Include(x => x.CsAuthority)
                .Include(x => x.Status)
                .Include(x => x.BulletinAuthority)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return query;
        }

        public async Task<BBulletin> GetBulletinByIdAsync(string id)
        {
            var bulletin = await _dbContext.BBulletins.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return bulletin;
        }

        public IQueryable<BaseNomenclatureDTO> GetBulletinStatusesByHistory(string aId)
        {
            var result = _dbContext.BBulletinStatusHes.AsNoTracking()
                .Where(x => x.BulletinId == aId)
                .Include(x => x.NewStatusCodeNavigation)
                .Select(x => new BaseNomenclatureDTO
                {
                    Id = x.NewStatusCode,
                    Code = x.NewStatusCode,
                    Name = x.NewStatusCodeNavigation.Name
                })
                .Distinct();

            return result;
        }

        private static IQueryable<BBulletin> ApplyFormFilter(IQueryable<BBulletin> query, BulletinAdministrationSearchParamDTO searchParams)
        {
            if (searchParams == null) return query;

            if (!string.IsNullOrEmpty(searchParams.RegistrationNumber))
                query = query.Where(x => x.RegistrationNumber == searchParams.RegistrationNumber);

            if (!string.IsNullOrEmpty(searchParams.BulletinType))
                query = query.Where(x => x.BulletinType == searchParams.BulletinType);

            if (!string.IsNullOrEmpty(searchParams.StatusId))
                query = query.Where(x => x.StatusId == searchParams.StatusId);

            if (!string.IsNullOrEmpty(searchParams.Firstname))
                query = query.Where(x => x.Firstname == searchParams.Firstname);

            if (!string.IsNullOrEmpty(searchParams.Surname))
                query = query.Where(x => x.Surname == searchParams.Surname);

            if (!string.IsNullOrEmpty(searchParams.Familyname))
                query = query.Where(x => x.Familyname == searchParams.Familyname);

            if (!string.IsNullOrEmpty(searchParams.Egn))
                query = query.Where(x => x.Egn == searchParams.Egn);

            if (!string.IsNullOrEmpty(searchParams.Lnch))
                query = query.Where(x => x.Lnch == searchParams.Lnch);

            if (searchParams.BirthDate.HasValue)
                query = query.Where(x => x.BirthDate == searchParams.BirthDate.Value);

            if (searchParams.FromDate.HasValue)
                query = query.Where(x => x.CreatedOn >= searchParams.FromDate.Value.AddDays(-1).Date);

            if (searchParams.ToDate.HasValue)
                query = query.Where(x => x.CreatedOn <= searchParams.ToDate.Value);

            return query;
        }
    }
}
