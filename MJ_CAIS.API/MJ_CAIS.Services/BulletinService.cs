using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.ExternalServicesHost;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.DTO.Shared;
using MJ_CAIS.EcrisObjectsServices.Contracts;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;
using System.Text;
using System.Transactions;
using System.Xml.Xsl;
using static MJ_CAIS.Common.Constants.BulletinConstants;
using static MJ_CAIS.Common.Constants.PersonConstants;

namespace MJ_CAIS.Services
{
    public class BulletinService : BaseAsyncService<BulletinBaseDTO, BulletinBaseDTO, BulletinGridDTO, BBulletin, string, CaisDbContext>, IBulletinService
    {
        private readonly IBulletinRepository _bulletinRepository;
        private readonly IManagePersonService _managePersonService;
        private readonly INotificationService _notificationService;
        private readonly IBulletinEventService _bulletinEventService;
        private readonly IRehabilitationService _rehabilitationService;
        private readonly IRegisterTypeService _registerTypeService;
        private readonly IUserContext _userContext;

        public BulletinService(IMapper mapper,
            IBulletinRepository bulletinRepository,
            INotificationService notificationService,
            IBulletinEventService bulletinEventService,
            IRehabilitationService rehabilitationService,
            IUserContext userContext,
            IRegisterTypeService registerTypeService,
            IManagePersonService managePersonService)
            : base(mapper, bulletinRepository)
        {
            _bulletinRepository = bulletinRepository;
            _notificationService = notificationService;
            _bulletinEventService = bulletinEventService;
            _rehabilitationService = rehabilitationService;
            _userContext = userContext;
            _registerTypeService = registerTypeService;
            _managePersonService = managePersonService;
        }

        public virtual async Task<IgPageResult<BulletinGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<BulletinGridDTO> aQueryOptions, string? statusId)
        {
            var entityQuery = this.GetSelectAllQueryable();
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
            var entityQuery = this.GetSelectAllQueryable();
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
            var authId = _userContext.CsAuthorityId;
            var auth = await _bulletinRepository.SingleOrDefaultAsync<GCsAuthority>(x => x.Id == authId);
            result.CsAuthorityName = auth?.Name;
            var person = await _managePersonService.SelectWithBirthInfoAsync(personId);
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
            var bulletinDb = await _bulletinRepository.SingleOrDefaultAsync<BBulletin>(x => x.Id == aInDto.Id);

            if (bulletinDb == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.bulletinDoesNotExist, aInDto.Id));

            if (bulletinDb.CsAuthorityId != _userContext.CsAuthorityId)
                throw new BusinessLogicException(BusinessLogicExceptionResources.editIsUnauthorized);

            var oldBulletinStatus = bulletinDb.StatusId;
            var bulletinToUpdate = mapper.MapToEntity<BulletinEditDTO, BBulletin>(aInDto, false);
            bulletinToUpdate.CsAuthorityId = bulletinDb.CsAuthorityId;

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

            // todo: TransactionScope
            //using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);
            // save entities before check for events and rehabilitation
            await _bulletinRepository.SaveChangesAsync(clearTracker: true);

            // person must be created (normal flow)
            var oldStatusIsNewBull = oldBulletinStatus == null || oldBulletinStatus == Status.NewEISS || oldBulletinStatus == Status.NewOffice;
            var newStatusForUpdatePerson = bulletinToUpdate.StatusId != Status.NewEISS && bulletinToUpdate.StatusId != Status.NewOffice;
            var normalFlowForCreatePerson = oldStatusIsNewBull && newStatusForUpdatePerson;

            var newSuid = await _managePersonService.GenerateSuidAsync(aInDto.Person);
            bulletinToUpdate.Suid = newSuid;

            // when bulletin is unlocked and person data is changed
            var changesOnPersonData = bulletinDb.Locked == false &&
                newStatusForUpdatePerson &&
                (IsBulletinUlockedAndPersonDataChanged(bulletinDb, aInDto.Person) ||
                bulletinDb.Suid != bulletinToUpdate.Suid);

            if (normalFlowForCreatePerson || changesOnPersonData)
            {
                var person = await CreatePersonFromBulletinAsync(bulletinToUpdate);
                await UpdateRehabilitationAndEventDataAsync(bulletinToUpdate, person);
            }

