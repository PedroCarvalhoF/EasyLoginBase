using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.UsuarioVinculadoCliente;
using System.Security.Claims;

namespace EasyLoginBase.Application.Services.Intefaces.UsuarioClienteVinculo;
public interface IUsuarioClienteVinculoServices<DTO> where DTO : UsuarioVinculadoClienteDto
{
    Task<RequestResult<DTO>> VincularUsuarioAoClienteAsync(UsuarioVinculadoClienteDtoRegistrarVinculo dtoRegistrarVinculo, ClaimsPrincipal users);
    Task<RequestResult<DTO>> VincularClienteAoClienteAsync(Guid clienteId, Guid usuarioId);
}
