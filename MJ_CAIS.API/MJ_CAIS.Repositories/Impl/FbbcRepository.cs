using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DTO.Fbbc;
using MJ_CAIS.DTO.Home;

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
                            CreatedOn = fbbc.CreatedOn
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

        public IQueryable<ObjectStatusCountDTO> GetStatusCount()
        {
            var query = _dbContext.Fbbcs.AsNoTracking()
                .Where(x => x.StatusCode == FbbcConstants.FBBCStatus.Active ||
                                                                              x.StatusCode == FbbcConstants.FBBCStatus.ForDelete ||
                                                                              x.StatusCode == FbbcConstants.FBBCStatus.Deleted)
                .GroupBy(x => x.StatusCode)
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count()
                });

            return query;
        }

        public IQueryable<FbbcByPersonIdDTO> GetAllFbbcsByPersonId(string personId)
        {
            var fbbcByEgn = from fbbc in _dbContext.Fbbcs.AsNoTracking()
                            join egn in _dbContext.PPersonIds.AsNoTracking() on fbbc.PersonId equals egn.Id
                            where egn.PersonId == personId
                            select new FbbcByPersonIdDTO
                            {
                                Id = fbbc.Id,
                                DocTypeId = fbbc.DocTypeId,
                                ReceiveDate = fbbc.ReceiveDate,
                                CountryId = fbbc.CountryId,
                                Egn = fbbc.Egn,
                                Firstname = fbbc.Firstname,
                                Surname = fbbc.Surname,
                                Familyname = fbbc.Familyname,
                                BirthDate = fbbc.BirthDate,
                                CreatedOn = fbbc.CreatedOn
                            };

            var fbbcBySuid = from fbbc in _dbContext.Fbbcs.AsNoTracking()
                             join suid in _dbContext.PPersonIds.AsNoTracking() on fbbc.SuidId equals suid.Id
                             where suid.PersonId == personId
                             select new FbbcByPersonIdDTO
                             {
                                 Id = fbbc.Id,
                                 DocTypeId = fbbc.DocTypeId,
                                 ReceiveDate = fbbc.ReceiveDate,
                                 CountryId = fbbc.CountryId,
                                 Egn = fbbc.Egn,
                                 Firstname = fbbc.Firstname,
                                 Surname = fbbc.Surname,
                                 Familyname = fbbc.Familyname,
                                 BirthDate = fbbc.BirthDate,
                                 CreatedOn = fbbc.CreatedOn
                             };

            var allFbbcs = fbbcByEgn.Union(fbbcBySuid);

            return allFbbcs;
        }
    }
}
