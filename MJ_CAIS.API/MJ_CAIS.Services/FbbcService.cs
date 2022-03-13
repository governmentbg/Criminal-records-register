using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DTO.Fbbc;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System.Collections.Generic;

namespace MJ_CAIS.Services
{
    public class FbbcService : BaseAsyncService<FbbcDTO, FbbcDTO, FbbcGridDTO, Fbbc, string, CaisDbContext>, IFbbcService
    {
        private readonly IFbbcRepository _fbbcRepository;

        public FbbcService(IMapper mapper, IFbbcRepository fbbcRepository)
            : base(mapper, fbbcRepository)
        {
            _fbbcRepository = fbbcRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
