using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.PessoaCliente;
using EasyLoginBase.Application.Dto.PessoaClienteVinculada;
using EasyLoginBase.Application.Services.Intefaces.PessoaClienteVinculada;
using EasyLoginBase.Application.Tools;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EasyLoginBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PessoaClienteVinculadaController(IPessoaClienteVinculadaServices _services) : ControllerBase
{
    [HttpPost("vincular-usuario-ao-cliente")]
    public async Task<ActionResult<RequestResult<PessoaClienteVinculadaDto>>> VincularUsuarioCliente([FromBody] PessoaClienteVinculadaDtoCreate command)
    {
        try
        {
            if (command == null)
                return new ReturnActionResult<PessoaClienteDto>().BadRequest("Parametros inváidos.");

            var result = await _services.AdicionarUsuarioVinculadoAsync(command);

            return new ReturnActionResult<PessoaClienteVinculadaDto>().ParseToActionResult(RequestResult<PessoaClienteVinculadaDto>.Ok(result));
        }
        catch (Exception ex)
        {

            return new ReturnActionResult<PessoaClienteDto>().BadRequest(ex.Message);
        }
    }

    [HttpGet("pessoas-vinculadas")]
    public async Task<ActionResult<RequestResult<IEnumerable<PessoaClienteVinculadaDto>>>> GetPessoasVinculadas()
    {
        try
        {
            IEnumerable<PessoaClienteVinculadaDto> result = await _services.GetPessoasVinculadas();

            return new ReturnActionResult<IEnumerable<PessoaClienteVinculadaDto>>().ParseToActionResult(RequestResult<IEnumerable<PessoaClienteVinculadaDto>>.Ok(result));
        }
        catch (Exception ex)
        {

            return new ReturnActionResult<PessoaClienteDto>().BadRequest(ex.Message);
        }
    }
}
