using AutoMapper;
using MJ_CAIS.AutoMapperContainer.Resolvers;
using MJ_CAIS.DTO.AbstractMessageType;
using MJ_CAIS.DTO.EcrisService;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class AbstractMessageTypeProfile : Profile
    {
        public AbstractMessageTypeProfile()
        {
            CreateMap<RequestMessageType, EcrisRequestDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.MessageIdentifier))
                .ForMember(d => d.EcrisId, opt => opt.MapFrom(src => src.MessageEcrisIdentifier))
                //Запитващ орган
                .ForMember(d => d.RequestAuthorityName, opt => opt.MapFrom(src => src.RequestMessageRequestingAuthority.RequestingAuthorityName[0].Value))
                .ForMember(d => d.RequestAuthorityType, opt => opt.MapFrom(src => src.RequestMessageRequestingAuthority.RequestingAuthorityTypeReference.Value))
                .ForMember(d => d.RequestAuthorityCode, opt => opt.MapFrom(src => src.RequestMessageRequestingAuthority.RequestingAuthorityCode.Value))
                //Основни данни за лицето
                .ForMember(d => d.LastName, opt => opt.MapFrom(src => src.MessagePerson.PersonName.Surname[0].Value))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.MessagePerson.PersonName.Forename[0].Value))
                .ForMember(d => d.FullName, opt => opt.MapFrom(src => src.MessagePerson.PersonName.FullName[0].Value))
                .ForMember(d => d.LastNameSecond, opt => opt.MapFrom(src => src.MessagePerson.PersonName.SecondSurname[0].Value))
                .ForMember(d => d.Nationality, opt => opt.MapFrom(src => src.MessagePerson.PersonNationalityReference[0].Value))
                .ForMember(d => d.Birthday, opt => opt.MapFrom(src => AbstractMessageTypeResolver.GetDateType(src.MessagePerson.PersonBirthDate)))
                .ForMember(d => d.CountryPerson, opt => opt.MapFrom(src => src.MessagePerson.PersonBirthPlace.PlaceCountryReference.Value))
                .ForMember(d => d.MunicipalityPerson, opt => opt.MapFrom(src => src.MessagePerson.PersonBirthPlace.PlaceCountrySubdivisionReference.Value))
                .ForMember(d => d.CityPerson, opt => opt.MapFrom(src => src.MessagePerson.PersonBirthPlace.PlaceTownName[0].Value))
                .ForMember(d => d.PersonId, opt => opt.MapFrom(src => src.MessagePerson.PersonIdentityNumber.Value))
                .ForMember(d => d.Sex, opt => opt.MapFrom(src => src.MessagePerson.PersonSex))
                //Предишни имена:
                .ForMember(d => d.FirstNameFormer, opt => opt.MapFrom(src => src.MessagePerson.PersonFormerForename))
                .ForMember(d => d.MiddleNameFormer, opt => opt.MapFrom(src => src.MessagePerson.PersonFormerSecondSurname))
                .ForMember(d => d.LastNameFormer, opt => opt.MapFrom(src => src.MessagePerson.PersonFormerSurname))
                //Информация за адреса
                .ForMember(d => d.Country, opt => opt.MapFrom(src => src.MessagePerson.PersonAddress[0].AddressPlace.PlaceCountryReference.Value))
                .ForMember(d => d.Municipality, opt => opt.MapFrom(src => src.MessagePerson.PersonAddress[0].AddressPlace.PlaceCountrySubdivisionReference.Value))
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.MessagePerson.PersonAddress[0].AddressPlace.PlaceTownName[0].Value))
                .ForMember(d => d.Street, opt => opt.MapFrom(src => src.MessagePerson.PersonAddress[0].AddressStreet[0].Value))
                .ForMember(d => d.PostCode, opt => opt.MapFrom(src => src.MessagePerson.PersonAddress[0].AddressStreet[0].Value))
                .ForMember(d => d.FullAdress, opt => opt.MapFrom(src => src.MessagePerson.PersonAddress[0].AddressFull[0].Value))
                .ForMember(d => d.AdressNumber, opt => opt.MapFrom(src => src.MessagePerson.PersonAddress[0].AddressHouseNumber[0].Value))
                //Цел
                .ForMember(d => d.RequestPurposeCategory, opt => opt.MapFrom(src => src.MessagePerson.PersonAddress[0].AddressHouseNumber[0].Value))
                .ForMember(d => d.RequestPurpose, opt => opt.MapFrom(src => src.RequestMessageRequestPurpose[0].Value))
                .ForMember(d => d.ConcernedPеrsonConsent, opt => opt.MapFrom(src => src.RequestMessageConcernedPersonConsent.Value))
                .ForMember(d => d.MessageUrgency, opt => opt.MapFrom(src => src.RequestMessageUrgency.Value))
                //Обвинение
                .ForMember(d => d.AccusationOffenceCategory, opt => opt.MapFrom(src => src.RequestMessageAccusationOffenceCategoryReference[0].Value))
                .ForMember(d => d.MessageAccusation, opt => opt.MapFrom(src => src.RequestMessageAccusation[0].Value))
                .ForMember(d => d.CaseRefereranceNumber, opt => opt.MapFrom(src => src.RequestMessageCaseReferenceNumber.Value));



        }
    }
}
