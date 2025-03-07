using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.User;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Services.CQRS.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class AccountController(IMediator _mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("criar-conta")]
    public async Task<ActionResult<RequestResult<UserDto>>> CadastrarUsuario([FromBody] UserCreateCommand command)
    {
        return new ReturnActionResult<UserDto>().ParseToActionResult(await _mediator.Send(command));
    }
}
