using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.IsinData;
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

        public virtual async Task<IgPageResult<IsinDataGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<IsinDataGridDTO> aQueryOptions, string status)
        {
            // todo: remove
            //var entityQuery = this.GetSelectAllQueriable().Where(x => x.Status == status);

            var dbContext = _isinDataRepository.GetDbContext();

            var fullQuery = from isin in dbContext.EIsinData.AsNoTracking()
                            join isinMessage in dbContext.EWebRequests.AsNoTracking() on isin.WebRequestId equals isinMessage.Id
                                      into isinMessageLeft
                            from isinMessage in isinMessageLeft.DefaultIfEmpty()

                            join decisionTypes in dbContext.BDecisionTypes.AsNoTracking() on isin.DecisionTypeId equals decisionTypes.Code
                                        into isinDecisionLeft
                            from isinDecision in isinDecisionLeft.DefaultIfEmpty()

                            join caseTypes in dbContext.BCaseTypes.AsNoTracking() on isin.CaseTypeId equals caseTypes.Code
                            into isinCaseLeft
                            from isinCase in isinCaseLeft.DefaultIfEmpty()
                            where isin.Status == status
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
                                SanctionEndDate = isin.SanctionEndDate
                            };

            // todo: remove
            //var baseQuery = entityQuery.ProjectTo<IsinDataGridDTO>(mapperConfiguration);
            var resultQuery = await this.ApplyOData(fullQuery, aQueryOptions);
            var pageResult = new IgPageResult<IsinDataGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, fullQuery, resultQuery);
            return pageResult;
        }

        public override async Task<IsinDataDTO> SelectAsync(string aId)
        {
            var dbContext = _isinDataRepository.GetDbContext();

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
                        select new IsinDataDTO
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
                        };

            return await query.FirstOrDefaultAsync();
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
