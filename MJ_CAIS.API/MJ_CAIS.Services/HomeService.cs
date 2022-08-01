using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DTO.Home;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using static MJ_CAIS.Common.Constants.ECRISConstants;

namespace MJ_CAIS.Services
{
    public class HomeService : IHomeService
    {
        private readonly IViewsCountsRepository _viewsCountsRepository;

        public HomeService(
            IViewsCountsRepository viewsCountsRepository)
        {
            _viewsCountsRepository = viewsCountsRepository;
        }

        public async Task<BulletinCountDTO> GetBulletinCountByCurrentAuthorityAsync()
        {
            var result = new BulletinCountDTO();

            var bulletinStatuses = await _viewsCountsRepository.GetBulletinsStatusCountByCurrentAuthority().ToListAsync();
            result.NewEISS = bulletinStatuses.FirstOrDefault(x => x.Status == BulletinConstants.Status.NewEISS)?.Count ?? 0;
            result.ForRehabilitation = bulletinStatuses.FirstOrDefault(x => x.Status == BulletinConstants.Status.ForRehabilitation)?.Count ?? 0;
            result.ForDestruction = bulletinStatuses.FirstOrDefault(x => x.Status == BulletinConstants.Status.ForDestruction)?.Count ?? 0;
            result.Article2211 = bulletinStatuses
                .FirstOrDefault(x => x.Status == BulletinEventConstants.Type.Article2211)?.Count ?? 0;
            result.Article2212 = bulletinStatuses
                .FirstOrDefault(x => x.Status == BulletinEventConstants.Type.Article2212)?.Count ?? 0;
            result.Article3000 = bulletinStatuses
                .FirstOrDefault(x => x.Status == BulletinEventConstants.Type.Article3000)?.Count ?? 0;
            result.NewDocument = bulletinStatuses
                .FirstOrDefault(x => x.Status == BulletinEventConstants.Type.NewDocument)?.Count ?? 0;
            //result.IsinNew = bulletinStatuses.FirstOrDefault(x => x.Status == IsinDataConstants.Status.New)?.Count ?? 0; // todo?
            //result.IsinIdentified = bulletinStatuses.FirstOrDefault(x => x.Status == IsinDataConstants.Status.Identified)?.Count ?? 0;// todo?
            result.IsinIdentified = bulletinStatuses.FirstOrDefault(x => x.Status == "isin")?.Count ?? 0;// todo?

            return result;
        }

        public async Task<CentralAuthorityCountDTO> GetCentralAuthorityCountsAsync()
        {
            var result = new CentralAuthorityCountDTO();
            var ercisStatuses = await _viewsCountsRepository.GetCentralAuthorityCounts().ToListAsync();

            result.ForIdentification = ercisStatuses.FirstOrDefault(x => x.Status == EcrisMessageStatuses.ForIdentification)?.Count ?? 0;
            result.Tcn = ercisStatuses.FirstOrDefault(x => x.Status == nameof(EcrisTcnStatus.New))?.Count ?? 0;
            result.ForDestruction = ercisStatuses
                .FirstOrDefault(x => x.Status == FbbcConstants.FBBCStatus.ForDelete)?.Count ?? 0;

            result.WebCheckTaxFree = ercisStatuses
                .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationWebStatuses.WebCheckTaxFree)?.Count ?? 0;

            return result;
        }

        public async Task<ApplicationCountDTO> GetApplicationCountByCurrentAuthorityAsync()
        {
            var result = new ApplicationCountDTO();
            var applicationStatuses = await _viewsCountsRepository.GetApplicationsCountByCurrentAuthority().ToListAsync();

            result.NewId = applicationStatuses
                .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationStatuses.NewId)?.Count ?? 0;
            result.CheckPayment = applicationStatuses
                .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationStatuses.CheckPayment)?.Count ?? 0;
            result.CheckTaxFree = applicationStatuses
                .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationStatuses.CheckTaxFree)?.Count ?? 0;
            result.BulletinsCheck = applicationStatuses
                .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationStatuses.BulletinsCheck)?.Count ?? 0;

            result.TaxFree = applicationStatuses
                .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationStatuses.CheckTaxFree)?.Count ?? 0;
            result.ForSigningByJudge = (applicationStatuses
                                           .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationStatuses.CertificateContentReady)?.Count ?? 0) +
                                       (applicationStatuses
                                           .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationStatuses.CertificatePaperPrint)?.Count ?? 0);

            result.BulletinSelection = applicationStatuses
                .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationStatuses.BulletinsSelection)?.Count ?? 0;

            return result;
        }
    }
}