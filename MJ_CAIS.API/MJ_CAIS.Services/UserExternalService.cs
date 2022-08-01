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
        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public async Task<string?> GetUserAdministrationNameAsync(string userId)
            => await _userExternalRepository.GetUserAdministrationNameAsync(userId);

        public async Task<string?> GetUserAdministrationIdAsync(string userId)
           => await _userExternalRepository.GetUserAdministrationIdAsync(userId);

        public async Task<string> SaveUserExternalAsync(UserExternalDTO aInDto, bool isAdded)
        {
            var entity = mapper.MapToEntity<UserExternalDTO, GUsersExt>(aInDto, isAdded: isAdded);
            if (isAdded)
            {
                this.TransformDataOnInsertAsync(entity);
            }

            await this.SaveEntityAsync(entity);
            return entity.Id;
        }

        public async Task<GUsersExt> AuthenticateExternalUserAsync(UserExternalDTO userDTO)
        {
            var entity = await _userExternalRepository.SingleOrDefaultAsync<GUsersExt>(x => x.Egn == userDTO.Egn);
            //await dbContext.GUsersExts.AsNoTracking()
            //.FirstOrDefaultAsync(x => x.Egn == userDTO.Egn);

            if (entity == null)
            {
                entity = mapper.MapToEntity<UserExternalDTO, GUsersExt>(userDTO, true);
                entity.Id = BaseEntity.GenerateNewId();
                entity.EntityState = Common.Enums.EntityStateEnum.Added;
                await _userExternalRepository.SaveEntityAsync(entity,false);
            }

            return entity;
        }

        public async Task<IQueryable<UserExternalGridDTO>> SelectExternalUsersByUserIdAsync(string userId)
        {
            var administrationId = await GetUserAdministrationIdAsync(userId);
            var query = _userExternalRepository.GetUsersByAdministration(administrationId);
            return query;
        }

        public async Task<UserExternalDTO> GetUserExternalDTOAsync(string userId)
        {
            var entity = await _userExternalRepository.SelectAsync(userId);
            return mapper.Map<UserExternalDTO>(entity);
        }
    }
}
