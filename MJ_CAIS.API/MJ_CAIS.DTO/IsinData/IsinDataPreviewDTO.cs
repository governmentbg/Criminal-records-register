using MJ_CAIS.DTO.Shared;

namespace MJ_CAIS.DTO.IsinData
{
    public class IsinDataPreviewDTO : IsinDataDTO
    {
        public BulletinPersonInfoModelDTO BulletinPersonInfo { get; set; } = new BulletinPersonInfoModelDTO();
    }
}