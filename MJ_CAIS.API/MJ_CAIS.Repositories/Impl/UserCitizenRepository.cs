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
    public class UserCitizenRepository : BaseAsyncRepository<GUsersCitizen, CaisDbContext>, IUserCitizenRepository
    {
        public UserCitizenRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}
