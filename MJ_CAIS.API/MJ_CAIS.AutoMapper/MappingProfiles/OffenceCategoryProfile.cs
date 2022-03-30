using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.OffenceCategory;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class OffenceCategoryProfile : Profile
    {
        public OffenceCategoryProfile()
        {
            CreateMap<BOffenceCategory, OffenceCategoryGridDTO>();
        }
    }
}