using Microsoft.AspNetCore.Identity;
using MJ_CAIS.IdentityServer.CAISExternalCredentials.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MJ_CAIS.IdentityServer.CAISExternalCredentials
{
    public class CompatibilityPasswordHasher:PasswordHasher<GUsersExt>
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
