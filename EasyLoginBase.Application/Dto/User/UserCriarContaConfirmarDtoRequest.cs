namespace EasyLoginBase.Application.Dto.User;

public class UserCriarContaConfirmarDtoRequest
{
    public required string Email { get; set; }
    public required string Codigo { get; set; }
}
