using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Inquiry;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Repositories.Impl;
using static MJ_CAIS.Common.Constants.GlobalConstants;

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
            var bulletinsQuery = from bulletin in _dbContext.VBulletins select bulletin;

            var queryResult = ApplyFilters(bulletinsQuery, searchParams);

            var result = from bulletin in queryResult
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
                             Familyname = bulletin.Familyname,
                             Firstname = bulletin.Firstname,
                             Ln = bulletin.Ln,
                             Lnch = bulletin.Lnch,
                             RegistrationNumber = bulletin.RegistrationNumber,
                             StatusId = bulletin.StatusId,
                             StatusName = bulletinStatus.Name,
                             Surname = bulletin.Surname,
                             Version = bulletin.Version,
                             CreatedOn = bulletin.CreatedOn,
                         };

            return result;
        }

        public IQueryable<VBulletinsFull> FilterBulletinsForExport(InquirySearchBulletinDTO searchParams)
        {
            var bulletinsQuery = from bulletin in _dbContext.VBulletinsFulls select bulletin;
            var queryResult = ApplyFilters(bulletinsQuery, searchParams);

            return queryResult;
        }

        public IQueryable<InquiryBulletinByPersonGridDTO> FilterBulletinsByPerson(InquirySearchBulletinByPersonDTO searchParams)
        {
            var query = from bulletin in _dbContext.VBulletins select bulletin;

            var bulletinsQuery = ApplyFiltersByPerson(query, searchParams);

            var result = from bulletin in bulletinsQuery
                         join bulletinStatus in _dbContext.BBulletinStatuses on bulletin.StatusId equals bulletinStatus.Code
                         into bulletinStatusLeft
                         from bulletinStatus in bulletinStatusLeft.DefaultIfEmpty()
                         select new InquiryBulletinByPersonGridDTO
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

        public IQueryable<VBulletinsFull> FilterBulletinsByPersonDataForExport(InquirySearchBulletinByPersonDTO searchParams)
        {
            var bulletinsQuery = from bulletin in _dbContext.VBulletinsFulls select bulletin;
            var queryResult = ApplyFiltersByPerson(bulletinsQuery, searchParams);

            return queryResult;
        }

        private IQueryable<T> ApplyFiltersByPerson<T>(IQueryable<T> bulletinsQuery, InquirySearchBulletinByPersonDTO searchParams)
            where T : class, IInquiryBulletinFilterable
        {

            if (!string.IsNullOrEmpty(searchParams.Firstname))
                bulletinsQuery = bulletinsQuery.Where(x => x.Firstname == searchParams.Firstname);

            if (!string.IsNullOrEmpty(searchParams.Surname))
                bulletinsQuery = bulletinsQuery.Where(x => x.Surname == searchParams.Surname);

            if (!string.IsNullOrEmpty(searchParams.Familyname))
                bulletinsQuery = bulletinsQuery.Where(x => x.Familyname == searchParams.Familyname);

            if (!string.IsNullOrEmpty(searchParams.Egn))
                bulletinsQuery = bulletinsQuery.Where(x => x.Egn == searchParams.Egn);

            if (!string.IsNullOrEmpty(searchParams.Lnch))
                bulletinsQuery = bulletinsQuery.Where(x => x.Lnch == searchParams.Lnch);

            if (searchParams.BirthDate.HasValue)
                bulletinsQuery = bulletinsQuery.Where(x => x.BirthDate == searchParams.BirthDate);

            if (!string.IsNullOrEmpty(searchParams.BirthPlaceCountryId))
                bulletinsQuery = bulletinsQuery.Where(x => x.BirthCountryId == searchParams.BirthPlaceCountryId);

            if (!string.IsNullOrEmpty(searchParams.BirthPlaceCityId))
                bulletinsQuery = bulletinsQuery.Where(x => x.BirthCityId == searchParams.BirthPlaceCityId);

            if (!string.IsNullOrEmpty(searchParams.BirthPlaceDesc))
                bulletinsQuery = bulletinsQuery.Where(x => x.BirthPlaceOther == searchParams.BirthPlaceDesc);

            if (searchParams.Sex.HasValue)
                bulletinsQuery = bulletinsQuery.Where(x => x.Sex == searchParams.Sex);

            if (!string.IsNullOrEmpty(searchParams.IdDocNumber))
                bulletinsQuery = bulletinsQuery.Where(x => x.IdDocNumber == searchParams.IdDocNumber);

            if (searchParams.IdDocIssuingDate.HasValue)
                bulletinsQuery = bulletinsQuery.Where(x => x.IdDocIssuingDate == searchParams.IdDocIssuingDate);

            if (searchParams.IdDocValidDate.HasValue)
                bulletinsQuery = bulletinsQuery.Where(x => x.IdDocValidDate == searchParams.IdDocValidDate);

            if (searchParams.FromDate.HasValue)
                bulletinsQuery = bulletinsQuery.Where(x => x.CreatedOn >= searchParams.FromDate);

            if (searchParams.ToDate.HasValue)
                bulletinsQuery = bulletinsQuery.Where(x => x.CreatedOn <= searchParams.ToDate);

            if (!string.IsNullOrEmpty(searchParams.NationalityTypeCode) || !string.IsNullOrEmpty(searchParams.NationalityCountryId))
            {
                bulletinsQuery = from bulletin in bulletinsQuery
                                 join nationality in _dbContext.BPersNationalities on bulletin.Id equals nationality.BulletinId
                                         into nationalityLeft
                                 from nationality in nationalityLeft.DefaultIfEmpty()

                                 where (searchParams.NationalityTypeCode == NationalityType.Country && nationality.CountryId == searchParams.NationalityCountryId) ||
                                 (searchParams.NationalityTypeCode == NationalityType.Eu && bulletin.EuCitizen == true) ||
                                 (searchParams.NationalityTypeCode == NationalityType.Tcn && bulletin.TcnCitizen == true) ||
                                 (searchParams.NationalityTypeCode == NationalityType.BgAndEU && bulletin.EuCitizen == true && nationality.CountryId == BGCountryId) ||
                                 (searchParams.NationalityTypeCode == NationalityType.BgAndTcn && bulletin.TcnCitizen == true && nationality.CountryId == BGCountryId)
                                 select bulletin;
            }

            return bulletinsQuery;
        }

        private IQueryable<T> ApplyFilters<T>(IQueryable<T> bulletinsQuery, InquirySearchBulletinDTO searchParams)
            where T : class, IInquiryBulletinFilterable
        {
            if (!string.IsNullOrEmpty(searchParams.RegistrationNumber))
                bulletinsQuery = bulletinsQuery.Where(x => x.RegistrationNumber == searchParams.RegistrationNumber);

            if (!string.IsNullOrEmpty(searchParams.BulletinType))
                bulletinsQuery = bulletinsQuery.Where(x => x.BulletinType == searchParams.BulletinType);

            if (searchParams.BulletinReceivedDate.HasValue)
                bulletinsQuery = bulletinsQuery.Where(x => x.BulletinReceivedDate == searchParams.BulletinReceivedDate);

            if (!string.IsNullOrEmpty(searchParams.CaseTypeId))
                bulletinsQuery = bulletinsQuery.Where(x => x.CaseTypeId == searchParams.CaseTypeId);

            if (!string.IsNullOrEmpty(searchParams.CaseNumber))
                bulletinsQuery = bulletinsQuery.Where(x => x.CaseNumber == searchParams.CaseNumber);

            if (searchParams.CaseYear.HasValue)
                bulletinsQuery = bulletinsQuery.Where(x => x.CaseYear == searchParams.CaseYear);

            if (!string.IsNullOrEmpty(searchParams.DecidingAuthId))
                bulletinsQuery = bulletinsQuery.Where(x => x.DecidingAuthId == searchParams.DecidingAuthId);

            if (!string.IsNullOrEmpty(searchParams.DecisionNumber))
                bulletinsQuery = bulletinsQuery.Where(x => x.DecisionNumber == searchParams.DecisionNumber);

            if (searchParams.DecisionDate.HasValue)
                bulletinsQuery = bulletinsQuery.Where(x => x.DecisionDate == searchParams.DecisionDate);

            if (searchParams.DecisionFinalDate.HasValue)
                bulletinsQuery = bulletinsQuery.Where(x => x.DecisionFinalDate == searchParams.DecisionFinalDate);

            if (!string.IsNullOrEmpty(searchParams.DecisionTypeId))
                bulletinsQuery = bulletinsQuery.Where(x => x.DecisionTypeId == searchParams.DecisionTypeId);

            if (!string.IsNullOrEmpty(searchParams.StatusId))
                bulletinsQuery = bulletinsQuery.Where(x => x.StatusId == searchParams.StatusId);

            if (searchParams.FromDate.HasValue)
                bulletinsQuery = bulletinsQuery.Where(x => x.CreatedOn >= searchParams.FromDate);

            if (searchParams.ToDate.HasValue)
                bulletinsQuery = bulletinsQuery.Where(x => x.CreatedOn <= searchParams.ToDate);

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
