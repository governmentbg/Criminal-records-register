using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Repositories.Impl
{
    public class RoleRepository : BaseAsyncRepository<GRole, CaisDbContext>, IRoleRepository
    {
        public RoleRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}
