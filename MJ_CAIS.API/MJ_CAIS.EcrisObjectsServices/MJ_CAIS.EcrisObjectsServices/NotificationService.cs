using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.EcrisObjectsServices
{
    public class NotificationService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<NotificationService> _logger;
        public NotificationService(CaisDbContext dbContext, ILogger<NotificationService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task CreateNotificationFromBulletin(BBulletin bulletin, string joinSeparator = " ")
        {
            string? bgCode = (await _dbContext.GCountries.FirstOrDefaultAsync(c => c.Iso3166Alpha2 == "BG"))?.EcrisTechnId;
            NotificationMessageType msg = new NotificationMessageType();

            msg.NotificationMessageConviction = CommonService.GetConvictionFromBuletin(bulletin, bgCode);

            LoadCommonDataFromBulletin(msg, bulletin);

            //дали може да се вземе от някъде?!
            //msg.NotificationMessageOtherAffectedConviction = new ConvictionToConvictionsRelationshipType();
            //msg.NotificationMessageOtherAffectedConviction.SourceConviction = "";
            //msg.NotificationMessageOtherAffectedConviction.DestinationConviction = new StructuredConvictionReferenceType[1];
            //msg.NotificationMessageOtherAffectedConviction.DestinationConviction[0].Item =?

            //дали може да се вземе от някъде?!
            //msg.NotificationMessageUpdatedConvictionReference = new UpdateConvictionReferenceType();
            //msg.NotificationMessageUpdatedConvictionReference.Item = ?



            await CommonService.AddMessageToDBContextAsync(msg, bulletin.EcrisConvictionId, joinSeparator,_dbContext);

            await _dbContext.SaveChangesAsync();

        }


        private void LoadCommonDataFromBulletin(NotificationMessageType msg, BBulletin bulletin)
        {
            msg.MessageType = EcrisMessageType.NOT;
            msg.MessageTypeSpecified = true;

            msg.MessageSendingMemberStateSpecified = true;
            msg.MessageSendingMemberState = MemberStateCodeType.BG;
            var notBGNacionality = bulletin.BPersNationalities.Where(nacionality => nacionality.Country != null && nacionality.Country.Iso3166Alpha2 != "BG");
            List<MemberStateCodeType> notBGNacionalityInEU = new List<MemberStateCodeType>();
            foreach (var nacionality in notBGNacionality)
            {
                object? res;
                if (Enum.TryParse(typeof(MemberStateCodeType), nacionality.Country?.Iso3166Alpha2?.ToUpper(), out res))
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
            msg.MessagePerson.PersonBirthPlace = new AliasBirthPlaceType();
            if (bulletin.BirthCity != null)
            {
                msg.MessagePerson.PersonBirthPlace.PlaceTownName = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation[2];
                msg.MessagePerson.PersonBirthPlace.PlaceTownName[0].Value = bulletin.BirthCity.Name;
                msg.MessagePerson.PersonBirthPlace.PlaceTownName[0].languageCode = "BG";
                msg.MessagePerson.PersonBirthPlace.PlaceTownName[1].Value = bulletin.BirthCity.NameEn;
                msg.MessagePerson.PersonBirthPlace.PlaceTownName[1].languageCode = "EN";
                msg.MessagePerson.PersonBirthPlace.PlaceTownReference = new CityExternalReferenceType();
                msg.MessagePerson.PersonBirthPlace.PlaceTownReference.Value = bulletin.BirthCity.EcrisTechnId;
            }
            else
            {
                msg.MessagePerson.PersonBirthPlace.PlaceTownName = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation[1];
                msg.MessagePerson.PersonBirthPlace.PlaceTownName[0].Value = bulletin.BirthPlaceOther;
            }
            msg.MessagePerson.PersonBirthPlace.PlaceCountryReference = new CountryExternalReferenceType();
            msg.MessagePerson.PersonBirthPlace.PlaceCountryReference.Value = bulletin.BirthCountry.EcrisTechnId;


            if (bulletin.BirthDate.HasValue)
            {
                msg.MessagePerson.PersonBirthDate = CommonService.GetDateTypeFromDateAndPrecission(bulletin.BirthDate.Value, bulletin.BirthDatePrecision);
            }



            msg.MessagePerson.PersonFatherForename = new NameTextType[1];
            msg.MessagePerson.PersonFatherForename[0].Value = bulletin.FatherFirstname;
            msg.MessagePerson.PersonFatherSecondSurname = new NameTextType[1];
            msg.MessagePerson.PersonFatherSecondSurname[0].Value = bulletin.FatherFamilyname;
            msg.MessagePerson.PersonFatherSurname = new NameTextType[1];
            msg.MessagePerson.PersonFatherSurname[0].Value = bulletin.FatherSurname;
            //msg.MessagePerson.PersonFormerForename;
            //msg.MessagePerson.PersonFormerSecondSurname;
            //msg.MessagePerson.PersonFormerSurname;

            msg.MessagePerson.PersonIdentificationDocument = new IdentificationDocumentType[1];
            msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentNumber = bulletin.IdDocNumber;
            if (bulletin.IdDocIssuingDate.HasValue)
            {
                msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentIssuingDate = CommonService.GetDateTypeFromDateAndPrecission(bulletin.IdDocIssuingDate.Value, bulletin.IdDocIssuingDatePrec);
            }


            if (bulletin.IdDocValidDate.HasValue)
            {
                msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentValidUntil = CommonService.GetDateTypeFromDateAndPrecission(bulletin.IdDocValidDate.Value, bulletin.IdDocValidDatePrec);
            }
            msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentType1 = new MultilingualTextType50CharsMultilingualTextLinguisticRepresentation[1];
            msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentType1[0].Value = bulletin.IdDocTypeDescr;
            msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentCategoryReference = new IdentificationDocumentCategoryExternalReferenceType();
            msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentCategoryReference.Value = bulletin.IdDocCategory.EcrisTechnId;
            msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentIssuingAuthority = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[1];
            msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentIssuingAuthority[0].Value = bulletin.IdDocIssuingAuthority;


            //egn, lnch или лн?!
            msg.MessagePerson.PersonIdentityNumber = new RestrictedStringType50Chars();
            msg.MessagePerson.PersonIdentityNumber.Value = string.Join(';', bulletin.Ln, bulletin.Lnch, bulletin.Egn);

            msg.MessagePerson.PersonMotherForename = CommonService.GetNameTextType(new List<string?>() { bulletin.MotherFirstname }, new List<string>())?.ToArray();
            msg.MessagePerson.PersonMotherSecondSurname = CommonService.GetNameTextType(new List<string?>() { bulletin.MotherFamilyname }, new List<string>())?.ToArray();
            msg.MessagePerson.PersonMotherSurname = CommonService.GetNameTextType(new List<string?>() { bulletin.MotherSurname }, new List<string>())?.ToArray();





            msg.MessagePerson.PersonName = new PersonNameType();
            msg.MessagePerson.PersonName.Forename = CommonService.GetNameTextType(new List<string?>() { bulletin.Firstname, bulletin.FirstnameLat }, new List<string>() { "BG", "EN" })?.ToArray();


            msg.MessagePerson.PersonName.SecondSurname = CommonService.GetNameTextType(new List<string?>() { bulletin.Familyname, bulletin.FamilynameLat }, new List<string>() { "BG", "EN" })?.ToArray();

            msg.MessagePerson.PersonName.Surname = CommonService.GetNameTextType(new List<string?>() { bulletin.Surname, bulletin.SurnameLat }, new List<string>() { "BG", "EN" })?.ToArray();


            msg.MessagePerson.PersonName.FullName = CommonService.GetFullNameTextType(new List<string?>() { bulletin.Fullname, bulletin.FullnameLat }, new List<string>() { "BG", "EN" })?.ToArray();



            msg.MessagePerson.PersonNationalityReference = bulletin.BPersNationalities.Where(n => n.Country != null && n.Country.Iso3166Alpha2 != null).Select(n => new CountryExternalReferenceType() { Value = n.Country.Iso3166Alpha2 }).ToArray();

            //msg.MessagePerson.PersonRemarks ;
            //1-мъж, 2 - жена
            msg.MessagePerson.PersonSex = (int)bulletin.Sex;
            msg.MessagePerson.PersonSexSpecified = bulletin.Sex == 0;


        }
       



    }
}
