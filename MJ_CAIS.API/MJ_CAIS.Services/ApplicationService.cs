using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System.Collections.Generic;

namespace MJ_CAIS.Services
{
    public class ApplicationService : BaseAsyncService<ApplicationDTO, ApplicationDTO, ApplicationDTO, AApplication, string, CaisDbContext>, IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IMapper mapper, IApplicationRepository applicationRepository)
            : base(mapper, applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
