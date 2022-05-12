using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DTO.User;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;

namespace MJ_CAIS.Services
{
    public class UserService : BaseAsyncService<UserDTO, UserDTO, UserGridDTO, GUser, string, CaisDbContext>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
            : base(mapper, userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> AuthenticatePublicUser(UserDTO userDTO)
        {
            var entity = await dbContext.GUsers.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Egn == userDTO.Egn);

            if (entity == null)
            {
                entity = mapper.MapToEntity<UserDTO, GUser>(userDTO, true);
                await dbContext.SaveEntityAsync(entity);
            }

            var result = mapper.Map<UserDTO>(entity);
            return result;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
