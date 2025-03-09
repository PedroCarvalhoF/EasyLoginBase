using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Services.CQRS.Filial.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers;

[Route("[controller]")]
[ApiController]
//[Authorize]
public class FilialController(IMediator _mediator) : ControllerBase
{
    [HttpPost("cadastrar-filial")]
    public async Task<ActionResult<RequestResult<FilialDto>>> CadastrarFilial([FromBody] FilialCommandCadastrarFilial command)
    {
        if (command == null)
            return BadRequest("Requisição inválida.");

        var result = await _mediator.Send(command);
        return new ReturnActionResult<FilialDto>().ParseToActionResult(result);
    }
}
