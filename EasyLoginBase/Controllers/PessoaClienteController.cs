using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.PessoaCliente;
using EasyLoginBase.Application.Dto.Produto.Produto;
using EasyLoginBase.Application.Services.Intefaces.PessoaCliente;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Services.CQRS.PessoaCliente.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers
{
    //pessoa cliente é a pessaa que contrata o serviços
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaClienteController(IMediator _mediator, IPessoaClienteServices<PessoaClienteDto> _pessoaClienteServices) : ControllerBase
    {
        [HttpPost("cadastrar-pessoa-cliente")]
        public async Task<ActionResult<RequestResult<PessoaClienteDto>>> CadastrarFilial([FromBody] PessoaClienteCommandCreate command)
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

        [HttpPost("consultar-cliente-by-id")]
        public async Task<ActionResult<RequestResult<PessoaClienteDto>>> ConsultarClienteByIdAscyn([FromBody] DtoRequestId requestId)
        {
            try
            {
                var resull = await _pessoaClienteServices.ConsultarClientes(requestId.Id);
                if (resull == null)
                    return new ReturnActionResult<PessoaClienteDto>().BadRequest("Cliente não encontrado.");

                return new ReturnActionResult<PessoaClienteDto>().ParseToActionResult(RequestResult<PessoaClienteDto>.Ok(resull));

            }
            catch (Exception ex)
            {
                return new ReturnActionResult<PessoaClienteDto>().ParseToActionResult(RequestResult<PessoaClienteDto>.BadRequest(ex.Message));
            }
        }
    }
}
