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
    public class BulletinService : BaseAsyncService<BulletinBaseDTO, BulletinBaseDTO, BulletinGridDTO, BBulletin, string, CaisDbContext>, IBulletinService
    {
        private readonly IBulletinRepository _bulletinRepository;

        public BulletinService(IMapper mapper, IBulletinRepository bulletinRepository)
            : base(mapper, bulletinRepository)
        {
            _bulletinRepository = bulletinRepository;
        }

        public virtual async Task<IgPageResult<BulletinGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<BulletinGridDTO> aQueryOptions, string? statusId)
        {
            var entityQuery = this.GetSelectAllQueriable();
            if (!string.IsNullOrEmpty(statusId))
            {
                entityQuery = entityQuery.Where(x => x.StatusId == statusId);
            }

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

        /// <summary>
        /// Manually add a bulletin by an employee
        /// </summary>
        /// <param name="aInDto"></param>
        /// <returns></returns>
        public async Task<string> InsertAsync(BulletinAddDTO aInDto)
        {
            var bulletin = mapper.MapToEntity<BulletinAddDTO, BBulletin>(aInDto, true);
            // entry of a bulletin is possible only by an employee 
            bulletin.StatusId = BulletinConstants.Status.NewOffice;
            return await UpdateBulletinAsync(aInDto, bulletin, null);
        }

        /// <summary>
        /// Update data in the bulletin according to status
        /// NewOffice => allowed change of all data
        /// NewEISS => registration information only
        /// Active => decision information only
        /// ForDestruction, Deleted, ForRehabilitation, Rehabilitated => editing is not allowed
        /// </summary>
        /// <param name="aInDto"></param>
        /// <returns></returns>
        public async Task UpdateAsync(BulletinEditDTO aInDto)
        {
            var bulletinDb = await dbContext.BBulletins.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == aInDto.Id);

            if (bulletinDb == null)
                throw new ArgumentException($"Bulletin with id {aInDto.Id} is missing");

            var bulletin = mapper.MapToEntity<BulletinEditDTO, BBulletin>(aInDto, false);

            // if the bulletin is locked for editing,
            // we add property according to the status
            if (bulletinDb.Locked.HasValue && bulletinDb.Locked.Value)
            {
                if (bulletinDb.StatusId != BulletinConstants.Status.NewEISS ||
                    bulletinDb.StatusId != BulletinConstants.Status.NewOffice)
                {
                    // nothing of the main object is edited
                    // only decisions added
                    bulletin.ModifiedProperties = new List<string>();
                }
                else if (bulletinDb.StatusId == BulletinConstants.Status.NewEISS)
                {
                    // when updating a bulletin in NewEISS status
                    // only registration information is changed
                    bulletin.ModifiedProperties = new List<string>
                    {
                        nameof(bulletin.RegistrationNumber),
                        nameof(bulletin.SequentialIndex),
                        nameof(bulletin.AlphabeticalIndex),
                        nameof(bulletin.EcrisConvictionId),
                        nameof(bulletin.BulletinType),
                        nameof(bulletin.BulletinReceivedDate),
                    };
                }

            }

            await UpdateBulletinAsync(aInDto, bulletin, bulletinDb.StatusId);
        }

        /// <summary>
        /// Change of the status of a bulletin by a employee
        /// </summary>
        /// <param name="aInDto">Bulletin ID</param>
        /// <param name="statusId">Status</param>
        /// <exception cref="ArgumentException"></exception>
        public async Task ChangeStatusAsync(string aInDto, string statusId)
        {
            var bulletin = await dbContext.BBulletins
               .FirstOrDefaultAsync(x => x.Id == aInDto);

            if (bulletin == null)
                throw new ArgumentException($"Bulletin with id: {aInDto} is missing");

            AddBulletinStatusH(bulletin.StatusId, statusId, aInDto);

            // All active bulletins are locked for editing
            // only decisions can be added
            if (statusId == BulletinConstants.Status.Active)
            {
                bulletin.Locked = true;
            }

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

        #region Helpers

        private async Task<string> UpdateBulletinAsync(BulletinBaseDTO aInDto, BBulletin entity, string oldStatus)
        {
            UpdateDataForDestruction(entity);

            UpdateTransactions(aInDto, entity);

            UpdateStatusByDecisions(entity, oldStatus);

            await SaveEntityAsync(entity);
            return entity.Id;
        }

        /// <summary>
        /// Change the status of the bulletin depending on the added decision information
        /// (ReplacedAct425, Rehabilitated)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private void UpdateStatusByDecisions(BBulletin entityToSave, string oldStatus)
        {
            const string judicialAnnulmentId = "DCH-00-Y";
            const string rehabilitationId = "DCH-00-R";

            if (entityToSave.BDecisions.Any(x => x.DecisionChTypeId == rehabilitationId))
            {
                entityToSave.StatusId = BulletinConstants.Status.Rehabilitated;
            }

            if (entityToSave.BDecisions.Any(x => x.DecisionChTypeId == judicialAnnulmentId))
            {
                entityToSave.StatusId = BulletinConstants.Status.ReplacedAct425;
            }

            if (entityToSave.EntityState == EntityStateEnum.Modified)
            {
                var isAddedHistory = AddBulletinStatusH(oldStatus, entityToSave.StatusId, entityToSave.Id);
                if (isAddedHistory)
                {
                    UpdateModifiedProperties(entityToSave, nameof(entityToSave.StatusId));
                }
            }

            // All active bulletins are locked for editing
            // only decisions can be added
            if (entityToSave.StatusId == BulletinConstants.Status.Active)
            {
                entityToSave.Locked = true;
                UpdateModifiedProperties(entityToSave, nameof(entityToSave.Locked));
            }
        }

        /// <summary>
        /// If there is a change in the status of the bulletin is added to the history table
        /// </summary>
        /// <param name="oldStatus">Previous status</param>
        /// <param name="newStatus">New status</param>
        /// <param name="bulletinId">ID</param>
        private bool AddBulletinStatusH(string oldStatus, string newStatus, string bulletinId)
        {
            if (!string.IsNullOrEmpty(oldStatus) && oldStatus != newStatus)
            {
                var satusHistory = new BBulletinStatusH
                {
                    Id = Guid.NewGuid().ToString(),
                    BulletinId = bulletinId,
                    OldStatusCode = oldStatus,
                    NewStatusCode = newStatus,
                    EntityState = EntityStateEnum.Added,
                };

                dbContext.BBulletinStatusHes.Add(satusHistory);
                return true;
            }

            return false;
        }

        private void UpdateTransactions(BulletinBaseDTO aInDto, BBulletin entity)
        {
            entity.BOffences = mapper.MapTransactions<OffenceDTO, BOffence>(aInDto.OffancesTransactions);
            entity.BSanctions = mapper.MapTransactions<SanctionDTO, BSanction>(aInDto.SanctionsTransactions);
            entity.BDecisions = mapper.MapTransactions<DecisionDTO, BDecision>(aInDto.DecisionsTransactions);
            entity.BBullPersAliases = mapper.MapTransactions<PersonAliasDTO, BBullPersAlias>(aInDto.PersonAliasTransactions);
            entity.BPersNationalities = CaisMapper.MapMultipleChooseToEntityList<BPersNationality, string, string>(aInDto.Nationalities, nameof(BPersNationality.Id), nameof(BPersNationality.CountryId));
        }

        /// <summary>
        /// Destruction
        /// For criminal records - 100 years from the date of birth of the convicted person
        /// For bulletins for administrative sanctions under Art. 78a - 
        ///     15 years from the date of entry into force of the judicial act
        /// </summary>
        /// <param name="bulletin"></param>
        private void UpdateDataForDestruction(BBulletin bulletin)
        {
            if (bulletin.BulletinType == nameof(BulletinConstants.Type.ConvictionBulletin) && bulletin.BirthDate.HasValue)
            {
                bulletin.DeleteDate = bulletin.BirthDate.Value.AddYears(100);
                UpdateModifiedProperties(bulletin, nameof(bulletin.DeleteDate));
            }
            else if (bulletin.BulletinType == nameof(BulletinConstants.Type.Bulletin78A) && bulletin.DecisionFinalDate.HasValue)
            {
                bulletin.DeleteDate = bulletin.DecisionFinalDate.Value.AddYears(15);
                UpdateModifiedProperties(bulletin, nameof(bulletin.DeleteDate));
            }

            if (bulletin.DeleteDate.HasValue && bulletin.DeleteDate <= DateTime.Now)
            {
                bulletin.StatusId = BulletinConstants.Status.ForDestruction;
                UpdateModifiedProperties(bulletin, nameof(bulletin.StatusId));
            }
        }

        private void UpdateModifiedProperties(BBulletin entityToSave, string nameOfProp)
        {
            if (entityToSave.ModifiedProperties == null)
            {
                entityToSave.ModifiedProperties = new List<string>();
            }

            entityToSave.ModifiedProperties.Add(nameOfProp);
        }


        #endregion
    }
}
