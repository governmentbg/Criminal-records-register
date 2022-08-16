using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class EWebRequestsRepository : BaseAsyncRepository<EWebRequest, CaisDbContext>, IEWebRequestsRepository
    {
        public EWebRequestsRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }


        public virtual async Task<EWebRequest> SelectAsync(string id)
        {
            var result = await _dbContext.Set<EWebRequest>().Include(x => x.WebService).AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<IQueryable<EWebRequest>> SelectAllByApplicationId(string aId)
        {
            return
                await Task.FromResult(
                    _dbContext.Set<EWebRequest>()
                        .Where(x => x.ApplicationId == aId)
                        .Select(x => new EWebRequest
                        {
                            Id = x.Id,
                            CreatedOn = x.CreatedOn,
                            Error = x.Error,
                            HasError = x.HasError,
                            WebService = new EWebService { Id = x.WebService.Id, Name = x.WebService.Name },
                            WebServiceId = x.WebServiceId,
                            ApiServiceCallId = x.ApiServiceCallId
                        })
                        .Union(_dbContext.Set<EWebRequest>()
                            .Where(x => _dbContext.Set<AApplication>().Where(a => a.Id == aId).Select(a => a.WApplicationId).Contains(x.WApplicationId))
                            .Select(x => new EWebRequest
                            {
                                Id = x.Id,
                                CreatedOn = x.CreatedOn,
                                Error = x.Error,
                                HasError = x.HasError,
                                WebService = new EWebService { Id = x.WebService.Id, Name = x.WebService.Name },
                                WebServiceId = x.WebServiceId,
                                ApiServiceCallId = x.ApiServiceCallId
                            }))
                        .AsNoTracking()
                );
        }
    }
}