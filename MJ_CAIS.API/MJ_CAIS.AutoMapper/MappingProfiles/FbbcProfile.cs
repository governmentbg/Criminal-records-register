using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.Common;
using MJ_CAIS.DTO.Fbbc;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class FbbcProfile : Profile
    {
        public FbbcProfile()
        {
            CreateMap<Fbbc, FbbcGridDTO>();
            CreateMap<FbbcDTO, Fbbc>()
                .ForMember(d => d.BirthCountryId, opt => opt.MapFrom(src => src.Address.Country.Id))
                .ForMember(d => d.BirthCityId, opt => opt.MapFrom(src => src.Address.CityId))
               /* .ForMember(d => d.CountryId, opt => opt.MapFrom(src => src.Country.Id))*/;
            CreateMap<Fbbc, FbbcDTO>()
                // .ForPath(d => d.Country, opt => opt.MapFrom(src => new LookupDTO
                // {
                //    Id = src.Country.Id,
                //    DisplayName = src.Country.Name
                // }))
                .ForPath(d => d.Address.Country, opt => opt.MapFrom(src => new LookupDTO
                {
                    Id = src.BirthCountry.Id,
                    DisplayName = src.BirthCountry.Name
                }))
                .ForPath(d => d.Address.CityId, opt => opt.MapFrom(src => src.BirthCityId))
                .ForPath(d => d.Address.MunicipalityId, opt => opt.MapFrom(src => src.BirthCity != null ? src.BirthCity.MunicipalityId : null))
                .ForPath(d => d.Address.DistrictId, opt => opt.MapFrom(src => src.BirthCity != null && src.BirthCity.Municipality != null ? src.BirthCity.Municipality.DistrictId : null));
            CreateMap<FbbcDocumentDTO, DDocument>()
             .ForMember(d => d.DocContentId, opt => opt.MapFrom(src => src.DocumentContentId))
             .ForMember(d => d.DocContent, opt => opt.Ignore());
            CreateMap<DDocument, FbbcDocumentDTO>()
                .ForMember(d => d.DocumentContentId, opt => opt.MapFrom(src => src.DocContent.Id))
             .ForMember(d => d.DocumentContent, opt => opt.Ignore());
        }
    }
}
