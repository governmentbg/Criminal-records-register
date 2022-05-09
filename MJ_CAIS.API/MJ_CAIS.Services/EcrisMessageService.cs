using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
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
            var baseQuery = this.CustomGetAll().Where(x => x.EcrisMsgStatus == statusId);
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

        private IQueryable<EcrisMessageGridDTO> CustomGetAll()
        {
            var result =
                from ecrisMsg in this.dbContext.EEcrisMessages.AsNoTracking()
                    
                join ecrisMsgStatus in this.dbContext.EEcrisMsgStatuses.AsNoTracking()
                    on ecrisMsg.EcrisMsgStatus equals ecrisMsgStatus.Code

                join doc in this.dbContext.DDocuments.AsNoTracking()
                    on ecrisMsg.Id equals doc.EcrisMsgId into doc_left
                from doc in doc_left.DefaultIfEmpty()

                join docType in this.dbContext.DDocTypes.AsNoTracking()
                    on doc.DocTypeId equals docType.Id into docType_left
                from docType in docType_left.DefaultIfEmpty()

                join birthCountry in this.dbContext.GCountries.AsNoTracking()
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
