using MJ_CAIS.DTO.OffenceCategory;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Services.Contracts
{
    public interface IOffenceCategoryService : IBaseAsyncService<OffenceCategoryDTO, OffenceCategoryDTO, OffenceCategoryGridDTO, BOffenceCategory, string>
    {
    }
}
