using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess.Entities;
using TechnoLogica.RegiX.GraoNBDAdapter;
using TechnoLogica.RegiX.MVRERChAdapterV2;

namespace MJ_CAIS.ExternalWebServices.DbServices
{
    public class FactoryRegix
    {
        public static EWebRequest CreatePersonWebRequest(string egn,
            bool isAsync,
            string webServiceId,
            string? bulletinId = null,
            string? applicationId = null,
            string? wApplicationId = null,
            string? ecrisMsgId = null)
        {
            var request = new PersonDataRequestType { EGN = egn };
            var requestXml = XmlUtils.SerializeToXml(request);

            var webRequestEntity = CreateWebRequest(isAsync, bulletinId, applicationId, wApplicationId, ecrisMsgId);
            webRequestEntity.RequestXml = requestXml;
            webRequestEntity.WebServiceId = webServiceId;

            return webRequestEntity;
        }

        public static EWebRequest CreateForeignPersonWebRequest(string lnch,
            bool isAsync,
            string webServiceId,
            string? bulletinId = null,
            string? applicationId = null,
            string? wApplicationId = null,
            string? ecrisMsgId = null)
        {
            var request = new ForeignIdentityInfoRequestType { Identifier = lnch };
            var requestXml = XmlUtils.SerializeToXml(request);

            var webRequestEntity = CreateWebRequest(isAsync, bulletinId, applicationId, wApplicationId, ecrisMsgId);
            webRequestEntity.RequestXml = requestXml;
            webRequestEntity.WebServiceId = webServiceId;

            return webRequestEntity;
        }

        private static EWebRequest CreateWebRequest(bool isAsync, 
            string? bulletinId = null, 
            string? applicationId = null,
            string? wApplicationId = null,
            string? ecrisMsgId = null)
        {
            var result = new EWebRequest()
            {
                Id = EWebRequest.GenerateNewId(),
                BulletinId = bulletinId,
                ApplicationId = applicationId,
                WApplicationId = wApplicationId,
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
