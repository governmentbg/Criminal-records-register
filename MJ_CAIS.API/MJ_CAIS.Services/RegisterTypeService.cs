using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DTO.RegisterType;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;

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

      

        public async Task<string> GetRegisterNumberForApplicationOnDesk(string authorityID)
        {
            return await _registerTypeRepository.GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.ApplicationOnDesk);
        }

        public async Task<string> GetRegisterNumberForApplicationWeb(string authorityID)
        {
            return await _registerTypeRepository.GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.ApplicationWeb);
        }

        public async Task<string> GetRegisterNumberForApplicationWebExternal(string authorityID)
        {
            return await _registerTypeRepository.GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.ApplicationWebExternal);
        }

        public async Task<string> GetRegisterNumberForCertificateOnDesk(string authorityID)
        {
            return await _registerTypeRepository.GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.CertificateOnDesk);
        }

        public async Task<string> GetRegisterNumberForCertificateWeb(string authorityID)
        {
            return await _registerTypeRepository.GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.CertificateWeb);

        }

        public async Task<string> GetRegisterNumberForCertificateWebExternal(string authorityID)
        {
            return await _registerTypeRepository.GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.CertificateWebExternal);
        }

        public async Task<string> GetRegisterNumberForBulletin(string authorityID)
        {
            return await _registerTypeRepository.GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.Bulletin);

        }
        public async Task<string> GetRegisterNumberForBulletin78a(string authorityID)
        {
            return await _registerTypeRepository.GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.Bulletin78a);

        }

        public async Task<string> GetRegisterNumberForBulletinUndefined(string authorityID)
        {
            return await _registerTypeRepository.GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.BulletinUndefined);

        }
        public async Task<string> GetRegisterNumberForReport(string authorityID)
        {
            return await _registerTypeRepository.GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.ConvictionRequest);

        }
        public async Task<string> GetRegisterNumberForBulletin(string authorityID, string bulletinType)
        {
            if (bulletinType == BulletinConstants.Type.Bulletin78A)
            {
                return await _registerTypeRepository.GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.Bulletin78a);
            }

            if (bulletinType == BulletinConstants.Type.ConvictionBulletin)
            {
                return await _registerTypeRepository.GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.Bulletin);
            }

            return await _registerTypeRepository.GetRegisterNumber(authorityID, RegistrationConstants.RegisterCodes.BulletinUndefined);
        }
    }
}
