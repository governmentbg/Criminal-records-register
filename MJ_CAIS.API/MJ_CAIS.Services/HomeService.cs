using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DTO.Home;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using static MJ_CAIS.Common.Constants.ECRISConstants;

namespace MJ_CAIS.Services
{
    public class HomeService : IHomeService
    {
        private readonly IBulletinRepository _bulletinRepository;
        private readonly IIsinDataRepository _isinDataRepository;
        private readonly IEcrisMessageRepository _ecrisMessageRepository;
        private readonly IEcrisTcnRepository _ecrisTcnRepository;
        private readonly IBulletinEventRepository _bulletinEventRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IInternalRequestRepository _internalRequestRepository;
        private readonly IFbbcRepository _fbbcRepository;
        private readonly IUserContext _userContext;

        public HomeService(IBulletinRepository bulletinRepository,
            IIsinDataRepository isinDataRepository,
            IEcrisMessageRepository ecrisMessageRepository,
            IEcrisTcnRepository ecrisTcnRepository,
            IBulletinEventRepository bulletinEventRepository,
            IApplicationRepository applicationRepository,
            IInternalRequestRepository internalRequestRepository,
            IFbbcRepository fbbcRepository,
            IUserContext userContext)
        {
            _bulletinRepository = bulletinRepository;
            _isinDataRepository = isinDataRepository;
            _ecrisMessageRepository = ecrisMessageRepository;
            _ecrisTcnRepository = ecrisTcnRepository;
            _bulletinEventRepository = bulletinEventRepository;
            _applicationRepository = applicationRepository;
            _internalRequestRepository = internalRequestRepository;
            _fbbcRepository = fbbcRepository;
            _userContext = userContext;
        }

        public async Task<BulletinCountDTO> GetBulletinCountByCurrentAuthorityAsync()
        {
            var result = new BulletinCountDTO();

            var bulletinStatusQuery = _bulletinRepository.GetStatusCountByCurrentAuthority();
            var bulletinStatuses = await bulletinStatusQuery.ToListAsync();

            result.NewOffice = bulletinStatuses.FirstOrDefault(x => x.Status == BulletinConstants.Status.NewOffice)?.Count ?? 0;
            result.NewEISS = bulletinStatuses.FirstOrDefault(x => x.Status == BulletinConstants.Status.NewEISS)?.Count ?? 0;
            result.ForRehabilitation = bulletinStatuses.FirstOrDefault(x => x.Status == BulletinConstants.Status.ForRehabilitation)?.Count ?? 0;
            result.ForDestruction = bulletinStatuses.FirstOrDefault(x => x.Status == BulletinConstants.Status.ForDestruction)?.Count ?? 0;

            return result;
        }

        public async Task<BulletinEventCountDTO> GetBulletinEventCountByCurrentAuthorityAsync()
        {
            var result = new BulletinEventCountDTO();
            var bulletinEventStatusesCountQuery = _bulletinEventRepository.GetStatusCountByCurrentAuthority();
            var bulletinStatusesEvent = await bulletinEventStatusesCountQuery.ToListAsync();

            result.Article2211 = bulletinStatusesEvent
                .FirstOrDefault(x => x.Status == BulletinEventConstants.Type.Article2211)?.Count ?? 0;
            result.Article2212 = bulletinStatusesEvent
                .FirstOrDefault(x => x.Status == BulletinEventConstants.Type.Article2212)?.Count ?? 0;
            result.Article3000 = bulletinStatusesEvent
                .FirstOrDefault(x => x.Status == BulletinEventConstants.Type.Article3000)?.Count ?? 0;
            result.NewDocument = bulletinStatusesEvent
                .FirstOrDefault(x => x.Status == BulletinEventConstants.Type.NewDocument)?.Count ?? 0;

            return result;
        }

        public async Task<IsinCountDTO> GetIsinCountByCurrentAuthorityAsync()
        {
            var result = new IsinCountDTO();
            var isinStatusQuery =  _isinDataRepository.GetStatusCountByCurrentAuthority();
            var isinStatuses = await isinStatusQuery.ToListAsync();

            result.New = isinStatuses.FirstOrDefault(x => x.Status == IsinDataConstants.Status.New)?.Count ?? 0;
            result.Identified = isinStatuses.FirstOrDefault(x => x.Status == IsinDataConstants.Status.Identified)?.Count ?? 0;

            return result;
        }

        public async Task<EcrisCountDTO> GetEcrisCountAsync()
        {
            var result = new EcrisCountDTO();
            var ercisStatusCountQuery = _ecrisMessageRepository.GetStatusCount();
            var ercisStatuses = await ercisStatusCountQuery.ToListAsync();

            result.ForIdentification = ercisStatuses.FirstOrDefault(x => x.Status == EcrisMessageStatuses.ForIdentification)?.Count ?? 0;
            result.WaitingForCSAuthority = ercisStatuses.FirstOrDefault(x => x.Status == EcrisMessageStatuses.ReqWaitingForCSAuthority)?.Count ?? 0;
            result.Tcn = await _ecrisTcnRepository.GetCountAsync();

            return result;
        }

        public async Task<ApplicationCountDTO> GetApplicationCountByCurrentAuthorityAsync()
        {
            var result = new ApplicationCountDTO();
            var applicationStatusesCountQuery = _applicationRepository.GetStatusCountByCurrentAuthority();
            var applicationStatuses = await applicationStatusesCountQuery.ToListAsync();

            result.NewId = applicationStatuses
                .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationStatuses.NewId)?.Count ?? 0;
            result.CheckPayment = applicationStatuses
                .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationStatuses.CheckPayment)?.Count ?? 0;
            result.CheckTaxFree = applicationStatuses
                .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationStatuses.CheckTaxFree)?.Count ?? 0;
            result.BulletinsCheck = applicationStatuses
                .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationStatuses.BulletinsCheck)?.Count ?? 0;

            return result;
        }

        public async Task<ForJudgeCountDTO> GetForJudgeCountByCurrentAuthorityAsync()
        {
            var result = new ForJudgeCountDTO();
            var applicationStatusesCountQuery = _applicationRepository.GetForJudgeCountByCurrentAuthority();
            var applicationStatuses = await applicationStatusesCountQuery.ToListAsync();

            result.TaxFree = applicationStatuses
                .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationStatuses.CheckTaxFree)?.Count ?? 0;
            result.ForSigningByJudge = (applicationStatuses
                .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationStatuses.CertificateContentReady)?.Count ?? 0) +
                                       (applicationStatuses
                                           .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationStatuses.CertificatePaperPrint)?.Count ?? 0);

            result.BulletinSelection = applicationStatuses
                .FirstOrDefault(x => x.Status == ApplicationConstants.ApplicationStatuses.BulletinsSelection)?.Count ?? 0;

            result.InternalRequests = await _internalRequestRepository.GetCountOfNewRequestsAsync();

            return result;
        }

        public async Task<FbbcCountDTO> GetFbbcCountByCurrentAuthorityAsync()
        {
            var result = new FbbcCountDTO();
            var fbbcStatusesCountQuery = _fbbcRepository.GetStatusCount();
            var fbbcStatuses = await fbbcStatusesCountQuery.ToListAsync();

            result.New = fbbcStatuses
                .FirstOrDefault(x => x.Status == FbbcConstants.FBBCStatus.Active)?.Count ?? 0;

            result.ForDestruction = fbbcStatuses
                .FirstOrDefault(x => x.Status == FbbcConstants.FBBCStatus.ForDelete)?.Count ?? 0;

            return result;
        }
    }
}