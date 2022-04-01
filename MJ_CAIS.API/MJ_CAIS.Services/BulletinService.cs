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
            var dbContext = _bulletinRepository.GetDbContext();
            var bulletin = await dbContext.BBulletins
               .FirstOrDefaultAsync(x => x.Id == aInDto);

            if (bulletin == null)
                throw new ArgumentException($"Bulletin with id: {aInDto} is missing");

            bulletin.StatusId = statusId;
            await dbContext.SaveChangesAsync();
        }

        public async Task<IQueryable<OffenceDTO>> GetOffencesByBulletinIdAsync(string aId)
        {
            var dbContext = _bulletinRepository.GetDbContext();

            var offances = dbContext.BOffences
                .AsNoTracking()
                .Include(x => x.OffenceCat)
                .Include(x => x.EcrisOffCat)
                .Include(x => x.OffPlaceCountry)
                .Include(x => x.OffPlaceCity)
                    .ThenInclude(x => x.Municipality)
                .Include(x => x.OffLvlCompl)
                .Include(x => x.OffLvlPart)
                .Where(x => x.BulletinId == aId)
                .ProjectTo<OffenceDTO>(mapperConfiguration);

            return await Task.FromResult(offances);
        }

        public async Task<IQueryable<SanctionDTO>> GetSanctionsByBulletinIdAsync(string aId)
        {
            var dbContext = _bulletinRepository.GetDbContext();

            var result = dbContext.BSanctions
                .AsNoTracking()
                .Include(x => x.EcrisSanctCateg)
                .Include(x => x.SanctActivity)
                .Include(x => x.SanctCategory)
                .Include(x => x.SanctProbCateg)
                .Include(x => x.SanctProbMeasure)
                .Where(x => x.BulletinId == aId)
                .ProjectTo<SanctionDTO>(mapper.ConfigurationProvider);

            return await Task.FromResult(result);
        }

        public async Task<IQueryable<DecisionDTO>> GetDecisionsByBulletinIdAsync(string aId)
        {
            var dbContext = _bulletinRepository.GetDbContext();

            var result = dbContext.BDecisions
                .AsNoTracking()
                .Include(x => x.DecisionAuth)
                .Include(x => x.DecisionChType)
                .Include(x => x.DecisionType)
                .Where(x => x.BulletinId == aId)
                .ProjectTo<DecisionDTO>(mapper.ConfigurationProvider);

            return await Task.FromResult(result);
        }

        public async Task<IQueryable<DocumentDTO>> GetDocumentsByBulletinIdAsync(string aId)
        {
            var dbContext = _bulletinRepository.GetDbContext();

            var result = dbContext.DDocuments
                .AsNoTracking()
                .Include(x => x.DocType)
                .Include(x => x.DocContent)
                .Where(x => x.BulletinId == aId)
                .ProjectTo<DocumentDTO>(mapper.ConfigurationProvider);

            return await Task.FromResult(result);
        }

        public async Task InsertBulletinDocumentAsync(string bulletinId, DocumentDTO aInDto)
        {
            if (aInDto == null)
                throw new ArgumentNullException(nameof(aInDto));

            if (aInDto.DocumentContent?.Length == 0)
                throw new ArgumentNullException("Documetn is empty");

            var context = _bulletinRepository.GetDbContext();
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

            context.Add(document);
            context.Add(documentContent);
            await context.SaveChangesAsync();
        }

        public async Task DeleteComplaintDocumentAsync(string documentId)
        {
            var context = _bulletinRepository.GetDbContext();

            var document = await context.Set<DDocument>().AsNoTracking()
                .Include(x => x.DocContent)
                .FirstOrDefaultAsync(x => x.Id == documentId);

            if (document == null)
            {
                throw new ArgumentException($"Document with id: {documentId} is missing");
            }

            document.EntityState = EntityStateEnum.Deleted;
            if (document.DocContent != null)
            {
                document.DocContent.EntityState = EntityStateEnum.Deleted;
            }

            await context.SaveEntityAsync(document, true);
        }

        public async Task<DocumentDTO> GetDocumentContentAsync(string documentId)
        {
            var context = _bulletinRepository.GetDbContext();

            var document = await context.Set<DDocument>().AsNoTracking()
                .Include(x => x.DocContent)
                .FirstOrDefaultAsync(x => x.Id == documentId);

            if (document == null || document.DocContent == null) return null;

            return new DocumentDTO
            {
                Name = document.Name,
                DocumentContent = document.DocContent.Content,
                MimeType = document.DocContent.MimeType
            };
        }

        public async Task<IQueryable<PersonAliasDTO>> GetPersonAliasByBulletinIdAsync(string aId)
        {
            var dbContext = _bulletinRepository.GetDbContext();

            var result = dbContext.BBullPersAliases
                .AsNoTracking()
                .Where(x => x.BulletinId == aId)
                .ProjectTo<PersonAliasDTO>(mapper.ConfigurationProvider);

            return await Task.FromResult(result);
        }

        private async Task<string> UpdateBulletinAsync(BulletinDTO aInDto, bool isAdded)
        {
            var entity = mapper.MapToEntity<BulletinDTO, BBulletin>(aInDto, isAdded);

            if (isAdded)
            {
                entity.StatusId = BulletinConstants.Status.NewEISS;
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
            else if (bulletin.BulletinType == nameof(BulletinConstants.Type.Bulletin78А) && bulletin.DecisionFinalDate.HasValue)
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
