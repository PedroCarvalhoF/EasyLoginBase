using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Cliente;
using EasyLoginBase.Application.Dto.Produto.Produto;
using EasyLoginBase.Application.Services.Intefaces.Cliente;
using EasyLoginBase.Application.Tools;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IClienteServices<ClienteDto> _services;

    public ClienteController(IClienteServices<ClienteDto> services)
    {
        _services = services;
    }

    [HttpGet]
    public async Task<ActionResult<RequestResult<IEnumerable<ClienteDto>>>> SelectAllAsync()
    {
        try
        {
            return new ReturnActionResult<IEnumerable<ClienteDto>>().ParseToActionResult(await _services.SelectAllAsync(include: true));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<IEnumerable<ClienteDto>>().ParseToActionResult(RequestResult<IEnumerable<ClienteDto>>.BadRequest(ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<RequestResult<IEnumerable<ClienteDto>>>> RegistrarClienteAsync([FromBody] ClienteDtoRegistrar clienteRegistro)
    {
        try
        {
            if (clienteRegistro == null)
                throw new ArgumentNullException(nameof(clienteRegistro), "ClienteRestro cannot be null");

            return new ReturnActionResult<ClienteDto>().ParseToActionResult(await _services.RegistrarClienteAsync(clienteRegistro, true));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<ClienteDto>().ParseToActionResult(RequestResult<ClienteDto>.BadRequest(ex.Message));
        }
    }
}
