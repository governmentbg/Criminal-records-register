using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisMessage;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Services
{
    public class GraoPersonService : BaseAsyncService<GraoPersonDTO, GraoPersonDTO, GraoPersonGridDTO, GraoPerson, string, CaisDbContext>, IGraoPersonService
    {
        public GraoPersonService(IMapper mapper,
            IGraoPersonRepository graoPersonRepository
            ) : base(mapper, graoPersonRepository)
        {
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

    }
}
