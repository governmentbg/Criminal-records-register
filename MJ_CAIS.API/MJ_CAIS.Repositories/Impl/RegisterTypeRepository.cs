using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Oracle.ManagedDataAccess.Client;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class RegisterTypeRepository : BaseAsyncRepository<DRegisterType, CaisDbContext>, IRegisterTypeRepository
    {
        public RegisterTypeRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<string> GetRegisterNumber(string authorityID, string registerCode)
        {
            var paramDate = new OracleParameter("P_DATE", OracleDbType.Date, DateTime.Now.Date, System.Data.ParameterDirection.Input);
            var paramAuthority = new OracleParameter("P_CS_AUTH_ID", OracleDbType.Varchar2, authorityID, System.Data.ParameterDirection.Input);
            var paramRegisterCode = new OracleParameter("P_REGISTER_CODE", OracleDbType.Varchar2, registerCode, System.Data.ParameterDirection.Input);

            //todo: как да махна размера?!
            var paramResult = new OracleParameter("P_OUT_REG_NUMBER", OracleDbType.NVarchar2, 100);
            paramResult.Direction = System.Data.ParameterDirection.Output;


            await _dbContext.Database.ExecuteSqlRawAsync("BEGIN GET_REGISTRATION_NUMBER(:P_DATE, :P_CS_AUTH_ID, :P_REGISTER_CODE, :P_OUT_REG_NUMBER); END;",
                                    paramDate, paramAuthority, paramRegisterCode, paramResult);

            return paramResult.Value.ToString();

        }
    }
}
