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
                .ForMember(d => d.Identifier, opt => opt.MapFrom(src =>
                        src.Bulletin.Egn +
                        (!string.IsNullOrEmpty(src.Bulletin.Egn) ? " / " + src.Bulletin.Lnch : src.Bulletin.Lnch) +
                        (!string.IsNullOrEmpty(src.Bulletin.Egn) || !string.IsNullOrEmpty(src.Bulletin.Lnch) ? " / " + 
                        src.Bulletin.Ln : src.Bulletin.Ln)));
        }
    }
}
