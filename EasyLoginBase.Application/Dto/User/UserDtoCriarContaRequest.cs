using System.ComponentModel.DataAnnotations;

namespace EasyLoginBase.Application.Dto.User;

public class UserDtoCriarContaRequest
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Nome { get; private set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string SobreNome { get; private set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} é inválido")]
    public string Email { get; private set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Senha { get; private set; }

    [Compare(nameof(Senha), ErrorMessage = "As senhas devem ser iguais")]
    [DataType(DataType.Password)]
    public string SenhaConfirmacao { get; private set; }
    public UserDtoCriarContaRequest(string nome, string sobreNome, string email, string senha, string senhaConfirmacao, string? imagemName)
    {
        Nome = nome;
        SobreNome = sobreNome;
        Email = email;
        Senha = senha;
        SenhaConfirmacao = senhaConfirmacao;
    }
}
