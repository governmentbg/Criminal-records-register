using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DTO.RegisterType;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System.Collections.Generic;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;

namespace MJ_CAIS.Services
{
    public class RegisterTypeService : BaseAsyncService<RegisterTypeDTO, RegisterTypeDTO, RegisterTypeGridDTO, DRegisterType, string, CaisDbContext>, IRegisterTypeService
    {
        private readonly IRegisterTypeRepository _registerTypeRepository;

        public RegisterTypeService(IMapper mapper, IRegisterTypeRepository registerTypeRepository)
            : base(mapper, registerTypeRepository)
        {
            _registerTypeRepository = registerTypeRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        private async Task<string> GetRegisterNumber(string authorityID, string registerCode)
        {   
            var paramDate = new OracleParameter("P_DATE", OracleDbType.Date, DateTime.UtcNow.Date, System.Data.ParameterDirection.Input);
            var paramAuthority = new OracleParameter("P_CS_AUTH_ID", OracleDbType.Varchar2, authorityID, System.Data.ParameterDirection.Input);
            var paramRegisterCode = new OracleParameter("P_REGISTER_CODE", OracleDbType.Varchar2,registerCode, System.Data.ParameterDirection.Input);
    
            //todo: как да махна размера?!
            var paramResult = new OracleParameter("P_OUT_REG_NUMBER", OracleDbType.NVarchar2,100);
            paramResult.Direction = System.Data.ParameterDirection.Output;
            

            await dbContext.Database.ExecuteSqlRawAsync("BEGIN GET_REGISTRATION_NUMBER(:P_DATE, :P_CS_AUTH_ID, :P_REGISTER_CODE, :P_OUT_REG_NUMBER); END;", 
                                    paramDate, paramAuthority, paramRegisterCode, paramResult);
            
            return paramResult.Value.ToString();

        }

        public async Task<string> GetRegisterNumberForApplicationOnDesk(string authorityID)
        {
            return await GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.ApplicationOnDesk);

        }

        public async Task<string> GetRegisterNumberForApplicationWeb(string authorityID)
        {
            return await GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.ApplicationWeb);

        }
        public async Task<string> GetRegisterNumberForApplicationWebInternal(string authorityID)
        {
            return await GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.ApplicationWebInternal);

        }

        public async Task<string> GetRegisterNumberForCertificateOnDesk(string authorityID)
        {
            return await GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.CertificateOnDesk);

        }
        public async Task<string> GetRegisterNumberForCertificateWeb(string authorityID)
        {
            return await GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.CertificateWeb);

        }
        public async Task<string> GetRegisterNumberForCertificateWebInternal(string authorityID)
        {
            return await GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.CertificateWebInternal);

        }

        public async Task<string> GetRegisterNumberForBulletin(string authorityID)
        {
            return await GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.Bulletin);

        }
        public async Task<string> GetRegisterNumberForBulletin78a(string authorityID)
        {
            return await GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.Bulletin78a);

        }

        public async Task<string> GetRegisterNumberForBulletinUndefined(string authorityID)
        {
            return await GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.BulletinUndefined);

        }
    }
}
