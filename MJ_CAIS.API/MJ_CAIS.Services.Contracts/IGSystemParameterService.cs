using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services.Contracts
{
    public interface IGSystemParameterService
    {
        Task<string?> GetValueString(string code);
        Task<decimal?> GetValueNumber(string code);
        Task<bool?> GetValueBool(string code);
        Task<DateTime?> GetValueDate(string code);
    }
}
