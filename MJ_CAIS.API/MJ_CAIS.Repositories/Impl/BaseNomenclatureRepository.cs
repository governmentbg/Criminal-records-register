using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Repositories.Impl
{
    //public class BaseNomenclatureRepository<TEntity, TContext> : BaseAsyncRepository<TEntity, CaisDbContext>, IBaseNomenclatureRepository<TEntity, TContext>
    //where TEntity : class, IBaseNomenclature, new()
    //    where TContext : CaisDbContext
    //{
       
    //    protected readonly TEntity _entity;
    //    public BaseNomenclatureRepository(TContext dbContext, TEntity entity) : base(dbContext) { 
        
    //    _entity = entity;
    //    }

    //    public virtual async Task<TEntity> SelectByCodeAsync(string code)
    //    {
    //        return null;
    //        //var result = await this._dbContext.Set<TEntity>().AsNoTracking()
    //           // .FirstOrDefaultAsync(x => x.Code == code);
    //        //return result;
    //    }




    //}


}
