using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Common;
using MJ_CAIS.DTO.ExternalServicesHost;
using MJ_CAIS.DTO.Person;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonDTO, PPerson>()
                .ForMember(d => d.BirthPlaceOther, opt => opt.MapFrom(src => src.BirthPlace.ForeignCountryAddress))
                .ForMember(d => d.BirthCountryId, opt => opt.MapFrom(src => src.BirthPlace.Country.Id))
                .ForMember(d => d.BirthCityId, opt => opt.MapFrom(src => src.BirthPlace.CityId));

            CreateMap<PPerson, PersonDTO>()
               .ForPath(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForPath(d => d.BirthPlace.ForeignCountryAddress, opt => opt.MapFrom(src => src.BirthPlaceOther))
                .ForPath(d => d.BirthPlace.Country.Id, opt => opt.MapFrom(src => src.BirthCountryId))
                .ForPath(d => d.BirthPlace.Country.DisplayName, opt => opt.MapFrom(src => src.BirthCountry.Name))
                .ForPath(d => d.BirthPlace.CityId, opt => opt.MapFrom(src => src.BirthCityId))
                .ForPath(d => d.BirthPlace.CityDisplayName, opt => opt.MapFrom(src => src.BirthCity.Name))
                .ForPath(d => d.BirthPlace.MunicipalityId, opt => opt.MapFrom(src => src.BirthCity != null ? src.BirthCity.MunicipalityId : null))
                .ForPath(d => d.BirthPlace.MunicipalityDisplayName, opt => opt.MapFrom(src => src.BirthCity.Municipality.Name))
                .ForPath(d => d.BirthPlace.DistrictId, opt => opt.MapFrom(src => src.BirthCity != null && src.BirthCity.Municipality != null ? src.BirthCity.Municipality.DistrictId : null))
                .ForPath(d => d.BirthPlace.DistrictDisplayName, opt => opt.MapFrom(src => src.BirthCity.Municipality.District.Name))
                .ForPath(d => d.BirthPlace.MunicipalityId, opt => opt.MapFrom(src => src.BirthCity != null ? src.BirthCity.MunicipalityId : null))
                .ForPath(d => d.Nationalities.SelectedForeignKeys, opt => opt.MapFrom(src => src.PPersonCitizenships.Select(x => x.CountryId)))
                .ForPath(d => d.NationalitiesNames, opt => opt.MapFrom(src => src.PPersonCitizenships.Select(x => x.Country.Name)));

            CreateMap<PPerson, PPersonH>();
            CreateMap<PPersonId, PPersonIdsH>();
            CreateMap<PPerson, PPerson>();
            CreateMap<PPersonCitizenship, PPersonHCitizenship>();

            CreateMap<BBulletin, PersonDTO>()
                .ForPath(d => d.BirthPlace.ForeignCountryAddress, opt => opt.MapFrom(src => src.BirthPlaceOther))
                .ForPath(d => d.BirthPlace.Country.Id, opt => opt.MapFrom(src => src.BirthCountryId))
                .ForPath(d => d.BirthPlace.CityId, opt => opt.MapFrom(src => src.BirthCityId))
                .ForPath(d => d.Nationalities, opt => opt.MapFrom(src => new MultipleChooseDTO
                {
                    SelectedForeignKeys = src.BPersNationalities.Select(x => x.CountryId)
                }));

            CreateMap<List<PPerson>, PersonIdentifierSearchResponseType>()
                .ConvertUsing(src =>
                    new PersonIdentifierSearchResponseType()
                    {
                        ReportResult =
                          src.Select(p => new CriminalRecordsPersonDataType()
                          {
                              AFISNumber = p.PPersonIds.Where(pid => pid.PidType.Code == "AFIS").Select(pid => pid.Pid).FirstOrDefault(),
                              Sex = Convert.ToInt32(p.Sex),
                              NamesBg = new PersonNameType()
                              {
                                  FirstName = p.Firstname,
                                  SurName = p.Surname,
                                  FamilyName = p.Familyname,
                                  FullName = p.Fullname,
                              },
                              NamesEn = new PersonNameType()
                              {
                                  FirstName = p.FirstnameLat,
                                  SurName = p.SurnameLat,
                                  FamilyName = p.FamilynameLat,
                                  FullName = p.FullnameLat
                              },
                              BirthDate = (p.BirthDate != null) ? new DateType()
                              {
                                  Date = p.BirthDate.Value,
                                  DatePrecision = p.BirthDatePrec != null ? Enum.Parse<DatePrecisionEnum>(p.BirthDatePrec) : DatePrecisionEnum.YMD,
                                  DatePrecisionSpecified = (p.BirthDatePrec != null)
                              } : null,
                              BirthPlace = (p.BirthCity != null || p.BirthCountry != null) ? new PlaceType()
                              {
                                  City = (p.BirthCity != null) ? new CityType()
                                  {
                                      CityName = p.BirthCity.Name,
                                      EKATTECode = p.BirthCity.EkatteCode
                                  } : null,
                                  Country = (p.BirthCountry != null) ? new CountryType()
                                  {
                                      CountryName = p.BirthCountry.Name,
                                      CountryISONumber = p.BirthCountry.Iso31662Number,
                                      CountryISOAlpha3 = p.BirthCountry.Iso31662Code
                                  } : null,
                                  Descr = p.BirthPlaceOther //???
                              } : null,
                              IdentityNumber = new PersonIdentityNumberType()
                              {
                                  EGN = p.PPersonIds.Where(pid => pid.PidType.Code == "EGN").Select(pid => pid.Pid).FirstOrDefault(),
                                  LN = p.PPersonIds.Where(pid => pid.PidType.Code == "LNCH").Select(pid => pid.Pid).FirstOrDefault(),
                                  LNCh = p.PPersonIds.Where(pid => pid.PidType.Code == "LN").Select(pid => pid.Pid).FirstOrDefault(),
                                  SUID = p.PPersonIds.Where(pid => pid.PidType.Code == "SYS").Select(pid => pid.Pid).FirstOrDefault()
                              }
                          }).ToArray()
                    }
                );
        }
    }
}
