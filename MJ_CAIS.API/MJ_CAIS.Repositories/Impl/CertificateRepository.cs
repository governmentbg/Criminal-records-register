using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Certificate;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class CertificateRepository : BaseAsyncRepository<ACertificate, CaisDbContext>, ICertificateRepository
    {
        public CertificateRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ACertificate> GetByApplicationIdAsync(string appId)
        {
            // todo: last cert ?
            var certificate = await _dbContext.ACertificates
                .Include(x => x.StatusCodeNavigation)
                .Include(x => x.Doc)
                .ThenInclude(x => x.DocContent)
                .AsNoTracking()
                .Where(x => x.ApplicationId == appId)
                .OrderByDescending(x => x.CreatedOn)
                .FirstOrDefaultAsync();

            return certificate;
        }

        public async Task<ACertificate> GetWithDocContentAsync(string certId)
        {
            var certificate = await _dbContext.ACertificates
                .Include(x => x.StatusCodeNavigation)
                .Include(x => x.Doc)
                .ThenInclude(x => x.DocContent)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == certId);

            return certificate;
        }

        public async Task<IQueryable<AAppBulletin>> GetBulletinsCheckByIdAsync(string certId, bool onlyApproved)
        {
            var query = _dbContext.AAppBulletins
                .Include(x => x.Bulletin).ThenInclude(x => x.Status)
                .Include(x => x.Bulletin).ThenInclude(x => x.CsAuthority)
                .Include(x => x.Certificate)
                .Where(x => x.CertificateId == certId);

            if (onlyApproved)
            {
                query = query.Where(x => x.Approved == true);
            }

            return await Task.FromResult(query);
        }

        public async Task<byte[]> GetCertificateContentByWebAppIdAsync(string webAppId)
        {
            //var content = await (from wApp in _dbContext.WApplications.AsNoTracking()
            //                     join app in _dbContext.AApplications.AsNoTracking()
            //                     on wApp.Id equals app.WApplicationId
            //                     into appLeft
            //                     from app in appLeft.DefaultIfEmpty()
            //                     join cert in _dbContext.ACertificates.AsNoTracking()
            //                         on app.Id equals cert.ApplicationId into certLeft
            //                     from cert in certLeft.DefaultIfEmpty()
            //                     join doc in _dbContext.DDocuments.AsNoTracking()
            //                         on cert.DocId equals doc.Id into docLeft
            //                     from doc in docLeft.DefaultIfEmpty()
            //                     join docCont in _dbContext.DDocContents.AsNoTracking()
            //                         on doc.DocContentId equals docCont.Id into docContLeft
            //                     from docCont in docContLeft.DefaultIfEmpty()
            //                     where wApp.Id == webAppId
            //                     select docCont.Content).FirstOrDefaultAsync();
            var content = (await _dbContext.WCertificates.AsNoTracking()
                .FirstOrDefaultAsync(wc => wc.WApplId == webAppId)).Content;

            return content;
        }

        public async Task<DDocContent> GetCertificateDocumentByAccessCode(string accessCode)
        {
            return await _dbContext.ACertificates.Where(x => x.AccessCode1 == accessCode && x.Doc != null).Select(x => x.Doc.DocContent).FirstOrDefaultAsync();
        }

        public async Task<ACertificate> GetCertificateData(string aId)
        {
            return await _dbContext.ACertificates.AsNoTracking()
                .Include(x => x.Application)
                .Include(x => x.AAppBulletins)
                .Where(x => x.Id == aId)
                .FirstOrDefaultAsync();
        }
        public async Task<ACertificate> GetCertificateWithDocumentContent(string certificateID)
        {
            return await _dbContext.ACertificates
                                  .Include(c => c.Doc)
                                  .ThenInclude(d => d.DocContent)
                                  .Include(c => c.Application)
                                  .Where(x => x.Id == certificateID)
                                  .FirstOrDefaultAsync();
        }

        public async Task<ACertificate> GetCertificateWithIncludedDataForApplicationAndBulletins(string certificateID)
        {
            return await _dbContext.ACertificates.AsNoTracking()
                                    .Include(c => c.AAppBulletins).AsNoTracking()
                                    .Include(c => c.Application)
                                    .ThenInclude(appl => appl.PurposeNavigation).AsNoTracking()
                                    .Include(c => c.Application.SrvcResRcptMeth).AsNoTracking()
                                    .Include(c => c.AStatusHes).AsNoTracking()
                                    .Include(c => c.Application.ApplicationType).AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == certificateID);
        }

        public async Task<ACertificate> GetCertificateDataWithContentAndType(string certificateID)
        {
            return await _dbContext.ACertificates
                                    .Include(c => c.Doc.DocType)
                                    .Include(c => c.Doc)
                                    .ThenInclude(d => d.DocContent)
                                    .Where(x => x.Id == certificateID)
                                    .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<CertificateExternalDTO>> SelectExternalCertificates(string userId)
        {
            return (from c in _dbContext.WCertificates
                    join wa in _dbContext.WApplications on c.WApplId equals wa.Id
                    join p in _dbContext.APurposes on wa.PurposeId equals p.Id
                    where wa.UserExtId == userId
                    select new CertificateExternalDTO()
                    {
                        Egn = wa.Egn,
                        Names = wa.Firstname + " " + wa.Surname + " " + wa.Familyname,
                        ValidFrom = c.ValidFrom,
                        AccessCode1 = c.AccessCode1,
                        PurposeName = p.Name,
                        Purpose = wa.Purpose,
                        WAppId = wa.Id
                    }).AsQueryable();
        }
        public async Task<IQueryable<CertificatePublicDTO>> SelectPublicCertificates(string userId)
        {
            return (from c in _dbContext.WCertificates
                    join wa in _dbContext.WApplications on c.WApplId equals wa.Id
                    join p in _dbContext.APurposes on wa.PurposeId equals p.Id
                    where wa.UserId == userId
                    orderby c.ValidFrom descending
                    select new CertificatePublicDTO()
                    {
                        ValidFrom = c.ValidFrom,
                        AccessCode1 = c.AccessCode1,
                        PurposeName = p.Name,
                        Purpose = wa.Purpose,
                        WAppId = wa.Id
                    }).AsQueryable();
        }
    }
}