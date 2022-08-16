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
                .Include(x => x.StatusCodeNavigation)
                .Where(x => x.Application.CsAuthorityId == _userContext.CsAuthorityId)
                .OrderByDescending(x => x.ValidFrom)
                .Select(x => new ApplicationSearchGridDTO
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
                    FirstSigner = x.FirstSigner.Firstname + " " + x.FirstSigner.Surname + " " + x.FirstSigner.Familyname,
                    SecondSigner = x.SecondSigner.Firstname + " " + x.SecondSigner.Surname + " " + x.SecondSigner.Familyname,
                    AccessCode = x.AccessCode1,
                    CreatedOn = x.CreatedOn
                });

            return query;
        }

        public async Task<ApplicationSearchDTO> SelectByIdAsync(string aId)
        {
            var query = _dbContext.ACertificates.AsNoTracking()
                .Include(x => x.Application)
                .Include(x => x.FirstSigner)
                .Include(x => x.SecondSigner)
                .Include(x => x.StatusCodeNavigation)
                .Where(x => x.Application.CsAuthorityId == _userContext.CsAuthorityId && x.Id == aId)
                .Select(x => new ApplicationSearchDTO
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
                    FirstSigner = x.FirstSigner.Firstname + " " + x.FirstSigner.Surname + " " + x.FirstSigner.Familyname,
                    SecondSigner = x.SecondSigner.Firstname + " " + x.SecondSigner.Surname + " " + x.SecondSigner.Familyname,
                    AccessCode = x.AccessCode1
                }).FirstOrDefault();

            return query;
        }
    }
}
