using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Web.Controllers.Common
{
    public abstract class BaseAsyncProtectiveController<TInDTO, TOutDTO, TGridDTO, TEntity, TPk> : ControllerBase
    {
        protected IBaseAsyncService<TInDTO, TOutDTO, TGridDTO, TEntity, TPk> baseService;

        protected BaseAsyncProtectiveController(IBaseAsyncService<TInDTO, TOutDTO, TGridDTO, TEntity, TPk> baseService)
        {
            this.baseService = baseService;
        }

        /// <summary>
        /// Get all data with pagination (skip and take)
        /// </summary>
        /// <param name="aQueryOptions"></param>
        /// <returns></returns>
        [HttpGet("")]
        protected virtual async Task<IActionResult> GetAll(ODataQueryOptions<TGridDTO> aQueryOptions)
        {
            var result = await this.baseService.SelectAllWithPaginationAsync(aQueryOptions);
            return Ok(result);
        }

        /// <summary>
        /// Get all data without pagination
        /// </summary>
        /// <returns>The IActionResult</returns>
        [HttpGet("getAll")]
        protected virtual async Task<IActionResult> GetAllNoWrap(ODataQueryOptions<TGridDTO> aQueryOptions)
        {
            var result = await this.baseService.SelectAllAsync(aQueryOptions);
            return Ok(result);
        }

        /// <summary>
        /// Get element by primary key
        /// </summary>
        /// <param name="aId"></param>
        /// <returns></returns>
        [HttpGet("{aId}")]
        protected virtual async Task<IActionResult> Get(TPk aId)
        {
            var result = await this.baseService.SelectAsync(aId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Insert data for element
        /// </summary>
        /// <param name="aInDto"></param>
        /// <returns></returns>
        [HttpPost("")]
        protected virtual async Task<IActionResult> Post([FromBody] TInDTO aInDto)
        {
            var id = await this.baseService.InsertAsync(aInDto);
            return Ok(new { id });
        }

        /// <summary>
        /// Update data for element
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="aInDto"></param>
        /// <returns></returns>
        [HttpPut("{aId}")]
        protected virtual async Task<IActionResult> Put(TPk aId, [FromBody] TInDTO aInDto)
        {
            await this.baseService.UpdateAsync(aId, aInDto);
            return Ok();
        }

        /// <summary>
        /// Delete data for element
        /// </summary>
        /// <param name="aId"></param>
        /// <returns></returns>
        [HttpDelete("{aId}")]
        protected virtual async Task<IActionResult> Delete(TPk aId)
        {
            await this.baseService.DeleteAsync(aId);
            return Ok();
        }
    }
}
