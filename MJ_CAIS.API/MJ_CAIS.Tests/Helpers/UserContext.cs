using MJ_CAIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Tests.Helpers
{
    internal class UserContext : IUserContext
    {
        public string? UserId { get; set; }

        public string UserName { get; set; }

        public string? CsAuthorityId => "660";

        public string[] Role => new string[] { "GlobalAdmin" };

        public bool IsGlobalAdmin => true;

        public bool IsAdmin => true;
    }
}
