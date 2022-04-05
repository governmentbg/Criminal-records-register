using AutoMapper;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.IsinData;
using MJ_CAIS.DTO.Shared;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
    public class IsinDataService : BaseAsyncService<IsinDataDTO, IsinDataDTO, IsinDataGridDTO, EIsinDatum, string, CaisDbContext>, IIsinDataService
    {
        private readonly IIsinDataRepository _isinDataRepository;

        public IsinDataService(IMapper mapper, IIsinDataRepository isinDataRepository)
            : base(mapper, isinDataRepository)
        {
            _isinDataRepository = isinDataRepository;
        }

        public virtual async Task<IgPageResult<IsinDataGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<IsinDataGridDTO> aQueryOptions, string? status, string? bulletinId)
        {
            var entityQuery = SelectAll(status, bulletinId);
            var resultQuery = await this.ApplyOData(entityQuery, aQueryOptions);
            var pageResult = new IgPageResult<IsinDataGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, entityQuery, resultQuery);
            return pageResult;
        }

        public override async Task<IsinDataDTO> SelectAsync(string aId)
        {
            return await SelectIsinDataAsync(aId);
        }

        public async Task<IgPageResult<IsinBulletinGridDTO>> SelectIsinBulletinAllWithPaginationAsync(ODataQueryOptions<IsinBulletinGridDTO> aQueryOptions)
        {
            var entityQuery = SelectAllBulletin();
            var resultQuery = await this.ApplyOData(entityQuery, aQueryOptions);
            var pageResult = new IgPageResult<IsinBulletinGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, entityQuery, resultQuery);
            return pageResult;
        }

        public async Task SelectBulletinAsync(string aInDto, string bulletinId)
        {
            var hasBulletin = await dbContext.BBulletins
               .AnyAsync(x => x.Id == bulletinId);

            if (!hasBulletin)
                throw new ArgumentException($"Bulletin with id: {bulletinId} is missing");

            var isinData = await dbContext.EIsinData
               .FirstOrDefaultAsync(x => x.Id == aInDto);

            if (isinData == null)
                throw new ArgumentException($"Isin message with id: {aInDto} is missing");

            isinData.Status = IsinDataConstants.Status.Identified;
            isinData.BulletinId = bulletinId;
            await dbContext.SaveChangesAsync();
        }

        public async Task<IsinDataPreviewDTO> SelectForPreviewAsync(string aIdDto)
        {
            var isin = await SelectIsinDataAsync(aIdDto);

            if (isin == null) return null;

            var bulleint = await dbContext.BBulletins.AsNoTracking()
                 .Include(x => x.BirthCountry)
                 .Include(x => x.BirthCity)
                 .Include(x => x.BirthCity)
                     .ThenInclude(x => x.Municipality)
                         .ThenInclude(x => x.District)
                 .Include(x => x.DecidingAuth)
                 .Include(x => x.DecisionType)
                 .Include(x => x.BPersNationalities)
                     .ThenInclude(x => x.Country)
                 .Include(x => x.BBullPersAliases)
            .FirstOrDefaultAsync(x => x.Id == isin.BulletinId);

            if (bulleint == null) return null;

            isin.BulletinPersonInfo = mapper.Map<BulletinPersonInfoModelDTO>(bulleint);
            return isin;
        }

        public async Task CloseAsync(string aInDto)
        {
            var isinData = await dbContext.EIsinData
               .FirstOrDefaultAsync(x => x.Id == aInDto);

            if (isinData == null)
                throw new ArgumentException($"Isin message with id: {aInDto} is missing");

            isinData.Status = IsinDataConstants.Status.Closed;
            await dbContext.SaveChangesAsync();
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        #region Helper methods 

        private IQueryable<IsinDataGridDTO> SelectAll(string? status, string? bulletinId)
        {
            var query = from isin in dbContext.EIsinData.AsNoTracking()
                        join isinMessage in dbContext.EWebRequests.AsNoTracking() on isin.WebRequestId equals isinMessage.Id
                                  into isinMessageLeft
                        from isinMessage in isinMessageLeft.DefaultIfEmpty()

                        join decisionTypes in dbContext.BDecisionTypes.AsNoTracking() on isin.DecisionTypeId equals decisionTypes.Code
                                    into isinDecisionLeft
                        from isinDecision in isinDecisionLeft.DefaultIfEmpty()

                        join caseTypes in dbContext.BCaseTypes.AsNoTracking() on isin.CaseTypeId equals caseTypes.Code
                        into isinCaseLeft
                        from isinCase in isinCaseLeft.DefaultIfEmpty()

                        where (isin.BulletinId == bulletinId && !string.IsNullOrEmpty(bulletinId)) || // ������ �� ������� ��� ������� �� ������� �� �������
                           (isin.Status == status && !string.IsNullOrEmpty(status)) //������ �� ������ � ���������� ����� ������ �� ������� �����������

                        select new IsinDataGridDTO
                        {
                            Id = isin.Id,
                            MsgDateTime = isinMessage.ExecutionDate,
                            BirthCountryName = isin.BirthcountryName,
                            BirthDate = isin.Birthdate,
                            BulletinId = isin.BulletinId,
                            CaseInfo = isinCase.Name + " " + isin.CaseNumber + " " + isin.CaseYear + " " + isin.CaseAuthName,
                            DecisionInfo = isinDecision.Name + " " + isin.DecisionNumber + " " + isin.DecisionDate + " " + isin.DecisionAuthName,
                            Identifier = isin.Identifier,
                            Nationalities = isin.Country1Name + " " + isin.Country2Name,
                            PersonName = isin.Firstname + " " + isin.Surname + " " + isin.Familyname,
                            SanctionEndDate = isin.SanctionEndDate,
                            SanctionStartDate = isin.SanctionStartDate,
                        };

            return query;
        }

        private async Task<IsinDataPreviewDTO> SelectIsinDataAsync(string aId)
        {
            var query = from isin in dbContext.EIsinData.AsNoTracking()
                        join isinMessage in dbContext.EWebRequests.AsNoTracking() on isin.WebRequestId equals isinMessage.Id
                                  into isinMessageLeft
                        from isinMessage in isinMessageLeft.DefaultIfEmpty()

                        join decisionTypes in dbContext.BDecisionTypes.AsNoTracking() on isin.DecisionTypeId equals decisionTypes.Code
                                    into isinDecisionLeft
                        from isinDecision in isinDecisionLeft.DefaultIfEmpty()

                        join caseTypes in dbContext.BCaseTypes.AsNoTracking() on isin.CaseTypeId equals caseTypes.Code
                        into isinCaseLeft
                        from isinCase in isinCaseLeft.DefaultIfEmpty()
                        where isin.Id == aId
                        select new IsinDataPreviewDTO
                        {
                            Id = isin.Id,
                            MsgDateTime = isinMessage.ExecutionDate,
                            Status = isin.Status,
                            Identifier = isin.Identifier,
                            FirstName = isin.Firstname,
                            SurName = isin.Surname,
                            FamilyName = isin.Familyname,
                            BirthDate = isin.Birthdate,
                            Sex = isin.Sex,
                            Country1Name = isin.Country1Name,
                            Country2Name = isin.Country2Name,
                            BirthCountryName = isin.BirthcountryName,
                            BirthPlace = isin.BirthPlace,
                            DecisionType = isinDecision.Name,
                            DecisionNumber = isin.DecisionNumber,
                            DecisionDate = isin.DecisionDate,
                            DecisionFinalDate = isin.DecisionFinalDate,
                            DecisionAuthName = isin.DecisionAuthName,
                            CaseType = isinCase.Name,
                            CaseNumber = isin.CaseNumber,
                            CaseYear = isin.CaseYear,
                            CaseAuthName = isin.CaseAuthName,
                            SanctionStartDate = isin.SanctionStartDate,
                            SanctionEndDate = isin.SanctionEndDate,
                            BulletinId = isin.BulletinId,
                        };

            return await query.FirstOrDefaultAsync();
        }

        private IQueryable<IsinBulletinGridDTO> SelectAllBulletin()
        {
            return dbContext.BBulletins.AsNoTracking()
                .Include(x => x.DecidingAuth)
                .Include(x => x.DecisionType)
                .Include(x => x.BirthCountry)
                .Include(x => x.BirthCity)
                .Include(x => x.BPersNationalities).ThenInclude(x => x.Country)
                   .Select(x => new IsinBulletinGridDTO
                   {
                       Id = x.Id,
                       BulletinType = x.BulletinType == nameof(BulletinConstants.Type.Bulletin78A) ? BulletinConstants.Type.Bulletin78A :
                            (x.BulletinType == nameof(BulletinConstants.Type.ConvictionBulletin) ? BulletinConstants.Type.ConvictionBulletin :
                            BulletinConstants.Type.Unspecified),
                       RegistrationNumber = x.RegistrationNumber,
                       BirthDate = x.BirthDate,
                       Identifier = x.Egn + " " + x.Lnch + " " + x.Ln,
                       Nationalities = x.BPersNationalities.Select(x => x.Country.Name), // todo: filter
                       PersonName = x.Firstname + " " + x.Surname + " " + x.Familyname,
                       DecisionType = x.DecisionType.Name,
                       DecisionNumber = x.DecisionNumber,
                       DecisionDate = x.DecisionDate,
                       DecisionAuthName = x.DecidingAuth.Name,
                       CaseNumber = x.CaseNumber,
                       BirthPlace = x.BirthCountry.Name + " " + x.BirthCity.Name
                   });
        }

        #endregion
    }
}
