using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DTO.Home;

namespace MJ_CAIS.Repositories.Impl
{
    public class IsinDataRepository : BaseAsyncRepository<EIsinDatum, CaisDbContext>, IIsinDataRepository
    {
        public IsinDataRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IQueryable<ObjectStatusCountDTO>> GetStatusCountAsync()
        {
            var query = _dbContext.EIsinData.AsNoTracking()
                .GroupBy(x => x.Status)
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count()
                });

            return await Task.FromResult(query);
        }

        // todo: remove
        //public override IQueryable<EIsinDatum> SelectAllAsync()
        //{ 
        //    return _dbContext.EIsinData.AsNoTracking()
        //        .Include(x => x.IsinMsg);
        //}

        //public override async Task<EIsinDatum> SelectAsync(string id)
        //{
        //    return await _dbContext.EIsinData
        //        .AsNoTracking()
        //        .Include(x => x.IsinMsg)
        //        .FirstOrDefaultAsync(x => x.Id == id);
        //}
    }
}
