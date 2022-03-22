using MJ_CAIS.DTO.EcrisMessage;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Services.Contracts
{
    public interface IEcrisMessageService : IBaseAsyncService<EcrisMessageDTO, EcrisMessageDTO, EcrisMessageGridDTO, EEcrisMessage, string>
    {
    }
}
