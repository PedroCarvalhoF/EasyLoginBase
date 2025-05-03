using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Application.Services.Intefaces.Filial;
using EasyLoginBase.Application.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FilialController(IFilialServices _services) : ControllerBase
{
    [HttpPost("cadastrar-filial")]
    public async Task<ActionResult<RequestResult<FilialDto>>> CadastrarFilial([FromBody] FilialDtoCreate command)
    {
        try
        {
            if (command == null)
                return BadRequest("Requisição inválida.");

            var result = await _services.CadastrarFilial(command, User);

            return new ReturnActionResult<FilialDto>().ParseToActionResult(RequestResult<FilialDto>.Ok(result));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<FilialDto>().ParseToActionResult(RequestResult<FilialDto>.BadRequest(ex.Message));
        }
    }


    [HttpGet("consultar-filiais")]
    public async Task<ActionResult<RequestResult<IEnumerable<FilialDto>>>> ConsultarFiliais()
    {
        try
        {
            var result = await _services.ConsultarFiliais(User);

            return new ReturnActionResult<IEnumerable<FilialDto>>().ParseToActionResult(RequestResult<IEnumerable<FilialDto>>.Ok(result));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<IEnumerable<FilialDto>>().ParseToActionResult(RequestResult<IEnumerable<FilialDto>>.BadRequest(ex.Message));
        }
    }
}
