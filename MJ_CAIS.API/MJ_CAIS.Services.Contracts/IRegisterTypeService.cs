using MJ_CAIS.DTO.RegisterType;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Services.Contracts
{
    public interface IRegisterTypeService : IBaseAsyncService<RegisterTypeDTO, RegisterTypeDTO, RegisterTypeGridDTO, DRegisterType, string>
    {
        Task<string> GetRegisterNumberForCertificateWebInternal(string authorityID);
        Task<string> GetRegisterNumberForCertificateWeb(string authorityID);
        Task<string>  GetRegisterNumberForCertificateOnDesk(string authorityID);

        Task<string> GetRegisterNumberForApplicationWebInternal(string authorityID);

        Task<string> GetRegisterNumberForApplicationWeb(string authorityID);

        Task<string> GetRegisterNumberForApplicationOnDesk(string authorityID);

        Task<string> GetRegisterNumberForBulletin(string authorityID);
        Task<string> GetRegisterNumberForBulletin78a(string authorityID);
        Task<string>  GetRegisterNumberForBulletinUndefined(string authorityID);

        Task<string> GetRegisterNumberForBulletin(string authorityID, string bulletinType);
        Task<string> GetRegisterNumberForReport(string authorityID);
    }
}
