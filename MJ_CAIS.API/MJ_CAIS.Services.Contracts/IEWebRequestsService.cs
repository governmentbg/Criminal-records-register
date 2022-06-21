using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EWebRequest;

namespace MJ_CAIS.Services.Contracts
{
    public interface IEWebRequestsService : IBaseAsyncService<EWebRequestDTO, EWebRequestDTO, EWebRequestGridDTO, EWebRequest, string>
    {
        Task<byte[]> GetXmlTransformationById(string aId);
    }
}
