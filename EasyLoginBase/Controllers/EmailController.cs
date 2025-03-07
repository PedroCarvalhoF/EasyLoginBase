using EasyLoginBase.Application.Dto.Email;
using EasyLoginBase.Services.Services.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;
    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [AllowAnonymous]
    [HttpPost("enviar")]
    public async Task<IActionResult> EnviarEmail([FromBody] EmailDto emailDto)
    {
        var enviado = await _emailService.EnviarEmailAsync(emailDto);

        if (enviado)
            return Ok("E-mail enviado com sucesso.");
        else
            return BadRequest("Erro ao enviar e-mail.");
    }
}
