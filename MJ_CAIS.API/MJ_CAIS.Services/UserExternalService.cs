using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DataAccess.ExtUsers;
using MJ_CAIS.DTO.UserExternal;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace MJ_CAIS.Services
{
    internal class UserExternalService : BaseAsyncService<UserExternalInDTO, UserExternalDTO, UserExternalGridDTO, GUsersExt, string, CaisDbContext>, IUserExternalService
    {
        private readonly IUserExternalRepository _userExternalRepository;
        private readonly IExtAdministrationRepository _extAdministrationRepository;
        
        public UserExternalService(
            IMapper mapper,
            IUserExternalRepository userExternalRepository, 
            IExtAdministrationRepository extAdministrationRepository)
            : base(mapper, userExternalRepository)
        {
            _userExternalRepository = userExternalRepository;
            _extAdministrationRepository = extAdministrationRepository;
        }

        public async override Task UpdateAsync(string aId, UserExternalInDTO aInDto)
        {
            // TODO: should not select from db, but it must check if the saveChanges has returned true (or 1)
            GUsersExt repoObj = await this.baseAsyncRepository.SelectAsync(aId);
            if (repoObj == null)
            {
                throw new Exception("Object with id [" + aId + "] was not found!");
            }

            this.ValidateData(aInDto);

            GUsersExt entity = mapper.MapToEntity<UserExternalInDTO, GUsersExt>(aInDto, isAdded: false);
            if (!string.IsNullOrEmpty(aInDto.Uic))
            {
                await _extAdministrationRepository.AddUicAsync(aInDto.AdministrationId, aInDto.Uic, aInDto.Ou);
                entity.RegCertSubject = null;
                entity.ModifiedProperties.Add(nameof(entity.RegCertSubject));
            }
            await this.SaveEntityAsync(entity);
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
                await _userExternalRepository.SaveEntityAsync(entity, false);
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


    //TODO: Duplicated in MJ_CAIS.IdentityServer.CAISExternalCredentials
    public class CompatibilityPasswordHasher : PasswordHasher<GUsersExt>
    {
        public override PasswordVerificationResult VerifyHashedPassword(GUsersExt user, string hashedPassword, string providedPassword)
        {
            var compatibilityPassword = Hash(providedPassword);
            return base.VerifyHashedPassword(user, hashedPassword, compatibilityPassword);
        }

        public override string HashPassword(GUsersExt user, string password)
        {
            var compatibilityPassword = Hash(password);
            return base.HashPassword(user, compatibilityPassword);
        }

        /// <summary>
        /// Compute hash for backward compatiblity
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes("CSCS" + input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
