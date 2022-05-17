using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.Application.Public;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
    public class ApplicationService : BaseAsyncService<ApplicationDTO, ApplicationDTO, ApplicationDTO, AApplication, string, CaisDbContext>, IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IMapper mapper, IApplicationRepository applicationRepository)
            : base(mapper, applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public IQueryable<PublicApplicationGridDTO> SelectPublicApplications(string userId)
        {
            var result =
                from app in dbContext.AApplications.AsNoTracking()

                join status in dbContext.AApplicationStatuses.AsNoTracking()
                    on app.StatusCode equals status.Code

                where app.UserCitizenId == userId
                select new PublicApplicationGridDTO
                {
                    Id = app.Id,
                    RegistrationNumber = app.RegistrationNumber,
                    ApplicantName = app.ApplicantName,
                    Purpose = app.Purpose,
                    PurposeCountry = app.PurposeCountry,
                    PurposeId = app.PurposeId,
                    PurposePosition = app.PurposePosition,
                    StatusCode = app.StatusCode,
                    StatusName = status.Name,
                    CreatedOn = app.CreatedOn,
                };

            return result;
        }

        public virtual async Task<IgPageResult<ApplicationGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId)
        {
            var entityQuery = this.GetSelectAllQueriable();
            if (!string.IsNullOrEmpty(statusId))
            {
                entityQuery = entityQuery.Where(x => x.ApplicationTypeId == statusId);
            }

            var baseQuery = entityQuery.ProjectTo<ApplicationGridDTO>(mapperConfiguration);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<ApplicationGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
