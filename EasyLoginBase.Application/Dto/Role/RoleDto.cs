namespace EasyLoginBase.Application.Dto.Role;
public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;
    public string ConcurrencyStamp { get; set; } = string.Empty;
    public RoleDto(Guid id, string name, string normalizedName, string concurrencyStamp)
    {
        Id = id;
        Name = name;
        NormalizedName = normalizedName;
        ConcurrencyStamp = concurrencyStamp;
    }
}
