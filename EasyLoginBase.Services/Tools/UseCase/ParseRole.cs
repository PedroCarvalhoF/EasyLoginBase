using EasyLoginBase.Application.Dto.User.Role;
using EasyLoginBase.Domain.Entities.User;

namespace EasyLoginBase.Services.Tools.UseCase;
public static class ParseRole
{
    public static RoleDto Parce(this RoleEntity entity) => new RoleDto(entity.Id, entity.Name ?? "N/A");
    public static IEnumerable<RoleDto> Parce(this IEnumerable<RoleEntity> entities)
    {
        foreach (var entity in entities)
        {
            yield return entity.Parce();
        }
    }
}
