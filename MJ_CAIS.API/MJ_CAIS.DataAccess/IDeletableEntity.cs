namespace MJ_CAIS.DataAccess
{
    public interface IDeletableEntity : IBaseIdEntity
    {
        bool IsDeleted { get; set; }
    }
}
