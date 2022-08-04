using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services
{
    public class GSystemParameterService : IGSystemParameterService
    {
        private readonly CaisDbContext _dbContext;
        public GSystemParameterService(CaisDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<string?> GetValueString(string code)
        {
            var parameter = await GetParameter(code);
            return parameter?.ValueString;
        }
        public async Task<decimal?> GetValueNumber(string code)
        {
            var parameter = await GetParameter(code);
            return parameter?.ValueNumber;
        }
        public async Task<bool?> GetValueBool(string code)
        {
            var parameter = await GetParameter(code);
            return parameter?.ValueBool;
        }
        public async Task<DateTime?> GetValueDate(string code)
        {
            var parameter = await GetParameter(code);
            return parameter?.ValueDate;
        }

        protected Task<GSystemParameter?> GetParameter(string code)
        {
            return _dbContext.Set<GSystemParameter>()
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Code == code);
        }
    }
}
