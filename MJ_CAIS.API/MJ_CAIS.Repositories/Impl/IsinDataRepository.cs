using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class IsinDataRepository : BaseAsyncRepository<EIsinDatum, CaisDbContext>, IIsinDataRepository
    {
        public IsinDataRepository(CaisDbContext dbContext) : base(dbContext)
        {
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
