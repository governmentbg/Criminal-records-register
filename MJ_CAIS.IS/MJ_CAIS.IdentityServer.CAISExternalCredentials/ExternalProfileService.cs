using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using TechnoLogica.Authentication.Common;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using IdentityModel;
using TechnoLogica.Authentication.EAuthV2;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using MJ_CAIS.IdentityServer.CAISExternalCredentials.Entities;

namespace MJ_CAIS.IdentityServer.CAISExternalCredentials
{
    public class ExternalProfileService : IProfileClientService
    {
        public virtual string ClientId => "cais-external";

        protected CaisDbContext CaisDbContext { get; set; }
        private readonly SignInManager<GUsersExt> _signInManager;
        private readonly UserManager<GUsersExt> _userManager;

        public ExternalProfileService(
            SignInManager<GUsersExt> signInManager,
            UserManager<GUsersExt> userManager,
            CaisDbContext caisDbContext)
        {
            CaisDbContext = caisDbContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task SignOutAsync()
        {
            //No implementation needed
        }

        public async Task<bool> ValidateCredentials(string scheme, string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (_userManager.SupportsUserLockout && await _userManager.IsLockedOutAsync(user))
            {
                return false;
            }
            else
            {
                if (await this._userManager.CheckPasswordAsync(user, password))
                {
                    await _userManager.ResetAccessFailedCountAsync(user);
                    return true;
                }
                else
                {
                    if (_userManager.SupportsUserLockout && await _userManager.GetLockoutEnabledAsync(user))
                    {
                        await _userManager.AccessFailedAsync(user);
                    }
                    return false;
                }
            }
        }

        public async Task<bool> ChangePassword(string scheme, string username, string password, string newPassword)
        {
            var isValid = await ValidateCredentials(scheme, username, password);
            if (isValid)
            {
                var user = await _userManager.FindByNameAsync(username);
                var result = await _userManager.ChangePasswordAsync(user, password, newPassword);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    throw new ApplicationException(string.Join(Environment.NewLine, result.Errors.Select(er => er.Description).ToList()));
                }
            }
            else
            {
                return false;
            }
        }

        public Task<bool> SendPasswordResetTokenAsync(string scheme, string baseAddress, string email)
        {
            throw new NotImplementedException();
        }

        public Task<ResetPasswordResult> ResetPasswordAsync(string scheme, string email, string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<UserInfo> FindByUsername(string scheme, string username, List<Claim> externalClaims)
        {
            UserInfo res = null;
            if (!string.IsNullOrEmpty(username))
            {
                if (scheme == "EAuthHandlerV2" ||
                    scheme == "MockHandler")
                {
                    res = await FindUserByCertificate(username, externalClaims);
                }
                else if (scheme == "idsrv")
                {
                    res = await FindUserByUsername(username);
                }
            }
            return res;
        }

        private async Task<UserInfo> FindUserByCertificate(string username, List<Claim> externalClaims)
        {
            var certificateClaim = externalClaims.Where(c => c.Type == EAuthClaims.Certificate).FirstOrDefault();
            if (certificateClaim == null)
            {
                return null;
            }
            var egn = username.Replace("PNOBG-", "");
            var x509Cert = new X509Certificate2(Convert.FromBase64String(certificateClaim.Value));
            string uicValue = ExtractdUIC(x509Cert);
            var subject = x509Cert.Subject;

            if (uicValue == null)
            {
                return null;
            }
            var res = await
            (from u in CaisDbContext.GUsersExts
             join a in CaisDbContext.GExtAdministrations on u.AdministrationId equals a.Id
             join uic in CaisDbContext.GExtAdministrationUics on a.Id equals uic.ExtAdmId
             where u.Egn == egn
                && (uic.Value == uicValue || u.RegCertSubject == subject)
             select new UserInfo()
             {
                 Name = u.Name,
                 SubjectId = u.Id,
                 Active = true, // Allways returns active. Specific roles for inactive users is returned in the profile data
                 Username = u.Egn
             }).FirstOrDefaultAsync();
            return res;
        }

        private async Task<UserInfo> FindUserByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                return new UserInfo()
                {
                    Username = user.UserName,
                    Name = user.Name,
                    Active = user.Active,
                    SubjectId = user.Id.ToString()
                };
            }
            else
            {
                return null;
            }
        }

        private static string ExtractdUIC(X509Certificate2 x509Cert)
        {
            Regex regx = new Regex("(NTRBG-(?<EIK>[0-9]*))");
            var matches = regx.Matches(x509Cert.Subject);
            var uicValue = matches.Where(m => !string.IsNullOrEmpty(m.Groups["EIK"]?.Value)).Select(m => m.Groups["EIK"].Value).FirstOrDefault();
            return uicValue;
        }

