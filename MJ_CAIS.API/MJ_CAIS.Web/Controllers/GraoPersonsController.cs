using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisMessage;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers;

[Route("grao-persons")]
public class
    GraoPersonsController : BaseApiCrudController<GraoPersonDTO, GraoPersonDTO, GraoPersonGridDTO, GraoPerson, string>
{
    private readonly IGraoPersonService _graoPersonService;

    public GraoPersonsController(IGraoPersonService graoPersonService) : base(graoPersonService)
    {
        _graoPersonService = graoPersonService;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("{aId}")]
    protected virtual async Task<IActionResult> Get(string aId)
    {
        return StatusCode(405);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost("")]
    protected virtual async Task<IActionResult> Post([FromBody] GraoPersonDTO aInDto)
    {
        return StatusCode(405);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPut("{aId}")]
    protected virtual async Task<IActionResult> Put(string aId, [FromBody] GraoPersonDTO aInDto)
    {
        return StatusCode(405);
    }


    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{aId}")]
    protected virtual async Task<IActionResult> Delete(string aId)
    {
        return StatusCode(405);
    }
}