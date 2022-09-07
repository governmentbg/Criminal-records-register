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
        private readonly ICertificateRepository _certificateRepository;

        public BulletinService(IMapper mapper,
            IBulletinRepository bulletinRepository,
            INotificationService notificationService,
            IBulletinEventService bulletinEventService,
            IRehabilitationService rehabilitationService,
            IUserContext userContext,
            ICertificateRepository _certificateRepository,
            IRegisterTypeService registerTypeService,
            IManagePersonService managePersonService)
            : base(mapper, bulletinRepository)
        {
            _bulletinRepository = bulletinRepository;
            _notificationService = notificationService;
            _bulletinEventService = bulletinEventService;
            _rehabilitationService = rehabilitationService;
            _userContext = userContext;
            this._certificateRepository = _certificateRepository;
            _registerTypeService = registerTypeService;
            _managePersonService = managePersonService;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

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

        public async Task<IQueryable<BulletinConvictionDTO>> GetConvictionOnlyAsync(string aId)
        {
            var query = await _certificateRepository.GetBulletinsCheckByIdAsync(aId, false);
            var bulletins = query.Select(x => x.Bulletin);
            var result = mapper.ProjectTo<BulletinConvictionDTO>(bulletins, mapperConfiguration);

            return result;
        }

        public async Task<IgPageResult<BulletinGridDTO>> SearchBulletinAsync(ODataQueryOptions<BulletinGridDTO> aQueryOptions, BulletinSearchParamDTO searchParams)
        {
            var baseQuery = _bulletinRepository.SearchBulletins(searchParams);

            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<BulletinGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public async Task<List<BulletinGridDTO>> ExportAllAsync(ODataQueryOptions<BulletinGridDTO> aQueryOptions, BulletinSearchParamDTO searchParams)
        {
            var baseQuery = _bulletinRepository.SearchBulletins(searchParams);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var result = resultQuery.ToList();
            return result;
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

            // when status is changed
            if (bulletin.StatusId != Status.NewOffice)
            {
                var person = await CreatePersonFromBulletinAsync(bulletin);
                await UpdateRehabilitationAndEventDataAsync(bulletin, person);

                if (bulletin.TcnCitizen == true)
                {
                    UpdateEcrisTCN(bulletin.Id, null, bulletin.StatusId);
                }
            }

            await _bulletinRepository.SaveChangesAsync();
            if (bulletin.EuCitizen == true && bulletin.StatusId != Status.NoSanction && bulletin.StatusId != Status.NewOffice)
            {
                await this._notificationService.CreateNotificationFromBulletin(bulletin.Id);
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
        public async Task UpdateAsync(BulletinEditDTO aInDto, bool isFinalEdit)
        {
            var bulletinDb = await _bulletinRepository.SingleOrDefaultAsync<BBulletin>(x => x.Id == aInDto.Id);

            if (bulletinDb == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.bulletinDoesNotExist, aInDto.Id));

            if (bulletinDb.CsAuthorityId != _userContext.CsAuthorityId)
                throw new BusinessLogicException(BusinessLogicExceptionResources.editIsUnauthorized);

            var oldBulletinStatus = bulletinDb.StatusId;
            var bulletinToUpdate = mapper.MapToEntity<BulletinEditDTO, BBulletin>(aInDto, false);
            bulletinToUpdate.CsAuthorityId = bulletinDb.CsAuthorityId;
            if (isFinalEdit)
            {
                bulletinToUpdate.StatusId = Status.Active;
            }

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

            // person must be created (normal flow)
            var oldStatusIsNewBull = oldBulletinStatus == null || oldBulletinStatus == Status.NewEISS || oldBulletinStatus == Status.NewOffice;
            var newStatusForUpdatePerson = bulletinToUpdate.StatusId != Status.NewEISS && bulletinToUpdate.StatusId != Status.NewOffice;
            var normalFlowForCreatePerson = oldStatusIsNewBull && newStatusForUpdatePerson;

            var newSuid = await _managePersonService.GenerateSuidAsync(aInDto.Person);

            // when bulletin is unlocked and person data is changed
            var changesOnPersonData = bulletinDb.Locked == false &&
                newStatusForUpdatePerson &&
                (IsBulletinUlockedAndPersonDataChanged(bulletinDb, aInDto.Person) ||
                bulletinDb.Suid != newSuid);

            if (normalFlowForCreatePerson || changesOnPersonData)
            {
                var person = await CreatePersonFromBulletinAsync(bulletinToUpdate);
                await UpdateRehabilitationAndEventDataAsync(bulletinToUpdate, person);
            }

            if (bulletinToUpdate.TcnCitizen == true)
            {
                UpdateEcrisTCN(bulletinToUpdate.Id, oldBulletinStatus, bulletinToUpdate.StatusId);
            }

            // todo: use transaction 
            await _bulletinRepository.SaveChangesAsync();
            if (bulletinToUpdate.EuCitizen == true && bulletinToUpdate.StatusId != Status.NoSanction)
            {
                await this._notificationService.CreateNotificationFromBulletin(bulletinToUpdate.Id);
            }
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
                await _bulletinRepository.SaveEntityAsync(bulletin, true);
                return;
            }

            // All active bulletins are locked for editing
            // only decisions can be added

            await SetDataForNationalitiesAsync(bulletin);
            await _bulletinRepository.SaveChangesAsync();

            // create person
            var person = await CreatePersonFromBulletinAsync(bulletin);
            await UpdateRehabilitationAndEventDataAsync(bulletin, person);

            if (bulletin.TcnCitizen == true)
            {
                UpdateEcrisTCN(bulletin.Id, oldBulletinStatus, bulletin.StatusId);
            }

            // todo: use transaction
            await _bulletinRepository.SaveChangesAsync();
            if (bulletin.EuCitizen == true)
            {
                await this._notificationService.CreateNotificationFromBulletin(bulletin.Id);
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
            var allPersonBulletinsExcludeCurrent = await _bulletinRepository.GetBulletinsByPidsIdExcludeCurrentAsync(pidsIds, bulletin.Id);

            await _bulletinEventService.GenerateEventWhenChangeStatusOfBullAsync(bulletin, allPersonBulletinsExcludeCurrent);
            _rehabilitationService.ApplyRehabilitationData(bulletin, allPersonBulletinsExcludeCurrent);
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
            personDto.TableName = ContextTable.Bulletins;
            personDto.TableId = bulletin.Id;

            var person = await _managePersonService.CreatePersonAsync(personDto);

            _managePersonService.UpdatePidDataData(person.PPersonIds, bulletin);
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
                NewStatusCode = newStatus,
                OldStatusCode = oldStatus,
                EntityState = EntityStateEnum.Added,
                Locked = newStatus != Status.NewOffice,
                Version = 1
            };

            // do not save xml for deleted bulletin
            if (newStatus != Status.Deleted)
            {
                var bulletinXmlModel = mapper.Map<BBulletin, BulletinType>(itemToBeUpdated);
                var xml = XmlUtils.SerializeToXml(bulletinXmlModel);
                statusHistory.Content = xml;
                statusHistory.HasContent = true;
            }

            itemToBeUpdated.BBulletinStatusHes ??= new List<BBulletinStatusH>();
            itemToBeUpdated.BBulletinStatusHes.Add(statusHistory);
            _bulletinRepository.ApplyChanges(statusHistory);
            return true;
        }

        private async Task UpdateTransactionsAsync(BulletinBaseDTO aInDto, BBulletin entity)
        {
            entity.BOffences = mapper.MapTransactions<OffenceDTO, BOffence>(aInDto.OffancesTransactions);
            entity.BDecisions = mapper.MapTransactions<DecisionDTO, BDecision>(aInDto.DecisionsTransactions);
            entity.BBullPersAliases = mapper.MapTransactions<PersonAliasDTO, BBullPersAlias>(aInDto.Person.PersonAliasTransactions);
            entity.BPersNationalities = CaisMapper.MapMultipleChooseToEntityList<BPersNationality, string, string>(aInDto.Person.Nationalities, nameof(BPersNationality.Id), nameof(BPersNationality.CountryId));

            if (aInDto.NoSanction == true)
            {
                var bulletinSanctions = await _bulletinRepository.GetBulletinSanctionsAsync(entity.Id);
                foreach (var item in bulletinSanctions)
                {
                    item.EntityState = EntityStateEnum.Deleted;
                }
                entity.BSanctions = bulletinSanctions;

                return;
            }

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

        private void UpdateEcrisTCN(string bulletinId, string bOldStatus, string bNewStatus)
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
