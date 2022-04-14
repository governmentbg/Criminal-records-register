using MJ_CAIS.DTO.Home;

namespace MJ_CAIS.Services.Contracts
{
    public interface IHomeService
    {
        Task<ObjectsCountDTO> GetCountAsync();
    }
}