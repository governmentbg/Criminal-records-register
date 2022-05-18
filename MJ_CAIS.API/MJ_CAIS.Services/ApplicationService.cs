using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
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
                   // PurposeCountry = app.PurposeCountry,
                    PurposeId = app.PurposeId,
                   // PurposePosition = app.PurposePosition,
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

        public async Task GenerateCertificateFromApplication(AApplication application, string certificateWithoutBulletinStatusID = ApplicationConstants.ApplicationStatuses.CertificateReady, string certificateWithBulletinStatusID = ApplicationConstants.ApplicationStatuses.BulletinsForPurpose)
        {
            var pids = await dbContext.PAppIds.Where(p => p.ApplicationId == application.Id).Select(prop => prop.PersonId).ToListAsync();
            if (pids.Count > 0)
            {
                var bulletins = await dbContext.BBulletins.Where(b => b.Status.Code != BulletinConstants.Status.Deleted
                                 && b.PBulletinIds.Any(bulID => pids.Contains(bulID.PersonId))).ToListAsync();
                if (bulletins.Count > 0)
                {
                    ProcessApplicationWithBulletins(application, bulletins, certificateWithBulletinStatusID);
                    return;

                }
            }
            ProcessApplicationWithoutBulletins(application, certificateWithoutBulletinStatusID);

        }


        private void ProcessApplicationWithoutBulletins(AApplication application, string statusID)
        {
            ACertificate cert = CreateCertificate(application.Id, statusID);
            application.ACertificates.Add(cert);
            dbContext.ACertificates.Add(cert);
            dbContext.AApplications.Update(application);

        }

        private ACertificate CreateCertificate(string applicationId, string certificateStatus)
        {

            ACertificate cert = new ACertificate();
            cert.Id = BaseEntity.GenerateNewId();
            cert.ApplicationId = applicationId;
            cert.StatusCode = certificateStatus;
            //дали тук да се попълват?!
            cert.RegistrationNumber = Guid.NewGuid().ToString();
            cert.AccessCode1 = Guid.NewGuid().ToString();
            // cert.AccessCode2 = Guid.NewGuid().ToString();
            return cert;
        }

        private void ProcessApplicationWithBulletins(AApplication application, List<BBulletin> bulletins, string statusID)
        {
            ACertificate cert = CreateCertificate(application.Id, ApplicationConstants.ApplicationStatuses.BulletinsForPurpose);
            int orderNumber = 0;
            cert.AAppBulletins= bulletins.OrderByDescending(b => b.DecisionDate).Select(b =>
            {
                orderNumber++;
                return new AAppBulletin()
                {
                    Id = BaseEntity.GenerateNewId(),
                    BulletinId = b.Id,
                    CertificateId = cert.Id,
                    ConvictionText = b.ConvRemarks,
                    OrderNumber = orderNumber              
                };
            }).ToList();
            
            application.ACertificates.Add(cert);
            dbContext.ACertificates.Add(cert);
            dbContext.AAppBulletins.AddRange(cert.AAppBulletins);
            dbContext.AApplications.Update(application);
        }
    }
}
