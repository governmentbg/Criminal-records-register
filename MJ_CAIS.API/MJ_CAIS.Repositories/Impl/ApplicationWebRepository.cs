using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application.External;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DTO.Application.Public;
using static MJ_CAIS.Common.Constants.ApplicationConstants;
using MJ_CAIS.Common.Constants;

namespace MJ_CAIS.Repositories.Impl
{
    public class ApplicationWebRepository : BaseAsyncRepository<WApplication, CaisDbContext>, IApplicationWebRepository
    {
        public ApplicationWebRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<ExternalApplicationGridDTO> SelectExternalApplications(string userId)
        {
            var result =
                (from app in _dbContext.WApplications.AsNoTracking()

                 join status in _dbContext.WApplicationStatuses.AsNoTracking()
                     on app.StatusCode equals status.Code

                 join purposes in _dbContext.APurposes.AsNoTracking()
                 on app.PurposeId equals purposes.Id into purposesLeft
                 from purposes in purposesLeft.DefaultIfEmpty()

                 join application in _dbContext.AApplications.AsNoTracking()
                 on app.Id equals application.WApplicationId into applicationLeft
                 from application in applicationLeft.DefaultIfEmpty()

                 where app.UserExtId == userId
                 select new ExternalApplicationGridDTO
                 {
                     Id = app.Id,
                     RegistrationNumber = app.RegistrationNumber,
                     ApplicantName = app.ApplicantName,
                     Purpose = app.Purpose,
                     PurposeName = purposes.Name,
                     PurposeId = app.PurposeId,
                     StatusCode = app.StatusCode,
                     StatusName = status.Name,
                     CreatedOn = app.CreatedOn,
                     Egn = app.Egn,
                     Name = application.Firstname + " " + application.Surname + " " + application.Familyname,
                     Email = application.Email,
                 }).OrderByDescending(x => x.CreatedOn);

            return result;
        }

