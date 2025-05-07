using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Cliente;

namespace EasyLoginBase.Application.Services.Intefaces.Cliente;
public interface IClienteServices<DTO> where DTO : ClienteDto
{
    Task<RequestResult<DTO>> RegistrarClienteAsync(ClienteDtoRegistrar clienteRegistro, bool v);
    Task<RequestResult<IEnumerable<DTO>>> SelectAllAsync(bool include = true);
    Task<RequestResult<DTO?>> SelectByUsuarioClienteId(Guid UsuarioEntityClienteId, bool include = true);
}
