using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisMessage;

namespace MJ_CAIS.Services.Contracts
{
    public interface IEWebRequestsService : IBaseAsyncService<GraoPersonDTO, GraoPersonDTO, GraoPersonGridDTO, EWebRequest, string>
    {
     
    }
}