        public async Task<UserRegistrationResult> RegisterUser(string scheme, string name, string userName, string email, string password, Dictionary<string, string> additionalAttributes)
        {
            if (scheme == "EAuthHandlerV2" ||
                scheme == "MockHandler")
            {
                if (!string.IsNullOrEmpty(userName) && userName.StartsWith("PNOBG-"))
                {
                    var egn = userName.Replace("PNOBG-", "");
                    additionalAttributes.TryGetValue(EAuthClaims.Certificate, out string certificate);
                    string? certSubject = null;
                    string? adminId = null;
                    if (!string.IsNullOrEmpty(certificate))
                    {
                        var cert = new X509Certificate2(Convert.FromBase64String(certificate));
                        string uicValue = ExtractdUIC(cert);
                        if (uicValue == null)
                        {
                            return new UserRegistrationResult()
                            {
                                Succeeded = false,
                                Errors = new UserRegistrationError[] {
                                new UserRegistrationError() {
                                    Code = "NTRBG_NOT_PRESENT",
                                    Description = "Сертификатът трябва да съдържа информация за администрация!"
                                }
                            }
                            };
                        }
                        certSubject = cert.Subject;
                        adminId = (from uic in CaisDbContext.GExtAdministrationUics
                                   where uic.Value == uicValue
                                   select uic.ExtAdmId).FirstOrDefault();
                    }
                    else
                    {
                        return new UserRegistrationResult()
                        {
                            Succeeded = false,
                            Errors = new UserRegistrationError[] {
                                new UserRegistrationError() {
                                    Code = "CERT_NOT_PRESENT",
                                    Description = "Не е включен сертификат в информацията за регистрация!"
                                }
                            }
                        };
                    }
                    CaisDbContext.GUsersExts.Add(new Entities.GUsersExt()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Egn = egn,
                        Name = name,
                        Email = email,
                        Active = false,
                        AdministrationId = adminId,
                        RegCertSubject = (adminId == null) ? certSubject : null,
                        CreatedOn = DateTime.Now,
                        CreatedBy = "IdentityServer"
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
                            Description = "Поддържат се само сертификати съдържащи ЕГН!"
                        }
                    }
                    };
                }
            }
            else if (scheme == "local")
            {
                var user = new GUsersExt()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                    UserName = userName,
                    Email = email,
                    Active = false,
                    CreatedOn = DateTime.Now,
                    CreatedBy = "IdentityServer"
                };
                var result = await _userManager.CreateAsync(user, password);
                return
                    new UserRegistrationResult()
                    {
                        Succeeded = result.Succeeded
                    };
            }
            else
            {
                return new UserRegistrationResult()
                {
                    Succeeded = false,
                    Errors = new UserRegistrationError[] {
                        new UserRegistrationError() {
                    Code = "NOT_SUPPORTED _SCHEME",
                    Description = "Схемата за автентикация не се поддържа!"
                        }
                    }
                };
            }
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userID = (context.Subject.Identity as ClaimsIdentity).Claims.FirstOrDefault(c => c.Type == "sub").Value;
            var idpClaim = (context.Subject.Identity as ClaimsIdentity).Claims.FirstOrDefault(c => c.Type == "idp").Value;
            var user =
                CaisDbContext.GUsersExts
                .AsNoTracking()
                .Include(e => e.Administration)
                .Where(u => u.Id == userID)
                .Select(u => new
                {
                    u.Position,
                    u.AdministrationId,
                    u.IsAdmin,
                    u.Name,
                    u.Active,
                    u.Egn,
                    u.Email,
                    AdministrationName = u.Administration.Name,
                    Role = u.Administration.Role,
                })
                .FirstOrDefault();
            if (user != null)
            {
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Name, user.Name));
                if (!string.IsNullOrEmpty(user.Position))
                {
                    context.IssuedClaims.Add(new Claim("Position", user.Position));
                }
                if (!string.IsNullOrEmpty(user.AdministrationName))
                {
                    context.IssuedClaims.Add(new Claim("AdministrationName", user.AdministrationName));
                }
                if (!string.IsNullOrEmpty(user.Email))
                {
                    context.IssuedClaims.Add(new Claim("Email", user.Email));
                }
                if (idpClaim == "EAuthHandlerV2" ||
                    idpClaim == "MockHandler")
                {
                    context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, "ECertificates"));
                }
                if (idpClaim == "local")
                {
                    context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, "EReports"));
                }
                if (!string.IsNullOrEmpty(user.AdministrationId))
                {
                    context.IssuedClaims.Add(new Claim("AdministrationId", user.AdministrationId));
                }
                if (user.IsAdmin.HasValue && user.IsAdmin.Value)
                {
                    context.IssuedClaims.Add(new Claim("isAdmin", "true"));
                }
                if (user.Active.HasValue && user.Active.Value)
                {
                    context.IssuedClaims.Add(new Claim("Active", "true"));
                }
                else
                {
                    context.IssuedClaims.Add(new Claim("NotActive", "true"));
                }
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

            var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;
            var user = CaisDbContext.GUsersExts.Where(u => u.Id == subjectId).FirstOrDefault();
            context.IsActive = user != null;
        }
    }
}
