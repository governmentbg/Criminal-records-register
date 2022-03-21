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
using MJ_CAIS.DTO.Common;
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

        public async Task<IgPageResult<BulletinGridDTO>> GetAllCustomAsync(ODataQueryOptions<BulletinGridDTO> aQueryOptions, string statusId)
        {
            var context = _bulletinRepository.GetDbContext();

            var baseQuery = context.BBulletins.AsNoTracking()
                .Include(x => x.BulletinAuthority)
                .Where(x => x.StatusId == statusId)
                .Select(x => new BulletinGridDTO
                {
                    Id = x.Id,
                    FirstName = x.Firstname,
                    SurName = x.Surname,
                    FamilyName = x.Familyname,
                    RegistrationNumber = x.RegistrationNumber,
                    StatusId = statusId,
                    CreatedOn = x.CreatedOn,
                    AlphabeticalIndex = x.AlphabeticalIndex,
                    BulletinAuthorityName = x.BulletinAuthority != null ? x.BulletinAuthority.Name : string.Empty,
                    Ln = x.Ln,
                    Lnch = x.Lnch,
                    Egn = x.Egn,
                    DeleteDate = x.DeleteDate,
                    RehabilitationDate = x.RehabilitationDate
                });

            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<BulletinGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        public override async Task<BulletinDTO> SelectAsync(string aId)
        {
            var context = _bulletinRepository.GetDbContext();

            var bulletin = await context.BBulletins
                .Include(x => x.BPersNationalities)
                .Include(x => x.BirthCity)
                    .ThenInclude(x => x.Municipality)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == aId);

            if (bulletin == null) return null;

            var result = mapper.Map<BulletinDTO>(bulletin);
            return result;
        }

        public override async Task<string> InsertAsync(BulletinDTO aInDto)
            => await UpdateBulletinAsync(aInDto, true);

        public override async Task UpdateAsync(string aId, BulletinDTO aInDto)
            => await UpdateBulletinAsync(aInDto, false);

        public async Task<IQueryable<OffenceDTO>> GetOffencesByBulletinIdAsync(string aId)
        {
            var dbContext = _bulletinRepository.GetDbContext();

            var result = dbContext.BOffences
                .AsNoTracking()
                .Include(x => x.OffenceCat)
                .Include(x => x.EcrisOffCat)
                .Include(x => x.OffPlaceCountry)
                .Include(x => x.OffPlaceSubdiv)
                .Include(x => x.OffPlaceCity)
                .Include(x => x.OffLvlCompl)
                .Include(x => x.OffLvlPart)
                .Where(x => x.BulletinId == aId)
                .ProjectTo<OffenceDTO>(mapper.ConfigurationProvider);

            return await Task.FromResult(result);
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
                entity.StatusId = BulletinStatusTypeConstants.NewEISS;
            }

            entity.BOffences = mapper.MapTransactions<OffenceDTO, BOffence>(aInDto.OffancesTransactions);
            entity.BSanctions = mapper.MapTransactions<SanctionDTO, BSanction>(aInDto.SanctionsTransactions);
            entity.BDecisions = mapper.MapTransactions<DecisionDTO, BDecision>(aInDto.DecisionsTransactions);
            entity.BBullPersAliases = mapper.MapTransactions<PersonAliasDTO, BBullPersAlias>(aInDto.PersonAliasTransactions);

            entity.BPersNationalities = CaisMapper.MapMultipleChooseToEntityList<BPersNationality, string, string>(aInDto.Nationalities, nameof(BPersNationality.Id), nameof(BPersNationality.CountryId));

            await SaveEntityAsync(entity);
            return entity.Id;
        }
    }
}
