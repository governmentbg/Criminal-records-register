using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO;
using MJ_CAIS.DTO.Inquiry;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IInquiryService : IBaseAsyncService<BaseDTO, BaseDTO, BaseGridDTO, VBulletin, string>
    {
        Task<IgPageResult<InquiryBulletinGridDTO>> SearchBulletinsWithPaginationAsync(ODataQueryOptions<InquiryBulletinGridDTO> aQueryOptions, InquirySearchBulletinDTO searchParams);

        Task<List<ExportInquiryBulletinGridDTO>> ExportBulletinsAsync(InquirySearchBulletinDTO searchParams);

        Task<IgPageResult<InquiryBulletinByPersonGridDTO>> SearchBulletinsByPersonWithPaginationAsync(ODataQueryOptions<InquiryBulletinByPersonGridDTO> aQueryOptions, InquirySearchBulletinByPersonDTO searchParams);

        Task<List<ExportInquiryBulletinGridDTO>> ExportBulletinsByPersonDataAsync(InquirySearchBulletinByPersonDTO searchParams);
    }
}
