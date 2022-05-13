using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.UserCitizen;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services
{
    public class UserCitizenService : BaseAsyncService<UserCitizenDTO, UserCitizenDTO, UserCitizenGridDTO, GUsersCitizen, string, CaisDbContext>, IUserCitizenService
    {
        private readonly IUserCitizenRepository _userCitizenRepository;
        public UserCitizenService(IMapper mapper, IUserCitizenRepository userCitizenRepository) : base(mapper, userCitizenRepository)
        {
            _userCitizenRepository = userCitizenRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
