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
using MJ_CAIS.Common.Enums;

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
                    PurposeId = app.PurposeId,
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
                entityQuery = entityQuery.Where(x => x.StatusCode == statusId);
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

        public async Task GenerateCertificateFromApplication(AApplication application,int certificateValidityMonths =6, string certificateWithoutBulletinStatusID = ApplicationConstants.ApplicationStatuses.CertificateContentReady, string certificateWithBulletinStatusID = ApplicationConstants.ApplicationStatuses.BulletinsCheck)
        {
            var pids = await dbContext.PAppIds.Where(p => p.ApplicationId == application.Id).Select(prop => prop.PersonId).ToListAsync();
            if (pids.Count > 0)
            {
                var bulletins = await dbContext.BBulletins.Where(b => b.Status.Code != BulletinConstants.Status.Deleted
                                 && b.PBulletinIds.Any(bulID => pids.Contains(bulID.PersonId))).ToListAsync();
                if (bulletins.Count > 0)
                {
                    ProcessApplicationWithBulletins(application, bulletins, certificateWithBulletinStatusID, certificateValidityMonths);
                    return;

                }
            }
            ProcessApplicationWithoutBulletins(application, certificateWithoutBulletinStatusID, certificateValidityMonths);

        }


        private void ProcessApplicationWithoutBulletins(AApplication application, string statusID, int certificateValidityMonths)
        {
            ACertificate cert = CreateCertificate(application.Id, statusID, certificateValidityMonths);
            application.ACertificates.Add(cert);
            dbContext.ACertificates.Add(cert);
            dbContext.AApplications.Update(application);

        }

        private ACertificate CreateCertificate(string applicationId, string certificateStatus, int certificateValidityMonths)
        {

            ACertificate cert = new ACertificate();
            cert.Id = BaseEntity.GenerateNewId();
            cert.ApplicationId = applicationId;
            cert.StatusCode = certificateStatus;
            //дали тук да се попълват?!
            cert.RegistrationNumber = Guid.NewGuid().ToString();
            cert.AccessCode1 = Guid.NewGuid().ToString();
            cert.ValidFrom = DateTime.UtcNow;
            cert.ValidTo = DateTime.UtcNow.AddMonths(certificateValidityMonths);
            // cert.AccessCode2 = Guid.NewGuid().ToString();
            return cert;
        }

        private void ProcessApplicationWithBulletins(AApplication application, List<BBulletin> bulletins, string statusID, int certificateValidityMonths)
        {
            ACertificate cert = CreateCertificate(application.Id, statusID, certificateValidityMonths);
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


        public async Task<IQueryable<ApplicationDocumentDTO>> GetDocumentsByApplicationIdAsync(string aId)
        {
            var result = dbContext.DDocuments
                .AsNoTracking()
                .Include(x => x.DocType)
                .Include(x => x.DocContent)
                .Where(x => x.FbbcId == aId)
                .ProjectTo<ApplicationDocumentDTO>(mapper.ConfigurationProvider);

            return await Task.FromResult(result);
        }

        public async Task InsertApplicationDocumentAsync(string applicationId, ApplicationDocumentDTO aInDto)
        {
            if (aInDto == null)
            {
                throw new ArgumentNullException(nameof(aInDto));
            }

            if (aInDto.DocumentContent?.Length == 0)
            {
                throw new ArgumentNullException("Document is empty");
            }

            var docContentId = string.IsNullOrEmpty(aInDto.DocumentContentId) ?
                Guid.NewGuid().ToString() : aInDto.DocumentContentId;

            var document = mapper.Map<ApplicationDocumentDTO, DDocument>(aInDto);
            document.ApplicationId = applicationId;
            document.DocContentId = docContentId;

            var documentContent = new DDocContent()
            {
                Id = docContentId,
                Content = aInDto.DocumentContent,
                MimeType = aInDto.MimeType
            };

            dbContext.Add(document);
            dbContext.Add(documentContent);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteDocumentAsync(string documentId)
        {
            var document = await dbContext.Set<DDocument>().AsNoTracking()
                .Include(x => x.DocContent)
                .FirstOrDefaultAsync(x => x.Id == documentId);

            if (document == null)
            {
                throw new ArgumentException($"Document with id: {documentId} is missing");
            }

            document.EntityState = EntityStateEnum.Deleted;
            if (document.DocContent != null)
            {
                document.DocContent.EntityState = EntityStateEnum.Deleted;
            }

            await dbContext.SaveEntityAsync(document, true);
        }

        public async Task<ApplicationDocumentDTO> GetDocumentContentAsync(string documentId)
        {
            var document = await dbContext.Set<DDocument>().AsNoTracking()
                .Include(x => x.DocContent)
                .FirstOrDefaultAsync(x => x.Id == documentId);

            if (document == null || document.DocContent == null) return null;

            return new ApplicationDocumentDTO
            {
                Name = document.Name,
                DocumentContent = document.DocContent.Content,
                MimeType = document.DocContent.MimeType
            };
        }
    }
}
