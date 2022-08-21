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

        public async Task<UserInfo> FindByUsername(string scheme, string username, List<Claim> externalClaims)
        {
            UserInfo res = null;
            if (!string.IsNullOrEmpty(username))
            {
                var certificateClaim = externalClaims.Where(c => c.Type == EAuthClaims.Certificate).FirstOrDefault();
                if (certificateClaim == null)
                {
                    return res;
                }
                var egn = username.Replace("PNOBG-", "");
                var x509Cert = new X509Certificate2(Convert.FromBase64String(certificateClaim.Value));
                string uicValue = ExtractdUIC(x509Cert);
                var subject = x509Cert.Subject;

                if (uicValue == null)
                {
                    return res;
                }
                res =
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
                 }).FirstOrDefault();
            }
            return res;
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
                    adminId = (from a in CaisDbContext.GExtAdministrations
                             join uic in CaisDbContext.GExtAdministrationUics on a.Id equals uic.ExtAdmId
                             select a.Id).FirstOrDefault();

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
                    RegCertSubject = (adminId == null) ? certSubject : null
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
                context.IssuedClaims.Add(new Claim("Position", user.Position));
                context.IssuedClaims.Add(new Claim("AdministrationName", user.AdministrationName));
                context.IssuedClaims.Add(new Claim("Email", user.Email));
                //if (!string.IsNullOrEmpty(user.Role))
                //{
                //    foreach (var role in user.Role.Split(","))
                //    {
                //        context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, role));
                //    }
                //}
                if (idpClaim == "EAuthV2" ||
                    idpClaim == "MockHandler")
                {
                    context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, "ECertificates"));
                }
                if (idpClaim == "idsrv")
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
            throw new NotImplementedException();
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
