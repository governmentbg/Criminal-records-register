using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
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
            var content = await (from wApp in _dbContext.WApplications.AsNoTracking()
                                 join app in _dbContext.AApplications.AsNoTracking()
                                 on wApp.Id equals app.WApplicationId
                                 into appLeft
                                 from app in appLeft.DefaultIfEmpty()
                                 join cert in _dbContext.ACertificates.AsNoTracking()
                                     on app.Id equals cert.ApplicationId into certLeft
                                 from cert in certLeft.DefaultIfEmpty()
                                 join doc in _dbContext.DDocuments.AsNoTracking()
                                     on cert.DocId equals doc.Id into docLeft
                                 from doc in docLeft.DefaultIfEmpty()
                                 join docCont in _dbContext.DDocContents.AsNoTracking()
                                     on doc.DocContentId equals docCont.Id into docContLeft
                                 from docCont in docContLeft.DefaultIfEmpty()
                                 where wApp.Id == webAppId
                                 select docCont.Content).FirstOrDefaultAsync();

            return content;
        }
    }
}