using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DTO.Nomenclature;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<IBaseNomenclature, BaseNomenclatureDTO>();
        }
    }
}
