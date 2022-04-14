using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.Shared;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
    public class BulletinService : BaseAsyncService<BulletinDTO, BulletinDTO, BulletinGridDTO, BBulletin, string, CaisDbContext>, IBulletinService
    {
        private readonly IBulletinRepository _bulletinRepository;

        public BulletinService(IMapper mapper, IBulletinRepository bulletinRepository)
            : base(mapper, bulletinRepository)
        {
            _bulletinRepository = bulletinRepository;
        }

        public virtual async Task<IgPageResult<BulletinGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<BulletinGridDTO> aQueryOptions, string statusId)
        {
            var entityQuery = this.GetSelectAllQueriable().Where(x => x.StatusId == statusId);
            var baseQuery = entityQuery.ProjectTo<BulletinGridDTO>(mapperConfiguration);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<BulletinGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        public override async Task<string> InsertAsync(BulletinDTO aInDto)
            => await UpdateBulletinAsync(aInDto, true);

        public override async Task UpdateAsync(string aId, BulletinDTO aInDto)
            => await UpdateBulletinAsync(aInDto, false);

        public async Task ChangeStatusAsync(string aInDto, string statusId)
        {
            var bulletin = await dbContext.BBulletins
               .FirstOrDefaultAsync(x => x.Id == aInDto);

            if (bulletin == null)
                throw new ArgumentException($"Bulletin with id: {aInDto} is missing");

            bulletin.StatusId = statusId;
            await dbContext.SaveChangesAsync();
        }

        public async Task<IQueryable<OffenceDTO>> GetOffencesByBulletinIdAsync(string aId)
        {
            var offances = await _bulletinRepository.SelectAllOffencesAsync();
            var filteredOffances = offances.Where(x => x.BulletinId == aId);
            return filteredOffances.ProjectTo<OffenceDTO>(mapperConfiguration);
        }

        public async Task<IQueryable<SanctionDTO>> GetSanctionsByBulletinIdAsync(string aId)
        {
            var sanctions = await _bulletinRepository.SelectAllSanctionsAsync();
            var filteredSanctions = sanctions.Where(x => x.BulletinId == aId);         
            return filteredSanctions.ProjectTo<SanctionDTO>(mapperConfiguration);
        }

        public async Task<IQueryable<DecisionDTO>> GetDecisionsByBulletinIdAsync(string aId)
        {
            var decisions = await _bulletinRepository.SelectAllDecisionsAsync();
            var filteredDecisions = decisions.Where(x => x.BulletinId == aId);
            return filteredDecisions.ProjectTo<DecisionDTO>(mapperConfiguration);           
        }

        public async Task<IQueryable<DocumentDTO>> GetDocumentsByBulletinIdAsync(string aId)
        {
            var documents = await _bulletinRepository.SelectAllDocumentsAsync();
            var filteredDocuments = documents.Where(x => x.BulletinId == aId);
            return filteredDocuments.ProjectTo<DocumentDTO>(mapperConfiguration);
        }

        public async Task InsertBulletinDocumentAsync(string bulletinId, DocumentDTO aInDto)
        {
            if (aInDto.DocumentContent?.Length == 0)
                throw new ArgumentNullException("Documetn is empty");

            var docContentId = string.IsNullOrEmpty(aInDto.DocumentContentId) ?
                Guid.NewGuid().ToString() :
                aInDto.DocumentContentId;

            var document = mapper.Map<DocumentDTO, DDocument>(aInDto);
            document.BulletinId = bulletinId;
            document.DocContentId = docContentId;

            var documentContent = new DDocContent()
            {
                Id = docContentId,
                Content = aInDto.DocumentContent,
                MimeType = aInDto.MimeType
            };

            dbContext.Add(document);
            dbContext.Add(documentContent);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteDocumentAsync(string documentId)
        {
            var document = await _bulletinRepository.SelectDocumentAsync(documentId);
            if (document == null)
            {
                throw new ArgumentException($"Document with id: {documentId} is missing");
            }

            document.EntityState = EntityStateEnum.Deleted;
            if (document.DocContent != null)
            {
                document.DocContent.EntityState = EntityStateEnum.Deleted;
            }

            await dbContext.SaveEntityAsync(document, true);
        }

        public async Task<DocumentDTO> GetDocumentContentAsync(string documentId)
        {
            var document = await _bulletinRepository.SelectDocumentAsync(documentId);
            if (document == null) return null;

            var documentDTO = mapper.Map<DocumentDTO>(document);
            documentDTO.DocumentContent = document.DocContent.Content;
            return documentDTO;
        }

        public async Task<IQueryable<PersonAliasDTO>> GetPersonAliasByBulletinIdAsync(string aId)
        {
            var result = await _bulletinRepository.SelectBullPersAliasByBulletinIdAsync(aId);
            return result.ProjectTo<PersonAliasDTO>(mapper.ConfigurationProvider);
        }

        private async Task<string> UpdateBulletinAsync(BulletinDTO aInDto, bool isAdded)
        {
            var entity = mapper.MapToEntity<BulletinDTO, BBulletin>(aInDto, isAdded);

            // въвеждане на бюлетин е възможно единствено от служител БС
            if (isAdded)
            {
                entity.StatusId = BulletinConstants.Status.NewOffice;
            }

            UpdateDataForDestruction(entity);

            entity.BOffences = mapper.MapTransactions<OffenceDTO, BOffence>(aInDto.OffancesTransactions);
            entity.BSanctions = mapper.MapTransactions<SanctionDTO, BSanction>(aInDto.SanctionsTransactions);
            entity.BDecisions = mapper.MapTransactions<DecisionDTO, BDecision>(aInDto.DecisionsTransactions);
            entity.BBullPersAliases = mapper.MapTransactions<PersonAliasDTO, BBullPersAlias>(aInDto.PersonAliasTransactions);

            entity.BPersNationalities = CaisMapper.MapMultipleChooseToEntityList<BPersNationality, string, string>(aInDto.Nationalities, nameof(BPersNationality.Id), nameof(BPersNationality.CountryId));

            await SaveEntityAsync(entity);
            return entity.Id;
        }

        /// <summary>
        /// Унищожаване
        /// За бюлетини за съдимост - 100 години от рождената дата на осъденото лице;
        /// За бюлетини за административни наказания по чл. 78а от НК - 15 години от датата на влизане в сила на съдебния акт.
        /// </summary>
        /// <param name="bulletin"></param>
        public void UpdateDataForDestruction(BBulletin bulletin)
        {
            if (bulletin.ModifiedProperties == null)
                bulletin.ModifiedProperties = new List<string>();

            if (bulletin.BulletinType == nameof(BulletinConstants.Type.ConvictionBulletin) && bulletin.BirthDate.HasValue)
            {
                bulletin.DeleteDate = bulletin.BirthDate.Value.AddYears(100);
                bulletin.ModifiedProperties.Add(nameof(bulletin.DeleteDate));
            }
            else if (bulletin.BulletinType == nameof(BulletinConstants.Type.Bulletin78A) && bulletin.DecisionFinalDate.HasValue)
            {
                bulletin.DeleteDate = bulletin.DecisionFinalDate.Value.AddYears(15);
                bulletin.ModifiedProperties.Add(nameof(bulletin.DeleteDate));
            }

            // тодо:може ли да се променя типа на бюлетин ? от какво зависи той
            // и при промяната му какво се случва със статуса
            // например може бюлетин според чл. 78а да енастъпило време за унищожаване, но потребителя ако редактира 
            // бюлетина и смени типа, статуса активен ли трябва да бъде ? 
            // промяна на статус на бюлетин за унищожавне
            if (bulletin.DeleteDate.HasValue && bulletin.DeleteDate <= DateTime.Now)
            {
                bulletin.StatusId = BulletinConstants.Status.ForDestruction;
                bulletin.ModifiedProperties.Add(nameof(bulletin.StatusId));
            }
        }
    }
}
