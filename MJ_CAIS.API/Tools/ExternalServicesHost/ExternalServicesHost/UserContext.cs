using MJ_CAIS.DataAccess;

namespace MJ_CAIS.ExternalServicesHost
{
    internal class UserContext : IUserContext
    {
        public string? UserId { get; set; }

        public string UserName { get; set; }

        public string? CsAuthorityId => null;

        public string[] Role => new string[] { "GlobalAdmin" };

        public bool IsGlobalAdmin => true;

        public bool IsAdmin => true;
    }
}
