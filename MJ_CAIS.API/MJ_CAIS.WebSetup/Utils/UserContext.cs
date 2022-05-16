using IdentityModel;
using MJ_CAIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.WebSetup.Utils
{
    public class UserContext : IUserContext
    {
        public UserContext(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal != null)
            {
                var subjectClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Subject);
                if (subjectClaim != null)
                {
                    UserId = subjectClaim.Value;
                }
                var csAuthorityId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "CsAuthorityId");
                if (csAuthorityId != null)
                {
                    CsAuthorityId = csAuthorityId.Value;
                }
                var roleTypeClaims = claimsPrincipal.Claims.Where(c => c.Type == JwtClaimTypes.Role).Select(c => c.Value);
                if (roleTypeClaims != null)
                {
                    Role = roleTypeClaims.ToArray();
                }
                var userNameClaim = claimsPrincipal.Claims.Where(c => c.Type == "Name").Select(c => c.Value).SingleOrDefault();
                if (userNameClaim != null)
                {
                    UserName = userNameClaim;
                }
            }
        }

        public string? CsAuthorityId { get; private set; }

        public string[] Role { get; private set; }

        public string? UserId { get; private set; }

        public bool IsGlobalAdmin => Role.Contains("GlobalAdmin");

        public bool IsAdmin => Role.Contains("Admin");

        public string UserName { get; private set; }
    }
}
