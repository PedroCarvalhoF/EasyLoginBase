using EasyLoginBase.Application.Dto.PDV.Usuario;
using System.Security.Claims;

namespace EasyLoginBase.Application.Services.Intefaces.PDV;

public interface IUsuarioPdvServices
{
    Task<IEnumerable<UsuarioPdvDto>> ConsultarUsuarios(ClaimsPrincipal user);
    Task<UsuarioPdvDto> CadastrarUsuarioPdv(UsuarioPdvDtoCreate newUserPdv, ClaimsPrincipal user);
    Task<UsuarioPdvDto> ConsultarUsuarioPorId(UsuarioPdvDtoRequestId usuarioPdvDtoRequestId, ClaimsPrincipal user);
    Task<UsuarioPdvDto> AtivarAcessoCaixa(UsuarioPdvDtoRequestId usuarioPdvDtoRequestId, ClaimsPrincipal user);
    Task<UsuarioPdvDto> DesativarAcessoCaixa(UsuarioPdvDtoRequestId usuarioPdvDtoRequestId, ClaimsPrincipal user);    
}
