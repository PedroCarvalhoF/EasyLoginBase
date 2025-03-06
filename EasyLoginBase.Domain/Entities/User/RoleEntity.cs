using Microsoft.AspNetCore.Identity;

namespace EasyLoginBase.Domain.Entities.User;
public class RoleEntity : IdentityRole<Guid>
{
    public List<UserRoleEntity>? UserRoles { get; set; }
}
