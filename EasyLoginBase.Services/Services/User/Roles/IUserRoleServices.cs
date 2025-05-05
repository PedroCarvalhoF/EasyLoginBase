using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.User.Role;

namespace EasyLoginBase.Services.Services.User.Roles;
public interface IUserRoleServices<E, RU> where E : RoleDto where RU : RoleUserDto
{
    Task<RequestResult<IEnumerable<E>>> SelectAsync();
    Task<RequestResult<RU>> SelectRolesUserAscyn(DtoRequestId requestId);
    Task<RequestResult<IEnumerable<RU>>> SelectUsersByRole(DtoRequestId requestRoleId);
    Task<RequestResult<E>> CreateRoleAsync(RoleDtoCreate roleDtoCreate);
    Task<RequestResult<RU>> AdcionarRoleUser(RoleDtoAddRoleUser roleDtoAddRoleUser);
    Task<RequestResult<RU>> RemovarRoleUser(RoleDtoRemoverRoleUser roleDtoRemoverRoleUser);
    Task CriarRoles();
}
