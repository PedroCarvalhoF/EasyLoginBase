using MediatR;

namespace EasyLoginBase.Services.CQRS.User.Command.Notification;

public class UserEnviarTokenRecupecaoSenhaNotificacao : INotification
{
    public string Token { get; private set; }
    public string Email { get; private set; }

    public UserEnviarTokenRecupecaoSenhaNotificacao(string token, string email)
    {
        Token = token;
        Email = email;
    }

    public override string ToString()
    {
        return $"Token criado {Token}";
    }
}
