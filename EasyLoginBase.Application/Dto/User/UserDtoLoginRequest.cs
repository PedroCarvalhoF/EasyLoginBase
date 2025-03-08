using System.ComponentModel.DataAnnotations;

namespace EasyLoginBase.Application.Dto.User;

public class UserDtoLoginRequest
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} é inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Senha { get; set; }
    public UserDtoLoginRequest(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }
}
