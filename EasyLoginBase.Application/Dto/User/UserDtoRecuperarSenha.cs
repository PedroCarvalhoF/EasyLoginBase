using System.ComponentModel.DataAnnotations;

namespace EasyLoginBase.Application.Dto.User;
public class UserDtoRecuperarSenha
{
    [Required]
    [EmailAddress]
    public string Email { get; private set; }
    [Required]
    public string Token { get; private set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Nova Senha.")]
    public string NovaSenha { get; private set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(NovaSenha), ErrorMessage = "As senhas devem ser iguais")]
    public string ConfirmarNovaSenha { get; private set; }
    public UserDtoRecuperarSenha(string email, string token, string novaSenha, string confirmarNovaSenha)
    {
        this.Email = email;
        this.Token = token;
        NovaSenha = novaSenha;
        ConfirmarNovaSenha = confirmarNovaSenha;
    }
}
