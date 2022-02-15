using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DTO.Nomenclature;

namespace MJ_CAIS.Repositories.Impl
{
    public class NomenclatureDetailRepository : BaseAsyncRepository<GNomenclature, CaisDbContext>, INomenclatureDetailRepository
    {
        public NomenclatureDetailRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}
