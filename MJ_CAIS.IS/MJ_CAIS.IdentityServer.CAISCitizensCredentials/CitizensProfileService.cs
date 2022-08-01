using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using TechnoLogica.Authentication.Common;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.IdentityServer.CAISCitizensCredentials
{
    public class CitizensProfileService : IProfileClientService
    {
        public virtual string ClientId => "cais-public";

        protected CaisDbContext CaisDbContext { get; set; }

        public CitizensProfileService(
            CaisDbContext caisDbContext,
            IConfiguration configuration)
        {
            CaisDbContext = caisDbContext;
        }

        public async Task SignOutAsync()
        {
            // No implementation needed.
        }

        public Task<bool> ValidateCredentials(string scheme, string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePassword(string scheme, string username, string password, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendPasswordResetTokenAsync(string scheme, string baseAddress, string email)
        {
            throw new NotImplementedException();
        }

        public Task<ResetPasswordResult> ResetPasswordAsync(string scheme, string email, string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<UserInfo> FindByUsername(string scheme, string username)
        {
            UserInfo res = null;
            if (!string.IsNullOrEmpty(username))
            {
                var egn = username.Replace("PNOBG-", "");
                res =
                (from u in CaisDbContext.GUsersCitizen
                 where u.Egn == egn
                 select new UserInfo()
                 {
                     Name = u.Name ?? u.Egn,
                     SubjectId = u.Id,
                     Username = u.Egn,
                     Active = true
                 }).FirstOrDefault();
            }
            return res;
        }

        public async Task<UserRegistrationResult> RegisterUser(string scheme, string name, string userName, string email, string password, Dictionary<string, string> additionalAttributes)
        {
            if (!string.IsNullOrEmpty(userName) && userName.StartsWith("PNOBG-"))
            {
                var egn = userName.Replace("PNOBG-", "");
                CaisDbContext.GUsersCitizen.Add(new Entities.GUsersCitizen()
                {
                    Id = Guid.NewGuid().ToString(),
                    Egn = egn,
                    Name = name,
                    Email = email
                });
                await CaisDbContext.SaveChangesAsync();
                return new UserRegistrationResult() { Succeeded = true };
            }
            else
            {
                return new UserRegistrationResult() { 
                    Succeeded = false, 
                    Errors = new UserRegistrationError[] {
                        new UserRegistrationError() {
                            Code = "EGN_ONLY",
                            Description = "Only users with EGN allowed"
                        }
                    }
                };
            }
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userID = (context.Subject.Identity as ClaimsIdentity).Claims.FirstOrDefault(c => c.Type == "sub").Value;
            var user =
                CaisDbContext.GUsersCitizen
                .AsNoTracking()
                .Where(u => u.Id == userID)
                .Select(u => new
                {
                    u.Name,
                    u.Egn,
                    u.Email,
                    u.Id
                })
                .FirstOrDefault();
            if (user != null)
            {
                context.IssuedClaims.Add(new Claim("EgnIdentifier", user.Egn));
                context.IssuedClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                context.IssuedClaims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name));                
                context.IssuedClaims.Add(new Claim(ClaimTypes.Email, user.Email));
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

            var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;
            var user = CaisDbContext.GUsersCitizen.Where(u => u.Id == subjectId).FirstOrDefault();
            context.IsActive = user != null;
        }
    }

    /// <summary>
    /// For Test purposes
    /// </summary>
    public class LocalCitizensProfileService : CitizensProfileService
    {
        public override string ClientId => "cais-citizens-local";

        public LocalCitizensProfileService(
            CaisDbContext daisDbContext,
            IConfiguration configuration) : base(
                daisDbContext,
                configuration)
        {
        }
    }
}
