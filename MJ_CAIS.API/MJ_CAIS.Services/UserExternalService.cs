using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.UserExternal;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services
{
    internal class UserExternalService : BaseAsyncService<UserExternalDTO, UserExternalDTO, UserExternalGridDTO, GUsersExt, string, CaisDbContext>, IUserExternalService
    {
        private readonly IUserExternalRepository _userExternalRepository;

        public UserExternalService(IMapper mapper, IUserExternalRepository userExternalRepository)
            : base(mapper, userExternalRepository)
        {
            _userExternalRepository = userExternalRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
