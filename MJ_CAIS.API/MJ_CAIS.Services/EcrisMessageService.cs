using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DTO.EcrisMessage;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System.Collections.Generic;

namespace MJ_CAIS.Services
{
    public class EcrisMessageService : BaseAsyncService<EcrisMessageDTO, EcrisMessageDTO, EcrisMessageGridDTO, EEcrisMessage, string, CaisDbContext>, IEcrisMessageService
    {
        private readonly IEcrisMessageRepository _ecrisMessageRepository;

        public EcrisMessageService(IMapper mapper, IEcrisMessageRepository ecrisMessageRepository)
            : base(mapper, ecrisMessageRepository)
        {
            _ecrisMessageRepository = ecrisMessageRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
