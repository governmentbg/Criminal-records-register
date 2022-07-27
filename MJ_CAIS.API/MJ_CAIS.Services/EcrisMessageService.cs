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
    public class EcrisMessageService : BaseAsyncService<EcrisMessageDTO, EcrisMessageDTO, EcrisMessageGridDTO, EEcrisMessage, string, CaisDbContext>, IEcrisMessageService
    {
        private const string EcrisFbbcMessageType = FbbcConstants.MessageType.CodeECRIS;
        private readonly IEcrisMessageRepository _ecrisMessageRepository;
        private readonly IBulletinRepository _bulletinRepository;
        private readonly IFbbcRepository _fbcRepository;
        private readonly INomenclatureDetailRepository _nomenclatureDetailRepository;
        private readonly IDDocumentRepository _dDocumentRepository;
        private readonly CaisDbContext _dbContext;


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

        public virtual async Task<IgPageResult<EcrisMessageGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<EcrisMessageGridDTO> aQueryOptions, string statusId)
        {
            var baseQuery = _ecrisMessageRepository.CustomGetAll().Where(x => x.EcrisMsgStatus == statusId);
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

            var result = _bulletinRepository.SelectAll()
                .Where(x => x.Egn == ecrisMessage.Identifier)
                .ProjectTo<BulletinGridDTO>(mapperConfiguration);

            return result;
        }

        public Task ChangeStatusAsync(string aInDto, string statusId)
        {
            throw new NotImplementedException();
        }

        public async Task<DDocument> GetEcrisDocumentByIdAsync(string ecrisMessageId)
        {
            var ecrisMsg = "0cfd5df5-a14c-436e-afa3-3bdce104bbd9"; // for test
            var ecrisMessage = await _dDocumentRepository.SelectByEcrisIdAsync(ecrisMsg);
            if (ecrisMessage == null)
            {
                throw new BusinessLogicException($"Не е намерен документ с ID: {ecrisMessage}");
            }

            XmlDocument doc = new XmlDocument();
            string xml = Encoding.UTF8.GetString(ecrisMessage.DocContent.Content);
            AbstractMessageType msg = XmlUtils.DeserializeXml<AbstractMessageType>(xml);
            if (ecrisMessage.DocTypeId == "EcrisRequest")
            {
                var requestMessage = (RequestMessageType)msg;


                var country = await _dbContext.GCountries.AsNoTracking().FirstOrDefaultAsync(x =>
                    x.Id ==
                    requestMessage.MessagePerson.PersonBirthPlace.PlaceCountryReference.Value);


                requestMessage.MessagePerson.PersonBirthPlace.PlaceCountryReference.Value = country.Name;
                var result = mapper.Map<EcrisRequestDTO>((RequestMessageType)msg);
            }
            if (ecrisMessage.DocTypeId == "EcrisNot")
            {
                var result = mapper.Map<EcrisRequestDTO>((NotificationMessageType)msg);
            }
            if (ecrisMessage.DocTypeId == "EcrisRes")
            {
                var result = mapper.Map<EcrisRequestDTO>((RequestResponseMessageType)msg);
            }

            //var result = _bulletinRepository.SelectAll()
            //    .Where(x => x.Egn == ecrisMessage.Identifier)
            //    .ProjectTo<BulletinGridDTO>(mapperConfiguration);

            return ecrisMessage;
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

            var result = _fbcRepository.SelectAll()
                .Where(x => x.DocTypeId == docType.Id && x.Egn == ecrisMessage.Identifier)
                .ProjectTo<FbbcGridDTO>(mapperConfiguration);

            return result;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
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

       

        public async Task IdentifyAsync(string aInDto, string graoPersonId)
        {
            var ecrisMessage = await _ecrisMessageRepository.SingleOrDefaultAsync<EEcrisMessage>(x => x.Id == aInDto);
                //await dbContext.EEcrisMessages
                //.FirstOrDefaultAsync(x => x.Id == aInDto);
            var ecrisIdentif = await _ecrisMessageRepository.SingleOrDefaultAsync<EEcrisIdentification>(x => x.EcrisMsgId == aInDto && x.GraoPersonId == graoPersonId);
            //await dbContext.EEcrisIdentifications
            //    .Where(x => x.EcrisMsgId == aInDto && x.GraoPersonId == graoPersonId)
            //    .FirstOrDefaultAsync();

            if (ecrisMessage == null)
            {
                throw new ArgumentException($"Ecris message with id: {aInDto} is missing");
            }

            ecrisMessage.EcrisMsgStatus = "Identified";
            ecrisIdentif.Approved = 1;

            await _ecrisMessageRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<GraoPersonGridDTO>> GetGraoPeopleAsync(string aId)
        {
            return await _ecrisMessageRepository.GetGraoPeopleAsync(aId);
        }
    }
}
