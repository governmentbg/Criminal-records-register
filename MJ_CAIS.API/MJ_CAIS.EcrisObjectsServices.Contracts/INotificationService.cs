namespace MJ_CAIS.EcrisObjectsServices.Contracts
{
    public interface INotificationService
    {
        Task CreateNotificationFromBulletin(string bulletinID, string joinSeparator = " ");
    }
}
