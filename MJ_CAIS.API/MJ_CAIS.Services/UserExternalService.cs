using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.UserExternal;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

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

        public async Task<GUsersExt> AuthenticateExternalUserAsync(UserExternalDTO userDTO)
        {
            var entity = await dbContext.GUsersExts.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Egn == userDTO.Egn);

            if (entity == null)
            {
                entity = mapper.MapToEntity<UserExternalDTO, GUsersExt>(userDTO, true);
                entity.Id = BaseEntity.GenerateNewId();
                await dbContext.SaveEntityAsync(entity);
            }

            return entity;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
