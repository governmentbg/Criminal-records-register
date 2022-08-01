using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using TechnoLogica.Authentication.Common;
using Microsoft.Extensions.Configuration;
using MJ_CAIS.IdentityServer.CAISAppCredentials;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using IdentityModel;
using Serilog.Core;
using Serilog;

namespace TechnoLogica.RegiX.IdentityServer.AdminAppCredentials
{
    public class CAISAppProfileService : IProfileClientService
    {
        public virtual string ClientId => "cais-angular";

        protected CaisDbContext CaisDbContext { get; set; }

        public CAISAppProfileService(
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
                    (from u in CaisDbContext.GUsers
                     where u.Egn == egn
                     select new UserInfo()
                     {
                         Name = $"{u.Firstname} {u.Surname} {u.Familyname}",
                         SubjectId = u.Id,
                         Active = u.Active,
                         Username = u.Egn
                     }).FirstOrDefault();
            }
            return res;
        }

        public async Task<UserRegistrationResult> RegisterUser(string scheme, string name, string userName, string email, string password, Dictionary<string, string> additionalAttributes)
        {
            return new UserRegistrationResult() { Succeeded = false, Errors =  new List<UserRegistrationError>() { new UserRegistrationError() { Description = "User registration not allowed", Code="UserRegistrationNotAllowed" } } };
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            Log.Logger.Information(context.Caller);
            var userID = (context.Subject.Identity as ClaimsIdentity).Claims.FirstOrDefault(c => c.Type == "sub").Value;
            var userInfo =

                (from u in CaisDbContext.GUsers
                 join ur in CaisDbContext.GUserRoles on u.Id equals ur.UserId
                 join r in CaisDbContext.GRoles on ur.RoleId equals r.Id
                 where u.Id == userID
                 select new
                 {
                     u.Firstname,
                     u.Surname,
                     u.Familyname,
                     u.CsAuthorityId,
                     u.Position,
                     u.Egn,
                     RoleCode = r.Code
                 }).ToList();

            var user = userInfo.FirstOrDefault();
            if (user != null)
            {
                context.IssuedClaims.Add(new Claim("Name", $"{user.Firstname} {user.Familyname}"));
                if (!string.IsNullOrEmpty(user.CsAuthorityId))
                {
                    context.IssuedClaims.Add(new Claim("CsAuthorityId", user.CsAuthorityId));
                }
                foreach ( var role in userInfo.Select( u => u.RoleCode))
                { 
                    context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, role));
                }
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

            var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;
            var user = CaisDbContext.GUsers.Where(u => u.Id == subjectId).FirstOrDefault();
            context.IsActive = user != null;
        }
    }

    /// <summary>
    /// For Test purposes
    /// </summary>
    public class LocalAdminProfileService : CAISAppProfileService
    {
        public override string ClientId => "cais-angular-local";

        public LocalAdminProfileService(
            CaisDbContext daisDbContext, 
            IConfiguration configuration) : base(
                daisDbContext, 
                configuration)
        {
        }
    }
}
