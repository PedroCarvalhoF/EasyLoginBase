using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.PDV.Usuario;
using EasyLoginBase.Application.Services.Intefaces.PDV;
using EasyLoginBase.Application.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioPdvController(IUsuarioPdvServices _services) : ControllerBase
    {
        [HttpPost("cadastrar-usuario-pdv")]
        public async Task<ActionResult<RequestResult<UsuarioPdvDto>>> CadastrarFilial([FromBody] UsuarioPdvDtoCreate command)
        {
            try
            {
                if (command == null)
                    return BadRequest("Requisição inválida.");

                var result = await _services.CadastrarUsuarioPdv(command, User);

                return new ReturnActionResult<UsuarioPdvDto>().ParseToActionResult(RequestResult<UsuarioPdvDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return new ReturnActionResult<UsuarioPdvDto>().ParseToActionResult(RequestResult<UsuarioPdvDto>.BadRequest(ex.Message));
            }
        }


        [HttpGet("consultar-usuarios-pdvs")]
        public async Task<ActionResult<RequestResult<IEnumerable<UsuarioPdvDto>>>> ConsultarUsuariosPdvs()
        {
            try
            {

                var result = await _services.ConsultarUsuarios(User);

                return new ReturnActionResult<IEnumerable<UsuarioPdvDto>>().ParseToActionResult(RequestResult<IEnumerable<UsuarioPdvDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return new ReturnActionResult<IEnumerable<UsuarioPdvDto>>().ParseToActionResult(RequestResult<IEnumerable<UsuarioPdvDto>>.BadRequest(ex.Message));
            }
        }

        [HttpPost("consultar-usuario-pdv-by/usuarioId")]
        public async Task<ActionResult<RequestResult<UsuarioPdvDto>>> ConsultarUsuarioPdvByUsuarioId(UsuarioPdvDtoRequestId usuarioId)
        {
            try
            {

                UsuarioPdvDto result = await _services.ConsultarUsuarioPorId(usuarioId, User);

                return new ReturnActionResult<UsuarioPdvDto>().ParseToActionResult(RequestResult<UsuarioPdvDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return new ReturnActionResult<UsuarioPdvDto>().ParseToActionResult(RequestResult<UsuarioPdvDto>.BadRequest(ex.Message));
            }
        }

    }
}
