using MJ_CAIS.DTO.Home;

namespace MJ_CAIS.Services.Contracts
{
    public interface IHomeService
    {
        Task<BulletinCountDTO> GetBulletinCountByCurrentAuthorityAsync();

        Task<BulletinEventCountDTO> GetBulletinEventCountByCurrentAuthorityAsync();

        Task<IsinCountDTO> GetIsinCountByCurrentAuthorityAsync();

        Task<EcrisCountDTO> GetEcrisCountAsync();

        Task<ApplicationCountDTO> GetApplicationCountByCurrentAuthorityAsync();

        Task<ForJudgeCountDTO> GetForJudgeCountByCurrentAuthorityAsync();
    }
}