using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Common;
using MJ_CAIS.DTO.Nomenclature;
using MJ_CAIS.DTO.NomenclatureDetail;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<IBaseNomenclature, BaseNomenclatureDTO>();

            CreateMap<BBulletinStatus, BaseNomenclatureDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Code));

            CreateMap<FbbcDocType, BaseNomenclatureDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Code));

            CreateMap<FbbcSanctType, BaseNomenclatureDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Code));

            CreateMap<NIntternalReqType, BaseNomenclatureDTO>()
               .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Code));

            CreateMap<APurpose, PurposeDTO>();

            CreateMap<APaymentMethod, BaseNomenclatureDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Code));

            CreateMap<APaymentMethod, PaymentMethodDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Code));

            CreateMap<ASrvcResRcptMeth, BaseNomenclatureDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Code));

            CreateMap<GCountry, CountryDTO>();

            CreateMap<GCountry, LookupDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.DisplayName, opt => opt.MapFrom(src => src.Name));

            CreateMap<LookupDTO, GCountry>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<PPersonIdType, BaseNomenclatureDTO>();
            CreateMap<GDecidingAuthority, BaseNomenclatureDTO>();

            CreateMap<GCountry, BaseNomenclatureDTO>();

            // todo: remove this, use grid with pagination
            CreateMap<GUser, BaseNomenclatureDTO>()
               .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Firstname + " " + src.Familyname));

            CreateMap<EEcrisNomenclature, BaseNomenclatureDTO>()
                .ForMember(d => d.Code, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.NameBg))
                .ForMember(d => d.NameEn, opt => opt.MapFrom(src => src.NameEn));

            CreateMap<AApplicationStatus, NomenclatureTypeDTO>()
              .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Code))
              .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(d => d.Type, opt => opt.MapFrom(src => src.StatusType));
        }
    }
}
