using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.AbstractMessageType;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.EcrisMessage;
using MJ_CAIS.DTO.EcrisService;
using MJ_CAIS.DTO.Fbbc;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;
using System.Text;
using System.Xml;

namespace MJ_CAIS.Services
{
    public class EcrisMessageService :
        BaseAsyncService<EcrisMessageDTO, EcrisMessageDTO, EcrisMessageGridDTO, EEcrisMessage, string, CaisDbContext>,
        IEcrisMessageService
    {
        private const string EcrisFbbcMessageType = FbbcConstants.MessageType.CodeECRIS;
        private readonly IBulletinRepository _bulletinRepository;
        private readonly CaisDbContext _dbContext;
        private readonly IDDocumentRepository _dDocumentRepository;
        private readonly IEcrisMessageRepository _ecrisMessageRepository;
        private readonly IFbbcRepository _fbcRepository;
        private readonly INomenclatureDetailRepository _nomenclatureDetailRepository;


        public EcrisMessageService(IMapper mapper,
            IEcrisMessageRepository ecrisMessageRepository,
            IBulletinRepository bulletinRepository,
            IFbbcRepository fbbcRepository,
            INomenclatureDetailRepository nomenclatureDetailRepository,
            IDDocumentRepository dDocumentRepository, CaisDbContext dbContext) : base(mapper, ecrisMessageRepository)
        {
            _ecrisMessageRepository = ecrisMessageRepository;
            _dbContext = dbContext;
            _bulletinRepository = bulletinRepository;
            _fbcRepository = fbbcRepository;
            _nomenclatureDetailRepository = nomenclatureDetailRepository;
            _dDocumentRepository = dDocumentRepository;
        }

        public virtual async Task<IgPageResult<EcrisMessageGridDTO>> SelectAllWithPaginationAsync(
            ODataQueryOptions<EcrisMessageGridDTO> aQueryOptions, string statusId)
        {
            var baseQuery = CustomGetAll().Where(x => x.EcrisMsgStatus == statusId);
            var resultQuery = await ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<EcrisMessageGridDTO>();
            PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public async Task<IQueryable<BulletinGridDTO>> GetEcrisBulletinsByIdAsync(string ecrisMessageId)
        {
            var ecrisMessage = await SelectAsync(ecrisMessageId);
            if (ecrisMessage == null)
            {
                return new List<BulletinGridDTO>().AsQueryable();
            }

            var result = _bulletinRepository.SelectAll()
                .Where(x => x.Egn == ecrisMessage.Identifier)
                .ProjectTo<BulletinGridDTO>(mapperConfiguration);

            return result;
        }

        public Task ChangeStatusAsync(string aInDto, string statusId)
        {
            throw new NotImplementedException();
        }

        public async Task<EcrisRequestDTO> GetEcrisDocumentByIdAsync(string ecrisMessageId)
        {
            var ecrisMsg = "0cfd5df5-a14c-436e-afa3-3bdce104bbd9"; // for test
            var ecrisMessage = await _dDocumentRepository.SelectByEcrisIdAsync(ecrisMessageId);
            if (ecrisMessage == null)
            {
                throw new BusinessLogicException($"Не е намерен документ с ID: {ecrisMessage}");
            }

            var doc = new XmlDocument();
            var xml = Encoding.UTF8.GetString(ecrisMessage.DocContent.Content);
            var msg = XmlUtils.DeserializeXml<AbstractMessageType>(xml);
            EcrisRequestDTO result = new EcrisRequestDTO();

            if (ecrisMessage.DocTypeId == "EcrisRequest")
            {
                var requestMessage = (RequestMessageType)msg;

                await getRequestingAuthorityByCode(requestMessage);
                await getPersonCountryByCode(requestMessage);
                await getRequestPurposeCategoryByCode(requestMessage);
                var memberState = await getSendingMemberStateName(requestMessage);
                var receivingMemberState = await getReceivingMemberStateName(requestMessage);


                result = mapper.Map<EcrisRequestDTO>((RequestMessageType)msg);
                result.SendingMemberState = memberState.Name; //can't be done in mapper
                result.ReceivingMemberState = receivingMemberState.Name; //can't be done in mapper
            }

            if (ecrisMessage.DocTypeId == "EcrisNot")
            {
                result = mapper.Map<EcrisRequestDTO>((NotificationMessageType)msg);

            }

            if (ecrisMessage.DocTypeId == "EcrisRes")
            {
                result = mapper.Map<EcrisRequestDTO>((RequestResponseMessageType)msg);
            }

            //var result = _bulletinRepository.SelectAll()
            //    .Where(x => x.Egn == ecrisMessage.Identifier)
            //    .ProjectTo<BulletinGridDTO>(mapperConfiguration);

            return result;
        }



        public async Task<IQueryable<FbbcGridDTO>> GetEcrisFbbcsByIdAsync(string ecrisMessageId)
        {
            var ecrisMessage = await SelectAsync(ecrisMessageId);
            if (ecrisMessage == null)
            {
                return new List<FbbcGridDTO>().AsQueryable();
            }

            var docType = _nomenclatureDetailRepository.GetAllFbbcDocTypes()
                .FirstOrDefault(x => x.Code == EcrisFbbcMessageType);

            var result = _fbcRepository.SelectAll()
                .Where(x => x.DocTypeId == docType.Id && x.Egn == ecrisMessage.Identifier)
                .ProjectTo<FbbcGridDTO>(mapperConfiguration);

            return result;
        }

        public async Task<IQueryable<EcrisMsgNationalityDTO>> GetNationalitiesAsync(string aId)
        {
            var nationalities = await _ecrisMessageRepository.SelectAllNationalitiesAsync();
            var filteredNationalities = nationalities.Where(x => x.EEcrisMsgId == aId);
            return filteredNationalities.ProjectTo<EcrisMsgNationalityDTO>(mapperConfiguration);
        }

        public async Task<IQueryable<EcrisMsgNameDTO>> GetNamesAsync(string aId)
        {
            var names = await _ecrisMessageRepository.SelectAllNamesAsync();
            var filteredNames = names.Where(x => x.EEcrisMsgId == aId);
            return filteredNames.ProjectTo<EcrisMsgNameDTO>(mapperConfiguration);
        }

        public async Task<IQueryable<GraoPersonGridDTO>> GetGraoPeopleAsync(string aId)
        {
            var result =
                from ecrisIdentif in dbContext.EEcrisIdentifications.AsNoTracking()
                join ecrisMsg in dbContext.EEcrisMessages.AsNoTracking()
                    on ecrisIdentif.EcrisMsgId equals ecrisMsg.Id
                join graoPers in dbContext.GraoPeople.AsNoTracking()
                    on ecrisIdentif.GraoPersonId equals graoPers.Id
                select new GraoPersonGridDTO
                {
                    Id = graoPers.Id,
                    Egn = graoPers.Egn,
                    Firstname = graoPers.Firstname,
                    Surname = graoPers.Surname,
                    Familyname = graoPers.Familyname,
                    BirthDate = graoPers.BirthDate,
                    Sex = graoPers.Sex
                };
            return result;
        }

        public async Task IdentifyAsync(string aInDto, string graoPersonId)
        {
            var ecrisMessage = await dbContext.EEcrisMessages
                .FirstOrDefaultAsync(x => x.Id == aInDto);
            var ecrisIdentif = await dbContext.EEcrisIdentifications
                .Where(x => x.EcrisMsgId == aInDto && x.GraoPersonId == graoPersonId)
                .FirstOrDefaultAsync();

            if (ecrisMessage == null)
            {
                throw new ArgumentException($"Ecris message with id: {aInDto} is missing");
            }

            ecrisMessage.EcrisMsgStatus = "Identified";
            ecrisIdentif.Approved = 1;

            await dbContext.SaveChangesAsync();
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
        private async Task<EEcrisAuthority> getSendingMemberStateName(RequestMessageType requestMessage)
        {
            EEcrisAuthority? memberState = null;
            if (requestMessage.MessageSendingMemberState != null)
            {
                memberState =
                    await
                        _dbContext.EEcrisAuthorities
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x =>
                                x.MemberStateCode == (requestMessage.MessageSendingMemberState.ToString()));
            }

            return memberState;
        }

        private async Task<EEcrisAuthority> getReceivingMemberStateName(RequestMessageType requestMessage)
        {
            EEcrisAuthority? memberState = null;
            if (requestMessage.MessageSendingMemberState != null)
            {
                memberState =
                    await
                        _dbContext.EEcrisAuthorities
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x =>
                                x.MemberStateCode == (requestMessage.MessageReceivingMemberState[0].ToString()));
            }

            return memberState;
        }

        private async Task getRequestPurposeCategoryByCode(RequestMessageType requestMessage)
        {
            if (requestMessage.RequestMessageRequestPurposeCategoryReference.Value != null)
            {
                var requestPurposeCategory =
                    await
                        _dbContext.EEcrisNomenclatures
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x =>
                                x.EcrisTechId == requestMessage.RequestMessageRequestPurposeCategoryReference.Value);

                if (requestPurposeCategory != null)
                {
                    requestMessage.RequestMessageRequestPurposeCategoryReference.Value = requestPurposeCategory.NameBg;
                }
            }
        }

