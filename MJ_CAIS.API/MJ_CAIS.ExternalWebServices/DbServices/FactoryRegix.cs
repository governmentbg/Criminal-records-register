using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess.Entities;
using System.Text.Json;
using TechnoLogica.RegiX.GraoNBDAdapter;
using TechnoLogica.RegiX.MVRERChAdapterV2;

namespace MJ_CAIS.ExternalWebServices.DbServices
{
    public class FactoryRegix
    {
        public static EWebRequest CreatePersonWebRequest(string egn,
            bool isAsync,
            string createdBy,
            string webServiceId,
           // string? bulletinId = null,
            string? applicationId = null,
            string? wApplicationId = null,
          string? reportApplicationId = null
            )
        {
            var request = new PersonDataRequestType { EGN = egn };
            var requestXml = XmlUtils.SerializeToXml(request);

            var webRequestEntity = CreateWebRequest(isAsync: isAsync, createdBy: createdBy, applicationId: applicationId, wApplicationId: wApplicationId, reportApplicationId: reportApplicationId);
            webRequestEntity.RequestXml = requestXml;
            webRequestEntity.WebServiceId = webServiceId;
            string fieldsDescription = "";
            if (!(string.IsNullOrEmpty(applicationId)))
            { AAppCitizenship p = new AAppCitizenship();
                AAppPersAlias p1 = new AAppPersAlias();
                AApplication a= new AApplication();
                fieldsDescription = JsonSerializer.Serialize(FieldsForPopulationFactory.GetFieldsForPopulationApplicationPersonDataSearch(nameof(p.ApplicationId), nameof(p1.ApplicationId), nameof(a.AAppCitizenships), nameof (a.AAppPersAliases)));
            }
            if (!(string.IsNullOrEmpty(reportApplicationId)))
            {
                ARepCitizenship p = new ARepCitizenship();
                AReportApplication a= new AReportApplication();
                fieldsDescription = JsonSerializer.Serialize(FieldsForPopulationFactory.GetFieldsForPopulationApplicationPersonDataSearch(nameof(p.AReportApplId), null, nameof(a.ARepCitizenships), null));
            }
            if (!(string.IsNullOrEmpty(wApplicationId)))
            {
                WAppCitizenship p = new WAppCitizenship();
                WAppPersAlias p1 = new WAppPersAlias();
                WApplication a= new WApplication();
                fieldsDescription = JsonSerializer.Serialize(FieldsForPopulationFactory.GetFieldsForPopulationApplicationPersonDataSearch(nameof(p.WApplicationId), nameof(p1.WApplicationId), nameof(a.WAppCitizenships), nameof(a.WAppPersAliases)));
            }
            webRequestEntity.EFieldsRequests.First().FieldsDescription = fieldsDescription;

                return webRequestEntity;
        }

        public static EWebRequest CreateForeignPersonWebRequest(string lnch,
            bool isAsync,
            string createdBy,
            string webServiceId,
            //string? bulletinId = null,
            string? applicationId = null,
            string? wApplicationId = null,
            string? reportApplicationId  = null
            //string? ecrisMsgId = null
            )
        {
            var request = new ForeignIdentityInfoRequestType { Identifier = lnch , IdentifierType =  IdentifierType.LNCh};
            var requestXml = XmlUtils.SerializeToXml(request);

            var webRequestEntity = CreateWebRequest(isAsync: isAsync, createdBy: createdBy, applicationId: applicationId, wApplicationId:  wApplicationId, reportApplicationId: reportApplicationId);
            webRequestEntity.RequestXml = requestXml;
            webRequestEntity.WebServiceId = webServiceId;
            string fieldsDescription = "";
            if (!(string.IsNullOrEmpty(applicationId)))
            {
                AAppCitizenship p = new AAppCitizenship();
                AAppPersAlias p1 = new AAppPersAlias();
                AApplication a = new AApplication();
                fieldsDescription = JsonSerializer.Serialize(FieldsForPopulationFactory.GetFieldsForPopulationApplicationForeignPerson(nameof(p.ApplicationId), nameof(p1.ApplicationId), nameof(a.AAppCitizenships), nameof(a.AAppPersAliases)));
            }
            if (!(string.IsNullOrEmpty(reportApplicationId)))
            {
                ARepCitizenship p = new ARepCitizenship();
                AReportApplication a = new AReportApplication();
                fieldsDescription = JsonSerializer.Serialize(FieldsForPopulationFactory.GetFieldsForPopulationApplicationForeignPerson(nameof(p.AReportApplId), null, nameof(a.ARepCitizenships), null));
            }
            if (!(string.IsNullOrEmpty(wApplicationId)))
            {
                WAppCitizenship p = new WAppCitizenship();
                WAppPersAlias p1 = new WAppPersAlias();
                WApplication a = new WApplication();
                fieldsDescription = JsonSerializer.Serialize(FieldsForPopulationFactory.GetFieldsForPopulationApplicationForeignPerson(nameof(p.WApplicationId), nameof(p1.WApplicationId), nameof(a.WAppCitizenships), nameof(a.WAppPersAliases)));
            }
            webRequestEntity.EFieldsRequests.First().FieldsDescription = fieldsDescription;

            return webRequestEntity;
        }

