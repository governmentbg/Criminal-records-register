using MJ_CAIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticStepsExecutor
{
    internal class UserContext : IUserContext
    {
        public string? UserId { get; set; }

        public string UserName { get; set; }

        public string? CsAuthorityId => null;

        public string[] Role => new string[] { "GlobalAdmin" } ;

        public bool IsGlobalAdmin => true;

        public bool IsAdmin => true;
    }
}
