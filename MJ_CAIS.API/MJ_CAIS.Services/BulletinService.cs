using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.DTO.Shared;
using MJ_CAIS.EcrisObjectsServices.Contracts;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;
using static MJ_CAIS.Common.Constants.BulletinConstants;
using static MJ_CAIS.Common.Constants.PersonConstants;

namespace MJ_CAIS.Services
{
    public class BulletinService : BaseAsyncService<BulletinBaseDTO, BulletinBaseDTO, BulletinGridDTO, BBulletin, string, CaisDbContext>, IBulletinService
    {
        private readonly IBulletinRepository _bulletinRepository;
        private readonly IPersonService _personService;
        private readonly INotificationService _notificationService;
        private readonly IBulletinEventService _bulletinEventService;
        private readonly IRehabilitationService _rehabilitationService;
        private readonly IRegisterTypeService _registerTypeService;
        private readonly IUserContext _userContext;

        public BulletinService(IMapper mapper,
            IBulletinRepository bulletinRepository,
            IPersonService personService,
            INotificationService notificationService,
            IBulletinEventService bulletinEventService,
            IRehabilitationService rehabilitationService,
            IUserContext userContext,
            IRegisterTypeService registerTypeService)
            : base(mapper, bulletinRepository)
        {
            _bulletinRepository = bulletinRepository;
            _personService = personService;
            _notificationService = notificationService;
            _bulletinEventService = bulletinEventService;
            _rehabilitationService = rehabilitationService;
            _userContext = userContext;
            _registerTypeService = registerTypeService;
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

        public virtual async Task<List<BulletinGridDTO>> SelectAllNoWrapAsync(ODataQueryOptions<BulletinGridDTO> aQueryOptions, string? statusId)
        {
            var entityQuery = this.GetSelectAllQueriable();
            if (!string.IsNullOrEmpty(statusId))
            {
                entityQuery = entityQuery.Where(x => x.StatusId == statusId);
            }

            var baseQuery = entityQuery.ProjectTo<BulletinGridDTO>(mapperConfiguration);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions, MAX_EXCEL_SIZE);

            var result = resultQuery.ToList();
            return result;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        public async Task<BulletinBaseDTO> SelectWithPersonDataAsync(string personId)
        {
            var result = new BulletinBaseDTO();
            var dbContext = _bulletinRepository.GetDbContext();
            var authId = _userContext.CsAuthorityId;
            var auth = await dbContext.GCsAuthorities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == authId);
            result.CsAuthorityName = auth?.Name;
            var person = await _personService.SelectWithBirthInfoAsync(personId);
            result.Person = person ?? new PersonDTO();
            return result;
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
            bulletin.StatusId = Status.NewOffice;
            bulletin.Id = BaseEntity.GenerateNewId();

            bulletin.CsAuthorityId = _userContext.CsAuthorityId;
            var regNumber = await _registerTypeService.GetRegisterNumberForBulletin(bulletin.CsAuthorityId, bulletin.BulletinType);
            bulletin.RegistrationNumber = regNumber;

            await UpdateBulletinAsync(aInDto, bulletin);
            await _bulletinRepository.SaveChangesAsync();

            if (bulletin.StatusId == Status.NoSanction)
            {
                await _bulletinEventService.GenerateEventWhenUpdateBullAsync(bulletin);
                await _bulletinRepository.SaveChangesAsync();
            }

            return bulletin.Id;
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
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.bulletinDoesNotExist, aInDto.Id));

            if (bulletinDb.CsAuthorityId != _userContext.CsAuthorityId)
                throw new BusinessLogicException(BusinessLogicExceptionResources.editIsUnauthorized);

            var oldBulletinStatus = bulletinDb.StatusId;
            var bulletinToUpdate = mapper.MapToEntity<BulletinEditDTO, BBulletin>(aInDto, false);

            // if the bulletin is locked for editing,
            // we add property according to the status
            if (bulletinDb.Locked.HasValue && bulletinDb.Locked.Value)
            {
                SetModifiedPropertiesByStatus(bulletinDb, bulletinToUpdate);
            }

            await UpdateBulletinAsync(aInDto, bulletinToUpdate, oldBulletinStatus);