        public static EWebRequest CreatePersonRelationsWebRequest(string egn,
            bool isAsync,
            string createdBy,
            string webServiceId,
            //string? bulletinId = null,
            string? applicationId = null,
            string? wApplicationId = null,
            string? reportApplicationId = null
            )
        {
            var request = new RelationsRequestType() { EGN = egn };
            var requestXml = XmlUtils.SerializeToXml(request);

            var webRequestEntity = CreateWebRequest(isAsync: isAsync,createdBy: createdBy, applicationId: applicationId, wApplicationId: wApplicationId, reportApplicationId: reportApplicationId);
            webRequestEntity.RequestXml = requestXml;
            webRequestEntity.WebServiceId = webServiceId;
            string fieldsDescription = "";
            if (!(string.IsNullOrEmpty(applicationId)))
            {
                fieldsDescription = JsonSerializer.Serialize(FieldsForPopulationFactory.GetFieldsForPopulationApplicationRelationsDataSearch());
            }
            if (!(string.IsNullOrEmpty(reportApplicationId)))
            {
                fieldsDescription = JsonSerializer.Serialize(FieldsForPopulationFactory.GetFieldsForPopulationApplicationRelationsDataSearch());
            }
            if (!(string.IsNullOrEmpty(wApplicationId)))
            {
                fieldsDescription = JsonSerializer.Serialize(FieldsForPopulationFactory.GetFieldsForPopulationApplicationRelationsDataSearch());
            }
            webRequestEntity.EFieldsRequests.First().FieldsDescription = fieldsDescription;

            return webRequestEntity;
        }



        private static EWebRequest CreateWebRequest(bool isAsync, string createdBy,
            //string? bulletinId = null,
            string? applicationId = null,
            string? wApplicationId = null,
            string? reportApplicationId = null
            )
        {
            var result = new EWebRequest()
            {
                Id = EWebRequest.GenerateNewId(),
                //BulletinId = bulletinId,
                ApplicationId = applicationId,
                WApplicationId = wApplicationId,
                ARepApplId = reportApplicationId,
               // EcrisMsgId = ecrisMsgId,
                IsAsync = isAsync,
                Status = WebRequestStatusConstants.Pending,
                Attempts = 0,
                CreatedBy = createdBy,
                EntityState = EntityStateEnum.Added,
            };

            EFieldsRequest  fieldsReq = new EFieldsRequest();
            fieldsReq.Id = EWebRequest.GenerateNewId();
            fieldsReq.EntityState = EntityStateEnum.Added;
            fieldsReq.AApplId = applicationId;
            fieldsReq.WApplId = wApplicationId;
            fieldsReq.ARepApplId= reportApplicationId;
            fieldsReq.EWebReqId = result.Id;

            result.EFieldsRequests = new List<EFieldsRequest>() { fieldsReq };

            return result;
        }
    }
}
