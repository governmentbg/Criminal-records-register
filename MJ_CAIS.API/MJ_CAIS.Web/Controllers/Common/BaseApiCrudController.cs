using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Web.Controllers.Common
{
    [Authorize]
    [ApiController]
    public abstract class BaseApiCrudController<TInDTO, TOutDTO, TGridDTO, TEntity, TPk> : BaseAsyncProtectiveController<TInDTO, TOutDTO, TGridDTO, TEntity, TPk>
    {
        public BaseApiCrudController(IBaseAsyncService<TInDTO, TOutDTO, TGridDTO, TEntity, TPk> aBaseService) : base(aBaseService)
        {
        }
    }
}
