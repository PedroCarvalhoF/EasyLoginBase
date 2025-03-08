namespace EasyLoginBase.Application.Dto.User;

public class UserDtoRecuperarSenhaResult
{
    public bool Status { get; private set; }
    public string Mensagem { get; private set; }
    public UserDtoRecuperarSenhaResult(bool status)
    {
        Status = status;
        if (status)
            Mensagem = "Senha alterada";
        else
            Mensagem = "Não foi possível alterar senha";
    }
}
