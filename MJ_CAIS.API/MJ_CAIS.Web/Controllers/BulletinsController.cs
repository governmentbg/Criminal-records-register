using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("bulletins")]
    public class BulletinsController : BaseApiCrudController<BulletinDTO, BulletinDTO, BulletinGridDTO, Bulletin, string>
    {
        private readonly IBulletinService _bulletinService;

        public BulletinsController(IBulletinService bulletinService)
            : base(bulletinService)
        {
            _bulletinService = bulletinService;
        }

        [HttpGet("")]
        public new async Task<IActionResult> GetAll(ODataQueryOptions<BulletinGridDTO> aQueryOptions)
        {
            //return await base.GetAll(aQueryOptions);

            var data = new BulletinGridDTO[]
            {
                new BulletinGridDTO() { Id = "1", FirstName = "Ivan", FamilyName = "Ivanov" },
                new BulletinGridDTO() { Id = "2", FirstName = "Petar", FamilyName = "Petrov" },
            };
            return Ok(data);
        }

        //[HttpGet("getAll")]
        //public new async Task<IActionResult> GetAllNoWrap(ODataQueryOptions<BulletinGridDTO> aQueryOptions)
        //{
        //    return await base.GetAllNoWrap(aQueryOptions);
        //}

        //[HttpGet("{aId}")]
        //public new async Task<IActionResult> Get(string aId)
        //{
        //    return await base.Get(aId);
        //}

        //[HttpPost("")]
        //public new async Task<IActionResult> Post([FromBody] BulletinDTO aInDto)
        //{
        //    return await base.Post(aInDto);
        //}

        //[HttpPut("{aId}")]
        //public new async Task<IActionResult> Put(string aId, [FromBody] BulletinDTO aInDto)
        //{
        //    return await base.Put(aId, aInDto);
        //}

        //[HttpDelete("{aId}")]
        //public new async Task<IActionResult> Delete(string aId)
        //{
        //    return await base.Delete(aId);
        //}
    }
}
