using AutoMapper;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisInbox;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;
using System.Text;
using System.Xml;

namespace MJ_CAIS.Services
{
    public class EcrisInboxService : BaseAsyncService<EcrisInboxDTO, EcrisInboxDTO, EcrisInboxGridDTO, EEcrisInbox, string, CaisDbContext>, IEcrisInboxService
    {
        private readonly IEcrisInboxRepository _ecrisInboxRepository;

        public EcrisInboxService(IMapper mapper, IEcrisInboxRepository ecrisInboxRepository)
            : base(mapper, ecrisInboxRepository)
        {
            _ecrisInboxRepository = ecrisInboxRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public virtual async Task<IgPageResult<EcrisInboxGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<EcrisInboxGridDTO> aQueryOptions, string? statusId)
        {
            var entityQuery = _ecrisInboxRepository.SelectAllWithStatusData();
            if (!string.IsNullOrEmpty(statusId))
            {
                entityQuery = entityQuery.Where(x => x.Status == statusId);
            }

            var resultQuery = await this.ApplyOData(entityQuery, aQueryOptions);
            var pageResult = new IgPageResult<EcrisInboxGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, entityQuery, resultQuery);
            return pageResult;
        }

        public async Task<byte[]> GetXmlContentAsync(string aId, bool traits)
        {
            var msg = await _ecrisInboxRepository.SingleOrDefaultAsync<EEcrisInbox>(x => x.Id == aId);
            if (msg == null)
                throw new BusinessLogicException(string.Format(EcrisResources.msgEcrisInboxMsgNotExist, aId));

            var xml = traits ? msg.XmlMessageTraits : msg.XmlMessage;

            return Encoding.Default.GetBytes(xml);
        }

        public override async Task<EcrisInboxDTO> SelectAsync(string aId)
        {
            return await _ecrisInboxRepository.SelectWithStatusDataAsync(aId);
        }
    }
}
