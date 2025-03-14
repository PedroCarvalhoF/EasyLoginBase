using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.PessoaCliente;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Services.CQRS.PessoaCliente.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PessoaClienteController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("cadastrar-pessoa-cliente")]
        public async Task<ActionResult<RequestResult<PessoaClienteDto>>> CadastrarFilial([FromBody] PessoaClienteCommandCreate command)
        {
            if (command == null)
                return new ReturnActionResult<PessoaClienteDto>().BadRequest("Parametros inváidos.");

            var result = await _mediator.Send(command);
            return new ReturnActionResult<PessoaClienteDto>().ParseToActionResult(result);
        }

        [HttpPost("vincular-pessoa-ao-cliente")]
        public async Task<ActionResult<RequestResult<PessoaClienteDto>>> VincularUsuarioCliente([FromBody] PessoaClienteCommandCreate command)
        {
            if (command == null)
                return new ReturnActionResult<PessoaClienteDto>().BadRequest("Parametros inváidos.");

            var result = await _mediator.Send(command);
            return new ReturnActionResult<PessoaClienteDto>().ParseToActionResult(result);
        }


        [HttpGet("consultar-clientes")]
        public async Task<ActionResult<RequestResult<IEnumerable<PessoaClienteDto>>>> ConsultarClientes()
        {
            PessoaClienteCommandaConsultarClientes command = new PessoaClienteCommandaConsultarClientes();

            if (command == null)
                return new ReturnActionResult<PessoaClienteDto>().BadRequest("Parametros inváidos.");

            var result = await _mediator.Send(command);
            return new ReturnActionResult<IEnumerable<PessoaClienteDto>>().ParseToActionResult(result);
        }
    }
}
