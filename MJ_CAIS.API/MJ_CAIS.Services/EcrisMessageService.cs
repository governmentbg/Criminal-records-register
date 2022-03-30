using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.EcrisMessage;
using MJ_CAIS.DTO.Fbbc;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
    public class EcrisMessageService : BaseAsyncService<EcrisMessageDTO, EcrisMessageDTO, EcrisMessageGridDTO, EEcrisMessage, string, CaisDbContext>, IEcrisMessageService
    {
        private const string EcrisFbbcMessageType = FbbcConstants.MessageType.CodeECRIS;
        private readonly IEcrisMessageRepository _ecrisMessageRepository;
        private readonly IBulletinRepository _bulletinRepository;
        private readonly IFbbcRepository _fbcRepository;
        private readonly INomenclatureDetailRepository _nomenclatureDetailRepository;

        public EcrisMessageService(IMapper mapper, 
            IEcrisMessageRepository ecrisMessageRepository,
            IBulletinRepository bulletinRepository,
            IFbbcRepository fbbcRepository,
            INomenclatureDetailRepository nomenclatureDetailRepository) : base(mapper, ecrisMessageRepository)
        {
            _ecrisMessageRepository = ecrisMessageRepository;
            _bulletinRepository = bulletinRepository;
            _fbcRepository = fbbcRepository;
            _nomenclatureDetailRepository = nomenclatureDetailRepository;
        }

        public virtual async Task<IgPageResult<EcrisMessageGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<EcrisMessageGridDTO> aQueryOptions, string statusId)
        {
            var entityQuery = this.GetSelectAllQueriable().Where(x => x.EcrisMsgStatus == statusId);
            var baseQuery = entityQuery.ProjectTo<EcrisMessageGridDTO>(mapperConfiguration);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<EcrisMessageGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public async Task<IQueryable<BulletinGridDTO>> GetEcrisBulletinsByIdAsync(string ecrisMessageId)
        {
            var ecrisMessage = await this.SelectAsync(ecrisMessageId);
            if (ecrisMessage == null)
            {
                return new List<BulletinGridDTO>().AsQueryable();
            }

            var result = _bulletinRepository.SelectAllAsync()
                .Where(x => x.Egn == ecrisMessage.Identifier)
                .ProjectTo<BulletinGridDTO>(mapperConfiguration);

            return result;
        }

        public async Task<IQueryable<FbbcGridDTO>> GetEcrisFbbcsByIdAsync(string ecrisMessageId)
        {
            var ecrisMessage = await this.SelectAsync(ecrisMessageId);
            if (ecrisMessage == null)
            {
                return new List<FbbcGridDTO>().AsQueryable();
            }

            var docType = _nomenclatureDetailRepository.GetAllFbbcDocTypes()
                .FirstOrDefault(x => x.Code == EcrisFbbcMessageType);

            var result = _fbcRepository.SelectAllAsync()
                .Where(x => x.DocTypeId == docType.Id && x.Egn == ecrisMessage.Identifier)
                .ProjectTo<FbbcGridDTO>(mapperConfiguration);

            return result;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
