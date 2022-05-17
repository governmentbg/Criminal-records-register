using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IExtAdministrationRepository : IBaseAsyncRepository<GExtAdministration, string, CaisDbContext>
    {
    }
}
