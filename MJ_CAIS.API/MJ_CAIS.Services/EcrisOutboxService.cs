using AutoMapper;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisOutbox;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;
using System.Text;

namespace MJ_CAIS.Services
{
    public class EcrisOutboxService : BaseAsyncService<EcrisOutboxDTO, EcrisOutboxDTO, EcrisOutboxGridDTO, EEcrisOutbox, string, CaisDbContext>, IEcrisOutboxService
    {
        private readonly IEcrisOutboxRepository _ecrisOutboxRepository;

        public EcrisOutboxService(IMapper mapper, IEcrisOutboxRepository ecrisOutboxRepository)
            : base(mapper, ecrisOutboxRepository)
        {
            _ecrisOutboxRepository = ecrisOutboxRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;


        public virtual async Task<IgPageResult<EcrisOutboxGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<EcrisOutboxGridDTO> aQueryOptions, string? statusId)
        {
            var entityQuery = _ecrisOutboxRepository.SelectAllWithStatusData();
            if (!string.IsNullOrEmpty(statusId))
            {
                entityQuery = entityQuery.Where(x => x.Status == statusId);
            }

            var resultQuery = await this.ApplyOData(entityQuery, aQueryOptions);
            var pageResult = new IgPageResult<EcrisOutboxGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, entityQuery, resultQuery);
            return pageResult;
        }

        public async Task<byte[]> GetXmlContentAsync(string aId)
        {
            var msg = await _ecrisOutboxRepository.SingleOrDefaultAsync<EEcrisOutbox>(x => x.Id == aId);
            if (msg == null)
                throw new BusinessLogicException(string.Format(EcrisResources.msgExrisInboxMsgNotExist, aId));

            return Encoding.Default.GetBytes(msg.XmlObject);
        }

        public override async Task<EcrisOutboxDTO> SelectAsync(string aId)
        {
            return await _ecrisOutboxRepository.SelectWithStatusDataAsync(aId);
        }
    }
}
