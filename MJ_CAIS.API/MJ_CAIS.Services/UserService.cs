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

        public override async Task<string> InsertAsync(UserDTO aInDto)
        {
            this.ValidateData(aInDto);
            GUser entity = mapper.MapToEntity<UserDTO, GUser>(aInDto, isAdded: true);
            this.TransformDataOnInsert(entity);
            entity.GUserRoles = CaisMapper.MapMultipleChooseToEntityList<GUserRole, string, string>(aInDto.Roles, nameof(GUserRole.Id), nameof(GUserRole.RoleId));
            await this.SaveEntityAsync(entity);
            return entity.Id;
        }

        public override async Task UpdateAsync(string aId, UserDTO aInDto)
        {
            // TODO: should not select from db, but it must check if the saveChanges has returned true (or 1)
            GUser repoObj = await this.baseAsyncRepository.SelectAsync(aId);
            if (repoObj == null)
            {
                throw new Exception("Object with id [" + aId + "] was not found!");
            }

            this.ValidateData(aInDto);

            GUser entity = mapper.MapToEntity<UserDTO, GUser>(aInDto, isAdded: false);
            entity.GUserRoles  = CaisMapper.MapMultipleChooseToEntityList<GUserRole, string, string>(aInDto.Roles, nameof(GUserRole.Id), nameof(GUserRole.RoleId));

            await this.SaveEntityAsync(entity);
        }
    }
}
