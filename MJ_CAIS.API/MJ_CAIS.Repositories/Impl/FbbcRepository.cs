using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DTO.Fbbc;

namespace MJ_CAIS.Repositories.Impl
{
    public class FbbcRepository : BaseAsyncRepository<Fbbc, CaisDbContext>, IFbbcRepository
    {
        public FbbcRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IQueryable<FbbcGridDTO>> SelectByStatusCodeAsync(string statusCode)
        {
            var query = from fbbc in _dbContext.Fbbcs.AsNoTracking()

                        join docType in _dbContext.FbbcDocTypes.AsNoTracking() on fbbc.DocTypeId equals docType.Id
                                    into docTypeLeft
                        from docType in docTypeLeft.DefaultIfEmpty()
                        where fbbc.StatusCode == statusCode
                        select new FbbcGridDTO
                        {
                            Id = fbbc.Id,
                            DocType = docType.Name,
                            BirthCityId = fbbc.BirthCityId,
                            BirthCountryId = fbbc.BirthCountryId,
                            BirthDate = fbbc.BirthDate,
                            DestroyedDate = fbbc.DestroyedDate,
                            Egn = fbbc.Egn,
                            Familyname = fbbc.Familyname,
                            Firstname = fbbc.Firstname,
                            ReceiveDate = fbbc.ReceiveDate,
                            Surname = fbbc.Surname,
                            Version = fbbc.Version,
                        };

            return await Task.FromResult(query);
        }

        public override async Task<Fbbc> SelectAsync(string aId)
        {
            var fbbc = await _dbContext.Fbbcs
               .Include(x => x.Country)
               .Include(x => x.BirthCountry)
               .Include(x => x.BirthCity)
                   .ThenInclude(x => x.Municipality)
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == aId);

            return fbbc;
        }
    }
}
