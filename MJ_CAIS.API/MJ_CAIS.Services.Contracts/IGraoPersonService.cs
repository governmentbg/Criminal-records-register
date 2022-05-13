using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisMessage;

namespace MJ_CAIS.Services.Contracts
{
    public interface IGraoPersonService : IBaseAsyncService<GraoPersonDTO, GraoPersonDTO, GraoPersonGridDTO, GraoPerson, string>
    {
     
    }
}
