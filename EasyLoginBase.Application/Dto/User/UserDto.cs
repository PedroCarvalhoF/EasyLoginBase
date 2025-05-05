namespace EasyLoginBase.Application.Dto.User;
public class UserDto
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? SobreNome { get; set; }
    public string? Email { get; set; }
    public UserDto() { }
    public UserDto(Guid id, string nome, string sobreNome, string email)
    {
        Id = id;
        Nome = nome;
        SobreNome = sobreNome;
        Email = email;
    }
}
