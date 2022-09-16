using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Inquiry;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class SearchReportApplicationRepository : BaseAsyncRepository<AReport, CaisDbContext>, ISearchReportApplicationRepository
    {
        private readonly IUserContext _userContext;
        public SearchReportApplicationRepository(CaisDbContext dbContext, IUserContext userContext)
            : base(dbContext)
        {
            _userContext = userContext;
        }

        public async Task<IQueryable<SearchReportApplicationGridDTO>> SelectAllAsync()
        {
            var query = _dbContext.AReports.AsNoTracking()
                .Include(x => x.ARepAppl)
                .Include(x => x.StatusCodeNavigation)
                .Include(x => x.FirstSigner)
                .Include(x => x.SecondSigner)
                .Where(x => x.ARepAppl.CsAuthorityId == _userContext.CsAuthorityId)
                .OrderByDescending(x => x.ValidFrom)
                .Select(x => new SearchReportApplicationGridDTO
                {
                    Id = x.ARepApplId,
                    RegistrationNumber = x.RegistrationNumber,
                    Egn = x.ARepAppl.Egn,
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
    }
}
