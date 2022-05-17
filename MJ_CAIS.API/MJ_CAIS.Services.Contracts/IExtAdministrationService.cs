using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExtAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services.Contracts
{
    public interface IExtAdministrationService : IBaseAsyncService<ExtAdministrationDTO, ExtAdministrationDTO, ExtAdministrationGridDTO, GExtAdministration, string>
    {
        public Task<List<ExtAdministrationGridDTO>> SelectAllAsync();
    }
}
