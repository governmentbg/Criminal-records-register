using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Inquiry;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class SearchInquiryRepository : BaseAsyncRepository<AReport, CaisDbContext>, ISearchInquiryRepository
    {
        private readonly IUserContext _userContext;
        public SearchInquiryRepository(CaisDbContext dbContext, IUserContext userContext)
            : base(dbContext)
        {
            _userContext = userContext;
        }

        public async Task<IQueryable<SearchInquiryGridDTO>> SelectAllAsync()
        {
            var query = _dbContext.AReports.AsNoTracking()
                .Include(x => x.ARepAppl)
                .Include(x => x.StatusCodeNavigation)
                .Include(x => x.FirstSigner)
                .Include(x => x.SecondSigner)
                .Where(x => x.ARepAppl.CsAuthorityId == _userContext.CsAuthorityId)
                .OrderByDescending(x => x.ValidFrom)
                .Select(x => new SearchInquiryGridDTO
                {
                    Id = x.Id,
                    RegistrationNumber = x.RegistrationNumber,
                    StatusCodeDisplayValue = x.StatusCodeNavigation.Name,
                    ValidFrom = x.ValidFrom,
                    ValidTo = x.ValidTo,
                    ReportApplicationRegistrationNumber = x.ARepAppl.RegistrationNumber,
                    ApplicantData = x.ARepAppl.ApplicantName + " / " + x.ARepAppl.ApplicantId + " / " + x.ARepAppl.ApplicantDescr,
                    PersonIdentificators = x.ARepAppl.Egn + " / " + x.ARepAppl.Lnch,
                    Names = x.ARepAppl.Firstname + " " + x.ARepAppl.Surname + " " + x.ARepAppl.Familyname,
                    Purpose = x.ARepAppl.Purpose + " - " + x.ARepAppl.PurposeId,
                    FirstSigner = x.FirstSigner.Firstname + " " + x.FirstSigner.Surname + " " + x.FirstSigner.Familyname,
                    SecondSigner = x.SecondSigner.Firstname + " " + x.SecondSigner.Surname + " " + x.SecondSigner.Familyname,
                    CreatedOn = x.CreatedOn
                });

            return query;
        }

        public async Task<SearchInquiryDTO> SelectByIdAsync(string aId)
        {
            var query = _dbContext.AReports.AsNoTracking()
                .Include(x => x.ARepAppl)
                .Include(x => x.StatusCodeNavigation)
                .Include(x => x.FirstSigner)
                .Include(x => x.SecondSigner)
                .Where(x => x.ARepAppl.CsAuthorityId == _userContext.CsAuthorityId && x.Id == aId)
                .OrderByDescending(x => x.ValidFrom)
                .Select(x => new SearchInquiryDTO
                {
                    Id=x.Id,
                    RegistrationNumber = x.RegistrationNumber,
                    StatusCodeDisplayValue = x.StatusCodeNavigation.Name,
                    ValidFrom = x.ValidFrom,
                    ValidTo = x.ValidTo,
                    RepApplRegistrationNumber = x.ARepAppl.RegistrationNumber,
                    ApplicantData = x.ARepAppl.ApplicantName + " / " + x.ARepAppl.ApplicantId + " / " + x.ARepAppl.ApplicantDescr,
                    PersonIdentificators = x.ARepAppl.Egn + " / " + x.ARepAppl.Lnch,
                    Names = x.ARepAppl.Firstname + " " + x.ARepAppl.Surname + " " + x.ARepAppl.Familyname,
                    Purpose = x.ARepAppl.Purpose + " - " + x.ARepAppl.PurposeId,
                    FirstSigner = x.FirstSigner.Firstname + " " + x.FirstSigner.Surname + " " + x.FirstSigner.Familyname,
                    SecondSigner = x.SecondSigner.Firstname + " " + x.SecondSigner.Surname + " " + x.SecondSigner.Familyname
                }).FirstOrDefault();

            return query;
        }
    }
}