            if (bulletinToUpdate.StatusId == Status.NewEISS && string.IsNullOrEmpty(bulletinDb.RegistrationNumber))
            {
                var regNumber = await _registerTypeService.GetRegisterNumberForBulletin(bulletinToUpdate?.CsAuthorityId, bulletinToUpdate.BulletinType);
                bulletinToUpdate.RegistrationNumber = regNumber;
                bulletinToUpdate.ModifiedProperties.Add(nameof(bulletinToUpdate.RegistrationNumber));
            }

            // save entities before check for events and rehabilitation
            await _bulletinRepository.SaveChangesAsync();

            switch (bulletinToUpdate.StatusId)
            {
                case Status.NoSanction:
                    await _bulletinEventService.GenerateEventWhenUpdateBullAsync(bulletinToUpdate);
                    break;
                case Status.Active or Status.ForRehabilitation:
                    await _rehabilitationService.ApplyRehabilitationOnUpdateAsync(bulletinToUpdate);
                    break;
            }

            await _bulletinRepository.SaveChangesAsync();

            try
            {
                await SendMessageToEcrisAsync(bulletinToUpdate.EuCitizen, bulletinToUpdate.TcnCitizen, bulletinToUpdate.Id, oldBulletinStatus, bulletinToUpdate.StatusId);
            }
            catch (Exception ex)
            {
                // todo
                throw;
            }
        }

        /// <summary>
        /// Change of the status of a bulletin by a employee
        /// </summary>
        /// <param name="aInDto">Bulletin ID</param>
        /// <param name="statusId">Status</param>
        /// <exception cref="ArgumentException"></exception>
        public async Task ChangeStatusAsync(string bulletinId, string statusId)
        {
            var bulletin = await dbContext.BBulletins
                .Include(x => x.BPersNationalities)
                    .ThenInclude(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == bulletinId);

            if (bulletin == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.bulletinDoesNotExist, bulletinId));

            var oldBulletinStatus = bulletin.StatusId;
            AddBulletinStatusH(oldBulletinStatus, statusId, bulletinId, bulletin.Locked);

            var mustUpdatePersonAndSendData = (oldBulletinStatus == Status.NewOffice || oldBulletinStatus == Status.NewEISS) &&
                statusId == Status.Active;

            // All active bulletins are locked for editing
            // only decisions can be added
            var isActiveBulletin = statusId == Status.Active;
            if (isActiveBulletin)
            {
                await SetDataForNationalitiesAsync(bulletin);
                bulletin.Locked = true;
            }

            bulletin.StatusId = statusId;
            bulletin.EntityState = EntityStateEnum.Modified;
            bulletin.ModifiedProperties = new List<string>
            {
                nameof(bulletin.Locked),
                nameof(bulletin.StatusId),
                nameof(bulletin.Version)
            };


            if (!mustUpdatePersonAndSendData)
            {
                await _bulletinRepository.SaveChangesAsync();
                return;
            }

            var personId = await CreatePersonFromBulletinAsync(bulletin);

            // Attempt to save changes to the database
            await _bulletinRepository.SaveChangesAsync();

            if (isActiveBulletin)
            {
                // only apply changes to the context
                await _bulletinEventService.GenerateEventWhenChangeStatusOfBullAsync(bulletin, personId);
                await _rehabilitationService.ApplyRehabilitationOnChangeStatusAsync(bulletin, personId);
                // save data in db
                await _bulletinRepository.SaveChangesAsync();
            }

            try
            {
                await SendMessageToEcrisAsync(bulletin.EuCitizen, bulletin.TcnCitizen, bulletin.Id, oldBulletinStatus, bulletin.StatusId);
            }
            catch (Exception ex)
            {
                // todo
                throw;
            }
        }

        public async Task<IQueryable<OffenceDTO>> GetOffencesByBulletinIdAsync(string aId)
        {
            var offences = await _bulletinRepository.SelectAllOffencesAsync();
            var filteredOffences = offences.Where(x => x.BulletinId == aId);
            return filteredOffences.ProjectTo<OffenceDTO>(mapperConfiguration);
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

        public async Task<IQueryable<BulletinStatusHistoryDTO>> GetStatusHistoryByBulletinIdAsync(string aId)
        {
            var statues = await _bulletinRepository.SelectAllStatusHistoryDataAsync();
            var filteredStatuses = statues.Where(x => x.BulletinId == aId);
            return filteredStatuses.ProjectTo<BulletinStatusHistoryDTO>(mapperConfiguration);
        }

        public async Task InsertBulletinDocumentAsync(string bulletinId, DocumentDTO aInDto)
        {
            if (aInDto.DocumentContent?.Length == 0)
                throw new BusinessLogicException(BusinessLogicExceptionResources.documentIsEmpty);

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

            // add an event when a user from another authority attaches a document
            var bulletinAuthId = await _bulletinRepository.GetBulletinAuthIdAsync(bulletinId);
            var currentUserAuth = _userContext.CsAuthorityId;
            if (bulletinAuthId != currentUserAuth)
            {
                var bullEvent = new BBulEvent
                {
                    BulletinId = bulletinId,
                    Id = BaseEntity.GenerateNewId(),
                    StatusCode = BulletinEventConstants.Status.New,
                    EventType = BulletinEventConstants.Type.NewDocument,
                    EntityState = EntityStateEnum.Added
                };

                dbContext.Add(bullEvent);
            }

            dbContext.Add(document);
            dbContext.Add(documentContent);

            await _bulletinRepository.SaveChangesAsync();
        }

        public async Task DeleteDocumentAsync(string documentId)
        {
            var document = await _bulletinRepository.SelectDocumentAsync(documentId);
            if (document == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.documentDoesNotExist, documentId));

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

        /// <summary>
        /// Only apply changes
        /// </summary>
        /// <param name="aInDto"></param>
        /// <param name="entity"></param>
        /// <param name="oldStatus"></param>
        /// <returns></returns>
        private async Task UpdateBulletinAsync(BulletinBaseDTO aInDto, BBulletin entity, string oldStatus = null)
        {
            UpdateDataForDestruction(entity);

            await UpdateTransactionsAsync(aInDto, entity);

            UpdateStatusByDecisions(entity);

            // if no sanction is selected
            // change bulletin status
            if (entity.NoSanction == true)
            {
                entity.StatusId = Status.NoSanction;
            }

            if (entity.StatusId is Status.NoSanction or Status.ForDestruction)
            {
                // if new person is created 
                // set new person id
                aInDto.Person.Id = await CreatePersonFromBulletinAsync(entity);
            }

            // save old status

            var isAddedHistory = AddBulletinStatusH(oldStatus, entity.StatusId, entity.Id, entity.Locked);
            if (isAddedHistory)
            {
                UpdateModifiedProperties(entity, nameof(entity.StatusId));
            }

            // it is locked each time
            // unless the statue is NewOffice or NewEISS
            if (entity.StatusId != Status.NewOffice)
            {
                entity.Locked = true;
                UpdateModifiedProperties(entity, nameof(entity.Locked));
            }

            var passedNavigationProperties = new List<IBaseIdEntity>();
            dbContext.ApplyChanges(entity, passedNavigationProperties, true);
        }

        /// <summary>
        /// Creates a person from a bulletin and a link between them
        /// </summary>
        private async Task<string> CreatePersonFromBulletinAsync(BBulletin bulletin)
        {
            // when status is set to Active or NoSanction
            // save data for person and its identifiers

            // get person data from bulletin
            var personDto = mapper.Map<BBulletin, PersonDTO>(bulletin);
            // create person object, apply changes
            var person = await _personService.CreatePersonAsync(personDto);

            // create rehabilitation between person identifier and bulletin
            // create PBulletinId for all pids (locally added and saved in db)

            foreach (var personIdObj in person.PPersonIds)
            {
                bulletin.PBulletinIds.Add(new PBulletinId
                {
                    BulletinId = bulletin.Id,
                    Id = BaseEntity.GenerateNewId(),
                    EntityState = EntityStateEnum.Added,
                    PersonId = personIdObj.Id // table P_PERSON_IDS not P_PERSON,
                });

                if (personIdObj.PidTypeId == PidType.Suid)
                {
                    bulletin.Suid = personIdObj.Pid;
                }

                dbContext.ApplyChanges(bulletin, new List<IBaseIdEntity>());
            }

            return person.Id;
        }

        private static void SetModifiedPropertiesByStatus(BBulletin? bulletinDb, BBulletin bulletin)
        {
            if (bulletinDb.StatusId != Status.NewEISS &&
                bulletinDb.StatusId != Status.NewOffice)
            {
                // only version of the main object must be updated
                bulletin.ModifiedProperties = new List<string>() { nameof(bulletin.Version) };
            }
            else if (bulletinDb.StatusId == Status.NewEISS)
            {
                // when updating a bulletin in NewEISS status
                // only registration information is changed
                bulletin.ModifiedProperties = new List<string>
                    {
                        nameof(bulletin.RegistrationNumber),
                        nameof(bulletin.AlphabeticalIndex),
                        nameof(bulletin.EcrisConvictionId),
                        nameof(bulletin.BulletinType),
                        nameof(bulletin.BulletinReceivedDate),
                        nameof(bulletin.Version),
                    };
            }
        }

        /// <summary>
        /// Change the status of the bulletin depending on the added decision information
        /// (ReplacedAct425, Rehabilitated)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private void UpdateStatusByDecisions(BBulletin entityToSave)
        {
            if (entityToSave.BDecisions.Any(x => x.DecisionChTypeId == DecisionType.Rehabilitation))
            {
                entityToSave.StatusId = Status.Rehabilitated;
            }

            if (entityToSave.BDecisions.Any(x => x.DecisionChTypeId == DecisionType.JudicialAnnulment))
            {
                entityToSave.StatusId = Status.ReplacedAct425;
            }
        }

        /// <summary>
        /// If there is a change in the status of the bulletin is added to the history table
        /// </summary>
        /// <param name="oldStatus">Previous status</param>
        /// <param name="newStatus">New status</param>
        /// <param name="bulletinId">ID</param>
        private bool AddBulletinStatusH(string oldStatus, string newStatus, string bulletinId, bool? isLocked)
        {
            if (oldStatus == newStatus) return false;

            var statusHistory = new BBulletinStatusH
            {
                Id = Guid.NewGuid().ToString(),
                BulletinId = bulletinId,
                OldStatusCode = oldStatus,
                NewStatusCode = newStatus,
                EntityState = EntityStateEnum.Added,
                Locked = isLocked
            };

            dbContext.ApplyChanges(statusHistory, new List<IBaseIdEntity>());
            return true;
        }

        private async Task UpdateTransactionsAsync(BulletinBaseDTO aInDto, BBulletin entity)
        {
            entity.BOffences = mapper.MapTransactions<OffenceDTO, BOffence>(aInDto.OffancesTransactions);

            var deletedSanctionIds = aInDto.SanctionsTransactions
                    .Where(x => x.Type == TransactionTypesEnum.DELETE)
                    .Select(x => x.Id).ToList();

            var sanctions = await GetDeletedSanctionsAsync(deletedSanctionIds);

            // added or updated entities
            foreach (var currentTransaction in aInDto.SanctionsTransactions.Where(x => x.Type != TransactionTypesEnum.DELETE))
            {
                var probTrans = mapper.MapTransactions<BulletinProbationDTO, BProbation>(currentTransaction.NewValue.ProbationsTransactions);
                var sanction = mapper.MapTransaction<SanctionDTO, BSanction>(currentTransaction);
                sanction.BProbations = probTrans;

                foreach (var prob in sanction.BProbations)
                {
                    prob.SanctionId = sanction.Id;
                }

                sanctions.Add(sanction);
            }

            entity.BSanctions = sanctions;
            entity.BDecisions = mapper.MapTransactions<DecisionDTO, BDecision>(aInDto.DecisionsTransactions);
            entity.BBullPersAliases = mapper.MapTransactions<PersonAliasDTO, BBullPersAlias>(aInDto.Person.PersonAliasTransactions);
            entity.BPersNationalities = CaisMapper.MapMultipleChooseToEntityList<BPersNationality, string, string>(aInDto.Person.Nationalities, nameof(BPersNationality.Id), nameof(BPersNationality.CountryId));
        }

        private async Task<List<BSanction>> GetDeletedSanctionsAsync(List<string> deletedSanctionIds)
        {
            if (deletedSanctionIds.Count == 0) return new List<BSanction>();

            var deletedSanctionAndItsProbations = await dbContext.BSanctions.AsNoTracking()
                      .Where(x => deletedSanctionIds.Contains(x.Id))
                      .Include(x => x.BProbations)
                      .Select(x => new BSanction
                      {
                          Id = x.Id,
                          EntityState = EntityStateEnum.Deleted,
                          BProbations = x.BProbations.Select(x => new BProbation
                          {
                              Id = x.Id,
                              EntityState = EntityStateEnum.Deleted,
                          }).ToArray(),
                      }).ToListAsync();


            return deletedSanctionAndItsProbations;
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
            switch (bulletin.BulletinType)
            {
                case BulletinConstants.Type.ConvictionBulletin when bulletin.BirthDate.HasValue:
                    bulletin.DeleteDate = bulletin.BirthDate.Value.AddYears(100);
                    UpdateModifiedProperties(bulletin, nameof(bulletin.DeleteDate));
                    break;
                case BulletinConstants.Type.Bulletin78A when bulletin.DecisionFinalDate.HasValue:
                    bulletin.DeleteDate = bulletin.DecisionFinalDate.Value.AddYears(15);
                    UpdateModifiedProperties(bulletin, nameof(bulletin.DeleteDate));
                    break;
            }

            if (!bulletin.DeleteDate.HasValue || !(bulletin.DeleteDate <= DateTime.Now)) return;

            bulletin.StatusId = Status.ForDestruction;
            UpdateModifiedProperties(bulletin, nameof(bulletin.StatusId));
        }

        private void UpdateModifiedProperties(BaseEntity entityToSave, string nameOfProp)
        {
            if (entityToSave.ModifiedProperties == null)
            {
                entityToSave.ModifiedProperties = new List<string>();
            }

            entityToSave.ModifiedProperties.Add(nameOfProp);
        }

        /// <summary>
        /// Set data for nationalities.
        /// If person is EU Citizen message to ecris must be sent
        /// If person is not EU Citizen message to ecris tcn must be sent
        /// The person may be bouth
        /// </summary>
        /// <param name="bulletin"></param>
        /// <returns></returns>
        private async Task SetDataForNationalitiesAsync(BBulletin bulletin)
        {
            // if person is bulgarian citizen
            var skipEcris = bulletin.BPersNationalities is { Count: 1 } && bulletin.BPersNationalities.First().CountryId == BG;

            if (skipEcris) return;

            var personNationalities = bulletin.BPersNationalities.Select(x => x.Country?.Id).Where(x => x != "CO-00-100-BGR"); //todo:
            var isEuCitizen = await dbContext.EEcrisAuthorities.AsNoTracking().AnyAsync(x => personNationalities.Contains(x.CountryId));

            if (isEuCitizen)
            {
                bulletin.EuCitizen = true;
            }

            // todo: Except 
            var createEcrisTcn = personNationalities.Except(dbContext.EEcrisAuthorities.AsNoTracking().Select(x => x.CountryId)).Any();
            if (createEcrisTcn)
            {
                bulletin.TcnCitizen = true;
            }
        }

        private async Task SendMessageToEcrisAsync(bool? isEuCitizen, bool? isTcnCitizen, string bulletinId, string bOldStatus, string bNewStatus)
        {
            if (isEuCitizen == true)
            {
                await this._notificationService.CreateNotificationFromBulletin(bulletinId);
            }

            if (isTcnCitizen == true)
            {
                string? tcnAction;
                if (bOldStatus is Status.NewEISS or Status.NewOffice && bNewStatus == Status.Active)
                {
                    tcnAction = ECRISConstants.EcrisTcnActionType.Create;

                }
                else if (bNewStatus == Status.Deleted)
                {
                    tcnAction = ECRISConstants.EcrisTcnActionType.Delete;
                }
                else
                {
                    tcnAction = ECRISConstants.EcrisTcnActionType.Update;
                }

                _bulletinRepository.CreateEcrisTcn(bulletinId, tcnAction);
                await _bulletinRepository.SaveChangesAsync();
            }
        }

        #endregion
    }
}
