using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class ApplicationSearchRepository : BaseAsyncRepository<AApplication, CaisDbContext>, IApplicationSearchRepository
    {
        private readonly IUserContext _userContext;

        public ApplicationSearchRepository(CaisDbContext dbContext, IUserContext userContext) : base(dbContext)
        {
            _userContext = userContext;
        }

        public async Task<IQueryable<ApplicationSearchGridDTO>> SelectAllAsync()
        {
            var query = _dbContext.ACertificates.AsNoTracking()
                .Include(x => x.Application)
                .Include(x => x.FirstSigner)
                .Include(x => x.SecondSigner)
                .Where(x => x.Application.CsAuthorityId == _userContext.CsAuthorityId)
                .OrderByDescending(x => x.ValidFrom).Select(x => new ApplicationSearchGridDTO
                {
                    Id = x.Id,
                    CertificateRegistrationNumber = x.RegistrationNumber,
                    StatusCode = x.StatusCode,
                    StatusCodeDisplayValue = x.StatusCodeNavigation.Name,
                    ValidFrom = x.ValidFrom,
                    ValidTo = x.ValidTo,
                    RegistrationNumber = x.Application.RegistrationNumber,
                    PersonIdentificator = x.Application.Egn + "/" + x.Application.Lnch,
                    Names = x.Application.Firstname + " " + x.Application.Surname + " " + x.Application.Familyname,
                    FirstSigner = x.FirstSigner != null ? x.FirstSigner.Firstname + " " + x.FirstSigner.Surname + " " + x.FirstSigner.Familyname : null,
                    SecondSigner = x.SecondSigner != null ? x.SecondSigner.Firstname + " " + x.SecondSigner.Surname + " " + x.SecondSigner.Familyname : null,
                    AccessCode = x.AccessCode1
                });

            return query;
        }
    }
}
