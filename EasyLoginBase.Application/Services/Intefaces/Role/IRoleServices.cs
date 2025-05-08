using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Role;

namespace EasyLoginBase.Application.Services.Intefaces.Role;
public interface IRoleServices<R> where R : RoleDto
{
    Task<RequestResult<R>> GetRoleByIdAsync(Guid id);
    Task<RequestResult<IEnumerable<R>>> GetAllRolesAsync();
}
