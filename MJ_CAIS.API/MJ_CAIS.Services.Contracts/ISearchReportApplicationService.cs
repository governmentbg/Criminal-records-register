using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO;
using MJ_CAIS.DTO.Inquiry;
using MJ_CAIS.Services.Contracts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services.Contracts
{
    public interface ISearchReportApplicationService : IBaseAsyncService<BaseDTO, BaseDTO, BaseGridDTO, AReport, string>
    {
        Task<IgPageResult<SearchReportApplicationGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<SearchReportApplicationGridDTO> aQueryOptions);
    }
}
