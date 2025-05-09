namespace EasyLoginBase.Application.Dto.User.Role;
public class RoleUserDto
{
    public UserDto? UserDto { get; set; }
    public List<RoleDto>? Roles { get; set; } = new List<RoleDto>();
}
