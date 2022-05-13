using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.UserExternal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services.Contracts
{
    public interface IUserExternalService : IBaseAsyncService<UserExternalDTO, UserExternalDTO, UserExternalGridDTO, GUsersExt, string>
    {
    }
}
