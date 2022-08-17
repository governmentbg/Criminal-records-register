using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DTO.Home;
using static MJ_CAIS.Common.Constants.ECRISConstants;
using MJ_CAIS.DTO.EcrisMessage;

namespace MJ_CAIS.Repositories.Impl
{
    public class EcrisMessageRepository : BaseAsyncRepository<EEcrisMessage, CaisDbContext>, IEcrisMessageRepository
    {
        public EcrisMessageRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<ObjectStatusCountDTO> GetStatusCount()
        {
            var query = _dbContext.EEcrisMessages.AsNoTracking()
                 .Where(x => x.EcrisMsgStatus == EcrisMessageStatuses.ForIdentification ||
                             x.EcrisMsgStatus == EcrisMessageStatuses.ReqWaitingForCSAuthority)
                .GroupBy(x => x.EcrisMsgStatus)
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count()
                });

            return query;
        }

        public async Task<IQueryable<EEcrisMsgNationality>> SelectAllNationalitiesAsync()
        {
            var query = _dbContext.EEcrisMsgNationalities
                 .AsNoTracking()
                 .Include(x => x.Country);

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<EEcrisMsgName>> SelectAllNamesAsync()
        {
            var query = _dbContext.EEcrisMsgNames
                 .AsNoTracking();

            return await Task.FromResult(query);
        }

        public IQueryable<EcrisMessageGridDTO> CustomGetAll()
        {
            var result =
                from ecrisMsg in _dbContext.EEcrisMessages.AsNoTracking()

                join ecrisMsgStatus in _dbContext.EEcrisMsgStatuses.AsNoTracking()
                    on ecrisMsg.EcrisMsgStatus equals ecrisMsgStatus.Code

                join doc in _dbContext.DDocuments.AsNoTracking()
                    on ecrisMsg.Id equals doc.EcrisMsgId into doc_left
                from doc in doc_left.DefaultIfEmpty()

                join docType in _dbContext.DDocTypes.AsNoTracking()
                    on doc.DocTypeId equals docType.Id into docType_left
                from docType in docType_left.DefaultIfEmpty()

                join birthCountry in _dbContext.GCountries.AsNoTracking()
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

        public IQueryable<GraoPersonGridDTO> GetEcrisIdentifiedPeople(string aId)
        {
            var result =
                from ecrisIdentif in _dbContext.EEcrisIdentifications.AsNoTracking()

                join ecrisMsg in _dbContext.EEcrisMessages.AsNoTracking()
                    on ecrisIdentif.EcrisMsgId equals ecrisMsg.Id
                join graoPers in _dbContext.GraoPeople.AsNoTracking()
                    on ecrisIdentif.GraoPersonId equals graoPers.Id

                where ecrisIdentif.EcrisMsgId == aId
                select new GraoPersonGridDTO
                {
                    Id = graoPers.Id,
                    Identifier = graoPers.Egn,
                    FirstName = graoPers.Firstname,
                    SurName = graoPers.Surname,
                    FamilyName = graoPers.Familyname,
                    BirthDate = graoPers.BirthDate,
                    Sex = graoPers.Sex
                };

            return result;
        }

        public IQueryable<EEcrisIdentification> GetEcrisIdentificationsById(string aId)
        {
            var result = _dbContext.EEcrisIdentifications.AsNoTracking()
                .Where(x => x.EcrisMsgId == aId);

            return result;
        }
    }
}