        private async Task getPersonCountryByCode(RequestMessageType requestMessage)
        {
            if (requestMessage.MessagePerson.PersonBirthPlace.PlaceCountryReference.Value != null)
            {
                var country = await _dbContext.GCountries.AsNoTracking().FirstOrDefaultAsync(x =>
                    x.Id ==
                    requestMessage.MessagePerson.PersonBirthPlace.PlaceCountryReference.Value);

                if (country != null)
                {
                    requestMessage.MessagePerson.PersonBirthPlace.PlaceCountryReference.Value = country.Name;
                }
            }
        }

        private async Task getRequestingAuthorityByCode(RequestMessageType requestMessage)
        {
            if (requestMessage.RequestMessageRequestingAuthority.RequestingAuthorityTypeReference.Value != null)
            {
                var authorityType =
                    await
                        _dbContext.EEcrisNomenclatures
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x =>
                                x.EcrisTechId == requestMessage.RequestMessageRequestingAuthority
                                    .RequestingAuthorityTypeReference.Value);
                if (authorityType != null)
                {
                    requestMessage.RequestMessageRequestingAuthority
                        .RequestingAuthorityTypeReference.Value = authorityType.NameBg;
                }
            }
        }
        private IQueryable<EcrisMessageGridDTO> CustomGetAll()
        {
            var result =
                from ecrisMsg in dbContext.EEcrisMessages.AsNoTracking()
                join ecrisMsgStatus in dbContext.EEcrisMsgStatuses.AsNoTracking()
                    on ecrisMsg.EcrisMsgStatus equals ecrisMsgStatus.Code
                join doc in dbContext.DDocuments.AsNoTracking()
                    on ecrisMsg.Id equals doc.EcrisMsgId into doc_left
                from doc in doc_left.DefaultIfEmpty()
                join docType in dbContext.DDocTypes.AsNoTracking()
                    on doc.DocTypeId equals docType.Id into docType_left
                from docType in docType_left.DefaultIfEmpty()
                join birthCountry in dbContext.GCountries.AsNoTracking()
                    on ecrisMsg.BirthCountry equals birthCountry.Id into birthCountry_left
                from birthCountry in birthCountry_left.DefaultIfEmpty()

                    // join nationality1 in this.dbContext.GCountries.AsNoTracking()
                    //    on ecrisMsg.Nationality1Code equals nationality1.Id into nationality1_left
                    //from nationality1 in nationality1_left.DefaultIfEmpty()

                    // join nationality2 in this.dbContext.GCountries.AsNoTracking()
                    //    on ecrisMsg.Nationality2Code equals nationality2.Id into nationality2_left
                    //from nationality2 in nationality2_left.DefaultIfEmpty()
                select new EcrisMessageGridDTO
                {
                    Id = ecrisMsg.Id,
                    DocTypeId = doc.DocTypeId,
                    DocTypeName = docType.Name,
                    Identifier = ecrisMsg.Identifier,
                    EcrisIdentifier = ecrisMsg.EcrisIdentifier,
                    MsgTimestamp = ecrisMsg.MsgTimestamp,
                    EcrisMsgStatus = ecrisMsg.EcrisMsgStatus,
                    EcrisMsgStatusName = ecrisMsgStatus.Name,
                    BirthDate = ecrisMsg.BirthDate,
                    BirthCountry = ecrisMsg.BirthCountry,
                    BirthCountryName = birthCountry.Name,
                    BirthCity = ecrisMsg.BirthCity,
                    CreatedOn = ecrisMsg.CreatedOn
                    //Firstname = ecrisMsg.Firstname,
                    //Surname = ecrisMsg.Surname,
                    // Familyname = ecrisMsg.Familyname,
                    //Nationality1Code = ecrisMsg.Nationality1Code,
                    //Nationality1Name = nationality1.Name,
                    //Nationality2Code = ecrisMsg.Nationality2Code,
                    //Nationality2Name = nationality2.Name,
                };

            return result;
        }
    }
}