            //scope.Complete();
            await SendMessageToEcrisAsync(bulletinToUpdate.EuCitizen, bulletinToUpdate.TcnCitizen, bulletinToUpdate.Id, oldBulletinStatus, bulletinToUpdate.StatusId);
        }

        /// <summary>
        /// Change of the status of a bulletin by a employee
        /// </summary>
        /// <param name="aInDto">Bulletin ID</param>
        /// <param name="statusId">Active or Delete</param>
        /// <exception cref="ArgumentException"></exception>
        public async Task ChangeStatusAsync(string bulletinId, string statusId)
        {
            var bulletin = await _bulletinRepository.GetBulletinData(bulletinId);

            if (bulletin == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.bulletinDoesNotExist, bulletinId));

            // employee can change status only to active or deleted
            if (statusId != Status.Active && statusId != Status.Deleted)
                throw new BusinessLogicException(string.Format(BulletinResources.msgChangeStatusIsNotAllowed, statusId));

            // delete only bulletin in status for delete
            if (statusId == Status.Deleted && bulletin.StatusId != Status.ForDestruction)
                throw new BusinessLogicException(string.Format(BulletinResources.msgChangeStatusIsNotAllowed, statusId));

            var oldBulletinStatus = bulletin.StatusId;
            AddBulletinStatusH(bulletin, oldBulletinStatus, statusId);
            bulletin.StatusId = statusId;
            bulletin.EntityState = EntityStateEnum.Modified;
            bulletin.Locked = true;
            bulletin.ModifiedProperties = new List<string>
            {
                nameof(bulletin.StatusId),
                nameof(bulletin.Version),
                nameof(bulletin.Locked),
            };

            if (statusId == Status.Deleted)
            {
                await _bulletinRepository.SaveChangesAsync();
                return;
            }

            // All active bulletins are locked for editing
            // only decisions can be added

            await SetDataForNationalitiesAsync(bulletin);
            await _bulletinRepository.SaveChangesAsync();

            // create person
            var person = await CreatePersonFromBulletinAsync(bulletin);
            await UpdateRehabilitationAndEventDataAsync(bulletin, person);

            try
            {
                await SendMessageToEcrisAsync(bulletin.EuCitizen, bulletin.TcnCitizen, bulletin.Id, oldBulletinStatus, bulletin.StatusId);
            }
            catch (Exception ex)
            {
                // todo log
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

        public IQueryable<BulletinStatusHistoryDTO> GetStatusHistoryByBulletinId(string aId)
        {
            var statues = _bulletinRepository.SelectAllStatusHistoryData();
            var filteredStatuses = statues.Where(x => x.BulletinId == aId);
            return filteredStatuses;
        }

        public async Task<byte[]> GetHistoryContentAsync(string aId)
        {
            var historyObj = await _bulletinRepository.SingleOrDefaultAsync<BBulletinStatusH>(x => x.Id == aId);
            if (historyObj == null)
                throw new BusinessLogicException(string.Format(BulletinResources.msgHistoryObjDoesNotExist, aId));

            if (historyObj.Content == null)
                throw new BusinessLogicException(string.Format(BulletinResources.msgHistoryObjContentDoesNotExist, aId));

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExternalServicesHost", "Bulletin.xslt");

            var xsltContent = File.ReadAllText(filePath);

            var html = XmlUtils.XmlTransform(xsltContent, historyObj.Content);
            var result = Encoding.UTF8.GetBytes(html);

            return await Task.FromResult(result);
        }

        public async Task InsertBulletinDocumentAsync(string bulletinId, DocumentDTO aInDto)
        {
            if (aInDto.DocumentContent?.Length == 0)
                throw new BusinessLogicException(BusinessLogicExceptionResources.documentIsEmpty);

            var docContentId = string.IsNullOrEmpty(aInDto.DocumentContentId) ?
                Guid.NewGuid().ToString() :
                aInDto.DocumentContentId;

            var document = mapper.MapToEntity<DocumentDTO, DDocument>(aInDto, true);
            document.BulletinId = bulletinId;
            document.DocContentId = docContentId;

            var documentContent = new DDocContent()
            {
                Id = docContentId,
                Content = aInDto.DocumentContent,
                MimeType = aInDto.MimeType,
                EntityState = EntityStateEnum.Added
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

                _bulletinRepository.ApplyChanges(bullEvent, new List<IBaseIdEntity>());
            }

            _bulletinRepository.ApplyChanges(document, new List<IBaseIdEntity>());
            _bulletinRepository.ApplyChanges(documentContent, new List<IBaseIdEntity>());

            await _bulletinRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<PersonAliasDTO>> GetPersonAliasByBulletinIdAsync(string aId)
        {
            var result = await _bulletinRepository.SelectBullPersAliasByBulletinIdAsync(aId);
            return result.ProjectTo<PersonAliasDTO>(mapper.ConfigurationProvider);
        }

        public void UpdateDeleteDateData(BBulletin entity)
        {
            /// Destruction
            /// For criminal records - 100 years from the date of birth of the convicted person
            /// For bulletins for administrative sanctions under Art. 78a - 
            ///     15 years from the date of entry into force of the judicial act
            switch (entity.BulletinType)
            {
                case BulletinConstants.Type.ConvictionBulletin when entity.BirthDate.HasValue:
                    entity.DeleteDate = entity.BirthDate.Value.AddYears(100);
                    UpdateModifiedProperties(entity, nameof(entity.DeleteDate));
                    break;
                case BulletinConstants.Type.Bulletin78A when entity.DecisionFinalDate.HasValue:
                    entity.DeleteDate = entity.DecisionFinalDate.Value.AddYears(15);
                    UpdateModifiedProperties(entity, nameof(entity.DeleteDate));
                    break;
            }

            if (entity.DeleteDate.HasValue && entity.DeleteDate <= DateTime.Now)
            {
                entity.StatusId = Status.ForDestruction;
                UpdateModifiedProperties(entity, nameof(entity.StatusId));
            }
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
            await UpdateTransactionsAsync(aInDto, entity);

            CheckForBulletinStatusChange(entity, oldStatus);

            // it is locked each time
            // unless the statue is NewOffice or NewEISS
            if (entity.StatusId != Status.NewOffice)
            {
                entity.Locked = true;
                UpdateModifiedProperties(entity, nameof(entity.Locked));
            }

            if (entity.StatusId != Status.NewOffice && entity.StatusId != Status.NewEISS)
            {
                await SetDataForNationalitiesAsync(entity);
            }

            var passedNavigationProperties = new List<IBaseIdEntity>();
            _bulletinRepository.ApplyChanges(entity, passedNavigationProperties, true);
        }

        private void CheckForBulletinStatusChange(BBulletin entity, string oldStatus)
        {
            // Change the status of the bulletin depending on the added decision information
            // (ReplacedAct425, Rehabilitated)
            if (entity.BDecisions.Any(x => x.DecisionChTypeId == DecisionType.Rehabilitation))
            {
                entity.StatusId = Status.Rehabilitated;
            }

            if (entity.BDecisions.Any(x => x.DecisionChTypeId == DecisionType.JudicialAnnulment))
            {
                entity.StatusId = Status.ReplacedAct425;
            }

            UpdateDeleteDateData(entity);

            // if no sanction is selected
            // change bulletin status
            if (entity.NoSanction == true)
            {
                entity.StatusId = Status.NoSanction;
            }

            // save old status
            var isAddedHistory = AddBulletinStatusH(entity, oldStatus, entity.StatusId);
            if (isAddedHistory)
            {
                UpdateModifiedProperties(entity, nameof(entity.StatusId));
            }
        }

        private async Task UpdateRehabilitationAndEventDataAsync(BBulletin bulletin, PPerson person)
        {
            var pidsIds = person.PPersonIds.Select(x => x.Id).ToList();
            var allPersonBulletins = await _bulletinRepository.GetBulletinsByPidsIdAsync(pidsIds);

            // todo: call in one method and take only ones bulletins of a person
            // only apply changes to the context
            await _bulletinEventService.GenerateEventWhenChangeStatusOfBullAsync(bulletin, allPersonBulletins);
            _rehabilitationService.ApplyRehabilitationData(bulletin, allPersonBulletins);
            // save data in db
            await _bulletinRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Creates a person from a bulletin and a link between them
        /// </summary>
        private async Task<PPerson> CreatePersonFromBulletinAsync(BBulletin bulletin)
        {
            // when status is set to Active or NoSanction
            // save data for person and its identifiers

            // get person data from bulletin
            var personDto = mapper.Map<BBulletin, PersonDTO>(bulletin);
            // create person object, apply changes
            var person = await _managePersonService.CreatePersonAsync(personDto);

            // create PBulletinId for all pids (locally added and saved in db)

            foreach (var personIdObj in person.PPersonIds)
            {
                if (personIdObj.PidTypeId == PidType.Egn && bulletin.Egn == personIdObj.Pid)
                {
                    bulletin.ModifiedProperties.Add(nameof(bulletin.EgnId));
                    bulletin.EgnId = personIdObj.Id;
                }
                else if (personIdObj.PidTypeId == PidType.Lnch && bulletin.Lnch == personIdObj.Pid)
                {
                    bulletin.ModifiedProperties.Add(nameof(bulletin.LnchId));
                    bulletin.LnchId = personIdObj.Id;

                }
                else if (personIdObj.PidTypeId == PidType.Ln && bulletin.Ln == personIdObj.Pid)
                {
                    bulletin.ModifiedProperties.Add(nameof(bulletin.LnId));
                    bulletin.LnId = personIdObj.Id;

                }
                else if (personIdObj.PidTypeId == PidType.DocumentId && bulletin.IdDocNumber == personIdObj.Pid)
                {
                    bulletin.ModifiedProperties.Add(nameof(bulletin.IdDocNumberId));
                    bulletin.IdDocNumberId = personIdObj.Id;

                }
                else if (personIdObj.PidTypeId == PidType.Suid && (bulletin.Suid == personIdObj.Pid || string.IsNullOrEmpty(bulletin.Suid)))
                {
                    bulletin.ModifiedProperties.Add(nameof(bulletin.SuidId));
                    bulletin.ModifiedProperties.Add(nameof(bulletin.Suid));
                    bulletin.SuidId = personIdObj.Id;
                    bulletin.Suid = personIdObj.Pid;
                }

                _bulletinRepository.ApplyChanges(personIdObj);
            }

            bulletin.EntityState = EntityStateEnum.Modified;
            _bulletinRepository.ApplyChanges(bulletin);

            return person;
        }

        private static void SetModifiedPropertiesByStatus(BBulletin bulletinDb, BBulletin bulletin)
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
        /// If there is a change in the status of the bulletin is added to the history table
        /// </summary>
        /// <param name="oldStatus">Previous status</param>
        /// <param name="newStatus">New status</param>
        /// <param name="bulletinId">ID</param>
        public bool AddBulletinStatusH(BBulletin itemToBeUpdated, string oldStatus, string newStatus)
        {
            var statusAreEqueals = oldStatus == newStatus;
            var bulletinBeforeActive = oldStatus == Status.NewOffice || oldStatus == Status.NewEISS;
            if (statusAreEqueals && bulletinBeforeActive) return false;

            var statusHistory = new BBulletinStatusH
            {
                Id = Guid.NewGuid().ToString(),
                BulletinId = itemToBeUpdated.Id,
                OldStatusCode = oldStatus,
                NewStatusCode = newStatus,
                EntityState = EntityStateEnum.Added,
                Locked = newStatus != Status.NewOffice
            };

            var bulletinXmlModel = mapper.Map<BBulletin, BulletinType>(itemToBeUpdated);
            var xml = XmlUtils.SerializeToXml(bulletinXmlModel);
            statusHistory.Content = xml;
            statusHistory.Version = 1;

            _bulletinRepository.ApplyChanges(statusHistory, new List<IBaseIdEntity>());
            return true;
        }

        private async Task UpdateTransactionsAsync(BulletinBaseDTO aInDto, BBulletin entity)
        {
            entity.BOffences = mapper.MapTransactions<OffenceDTO, BOffence>(aInDto.OffancesTransactions);

            var deletedSanctionIds = aInDto.SanctionsTransactions
                    .Where(x => x.Type == TransactionTypesEnum.DELETE)
                    .Select(x => x.Id).ToList();

            var sanctions = await _bulletinRepository.GetDeletedSanctionsAsync(deletedSanctionIds);

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

        private void UpdateModifiedProperties(BaseEntity entityToSave, string nameOfProp)
        {
            entityToSave.ModifiedProperties ??= new List<string>();
            entityToSave.ModifiedProperties.Add(nameOfProp);
        }

        public void SetEcrisConvId(BBulletin bulletin)
        {
            int caseNum;
            try
            {
                if (string.IsNullOrEmpty(bulletin.CaseNumber))
                {
                    throw new Exception();
                }
                caseNum = Int32.Parse(bulletin.CaseNumber);
                if (caseNum > 99999)
                {
                    //������� �� ������ ����
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("������� �� �������� � � ������ ������.");
            }
            if (string.IsNullOrEmpty(bulletin.CsAuthorityId) || bulletin.CsAuthorityId.Length != 3)
            {
                throw new Exception("���������� ������������� �� ���� ��������.");
            }
            if (!bulletin.CaseYear.HasValue || bulletin.CaseYear > 9999 || bulletin.CaseYear < 1000)
            {
                throw new Exception("����������� ������.");
            }

            if (string.IsNullOrEmpty(bulletin.CaseTypeId))
            {

                throw new Exception("���������� ������������� �� ��� ����.");
            }

            //< select id = "case_id" > 
            // < option value = "01" > ���� </ option >  
            //  < option value = "02" > ���� </ option >   
            //   < option value = "03" > ��� </ option >    
            //    < option value = "04" > ���� </ option >     
            //     < option value = "05" > ����� </ option >      
            //      < option value = "06" > ����� </ option >       
            //       < option value = "07" > ���� </ option >        
            //        < option value = "08" > ����� </ option >         
            //         < option value = "09" > ����� </ option >          
            //          </ select >
            //            sign_naxd
            //sign_noxd
            //sign_ncd
            //sign_ncxd
            string caseTypeID;
            switch (bulletin.CaseTypeId)
            {
                //todo: �� �� ������� ��������������
                case "sign_noxd":
                    caseTypeID = "01";
                    break;
                case "sign_naxd":
                case "sign_and":
                    caseTypeID = "02";
                    break;
                case "sign_ncd":
                    caseTypeID = "03";
                    break;
                case "sign_ncxd":
                    caseTypeID = "04";
                    break;
                default:
                    throw new Exception("���������� ������������� �� ��� ����.");
            }
            bulletin.EcrisConvictionId = "BG-C-" + bulletin.CsAuthorityId + caseTypeID
             + "1" + Math.Truncate(bulletin.CaseYear.Value) + caseNum.ToString("D5");

            UpdateModifiedProperties(bulletin, nameof(bulletin.EcrisConvictionId));
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
            var personNat = bulletin.BPersNationalities.Where(x => x.EntityState != EntityStateEnum.Deleted);
            bulletin.BgCitizen = personNat.Any(x => x.CountryId == BG);
            UpdateModifiedProperties(bulletin, nameof(bulletin.BgCitizen));

            // if person is bulgarian citizen
            var skipEcris = personNat.Count() == 1 && personNat.First().CountryId == BG;

            if (skipEcris) return;

            var personNationalities = personNat.Select(x => x.CountryId).Where(x => x != BG);
            bool isEuCitizen = await _bulletinRepository.IsEuCitizen(personNationalities);

            if (isEuCitizen)
            {
                bulletin.EuCitizen = true;
                UpdateModifiedProperties(bulletin, nameof(bulletin.EuCitizen));
                SetEcrisConvId(bulletin);
            }

            var createEcrisTcn = personNationalities.Except(
               (await _bulletinRepository.FindAsync<EEcrisAuthority>(x => 1 == 1))
                //dbContext.EEcrisAuthorities.AsNoTracking()
                .Select(x => x.CountryId)).Any();

            if (createEcrisTcn)
            {
                bulletin.TcnCitizen = true;
                UpdateModifiedProperties(bulletin, nameof(bulletin.TcnCitizen));
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
            }
        }

        private bool IsBulletinUlockedAndPersonDataChanged(BBulletin bulletinFromDb, PersonDTO personFromForm)
        {
            if (bulletinFromDb.Egn != personFromForm.Egn) return true;
            if (bulletinFromDb.Lnch != personFromForm.Lnch) return true;
            if (bulletinFromDb.Ln != personFromForm.Ln) return true;
            if (bulletinFromDb.IdDocNumber != personFromForm.IdDocNumber) return true;

            return false;
        }

        #endregion
    }
}
