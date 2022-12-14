using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.UserCitizen;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Services
{
    public class UserCitizenService : BaseAsyncService<UserCitizenDTO, UserCitizenDTO, UserCitizenGridDTO, GUsersCitizen, string, CaisDbContext>, IUserCitizenService
    {
        private readonly IUserCitizenRepository _userCitizenRepository;
        public UserCitizenService(IMapper mapper, IUserCitizenRepository userCitizenRepository) : base(mapper, userCitizenRepository)
        {
            _userCitizenRepository = userCitizenRepository;
        }

        public async Task<GUsersCitizen> AuthenticatePublicUserAsync(UserCitizenDTO userDTO)
        {
            var entity = await _userCitizenRepository.SingleOrDefaultAsync<GUsersCitizen>(x => x.Egn == userDTO.Egn);// await dbContext.GUsersCitizens.AsNoTracking()
                //.FirstOrDefaultAsync(x => x.Egn == userDTO.Egn);

            if (entity == null)
            {
                entity = mapper.MapToEntity<UserCitizenDTO, GUsersCitizen>(userDTO, true);
                entity.Id = BaseEntity.GenerateNewId();
                entity.EntityState = Common.Enums.EntityStateEnum.Added;
                await _userCitizenRepository.SaveEntityAsync(entity,false);
            }

            return entity;
        }

        public async Task<GUsersCitizen> GetUserCitizenByEgnAsync(string egn)
        {
            var entity = await _userCitizenRepository.SingleOrDefaultAsync<GUsersCitizen>(x => x.Egn == egn);
            //await dbContext.GUsersCitizens.AsNoTracking()
            //.FirstOrDefaultAsync(u => u.Egn == egn);
            return entity;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
