using MJ_CAIS.DataAccess.Entities;
using System.Security.Claims;
using System.Security.Principal;

namespace MJ_CAIS.WebSetup.Utils
{
    public class CaisIdentity : ClaimsIdentity
    {
        public CaisIdentity(GUser user, string authenticationType, string nameType, string roleType)
            : base(authenticationType, nameType, roleType)
        {
        }

        public CaisIdentity(IIdentity identity) : base(identity)
        {
        }

        public string UserName => FindFirst(DefaultNameClaimType).Value;

        public string UserID => GetClaimValue<string>(ClaimTypes.NameIdentifier);

        public string RoleName => GetClaimValue<string>(DefaultRoleClaimType);

        protected virtual T GetClaimValue<T>(string claimType)
        {
            Claim claim = this.FindFirst(claimType);


            if (claim != null && claim.Value != null && claim.Value != string.Empty)
            {
                return (T)Convert.ChangeType(claim.Value, typeof(T));
            }

            return default(T);
        }
    }
}