        public async Task<DTO.Application.Public.ApplicationPreviewDTO> GetPublicForPreviewAsync(string id)
        {
            var result = await (from app in _dbContext.WApplications.AsNoTracking()

                                join status in _dbContext.WApplicationStatuses.AsNoTracking()
                                    on app.StatusCode equals status.Code

                                join purposes in _dbContext.APurposes.AsNoTracking()
                                    on app.PurposeId equals purposes.Id into purposesLeft
                                from purposes in purposesLeft.DefaultIfEmpty()

                                join paymentMethods in _dbContext.APaymentMethods.AsNoTracking()
                                    on app.PaymentMethodId equals paymentMethods.Id into paymentMethodsLeft
                                from paymentMethods in paymentMethodsLeft.DefaultIfEmpty()

                                join aPayments in _dbContext.APayments.AsNoTracking()
                                     on app.Id equals aPayments.WApplicationId into aPaymentsLeft
                                from aPayments in aPaymentsLeft.DefaultIfEmpty()

                                join ePayments in _dbContext.EPayments.AsNoTracking()
                                     on aPayments.EPaymentId equals ePayments.Id into ePaymentsLeft
                                from ePayments in ePaymentsLeft.DefaultIfEmpty()

                                join application in _dbContext.AApplications.AsNoTracking()
                                         on app.Id equals application.WApplicationId into applicationLeft
                                from application in applicationLeft.DefaultIfEmpty()

                                join cert in _dbContext.ACertificates.AsNoTracking()
                                    on application.Id equals cert.ApplicationId into certLeft
                                from cert in certLeft.DefaultIfEmpty()

                                select new DTO.Application.Public.ApplicationPreviewDTO
                                {
                                    Id = app.Id,
                                    CreatedOn = app.CreatedOn,
                                    Egn = app.Egn,
                                    Email = app.Email,
                                    PaymentMethodName = paymentMethods.Name,
                                    PurposeName = purposes.Name,
                                    Purpose = app.Purpose,
                                    RegistrationNumber = app.RegistrationNumber,
                                    Status = status.Name,
                                    StatusCode = status.Code,
                                    PaymentStatus = ePayments.PaymentStatus,
                                    CertificateStatusCode = cert.StatusCode,
                                    PaymentMethodCode = paymentMethods.Code,
                                    InvoiceNumber = ePayments.InvoiceNumber,
                                    PayEgovBGCode = ePayments.AccessCode
                                }).FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<DTO.Application.External.ApplicationPreviewDTO> GetExternalForPreviewAsync(string id)
        {
            var result = await (from app in _dbContext.WApplications.AsNoTracking()

                                join status in _dbContext.WApplicationStatuses.AsNoTracking()
                                    on app.StatusCode equals status.Code

                                join purposes in _dbContext.APurposes.AsNoTracking()
                                    on app.PurposeId equals purposes.Id into purposesLeft
                                from purposes in purposesLeft.DefaultIfEmpty()

                                join application in _dbContext.AApplications.AsNoTracking()
                                    on app.Id equals application.WApplicationId into applicationLeft
                                from application in applicationLeft.DefaultIfEmpty()

                                join cert in _dbContext.ACertificates.AsNoTracking()
                                    on application.Id equals cert.ApplicationId into certLeft
                                from cert in certLeft.DefaultIfEmpty()

                                select new DTO.Application.External.ApplicationPreviewDTO
                                {
                                    Id = app.Id,
                                    CreatedOn = app.CreatedOn,
                                    Egn = app.Egn,
                                    Email = app.Email,
                                    PurposeName = purposes.Name,
                                    Purpose = app.Purpose,
                                    RegistrationNumber = app.RegistrationNumber,
                                    Status = status.Name,
                                    StatusCode = status.Code,
                                    Name = application.Firstname + " " + application.Surname + " " + application.Familyname,
                                    CertificateStatusCode = cert.StatusCode

                                }).FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }



        public IQueryable<PublicApplicationGridDTO> SelectPublicApplications(string userId)
        {
            var result =
                (from app in _dbContext.WApplications.AsNoTracking()

                 join status in _dbContext.WApplicationStatuses.AsNoTracking()
                         on app.StatusCode equals status.Code

                 join purposes in _dbContext.APurposes.AsNoTracking()
                     on app.PurposeId equals purposes.Id into purposesLeft
                 from purposes in purposesLeft.DefaultIfEmpty()

                 where app.UserCitizenId == userId
                 select new PublicApplicationGridDTO
                 {
                     Id = app.Id,
                     RegistrationNumber = app.RegistrationNumber,
                     Purpose = app.Purpose,
                     PurposeTypeName = purposes.Name,
                     StatusCode = app.StatusCode,
                     StatusName = status.Name,
                     CreatedOn = app.CreatedOn,
                     Email = app.Email,
                     Version = app.Version,
                 }).OrderByDescending(x => x.CreatedOn);

            return result;
        }

        public async Task<bool> HasDublicates(string egn, string purposeId, string applicationTypeId, string applicantId)
        {
            if(applicationTypeId != ApplicationTypes.WebCertificate && applicationTypeId != ApplicationTypes.WebExternalCertificate)
            {
                throw new Exception("Необработен тип.");
            }
            if (applicationTypeId == ApplicationTypes.WebCertificate)
            {
                return await _dbContext.WApplications.AnyAsync(w => w.Egn == egn && w.PurposeId == purposeId
                && w.UserCitizenId == applicantId
                && w.ApplicationTypeId == applicationTypeId
                && w.StatusCode != ApplicationWebStatuses.WebCanceled && !w.WCertificates.Any());
            }
            else
            {
                return await _dbContext.WApplications.AnyAsync(w => w.Egn == egn && w.PurposeId == purposeId && w.UserExtId == applicantId
             && w.ApplicationTypeId == applicationTypeId
             && w.StatusCode != ApplicationWebStatuses.WebCanceled && !w.WCertificates.Any());
            }
        }

        public async Task<string?> FindDuplicate(string egn, string purposeId, string applicantId)
        {
            var applicationTypeId = ApplicationConstants.ApplicationTypes.WebCertificate;
            var canceledStatusCode = ApplicationWebStatuses.WebCanceled;

            var res = await (from a in _dbContext.WApplications
                       join c in _dbContext.WCertificates on a.Id equals c.WApplId into cLeft
                       from c in cLeft.DefaultIfEmpty()
                       where a.Egn == egn
                         && a.PurposeId == purposeId
                         && a.UserCitizenId == applicantId
                         && a.ApplicationTypeId == applicationTypeId
                         && a.StatusCode != canceledStatusCode
                         && c == null
                       select a.Id).FirstOrDefaultAsync();
           
            return res;
        }

        public Task<WApplicationStatus> GetWebCanceledStatus()
        {
            return _dbContext.WApplicationStatuses.AsNoTracking().Where(a =>
                             a.Code == ApplicationConstants.ApplicationStatuses.WebCanceled).FirstAsync();
        }
    }
}
