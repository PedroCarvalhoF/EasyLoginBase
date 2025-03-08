using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.User;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Services.CQRS.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountController(IMediator _mediator) : ControllerBase
{ /// <summary>
  /// Login de usuário
  /// </summary>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<RequestResult<UsuarioLoginResponse>>> Login([FromBody] UserLoginCommand command)
    {
        return await _mediator.Send(command);
    }


    /// <summary>
    /// Criar nova conta de usuário
    /// </summary>
    [AllowAnonymous]
    [HttpPost("criar-conta")]
    public async Task<ActionResult<RequestResult<UserDto>>> CadastrarUsuario([FromBody] UserCriarContaCommand command)
    {
        if (command == null)
            return BadRequest("Requisição inválida.");

        var result = await _mediator.Send(command);
        return new ReturnActionResult<UserDto>().ParseToActionResult(result);
    }

    /// <summary>
    /// Confirmar conta do usuário
    /// </summary>
    [AllowAnonymous]
    [HttpPost("confirmar-conta")]
    public async Task<ActionResult<RequestResult<bool>>> ConfirmarConta([FromBody] UserCriarContaConfirmarCommand command)
    {
        if (command == null)
            return BadRequest("Requisição inválida.");

        var result = await _mediator.Send(command);
        return new ReturnActionResult<bool>().ParseToActionResult(result);
    }

    /// <summary>
    /// Alterar senha do usuário autenticado
    /// </summary>
    [HttpPost("alterar-senha")]
    public async Task<ActionResult<RequestResult<bool>>> AlterarSenha([FromBody] UserAlterarSenhaCommand command)
    {
        if (command == null)
            return BadRequest("Requisição inválida.");

        var result = await _mediator.Send(command);
        return new ReturnActionResult<bool>().ParseToActionResult(result);
    }

    [AllowAnonymous]
    [HttpPost("solicitar-token-recuperar-senha-by-email")]
    public async Task<ActionResult<RequestResult<UserDtoSolicitarTokenResult>>> SolicitarTokenRecuperacaoSenhaByEmail([FromBody] SolicitarTokenRecuperacaoSenhaCommand command)
    {
        if (command == null)
            return BadRequest("Requisição inválida.");
        var result = await _mediator.Send(command);
        return new ReturnActionResult<UserDtoSolicitarTokenResult>().ParseToActionResult(result);
    }

    [AllowAnonymous]
    [HttpPost("recuperar-senha")]
    public async Task<ActionResult<RequestResult<UserDtoSolicitarTokenResult>>> RecuperarSenha([FromBody] SolicitarRecuperacaoSenhaCommand command)
    {
        if (command == null)
            return BadRequest("Requisição inválida.");
        var result = await _mediator.Send(command);
        return new ReturnActionResult<UserDtoRecuperarSenhaResult>().ParseToActionResult(result);
    }

}
