using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.AStatusH;
using MJ_CAIS.DTO.Shared;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
    public class ApplicationService : BaseAsyncService<ApplicationDTO, ApplicationDTO, ApplicationDTO, AApplication, string, CaisDbContext>, IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IRegisterTypeService _registerTypeService;
        private readonly ICertificateService _certificateService;

        public ApplicationService(IMapper mapper, IApplicationRepository applicationRepository, IRegisterTypeService registerTypeService, ICertificateService certificateService)
            : base(mapper, applicationRepository)
        {
            _applicationRepository = applicationRepository;
            _registerTypeService = registerTypeService;
            _certificateService = certificateService;
        }

        public virtual async Task<IgPageResult<ApplicationGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId)
        {
            var entityQuery = this.GetSelectAllQueriable();
            if (!string.IsNullOrEmpty(statusId))
            {
                var statues = statusId.Split(','); 
                entityQuery = entityQuery.Where(x => statues.Contains( x.StatusCode));
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

        public async Task GenerateCertificateFromApplication(AApplication application, AApplicationStatus applicationStatus, AApplicationStatus certificateWithBulletinStatus, AApplicationStatus certificateWithoutBulletinStatus, int certificateValidityMonths = 6)            //string certificateWithoutBulletinStatusID = ApplicationConstants.ApplicationStatuses.CertificateContentReady, string certificateWithBulletinStatusID = ApplicationConstants.ApplicationStatuses.BulletinsCheck)
        {   //трябва да са попълнени следните стойности:
            //       .Include(a => a.EgnNavigation)
            //       .Include(a => a.LnchNavigation)
            //       .Include(a => a.LnNavigation)
            //       .Include(a => a.SuidNavigation)
            var pids = new List<string>();
            if (application.EgnId != null && application.EgnNavigation!=null) {
                pids.Add(application.EgnNavigation.PersonId);

            }
            if (application.LnchId != null && application.LnchNavigation != null)
            {
                pids.Add(application.LnchNavigation.PersonId);

            }
            if (application.LnId != null && application.LnNavigation != null)
            {
                pids.Add(application.LnNavigation.PersonId);

            }
            if (application.SuidId != null && application.SuidNavigation != null)
            {
                pids.Add(application.SuidNavigation.PersonId);

            }
            //await dbContext.PAppIds.Where(p => p.ApplicationId == application.Id).Select(prop => prop.PersonId).ToListAsync();
            if (pids.Count > 0)
            {
                var bulletins = await dbContext.BBulletins.Where(b => b.Status.Code != BulletinConstants.Status.Deleted
                                 && b.PBulletinIds.Any(bulID => pids.Contains(bulID.PersonId))).Distinct().ToListAsync();
                if (bulletins.Count > 0)
                {
                   await  ProcessApplicationWithBulletinsAsync(application, bulletins, certificateWithBulletinStatus, certificateValidityMonths, applicationStatus);
                    return;

                }
            }
            await ProcessApplicationWithoutBulletinsAsync(application, certificateWithoutBulletinStatus, certificateValidityMonths, applicationStatus);

        }
        private async Task ProcessApplicationWithoutBulletinsAsync(AApplication application, AApplicationStatus certificateStatus, int certificateValidityMonths, AApplicationStatus aStatus)
        {
            ACertificate cert = await CreateCertificateAsync(application.Id, certificateStatus, certificateValidityMonths, application.CsAuthorityId, application.ApplicationType.Code);
            //todo: add resources
            SetApplicationStatus(application, aStatus, "Създаване на сертификат");
          //  application.StatusCode = ApplicationConstants.ApplicationStatuses.ApprovedApplication;
            application.ACertificates.Add(cert);
            dbContext.ACertificates.Add(cert);
            dbContext.AApplications.Update(application);

        }

        private async Task<ACertificate> CreateCertificateAsync(string applicationId, AApplicationStatus aStatus, int certificateValidityMonths, string csAuthorityId,string applicationTypeCode)
        {

            ACertificate cert = new ACertificate();
            cert.Id = BaseEntity.GenerateNewId();
            cert.ApplicationId = applicationId;
            _certificateService.SetCertificateStatus(cert, aStatus, "Създаване на сертификат");
         
           
            if (applicationTypeCode == ApplicationConstants.ApplicationTypes.DeskCertificate)
            {
                cert.RegistrationNumber = await _registerTypeService.GetRegisterNumberForCertificateOnDesk(csAuthorityId);
            }
            if (applicationTypeCode == ApplicationConstants.ApplicationTypes.WebCertificate)
            {
                cert.RegistrationNumber = await _registerTypeService.GetRegisterNumberForCertificateWeb(csAuthorityId);
            }
            if (applicationTypeCode == ApplicationConstants.ApplicationTypes.WebInternalCertificate)
            {
                cert.RegistrationNumber = await _registerTypeService.GetRegisterNumberForCertificateWebInternal(csAuthorityId);
            }

            cert.AccessCode1 = Guid.NewGuid().ToString();
            cert.ValidFrom = DateTime.UtcNow;
            cert.ValidTo = DateTime.UtcNow.AddMonths(certificateValidityMonths);
            // cert.AccessCode2 = Guid.NewGuid().ToString();
            return cert;
        }

        private async Task ProcessApplicationWithBulletinsAsync(AApplication application, List<BBulletin> bulletins, AApplicationStatus certificateStatus, int certificateValidityMonths, AApplicationStatus aStatus)
        {
            ACertificate cert = await CreateCertificateAsync(application.Id, certificateStatus, certificateValidityMonths, application.CsAuthorityId, application.ApplicationType.Code);
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
            //todo: add resources
            SetApplicationStatus(application, aStatus, "Създаване на сертификат");

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
        public  void SetApplicationStatus(AApplication application,  AApplicationStatus newStatus, string description, bool includeInDbContext = true)
        {
            application.StatusCode = newStatus.Code;
            application.StatusCodeNavigation = newStatus;
            AStatusH aStatusH = new AStatusH();
            aStatusH.Descr = description;
            aStatusH.StatusCode = newStatus.Code;
            aStatusH.StatusCodeNavigation = newStatus;
            if (application.AStatusHes == null)
            {
                application.AStatusHes = new List<AStatusH>();
            }
            aStatusH.ReportOrder = application.AStatusHes.Count(x => x.StatusCode == newStatus.Code) + 1;
            aStatusH.Id = BaseEntity.GenerateNewId();
            aStatusH.ApplicationId = application.Id;
            aStatusH.Application = application;

            application.AStatusHes.Add(aStatusH);
            if (includeInDbContext)
            {
                dbContext.AStatusHes.Add(aStatusH);
                dbContext.AApplications.Update(application);
            }


        }

        public async Task<IQueryable<PersonAliasDTO>> SelectApplicationPersAliasByApplicationIdAsync(string aId)
        {
            var result = await _applicationRepository.SelectApplicationPersAliasByApplicationIdAsync(aId);
            return result.ProjectTo<PersonAliasDTO>(mapper.ConfigurationProvider); //AAppPersAlias
        }

        public async Task<IQueryable<AStatusHGridDTO>> SelectApplicationPersStatusHAsync(string aId)
        {
            var result = await _applicationRepository.SelectApplicationPersStatusHAsync(aId);
            return result;
        }
    }
}
