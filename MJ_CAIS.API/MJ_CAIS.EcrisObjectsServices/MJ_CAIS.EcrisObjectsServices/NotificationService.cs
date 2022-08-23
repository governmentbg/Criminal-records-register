using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisService;
using MJ_CAIS.EcrisObjectsServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.EcrisObjectsServices
{
    public class NotificationService : INotificationService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<NotificationService> _logger;
        private const string NOT_DEFINED = "NOT DEFINED";
        public NotificationService(CaisDbContext dbContext, ILogger<NotificationService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task CreateNotificationFromBulletin(BBulletin bulletin, string joinSeparator = " ", bool recreate = false, List<string> ecrisMsgIds = null)
        {
            if (recreate && (ecrisMsgIds == null || ecrisMsgIds?.Count == 0 || ecrisMsgIds?.Where(x=>!string.IsNullOrEmpty(x))?.Count() == 0))
            {
                return;
            }
            string? bgCode = (await _dbContext.GCountries.AsNoTracking().FirstOrDefaultAsync(c => c.Iso3166Alpha2 == "BG"))?.EcrisTechnId;
            NotificationMessageType msg = new NotificationMessageType();

            msg.NotificationMessageConviction = await ServiceHelper.GetConvictionFromBuletin(bulletin, bgCode, _dbContext);

           await  LoadCommonDataFromBulletin(msg, bulletin);

            //дали може да се вземе от някъде?!
            //msg.NotificationMessageOtherAffectedConviction = new ConvictionToConvictionsRelationshipType();
            //msg.NotificationMessageOtherAffectedConviction.SourceConviction = "";
            //msg.NotificationMessageOtherAffectedConviction.DestinationConviction = new StructuredConvictionReferenceType[1];
            //msg.NotificationMessageOtherAffectedConviction.DestinationConviction[0].Item =?

            //дали може да се вземе от някъде?!
            //msg.NotificationMessageUpdatedConvictionReference = new UpdateConvictionReferenceType();
            //msg.NotificationMessageUpdatedConvictionReference.Item = ?

            if (recreate)
            {
                var ecrisMsgIdsNotNull = ecrisMsgIds.Where(x => !string.IsNullOrEmpty(x)).ToList();
                var contentXML = XmlUtils.SerializeToXml(msg);
                var doc = await _dbContext.DDocContents.AsNoTracking().Where(c => c.MimeType == "application/xml"&&
                c.DDocuments.Any(x=> x.EcrisMsgId!=null && ecrisMsgIdsNotNull.Contains(x.EcrisMsgId))).ToListAsync();
                doc.ForEach(x => {x.MimeType = "application/xml";
                    x.Content = Encoding.UTF8.GetBytes(contentXML);
                    x.Bytes = x.Content.Length; 
                });

                var emsg = await _dbContext.EEcrisMessages.AsNoTracking().Where(x => ecrisMsgIdsNotNull.Contains(x.Id)).ToListAsync();
                emsg.ForEach(x => x.EcrisMsgStatus = ECRISConstants.EcrisMessageStatuses.ForSending);
                _dbContext.UpdateRange(doc);
                _dbContext.UpdateRange(emsg);
            }
            else
            {

                await ServiceHelper.AddMessageToDBContextAsync(msg, bulletin.EcrisConvictionId, bulletin.Id, joinSeparator, _dbContext, "");

            }

            await _dbContext.SaveChangesAsync();

        }
        public async Task CreateNotificationFromBulletin(string bulletinID, string joinSeparator = " ", bool recreate = false, List<string> ecrisMsgIds = null)
        {
            //todo: дали да проверявам за статус, националност и някакви други условия?!
            var buletin = await  _dbContext.BBulletins
                                .Include(b=>b.BOffences)
                                .Include(b=>b.BSanctions)
                                .Include(b=>b.BDecisions)
                                .Include(b => b.BPersNationalities)
                                .Include(b=>b.BulletinAuthority)
                                .Include(b=>b.CsAuthority)
                                .Include(b=>b.BBullPersAliases)
                                .Include(b=>b.BirthCountry)
                                .Include(b => b.BirthCity)
                                .Include(b=>b.CaseAuth)
                                .Include(b=>b.DecidingAuth)             
                                .Include(b=>b.IdDocCategory)                              
                                .FirstOrDefaultAsync(b => b.Id == bulletinID);
            if(buletin==null)
            {
                throw new Exception($"Bulletin with ID {bulletinID} does not exist.");
            }

            await  CreateNotificationFromBulletin(buletin, joinSeparator,recreate, ecrisMsgIds);
        }

        private async Task LoadCommonDataFromBulletin(NotificationMessageType msg, BBulletin bulletin)
        {
            msg.MessageType = EcrisMessageType.NOT;
            msg.MessageTypeSpecified = true;

            msg.MessageSendingMemberStateSpecified = true;
            msg.MessageSendingMemberState = MemberStateCodeType.BG;
            var countriesIds = bulletin.BPersNationalities.Select(n => n.CountryId);
            var notBGNacionality = await _dbContext.GCountries.AsNoTracking().Where(c => countriesIds.Contains(c.Id) && c.Iso3166Alpha2 != "BG").ToListAsync();
               // bulletin.BPersNationalities.Where(nacionality => nacionality.Country != null && nacionality.Country.Iso3166Alpha2 != "BG");
            List<MemberStateCodeType> notBGNacionalityInEU = new List<MemberStateCodeType>();
            foreach (var nacionality in notBGNacionality)
            {
                object? res;
                if (Enum.TryParse(typeof(MemberStateCodeType), nacionality.Iso3166Alpha2?.ToUpper(), out res))
                {
                    notBGNacionalityInEU.Add((MemberStateCodeType)res);
                }

            }
            if (notBGNacionalityInEU.Count() > 0)
            {
                msg.MessageReceivingMemberState = new MemberStateCodeType[notBGNacionalityInEU.Count()];
                for (int i = 0; i < notBGNacionalityInEU.Count(); i++)
                {
                    msg.MessageReceivingMemberState[i] = notBGNacionalityInEU[i];

                }
            }
            else
            {
                throw new Exception("No receiving country in EU.");
            }

            msg.MessagePerson = new PersonType();
            //todo: add adress from somewhere
            //msg.MessagePerson.PersonAddress;

            //todo: да се добавят ли от групата?!
            //msg.MessagePerson.PersonAlias;
            msg.MessagePerson.PersonBirthPlace = new PlaceType();
            if (bulletin.BirthCity != null)
            {
                if (!string.IsNullOrEmpty(bulletin.BirthCity.NameEn))
                {
                    msg.MessagePerson.PersonBirthPlace.PlaceTownName = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation[2];
                    msg.MessagePerson.PersonBirthPlace.PlaceTownName[0] = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation();
                    msg.MessagePerson.PersonBirthPlace.PlaceTownName[1] = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation();
                    msg.MessagePerson.PersonBirthPlace.PlaceTownName[1].Value = bulletin.BirthCity.NameEn;
                    msg.MessagePerson.PersonBirthPlace.PlaceTownName[1].languageCode = ECRISConstants.LanguageCodes.En;
                }
                else
                {
                    msg.MessagePerson.PersonBirthPlace.PlaceTownName = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation[1];
                    msg.MessagePerson.PersonBirthPlace.PlaceTownName[0] = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation();
                }
                msg.MessagePerson.PersonBirthPlace.PlaceTownName[0].Value = bulletin.BirthCity.Name;
                msg.MessagePerson.PersonBirthPlace.PlaceTownName[0].languageCode = ECRISConstants.LanguageCodes.Bg;
              
                if (!string.IsNullOrEmpty(bulletin.BirthCity.EcrisTechnId))
                {
                    msg.MessagePerson.PersonBirthPlace.PlaceTownReference = new CityExternalReferenceType();
                    msg.MessagePerson.PersonBirthPlace.PlaceTownReference.Value = bulletin.BirthCity.EcrisTechnId;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(bulletin.BirthPlaceOther))
                {
                    msg.MessagePerson.PersonBirthPlace.PlaceTownName = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation[1];
                    msg.MessagePerson.PersonBirthPlace.PlaceTownName[0] = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation();
                    msg.MessagePerson.PersonBirthPlace.PlaceTownName[0].Value = bulletin.BirthPlaceOther;
                    msg.MessagePerson.PersonBirthPlace.PlaceTownName[0].languageCode = ECRISConstants.LanguageCodes.Bg;
                }
                else
                {
                    msg.MessagePerson.PersonBirthPlace.PlaceTownName = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation[1];
                    msg.MessagePerson.PersonBirthPlace.PlaceTownName[0] = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation();
                    msg.MessagePerson.PersonBirthPlace.PlaceTownName[0].Value = NOT_DEFINED;
                    msg.MessagePerson.PersonBirthPlace.PlaceTownName[0].languageCode = ECRISConstants.LanguageCodes.Bg;

                }
            }
            if (!string.IsNullOrEmpty(bulletin.BirthCountry?.EcrisTechnId))
            {
                msg.MessagePerson.PersonBirthPlace.PlaceCountryReference = new CountryExternalReferenceType();
                msg.MessagePerson.PersonBirthPlace.PlaceCountryReference.Value = bulletin.BirthCountry.EcrisTechnId;
            }


            if (bulletin.BirthDate.HasValue)
            {
                msg.MessagePerson.PersonBirthDate = ServiceHelper.GetDateTypeFromDateAndPrecission(bulletin.BirthDate.Value, bulletin.BirthDatePrecision);
            }



            msg.MessagePerson.PersonFatherForename = ServiceHelper.GetNameTextType(new List<string?>() { bulletin.FatherFirstname }, new List<string>())?.ToArray();          
            msg.MessagePerson.PersonFatherSecondSurname = ServiceHelper.GetNameTextType(new List<string?>() { bulletin.FatherFamilyname }, new List<string>())?.ToArray();      
            msg.MessagePerson.PersonFatherSurname = ServiceHelper.GetNameTextType(new List<string?>() { bulletin.FatherSurname }, new List<string>())?.ToArray();
         
            

            msg.MessagePerson.PersonIdentificationDocument = new IdentificationDocumentType[1];
            msg.MessagePerson.PersonIdentificationDocument[0] = new IdentificationDocumentType();
            msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentNumber = bulletin.IdDocNumber;
            if (bulletin.IdDocIssuingDate.HasValue)
            {
                msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentIssuingDate = ServiceHelper.GetDateTypeFromDateAndPrecission(bulletin.IdDocIssuingDate.Value, bulletin.IdDocIssuingDatePrec);
            }


            if (bulletin.IdDocValidDate.HasValue)
            {
                msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentValidUntil = ServiceHelper.GetDateTypeFromDateAndPrecission(bulletin.IdDocValidDate.Value, bulletin.IdDocValidDatePrec);
            }
  
            if (!string.IsNullOrEmpty(bulletin.IdDocTypeDescr))
            {
                msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentType1 = new MultilingualTextType50CharsMultilingualTextLinguisticRepresentation[1];
                msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentType1[0] = new MultilingualTextType50CharsMultilingualTextLinguisticRepresentation();
                msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentType1[0].Value = bulletin.IdDocTypeDescr;
                msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentType1[0].languageCode = ECRISConstants.LanguageCodes.Bg;
            }
            else
            {
                if (!string.IsNullOrEmpty(bulletin.IdDocCategory?.EcrisTechnId))
                {
                    msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentType1 = new MultilingualTextType50CharsMultilingualTextLinguisticRepresentation[1];
                    msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentType1[0] = new MultilingualTextType50CharsMultilingualTextLinguisticRepresentation();
                    msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentType1[0].Value = bulletin.IdDocCategory?.EcrisTechnId;
                    msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentType1[0].languageCode = ECRISConstants.LanguageCodes.Bg;
                }
            }
            if (!string.IsNullOrEmpty(bulletin.IdDocCategory?.EcrisTechnId))
            {
                msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentCategoryReference = new IdentificationDocumentCategoryExternalReferenceType();
                msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentCategoryReference.Value = bulletin.IdDocCategory.EcrisTechnId;
     
            }
            if (!string.IsNullOrEmpty(bulletin.IdDocIssuingAuthority))
            {
                msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentIssuingAuthority = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[1];
                msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentIssuingAuthority[0] = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation();
                msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentIssuingAuthority[0].Value = bulletin.IdDocIssuingAuthority;
                msg.MessagePerson.PersonIdentificationDocument[0].IdentificationDocumentIssuingAuthority[0].languageCode = ECRISConstants.LanguageCodes.Bg;
            }


            //egn, lnch или лн?!
            msg.MessagePerson.PersonIdentityNumber = new RestrictedStringType50Chars();
            msg.MessagePerson.PersonIdentityNumber.Value = string.Join(';', bulletin.Ln, bulletin.Lnch, bulletin.Egn);

            msg.MessagePerson.PersonMotherForename = ServiceHelper.GetNameTextType(new List<string?>() { bulletin.MotherFirstname }, new List<string>())?.ToArray();
            msg.MessagePerson.PersonMotherSecondSurname = ServiceHelper.GetNameTextType(new List<string?>() { bulletin.MotherFamilyname }, new List<string>())?.ToArray();
            msg.MessagePerson.PersonMotherSurname = ServiceHelper.GetNameTextType(new List<string?>() { bulletin.MotherSurname }, new List<string>())?.ToArray();





            msg.MessagePerson.PersonName = new PersonNameType();
            msg.MessagePerson.PersonName.Forename = ServiceHelper.GetNameTextType(new List<string?>() { bulletin.Firstname }, 
                                                            new List<string>() { ECRISConstants.LanguageCodes.Bg })?.ToArray();


            msg.MessagePerson.PersonName.SecondSurname = ServiceHelper.GetNameTextType(new List<string?>() { bulletin.Familyname},
                                                            new List<string>() { ECRISConstants.LanguageCodes.Bg})?.ToArray();

            msg.MessagePerson.PersonName.Surname = ServiceHelper.GetNameTextType(new List<string?>() { bulletin.Surname},
                                                                new List<string>() { ECRISConstants.LanguageCodes.Bg })?.ToArray();


            msg.MessagePerson.PersonName.FullName = ServiceHelper.GetFullNameTextType(new List<string?>() { bulletin.Fullname },
                                                    new List<string>() { ECRISConstants.LanguageCodes.Bg})?.ToArray();



            msg.MessagePerson.PersonNationalityReference = bulletin.BPersNationalities.Where(n => n.Country != null && n.Country.Iso3166Alpha2 != null).Select(n => new CountryExternalReferenceType() { Value = n.Country.EcrisTechnId }).ToArray();

            //msg.MessagePerson.PersonRemarks ;
            //1-мъж, 2 - жена
            msg.MessagePerson.PersonSex = (int)bulletin.Sex;
            msg.MessagePerson.PersonSexSpecified = true;


        }


        private async Task<NotificationResponseMessageType> CreateNotificationResponse(NotificationMessageType notification, string notResponseType)
        {


            NotificationResponseMessageType response = new NotificationResponseMessageType();
            response.MessagePerson = notification.MessagePerson;
            response.MessageSendingMemberState = MemberStateCodeType.BG;
            response.MessageSendingMemberStateSpecified = true;

            response.MessageReceivingMemberState = new MemberStateCodeType[1] {
                    notification.MessageSendingMemberState
            };
            response.MessageSendingMemberStateSpecified = true;
            response.MessageType = EcrisMessageType.NRS;
            response.MessageTypeSpecified = true;
            response.MessageResponseTo = new RestrictedIdentifiableMessageType()
            {
                MessageEcrisIdentifier = notification.MessageEcrisIdentifier,
                MessageIdentifier = notification.MessageIdentifier
            };

            //response.RequestResponseMessageOtherMemberState = new MemberStateCodeType[1] {
            //        notification.MessageSendingMemberState
            //    };
            response.NotificationResponseMessageNotificationResponseTypeReference = new NotificationResponseTypeExternalReferenceType()
            {
                Value = notResponseType// for successful - "NRT-00-00"

            };

            return response;
        }
         
        public async Task CreateNotificationResponseInContext(NotificationMessageType notification, string notResponseType, string msgId)
        {
            var notificationResponce = await CreateNotificationResponse(notification, notResponseType);
            await ServiceHelper.AddMessageToDBContextAsync(notificationResponce, "", "", ",", _dbContext, msgId);

            var ecrisMsg = await _dbContext.EEcrisMessages.FirstAsync(x=>x.Id == msgId);
            ecrisMsg.EcrisMsgStatus = ECRISConstants.EcrisMessageStatuses.ReplyCreated;
            _dbContext.Update(ecrisMsg);
        }
        public async Task CreateNotificationResponseInContext(string notificationEcrisMsgID, string notResponseType)
        {
           

            var content = await _dbContext.DDocuments.AsNoTracking().Where(dd => dd.DocContent.MimeType == "application/xml"
            && dd.EcrisMsgId == notificationEcrisMsgID
            && dd.DocType.Code==EcrisMessageType.NOT.ToString()).Select(d => d.DocContent.Content).FirstOrDefaultAsync();

            if (content == null)
            {
                throw new Exception("Не съществува такъв документ или документа не е нотификация.");
            }
            NotificationMessageType notification = XmlUtils.DeserializeXml<AbstractMessageType>(Encoding.UTF8.GetString(content)) as NotificationMessageType;

            await CreateNotificationResponseInContext(notification, notResponseType, notificationEcrisMsgID);

            await _dbContext.SaveChangesAsync();  
        }

    }
}
