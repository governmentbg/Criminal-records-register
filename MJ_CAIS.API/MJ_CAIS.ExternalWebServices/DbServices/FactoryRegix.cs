using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess.Entities;
using TechnoLogica.RegiX.GraoNBDAdapter;

namespace MJ_CAIS.ExternalWebServices.DbServices
{
    public class FactoryRegix
    {
        public static EWebRequest CreatePersonWebRequest(string egn,
            bool isAsync,
            string webServiceId,
            string? bulletinId = null,
            string? applicationId = null,
            string? ecrisMsgId = null)
        {
            var request = new PersonDataRequestType { EGN = egn };
            var requestXml = XmlUtils.SerializeToXml(request);

            var webRequestEntity = CreateWebRequest(isAsync, bulletinId, applicationId, ecrisMsgId);
            webRequestEntity.RequestXml = requestXml;
            webRequestEntity.WebServiceId = webServiceId;

            return webRequestEntity;
        }

        private static EWebRequest CreateWebRequest(bool isAsync, string? bulletinId = null, string? applicationId = null, string? ecrisMsgId = null)
        {
            var result = new EWebRequest()
            {
                Id = EWebRequest.GenerateNewId(),
                BulletinId = bulletinId,
                ApplicationId = applicationId,
                EcrisMsgId = ecrisMsgId,
                IsAsync = isAsync,
                Status = WebRequestStatusConstants.Pending,
                Attempts = 0,
                EntityState = EntityStateEnum.Added,
            };

            return result;
        }
    }
}
