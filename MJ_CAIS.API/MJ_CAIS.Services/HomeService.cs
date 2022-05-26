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
        private readonly IBulletinRepository _bulletinRepository;
        private readonly IIsinDataRepository _isinDataRepository;
        private readonly IEcrisMessageRepository _ecrisMessageRepository;
        private readonly IBulletinEventRepository _bulletinEventRepository;
        private readonly IApplicationRepository _applicationRepository;

        public HomeService(IBulletinRepository bulletinRepository, 
            IIsinDataRepository isinDataRepository,
            IEcrisMessageRepository ecrisMessageRepository,
            IBulletinEventRepository bulletinEventRepository,
            IApplicationRepository applicationRepository)
        {
            _bulletinRepository = bulletinRepository;
            _isinDataRepository = isinDataRepository;
            _ecrisMessageRepository = ecrisMessageRepository;
            _bulletinEventRepository = bulletinEventRepository;
            _applicationRepository = applicationRepository;
        }

        public async Task<ObjectsCountDTO> GetCountAsync()
        {
            var bulletinStatusQuery = await _bulletinRepository.GetStatusCountAsync();
            var bulletinStatuses = await bulletinStatusQuery.ToListAsync();

            var isinStatusQuery = await _isinDataRepository.GetStatusCountAsync();
            var isinStatuses = await isinStatusQuery.ToListAsync();

            var ercisStatusCountQuery = await _ecrisMessageRepository.GetStatusCountAsync();
            var ercisStatuses = await ercisStatusCountQuery.ToListAsync();

            var bulletinEventStatusesCountQuery = await _bulletinEventRepository.GetStatusCountAsync();
            var bulletinStatusesEvent = await bulletinEventStatusesCountQuery.ToListAsync();

            var applicationStatusesCountQuery = await _applicationRepository.GetStatusCountAsync();
            var applicationStatuses = await applicationStatusesCountQuery.ToListAsync();

            var result = new ObjectsCountDTO()
            {
                BulletinNewOfficeCount = bulletinStatuses.FirstOrDefault(x => x.Status == BulletinConstants.Status.NewOffice)?.Count ?? 0,
                BulletinNewEISSCount = bulletinStatuses.FirstOrDefault(x => x.Status == BulletinConstants.Status.NewEISS)?.Count ?? 0,
                BulletinForRehabilitationCount = bulletinStatuses.FirstOrDefault(x => x.Status == BulletinConstants.Status.ForRehabilitation)?.Count ?? 0,
                BulletinForDestructionCount = bulletinStatuses.FirstOrDefault(x => x.Status == BulletinConstants.Status.ForDestruction)?.Count ?? 0,
                IsinNewCount = isinStatuses.FirstOrDefault(x => x.Status == IsinDataConstants.Status.New)?.Count ?? 0,
                IsinIdentifiedCount = isinStatuses.FirstOrDefault(x => x.Status == IsinDataConstants.Status.Identified)?.Count ?? 0,
                EcrisForIdentificationCount = ercisStatuses.FirstOrDefault(x => x.Status == EcrisMessageStatuses.ForIdentification)?.Count ?? 0,
                EcrisWaitingForCSAuthorityCount = ercisStatuses.FirstOrDefault(x => x.Status == EcrisMessageStatuses.ReqWaitingForCSAuthority)?.Count ?? 0,
                BulletinEventArticle2211Count = bulletinStatusesEvent.FirstOrDefault(x => x.Status == BulletinEventConstants.Type.Article2211)?.Count ?? 0,
                BulletinEventArticle2212Count = bulletinStatusesEvent.FirstOrDefault(x => x.Status == BulletinEventConstants.Type.Article2212)?.Count ?? 0,
                BulletinEventArticle3000Count = bulletinStatusesEvent.FirstOrDefault(x => x.Status == BulletinEventConstants.Type.Article3000)?.Count ?? 0,
                BulletinEventNewDocumentCount = bulletinStatusesEvent.FirstOrDefault(x => x.Status == BulletinEventConstants.Type.NewDocument)?.Count ?? 0,
                ApplicationNewIdCount = applicationStatuses.FirstOrDefault(x=>x.Status == ApplicationConstants.ApplicationStatuses.NewId)?.Count ?? 0,
                ApplicationCheckPaymentCount = applicationStatuses.FirstOrDefault(x=>x.Status == ApplicationConstants.ApplicationStatuses.CheckPayment)?.Count ?? 0,
                ApplicationCheckTaxFreeCount = applicationStatuses.FirstOrDefault(x=>x.Status == ApplicationConstants.ApplicationStatuses.CheckTaxFree)?.Count ?? 0,
                ApplicationBulletinsCheckCount = applicationStatuses.FirstOrDefault(x=>x.Status == ApplicationConstants.ApplicationStatuses.BulletinsCheck)?.Count ?? 0,
            };

            return result;
        }
    }
}