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

        public HomeService(IBulletinRepository bulletinRepository, 
            IIsinDataRepository isinDataRepository,
            IEcrisMessageRepository ecrisMessageRepository)
        {
            _bulletinRepository = bulletinRepository;
            _isinDataRepository = isinDataRepository;
            _ecrisMessageRepository = ecrisMessageRepository;
        }

        public async Task<ObjectsCountDTO> GetCountAsync()
        {
            var bulletinStatusQuery = await _bulletinRepository.GetStatusCountAsync();
            var bulletinStatuses = await bulletinStatusQuery.ToListAsync();

            var isinStatusQuery = await _isinDataRepository.GetStatusCountAsync();
            var isinStatuses = await isinStatusQuery.ToListAsync();

            var ercisStatusCountQuery = await _ecrisMessageRepository.GetStatusCountAsync();
            var ercisStatuses = await ercisStatusCountQuery.ToListAsync();

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
            };

            return result;
        }
    }
}