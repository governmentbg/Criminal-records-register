using MJ_CAIS.Common.Enums;

namespace MJ_CAIS.DataAccess
{
    public interface IBaseIdEntity
    {
        string Id { get; set; }
        EntityStateEnum EntityState { get; set; }
        List<string> ModifiedProperties { get; set; }
    }
}
