using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisTcn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class EcrisTcnProfile : Profile
    {
        public EcrisTcnProfile()
        {
            CreateMap<EEcrisTcn, EcrisTcnDTO>();
            CreateMap<EcrisTcnDTO, EEcrisTcn>();
            CreateMap<EEcrisTcn, EcrisTcnGridDTO>()
                .ForMember(d => d.RegistrationNumber, opt => opt.MapFrom(src => src.Bulletin.RegistrationNumber))
                .ForMember(d => d.Identifier, opt => opt.MapFrom(src =>
                        src.Bulletin.Egn +
                        (!string.IsNullOrEmpty(src.Bulletin.Egn) && !string.IsNullOrEmpty(src.Bulletin.Lnch) ? " / " + src.Bulletin.Lnch : src.Bulletin.Lnch) +
                        (!string.IsNullOrEmpty(src.Bulletin.Egn) && !string.IsNullOrEmpty(src.Bulletin.Lnch) && !string.IsNullOrEmpty(src.Bulletin.Ln) ? " / " + src.Bulletin.Ln : src.Bulletin.Ln)))
                .ForMember(d => d.Firstname, opt => opt.MapFrom(src => src.Bulletin.Firstname))
                .ForMember(d => d.Surname, opt => opt.MapFrom(src => src.Bulletin.Surname))
                .ForMember(d => d.Familyname, opt => opt.MapFrom(src => src.Bulletin.Familyname))
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(src => src.Bulletin.BirthDate))
                .ForMember(d => d.BirthPlace, opt => opt.MapFrom(src => src.Bulletin.BirthCountry.Name +
                (!string.IsNullOrEmpty(src.Bulletin.BirthCountry.Name) && !string.IsNullOrEmpty(src.Bulletin.BirthCity.Name) ? ", " + src.Bulletin.BirthCity.Name : src.Bulletin.BirthCity.Name) +
                (!string.IsNullOrEmpty(src.Bulletin.BirthCountry.Name) && !string.IsNullOrEmpty(src.Bulletin.BirthCity.Name) && !string.IsNullOrEmpty(src.Bulletin.BirthPlaceOther) ? ", " + src.Bulletin.BirthPlaceOther : src.Bulletin.BirthPlaceOther)));

        }
    }
}
