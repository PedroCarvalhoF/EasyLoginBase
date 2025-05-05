namespace EasyLoginBase.Application.Dto.User.Role;
public class RoleDtoCreate
{
    public string NomeRole { get; set; }

    public RoleDtoCreate(string nomeRole)
    {
        NomeRole = nomeRole;
    }
}
