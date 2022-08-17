using MJ_CAIS.DTO.EcrisService;

namespace MJ_CAIS.EcrisObjectsServices.Contracts
{
    public interface INotificationService
    {
        Task CreateNotificationFromBulletin(string bulletinID, string joinSeparator = " ", bool recreate = false, List<string> ecrisMsgIds = null);
        Task CreateNotificationResponseInContext(NotificationMessageType notification, string notResponseType, string msgId);
        Task CreateNotificationResponseInContext(string notificationEcrisMsgID, string notResponseType);
     
    }
}
