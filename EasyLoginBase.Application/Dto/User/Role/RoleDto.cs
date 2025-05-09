namespace EasyLoginBase.Application.Dto.User.Role;
public class RoleDto
{
    public Guid Id { get; set; }
    public string? RoleName { get; set; }
    public RoleDto() { }
    public RoleDto(Guid id, string roleName)
    {
        Id = id;
        RoleName = roleName;
    }
}
