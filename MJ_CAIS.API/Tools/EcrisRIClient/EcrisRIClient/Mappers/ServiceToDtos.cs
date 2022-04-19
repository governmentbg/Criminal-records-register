using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

using MJ_CAIS.Common.XmlData;

namespace EcrisRIClient.Mappers
{
    public class ServiceToDtos : Profile
    {
        public ServiceToDtos()
        {
            CreateMap<MJ_CAIS.DTO.EcrisService.QueryType, EcrisRIClient.EcrisService.QueryType>();
            CreateMap<MJ_CAIS.DTO.EcrisService.QueryTypeQueryParameters, EcrisRIClient.EcrisService.QueryTypeQueryParameters>();

            CreateMap<MJ_CAIS.DTO.EcrisService.BirthDateQueryParameter, EcrisRIClient.EcrisService.BirthDateQueryParameter>();
            CreateMap<MJ_CAIS.DTO.EcrisService.CountryExternalReferenceType, EcrisRIClient.EcrisService.CountryExternalReferenceType>()
                .ReverseMap();
            CreateMap<MJ_CAIS.DTO.EcrisService.QueryTypeQueryParametersPersonTownOfBirthQueryParameter, EcrisRIClient.EcrisService.QueryTypeQueryParametersPersonTownOfBirthQueryParameter>();
            CreateMap<MJ_CAIS.DTO.EcrisService.QueryTypeQueryParametersMemberStateQueryParameter, EcrisRIClient.EcrisService.QueryTypeQueryParametersMemberStateQueryParameter>();
            CreateMap<MJ_CAIS.DTO.EcrisService.DateStrictRangeQueryParameter, EcrisRIClient.EcrisService.DateStrictRangeQueryParameter>();
            CreateMap<MJ_CAIS.DTO.EcrisService.MessageDateQueryParameterType, EcrisRIClient.EcrisService.MessageDateQueryParameterType>();
            CreateMap<MJ_CAIS.DTO.EcrisService.YesNoUnknownStringEnumerationType, EcrisRIClient.EcrisService.YesNoUnknownStringEnumerationType>()
                .ReverseMap();
            CreateMap<MJ_CAIS.DTO.EcrisService.RequestPurposeExternalReferenceType, EcrisRIClient.EcrisService.RequestPurposeExternalReferenceType>()
                .ReverseMap();
            CreateMap<MJ_CAIS.DTO.EcrisService.EcrisMessageTypeOrAlias, EcrisRIClient.EcrisService.EcrisMessageTypeOrAlias>()
                .AfterMap((dto, serv) => serv.Item = Enum.Parse(typeof(EcrisRIClient.EcrisService.EcrisMessageTypeOrAliasMessageType),
                ((MJ_CAIS.DTO.EcrisService.EcrisMessageTypeOrAliasMessageType)dto.Item).ToString()));
            CreateMap<MJ_CAIS.DTO.EcrisService.DateRange, EcrisRIClient.EcrisService.DateRange>()
                .ReverseMap();
            CreateMap<MJ_CAIS.DTO.EcrisService.DateType, EcrisRIClient.EcrisService.DateType>()
                .ReverseMap();
            CreateMap<MJ_CAIS.DTO.EcrisService.MonthDayType, EcrisRIClient.EcrisService.MonthDayType>()
                .ReverseMap();
            CreateMap<MJ_CAIS.DTO.EcrisService.StringType, EcrisRIClient.EcrisService.StringType>()
                .ReverseMap();
            CreateMap<MJ_CAIS.DTO.EcrisService.CityExternalReferenceType, EcrisRIClient.EcrisService.CityExternalReferenceType>()
                .ReverseMap();
            CreateMap<MJ_CAIS.DTO.EcrisService.NonBindingExternalReferenceType, EcrisRIClient.EcrisService.NonBindingExternalReferenceType>()
                .ReverseMap();
            CreateMap<MJ_CAIS.DTO.EcrisService.MemberStateCodeType, EcrisRIClient.EcrisService.MemberStateCodeType>()
                .ReverseMap();
            CreateMap<MJ_CAIS.DTO.EcrisService.QueryTypeQueryParametersMemberStateQueryParameterDestination, EcrisRIClient.EcrisService.QueryTypeQueryParametersMemberStateQueryParameterDestination>();
            CreateMap<MJ_CAIS.DTO.EcrisService.StrictDateRange, EcrisRIClient.EcrisService.StrictDateRange>()
                .ReverseMap();
            CreateMap<MJ_CAIS.DTO.EcrisService.StrictDateType, EcrisRIClient.EcrisService.StrictDateType>()
                .ReverseMap();
            CreateMap<MJ_CAIS.DTO.EcrisService.MessageDateTypeEnumeration, EcrisRIClient.EcrisService.MessageDateTypeEnumeration>()
                .ReverseMap();

            CreateMap<EcrisRIClient.EcrisService.SearchWSOutputDataType, MJ_CAIS.DTO.EcrisService.SearchWSOutputDataType>();

            CreateMap<EcrisRIClient.EcrisService.MessageShortViewType, MJ_CAIS.DTO.EcrisService.MessageShortViewType>()
                .AfterMap((serv, dto) => dto.SerializedXMLFromService = XmlUtils.SerializeToXml(serv));
            CreateMap<EcrisRIClient.EcrisService.ReadMessageWSOutputDataType, MJ_CAIS.DTO.EcrisService.ReadMessageWSOutputDataType>()
                .ForMember(dest => dest.EcrisRiMessage, opt => opt.MapFrom(src => AbstractTypeResolver<EcrisRIClient.EcrisService.AbstractMessageType,
                 MJ_CAIS.DTO.EcrisService.AbstractMessageType>(src.EcrisRiMessage)))//src=>MessageTypeResolver(src.EcrisRiMessage)))
                .AfterMap((serv, dto) => dto.SerializedXMLFromService = XmlUtils.SerializeToXml(serv));
            CreateMap<EcrisRIClient.EcrisService.FunctionalErrorsListType, MJ_CAIS.DTO.EcrisService.FunctionalErrorsListType>();
            CreateMap<EcrisRIClient.EcrisService.FunctionalErrorsListTypeFunctionalError, MJ_CAIS.DTO.EcrisService.FunctionalErrorsListTypeFunctionalError>();
            CreateMap<EcrisRIClient.EcrisService.PersonType, MJ_CAIS.DTO.EcrisService.PersonType>()
               .ForMember(dest => dest.PersonBirthPlace, opt => opt.MapFrom(src => AbstractTypeResolver<EcrisRIClient.EcrisService.AbstractPlaceType,
               MJ_CAIS.DTO.EcrisService.AbstractPlaceType>(src.PersonBirthPlace)));//BirthPlaceCustomResolver(src.PersonBirthPlace)));
            CreateMap<EcrisRIClient.EcrisService.RequestingAuthorityType, MJ_CAIS.DTO.EcrisService.RequestingAuthorityType>();
            CreateMap<EcrisRIClient.EcrisService.RequestingAuthorityTypeExternalReferenceType, MJ_CAIS.DTO.EcrisService.RequestingAuthorityTypeExternalReferenceType>();

            CreateMap<EcrisRIClient.EcrisService.ContactPersonType, MJ_CAIS.DTO.EcrisService.ContactPersonType>();
            CreateMap<EcrisRIClient.EcrisService.ConvictionType, MJ_CAIS.DTO.EcrisService.ConvictionType>()
                .ForMember(dest => dest.ConvictionRelationship, opt => opt.MapFrom(src =>

                     src.ConvictionRelationship.Select(r =>
                     AbstractTypeResolver<EcrisRIClient.EcrisService.AbstractRelationshipType,
                        MJ_CAIS.DTO.EcrisService.AbstractRelationshipType>(r)).ToArray()


            ));
            CreateMap<EcrisRIClient.EcrisService.DecidingAuthorityType, MJ_CAIS.DTO.EcrisService.DecidingAuthorityType>();

            CreateMap<EcrisRIClient.EcrisService.DecisionType, MJ_CAIS.DTO.EcrisService.DecisionType>();

            CreateMap<EcrisRIClient.EcrisService.OffenceType, MJ_CAIS.DTO.EcrisService.OffenceType>();
            CreateMap<EcrisRIClient.EcrisService.SanctionType, MJ_CAIS.DTO.EcrisService.SanctionType>();
            CreateMap<EcrisRIClient.EcrisService.AbstractRelationshipType, MJ_CAIS.DTO.EcrisService.AbstractRelationshipType>();
            CreateMap<EcrisRIClient.EcrisService.DecisionChangeTypeExternalReferenceType, MJ_CAIS.DTO.EcrisService.DecisionChangeTypeExternalReferenceType>();
            CreateMap<EcrisRIClient.EcrisService.SanctionNatureExternalReferenceType, MJ_CAIS.DTO.EcrisService.SanctionNatureExternalReferenceType>();
            CreateMap<EcrisRIClient.EcrisService.OffenceExternalReferenceType, MJ_CAIS.DTO.EcrisService.OffenceExternalReferenceType>();
            CreateMap<EcrisRIClient.EcrisService.OffenceLevelOfCompletionExternalReferenceType, MJ_CAIS.DTO.EcrisService.OffenceLevelOfCompletionExternalReferenceType>();
            CreateMap<EcrisRIClient.EcrisService.OffenceLevelOfParticipationExternalReferenceType, MJ_CAIS.DTO.EcrisService.OffenceLevelOfParticipationExternalReferenceType>();
            CreateMap<EcrisRIClient.EcrisService.SanctionExternalReferenceType, MJ_CAIS.DTO.EcrisService.SanctionExternalReferenceType>();
            CreateMap<EcrisRIClient.EcrisService.SanctionAlternativeTypeExternalReferenceType, MJ_CAIS.DTO.EcrisService.SanctionAlternativeTypeExternalReferenceType>();
            CreateMap<EcrisRIClient.EcrisService.SanctionSentencedPeriodType, MJ_CAIS.DTO.EcrisService.SanctionSentencedPeriodType>();
            CreateMap<EcrisRIClient.EcrisService.SanctionPeriodType, MJ_CAIS.DTO.EcrisService.SanctionPeriodType>();
            CreateMap<EcrisRIClient.EcrisService.RestrictedPositiveIntegerWithErrorsType, MJ_CAIS.DTO.EcrisService.RestrictedPositiveIntegerWithErrorsType>();
            CreateMap<EcrisRIClient.EcrisService.PositiveDecimalType, MJ_CAIS.DTO.EcrisService.PositiveDecimalType>();
            CreateMap<EcrisRIClient.EcrisService.CurrencyExternalReferenceType, MJ_CAIS.DTO.EcrisService.CurrencyExternalReferenceType>();
            CreateMap<EcrisRIClient.EcrisService.SanctionSuspensionType, MJ_CAIS.DTO.EcrisService.SanctionSuspensionType>();
            CreateMap<EcrisRIClient.EcrisService.SanctionInterruptionType, MJ_CAIS.DTO.EcrisService.SanctionInterruptionType>();
            CreateMap<EcrisRIClient.EcrisService.DecisionToSanctionsRelationshipType, MJ_CAIS.DTO.EcrisService.DecisionToSanctionsRelationshipType>();
            CreateMap<EcrisRIClient.EcrisService.DecisionToOffencesRelationshipType, MJ_CAIS.DTO.EcrisService.DecisionToOffencesRelationshipType>();
            CreateMap<EcrisRIClient.EcrisService.SanctionToSanctionsRelationshipType, MJ_CAIS.DTO.EcrisService.SanctionToSanctionsRelationshipType>();
            CreateMap<EcrisRIClient.EcrisService.SanctionToOffencesRelationshipType, MJ_CAIS.DTO.EcrisService.SanctionToOffencesRelationshipType>();
            CreateMap<EcrisRIClient.EcrisService.ConvictionToConvictionsRelationshipType, MJ_CAIS.DTO.EcrisService.ConvictionToConvictionsRelationshipType>();
            CreateMap<EcrisRIClient.EcrisService.UpdateConvictionReferenceType, MJ_CAIS.DTO.EcrisService.UpdateConvictionReferenceType>();




            CreateMap<EcrisRIClient.EcrisService.MessageShortViewPersonType, MJ_CAIS.DTO.EcrisService.MessageShortViewPersonType>()
                .ForMember(dest => dest.PersonBirthPlace, opt => opt.MapFrom(src => AbstractTypeResolver<EcrisRIClient.EcrisService.AbstractPlaceType,
                 MJ_CAIS.DTO.EcrisService.AbstractPlaceType>(src.PersonBirthPlace)));//src => BirthPlaceCustomResolver(src.PersonBirthPlace)));

            CreateMap<EcrisRIClient.EcrisService.IdentifiableFolderType, MJ_CAIS.DTO.EcrisService.IdentifiableFolderType>();

            CreateMap<EcrisRIClient.EcrisService.PersonNameType, MJ_CAIS.DTO.EcrisService.PersonNameType>();
            CreateMap<EcrisRIClient.EcrisService.NameTextType, MJ_CAIS.DTO.EcrisService.NameTextType>();
            CreateMap<EcrisRIClient.EcrisService.FullNameTextType, MJ_CAIS.DTO.EcrisService.FullNameTextType>();

            CreateMap<EcrisRIClient.EcrisService.CountryExternalReferenceType, MJ_CAIS.DTO.EcrisService.CountryExternalReferenceType>();
            CreateMap<EcrisRIClient.EcrisService.CountrySubdivisionExternalReferenceType, MJ_CAIS.DTO.EcrisService.CountrySubdivisionExternalReferenceType>();

            CreateMap<EcrisRIClient.EcrisService.RestrictedStringType50Chars, MJ_CAIS.DTO.EcrisService.RestrictedStringType50Chars>();
            CreateMap<EcrisRIClient.EcrisService.IdentificationDocumentType, MJ_CAIS.DTO.EcrisService.IdentificationDocumentType>();
            CreateMap<EcrisRIClient.EcrisService.PersonAddressType, MJ_CAIS.DTO.EcrisService.PersonAddressType>();
            CreateMap<EcrisRIClient.EcrisService.PersonAliasType, MJ_CAIS.DTO.EcrisService.PersonAliasType>();
            CreateMap<EcrisRIClient.EcrisService.UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation, MJ_CAIS.DTO.EcrisService.UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation>();
            CreateMap<EcrisRIClient.EcrisService.PlaceType, MJ_CAIS.DTO.EcrisService.PlaceType>();
            CreateMap<EcrisRIClient.EcrisService.MultilingualTextType400CharsMultilingualTextLinguisticRepresentation, MJ_CAIS.DTO.EcrisService.MultilingualTextType400CharsMultilingualTextLinguisticRepresentation>();
            CreateMap<EcrisRIClient.EcrisService.MultilingualTextType50CharsMultilingualTextLinguisticRepresentation, MJ_CAIS.DTO.EcrisService.MultilingualTextType50CharsMultilingualTextLinguisticRepresentation>();
            CreateMap<EcrisRIClient.EcrisService.CountrySubdivisionExternalReferenceType, MJ_CAIS.DTO.EcrisService.CountrySubdivisionExternalReferenceType>();

            CreateMap<EcrisRIClient.EcrisService.MultilingualTextType200CharsMultilingualTextLinguisticRepresentation, MJ_CAIS.DTO.EcrisService.MultilingualTextType200CharsMultilingualTextLinguisticRepresentation>();
            CreateMap<EcrisRIClient.EcrisService.BusinessStringType, MJ_CAIS.DTO.EcrisService.BusinessStringType>();
            CreateMap<EcrisRIClient.EcrisService.IdentificationDocumentCategoryExternalReferenceType, MJ_CAIS.DTO.EcrisService.IdentificationDocumentCategoryExternalReferenceType>();
            CreateMap<EcrisRIClient.EcrisService.DateType, MJ_CAIS.DTO.EcrisService.DateType>();
            CreateMap<EcrisRIClient.EcrisService.AliasBirthPlaceType, MJ_CAIS.DTO.EcrisService.AliasBirthPlaceType>();
            CreateMap<EcrisRIClient.EcrisService.StrictDateTimeType, MJ_CAIS.DTO.EcrisService.StrictDateTimeType>();
            CreateMap<EcrisRIClient.EcrisService.RequestMessageType, MJ_CAIS.DTO.EcrisService.RequestMessageType>();
            CreateMap<EcrisRIClient.EcrisService.NotificationMessageType, MJ_CAIS.DTO.EcrisService.NotificationMessageType>();



        }


        private T2 AbstractTypeResolver<T1, T2>(T1 ecrisRiMessage)
        {
            if (ecrisRiMessage == null) return default(T2);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ServiceToDtos>();
            });

            IMapper mapper = new Mapper(config);

            var a = Assembly.GetAssembly(typeof(T2));

            var newType = a.GetType(typeof(T2).Namespace + "." + ecrisRiMessage.GetType().Name);

            var result = Activator.CreateInstance(newType);
            result = mapper.Map(ecrisRiMessage, result, ecrisRiMessage.GetType(), newType);


            return (T2)result;
        }

    }
}
