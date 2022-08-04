using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using TechnoLogica.Authentication.Common;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.IdentityServer.CAISExternalCredentials
{
    public class ExternalProfileService : IProfileClientService
    {
        public virtual string ClientId => "cais-external";

        protected CaisDbContext CaisDbContext { get; set; }

        public ExternalProfileService(
            CaisDbContext caisDbContext)
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
                (from u in CaisDbContext.GUsersExt
                    where u.Egn == egn
                    select new UserInfo()
                    {
                        Name = u.Name,
                        SubjectId = u.Id,
                        Active = u.Active,
                        Username = u.Egn
                    }).FirstOrDefault();
            }
            return res;
        }

        public async Task<UserRegistrationResult> RegisterUser(string scheme, string name, string userName, string email, string password, Dictionary<string, string> additionalAttributes)
        {
            if (!string.IsNullOrEmpty(userName) && userName.StartsWith("PNOBG-"))
            {
                var egn = userName.Replace("PNOBG-", "");
                CaisDbContext.GUsersExt.Add(new Entities.GUsersExt()
                {
                    Id = Guid.NewGuid().ToString(),
                    Egn = egn,
                    Name = name,
                    Email = email,
                    Active = false
                });
                await CaisDbContext.SaveChangesAsync();
                return new UserRegistrationResult() { Succeeded = true };
            }
            else
            {
                return new UserRegistrationResult()
                {
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
                CaisDbContext.GUsersExt
                .AsNoTracking()
                .Where(u => u.Id == userID)
                .Select(u => new
                {
                    u.Position,
                    u.AdministrationId,
                    u.IsAdmin,
                    u.Name,
                    u.Active,
                    u.Egn
                })
                .FirstOrDefault();
            if (user != null)
            {
                context.IssuedClaims.Add(new Claim("Name", user.Name));
                if (!string.IsNullOrEmpty(user.AdministrationId))
                {
                    context.IssuedClaims.Add(new Claim("AdministrationId", user.AdministrationId));
                }
                if (user.IsAdmin.HasValue && user.IsAdmin.Value)
                {
                    context.IssuedClaims.Add(new Claim("isAdmin", "true"));
                }
            }
            throw new NotImplementedException();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

            var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;
            var user = CaisDbContext.GUsersExt.Where(u => u.Id == subjectId).FirstOrDefault();
            context.IsActive = user != null;
        }
    }
}
