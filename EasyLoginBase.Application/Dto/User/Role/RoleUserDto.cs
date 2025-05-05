namespace EasyLoginBase.Application.Dto.User.Role;
public class RoleUserDto
{
    public UserDto? UserDto { get; set; }
    public List<String>? Roles { get; set; } = new List<string>();
}
