using EasyLoginBase.Application.Dto.Role;
using EasyLoginBase.Domain.Entities.User;

namespace EasyLoginBase.Services.Tools.UseCase;
public static class ParseRole
{
    public static RoleDto ParseToRole(this RoleEntity role)
    {
        var roleId = Guid.Parse(role.Id.ToString());
        return new RoleDto(roleId, role.Name, role.NormalizedName, role.ConcurrencyStamp);
    }

    public static IEnumerable<RoleDto> ParseToRole(this IEnumerable<RoleEntity> roles)
    {
        return roles.Select(role => role.ParseToRole());
    }
}
