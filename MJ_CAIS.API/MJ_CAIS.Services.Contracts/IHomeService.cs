using MJ_CAIS.DTO.Home;

namespace MJ_CAIS.Services.Contracts
{
    public interface IHomeService
    {
        Task<(BulletinCountDTO, BulletinEventCountDTO, IsinCountDTO)> GetBulletinCountByCurrentAuthorityAsync();

        Task<(EcrisCountDTO, FbbcCountDTO)> GetEcrisCountAsync();

        Task<(ApplicationCountDTO, ForJudgeCountDTO)> GetApplicationCountByCurrentAuthorityAsync();

       
    }
}