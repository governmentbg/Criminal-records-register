﻿using System;
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

namespace TechnoLogica.RegiX.IdentityServer.AdminAppCredentials
{
    public class AdminProfileService : IProfileClientService
    {
        public virtual string ClientId => "cais-angular";

        protected CaisDbContext CaisDbContext { get; set; }

        public AdminProfileService(
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
            var res = 
            (from u in CaisDbContext.GUsers
            where u.Egn == username
            select new UserInfo()
            {
                Name = $"{u.Firstname} {u.Surname} {u.Familyname}",
                SubjectId = u.Id,
                Active = u.Active,
                Username = u.Egn
            }).FirstOrDefault();
            return res;
        }

        public Task<UserRegistrationResult> RegisterUser(string scheme, string name, string userName, string email, string password, Dictionary<string, string> additionalAttributes)
        {
            throw new NotImplementedException();
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userID = (context.Subject.Identity as ClaimsIdentity).Claims.FirstOrDefault(c => c.Type == "sub").Value;
            var user =
                CaisDbContext.GUsers
                .AsNoTracking()
                .Include(u => u.GUserRoles)
                .Where(u => u.Id == userID)
                .Select(u => new
                {
                    u.Firstname,
                    u.Surname,
                    u.Familyname,
                    u.CsAuthorityId,
                    u.Position,
                    u.Egn,
                    Roles = u.GUserRoles.Select( r => r.Role.Code).Distinct()
                })
                .FirstOrDefault();
            if (user != null)
            {
                context.IssuedClaims.Add(new Claim("FullName", $"{user.Firstname} {user.Surname} {user.Familyname}"));
                if (!string.IsNullOrEmpty(user.CsAuthorityId))
                {
                    context.IssuedClaims.Add(new Claim("AuthorityId", user.CsAuthorityId));
                }
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, "normal"));
                foreach ( var role in user.Roles)
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
    public class LocalAdminProfileService : AdminProfileService
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