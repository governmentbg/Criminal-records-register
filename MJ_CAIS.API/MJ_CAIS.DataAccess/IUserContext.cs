using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.DataAccess
{
    public interface IUserContext
    {
        string? UserId { get; }
        string UserName { get; }
        string? CsAuthorityId { get; }
        string[] Role { get; }
        bool IsGlobalAdmin { get; }
        bool IsAdmin { get; }
    }
}
