using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.AStatusH;
using MJ_CAIS.DTO.Shared;
using MJ_CAIS.DTO.WApplicaiton;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IWApplicationService : IBaseAsyncService<WApplicaitonDTO, WApplicaitonDTO, WApplicaitonGridDTO, WApplication, string>
    {
        Task<IgPageResult<WApplicaitonGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<WApplicaitonGridDTO> aQueryOptions, string? statusId);

        Task ConfirmPaymentAsync(string aId);

        Task ProcessTaxFreeAsync(string aId, bool approved);

        Task<PPerson> ProcessWebApplicationToApplicationAsync(WApplication wapplication, WApplicationStatus wapplicationStatus, AApplicationStatus applicationStatus);
        Task ProcessWApplicationCheckPayment(AApplicationStatus statusApprovedApplication, WApplicationStatus statusWebApprovedApplication, WApplicationStatus statusWebCancel, DateTime startDateWeb, WApplication wapplication);
        Task<IQueryable<PersonAliasDTO>> SelectApplicationPersAliasByApplicationIdAsync(string aId);

        Task<IQueryable<AStatusHGridDTO>> SelectApplicationPersStatusHAsync(string aId);
    }
}
