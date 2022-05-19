namespace MJ_CAIS.Services.Contracts
{
    public interface IRehabilitationService
    {
        Task ApplyRehabilitation(string bulletinId, string personId);
    }
}
