using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application.Public;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using static MJ_CAIS.Common.Constants.ApplicationConstants;

namespace MJ_CAIS.Services
{
    public class ApplicationWebService : BaseAsyncService<PublicApplicationDTO, PublicApplicationDTO, PublicApplicationDTO, WApplication, string, CaisDbContext>, IApplicationWebService
    {
        private readonly IApplicationWebRepository _applicationWebRepository;

        public ApplicationWebService(IMapper mapper, IApplicationWebRepository applicationWebRepository)
            : base(mapper, applicationWebRepository)
        {
            _applicationWebRepository = applicationWebRepository;
        }

        public string GetWebApplicationTypeId()
        {
            // TODO: use code, and filter by code!
            const string name = "Заявление за електронно свидетелство за съдимост";
            var result = dbContext.AApplicationTypes.AsNoTracking()
                .FirstOrDefault(x => x.Name == name);

            return result.Id;
        }

        protected override void TransformDataOnInsert(WApplication entity)
        {
            entity.ApplicationTypeId = GetWebApplicationTypeId();
            entity.StatusCode = ApplicationWebStatuses.NewWebApplication;
            entity.UserId = dbContext.CurrentUserId; // Should be nullable ???
            entity.UserCitizenId = dbContext.CurrentUserId;
            entity.WApplicationId = "-"; // TODO: remove, no such column

            base.TransformDataOnInsert(entity);
        }

        public IQueryable<PublicApplicationGridDTO> SelectPublicApplications(string userId)
        {
            var result =
                from app in dbContext.WApplications.AsNoTracking()

                join status in dbContext.WApplicationStatuses.AsNoTracking()
                    on app.StatusCode equals status.Code

                where app.UserCitizenId == userId
                select new PublicApplicationGridDTO
                {
                    Id = app.Id,
                    RegistrationNumber = app.RegistrationNumber,
                    ApplicantName = app.ApplicantName,
                    Purpose = app.Purpose,
                    PurposeId = app.PurposeId,
                    StatusCode = app.StatusCode,
                    StatusName = status.Name,
                    CreatedOn = app.CreatedOn,
                };

            return result;
        }


        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
