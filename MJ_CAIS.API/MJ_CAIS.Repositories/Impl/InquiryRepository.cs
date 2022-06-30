using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Inquiry;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Repositories.Impl;

namespace MJ_CAIS.Repositories
{
    public class InquiryRepository : BaseAsyncRepository<VBulletin, CaisDbContext>, IInquiryRepository
    {
        public InquiryRepository(CaisDbContext dbContext)
            : base(dbContext)
        {
        }

        public IQueryable<InquiryBulletinGridDTO> FilterBulletins(InquirySearchBulletinDTO searchParams)
        {
            var queryResult = ApplyFilters(searchParams);

            var result = from bulletin in _dbContext.VBulletins
                         join bulletinStatus in _dbContext.BBulletinStatuses on bulletin.StatusId equals bulletinStatus.Code
                         into bulletinStatusLeft
                         from bulletinStatus in bulletinStatusLeft.DefaultIfEmpty()
                         select new InquiryBulletinGridDTO
                         {
                             Id = bulletin.Id,
                             BulletinType = bulletin.BulletinType == BulletinConstants.Type.Bulletin78A ? BulletinResources.Bulletin78A :
                                            (bulletin.BulletinType == BulletinConstants.Type.ConvictionBulletin ? BulletinResources.ConvictionBulletin :
                                            BulletinResources.Unspecified),
                             Egn = bulletin.Egn,
                             FamilyName = bulletin.Familyname,
                             FirstName = bulletin.Firstname,
                             Ln = bulletin.Ln,
                             Lnch = bulletin.Lnch,
                             RegistrationNumber = bulletin.RegistrationNumber,
                             StatusId = bulletin.StatusId,
                             StatusName = bulletinStatus.Name,
                             SurName = bulletin.Surname,
                             Version = bulletin.Version,
                             CreatedOn = bulletin.CreatedOn,
                         };

            return result;
        }

        private IQueryable<VBulletin> ApplyFilters(InquirySearchBulletinDTO searchParams)
        {
            var bulletinsQuery = from bulletin in _dbContext.VBulletins select bulletin;

            if (!string.IsNullOrEmpty(searchParams.RegistrationNumber))
            {
                bulletinsQuery = bulletinsQuery.Where(x => x.RegistrationNumber == searchParams.RegistrationNumber);
            }

            if (!string.IsNullOrEmpty(searchParams.BulletinType))
            {
                bulletinsQuery = bulletinsQuery.Where(x => x.BulletinType == searchParams.BulletinType);
            }

            if (searchParams.BulletinReceivedDate.HasValue)
            {
                bulletinsQuery = bulletinsQuery.Where(x => x.BulletinReceivedDate == searchParams.BulletinReceivedDate);
            }

            if (!string.IsNullOrEmpty(searchParams.CaseTypeId))
            {
                bulletinsQuery = bulletinsQuery.Where(x => x.CaseTypeId == searchParams.CaseTypeId);
            }

            if (!string.IsNullOrEmpty(searchParams.CaseNumber))
            {
                bulletinsQuery = bulletinsQuery.Where(x => x.CaseNumber == searchParams.CaseNumber);
            }

            if (searchParams.CaseYear.HasValue)
            {
                bulletinsQuery = bulletinsQuery.Where(x => x.CaseYear == searchParams.CaseYear);
            }

            if (!string.IsNullOrEmpty(searchParams.DecidingAuthId))
            {
                bulletinsQuery = bulletinsQuery.Where(x => x.DecidingAuthId == searchParams.DecidingAuthId);
            }

            if (!string.IsNullOrEmpty(searchParams.DecisionNumber))
            {
                bulletinsQuery = bulletinsQuery.Where(x => x.DecisionNumber == searchParams.DecisionNumber);
            }

            if (searchParams.DecisionDate.HasValue)
            {
                bulletinsQuery = bulletinsQuery.Where(x => x.DecisionDate == searchParams.DecisionDate);
            }

            if (searchParams.DecisionFinalDate.HasValue)
            {
                bulletinsQuery = bulletinsQuery.Where(x => x.DecisionFinalDate == searchParams.DecisionFinalDate);
            }

            if (!string.IsNullOrEmpty(searchParams.DecisionTypeId))
            {
                bulletinsQuery = bulletinsQuery.Where(x => x.DecisionTypeId == searchParams.DecisionTypeId);
            }

            if (!string.IsNullOrEmpty(searchParams.StatusId))
            {
                bulletinsQuery = bulletinsQuery.Where(x => x.StatusId == searchParams.StatusId);
            }

            var queryResult = bulletinsQuery;

            if (!string.IsNullOrEmpty(searchParams.OffenceCatId))
            {
                queryResult = from bulletin in bulletinsQuery
                              join offence in _dbContext.VOffences on bulletin.Id equals offence.BulletinId
                                      into offenceLeft
                              from offence in offenceLeft.DefaultIfEmpty()
                              where offence.OffenceCatId == searchParams.OffenceCatId
                              select bulletin;
            }

            if (!string.IsNullOrEmpty(searchParams.SanctCategoryId) || searchParams.FineAmount.HasValue)
            {
                queryResult = from bulletin in bulletinsQuery
                              join sanction in _dbContext.VSanctions on bulletin.Id equals sanction.BulletinId
                                      into sanctionLeft
                              from sanction in sanctionLeft.DefaultIfEmpty()
                              where (sanction.SanctCategoryId == searchParams.SanctCategoryId || string.IsNullOrEmpty(searchParams.SanctCategoryId)) &&
                               (sanction.FineAmount == searchParams.FineAmount || !searchParams.FineAmount.HasValue)
                              select bulletin;
            }

            return queryResult;
        }
    }
}
