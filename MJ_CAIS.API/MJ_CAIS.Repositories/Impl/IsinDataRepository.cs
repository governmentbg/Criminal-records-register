using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DTO.Home;
using MJ_CAIS.Common.Constants;

namespace MJ_CAIS.Repositories.Impl
{
    public class IsinDataRepository : BaseAsyncRepository<EIsinDatum, CaisDbContext>, IIsinDataRepository
    {
        private readonly IUserContext _userContext;

        public IsinDataRepository(CaisDbContext dbContext,
            IUserContext userContext) : base(dbContext)
        {
            _userContext = userContext;
        }

        public IQueryable<ObjectStatusCountDTO> GetStatusCountByCurrentAuthority()
        {
            var query = _dbContext.EIsinData.AsNoTracking()
                .Include(x => x.Bulletin)
                .Where(x => x.Bulletin.CsAuthorityId == _userContext.CsAuthorityId && x.Status == IsinDataConstants.Status.New || x.Status == IsinDataConstants.Status.Identified)
                .GroupBy(x => x.Status)
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count()
                });

            return query;
        }
    }
}
