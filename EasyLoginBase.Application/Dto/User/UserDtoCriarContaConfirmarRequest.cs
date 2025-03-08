namespace EasyLoginBase.Application.Dto.User;

public class UserDtoCriarContaConfirmarRequest
{
    public required string Email { get; set; }
    public required string Codigo { get; set; }
}
