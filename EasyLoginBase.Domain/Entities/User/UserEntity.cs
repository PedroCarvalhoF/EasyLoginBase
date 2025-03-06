using Microsoft.AspNetCore.Identity;

namespace EasyLoginBase.Domain.Entities.User;

public class UserEntity : IdentityUser<Guid>
{
    public string? Nome { get; private set; }
    public string? SobreNome { get; private set; }
    public virtual ICollection<UserRoleEntity>? UserRoles { get; private set; }
    public UserEntity() { }
    public UserEntity(string nome, string sobreNome, string userName, string email)
    {
    }
    public static UserEntity Create(string nome, string sobreNome, string userName, string email)
        => new UserEntity(nome, sobreNome, userName, email);
}
