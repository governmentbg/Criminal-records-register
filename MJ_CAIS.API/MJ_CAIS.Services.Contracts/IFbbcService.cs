using MJ_CAIS.DTO.Fbbc;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Services.Contracts
{
    public interface IFbbcService : IBaseAsyncService<FbbcDTO, FbbcDTO, FbbcGridDTO, Fbbc, string>
    {
    }
}
