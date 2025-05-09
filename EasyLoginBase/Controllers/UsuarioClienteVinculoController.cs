using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.UsuarioVinculadoCliente;
using EasyLoginBase.Application.Services.Intefaces.UsuarioClienteVinculo;
using EasyLoginBase.Application.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class UsuarioClienteVinculoController : ControllerBase
{
    private readonly IUsuarioClienteVinculoServices<UsuarioVinculadoClienteDto> _usuarioClienteVinculoServices;

    public UsuarioClienteVinculoController(IUsuarioClienteVinculoServices<UsuarioVinculadoClienteDto> usuarioClienteVinculoServices)
    {
        _usuarioClienteVinculoServices = usuarioClienteVinculoServices;
    }


    [HttpGet("select-usuarios-vinculados-by-cliente-id")]
    public async Task<ActionResult<RequestResult<IEnumerable<UsuarioVinculadoClienteDto>>>> SelectUsuariosVinculadosByClienteAsync()
    {
        try
        {
            return new ReturnActionResult<IEnumerable<UsuarioVinculadoClienteDto>>()
                .ParseToActionResult(await _usuarioClienteVinculoServices.SelectUsuariosVinculadosByClienteAsync(User));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<IEnumerable<UsuarioVinculadoClienteDto>>().ParseToActionResult(RequestResult<IEnumerable<UsuarioVinculadoClienteDto>>.BadRequest(ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<RequestResult<UsuarioVinculadoClienteDto>>> VincularUsuarioAoCliente([FromBody] UsuarioVinculadoClienteDtoRegistrarVinculo dtoRegistrarVinculo)
    {
        try
        {
            if (dtoRegistrarVinculo == null)
                return BadRequest("Requisição inválida.");

            return new ReturnActionResult<UsuarioVinculadoClienteDto>().ParseToActionResult(await _usuarioClienteVinculoServices.VincularUsuarioAoClienteAsync(dtoRegistrarVinculo, User));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<UsuarioVinculadoClienteDto>().ParseToActionResult(RequestResult<UsuarioVinculadoClienteDto>.BadRequest(ex.Message));
        }
    }

    [HttpPost("liberar-remover-acesso-usuario-vinculado")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<RequestResult<UsuarioVinculadoClienteDto>>> LiberarBloquearAcessoUsuarioVinculadoAsync([FromBody] UsuarioVinculadoClienteDtoLiberarRemoverAcesso liberarRemoverAcesso)
    {
        try
        {
            if (liberarRemoverAcesso == null)
                return BadRequest("Requisição inválida.");

            return new ReturnActionResult<UsuarioVinculadoClienteDto>().ParseToActionResult(await _usuarioClienteVinculoServices.LiberarBloquearAcessoUsuarioVinculadoAsync(liberarRemoverAcesso, User));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<UsuarioVinculadoClienteDto>().ParseToActionResult(RequestResult<UsuarioVinculadoClienteDto>.BadRequest(ex.Message));
        }
    }
}
