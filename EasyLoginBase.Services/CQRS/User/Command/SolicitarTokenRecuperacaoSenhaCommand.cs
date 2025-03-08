using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Email;
using EasyLoginBase.Application.Dto.User;
using EasyLoginBase.Domain.Entities.User;
using EasyLoginBase.Services.CQRS.NotificationEmail;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EasyLoginBase.Services.CQRS.User.Command;

public class SolicitarTokenRecuperacaoSenhaCommand : BaseCommands<UserDtoSolicitarTokenResult>
{
    public required UserDtoRequestEmail UserDtoRequestEmail { get; set; }
    public class SolicitarTokenRecuperacaoSenhaCommandHandler(UserManager<UserEntity> _userManager, IMediator _mediator) : IRequestHandler<SolicitarTokenRecuperacaoSenhaCommand, RequestResult<UserDtoSolicitarTokenResult>>
    {
        public async Task<RequestResult<UserDtoSolicitarTokenResult>> Handle(SolicitarTokenRecuperacaoSenhaCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var user = await _userManager.FindByEmailAsync(request.UserDtoRequestEmail.Email);
                if (user == null)
                    return RequestResult<UserDtoSolicitarTokenResult>.BadRequest("Usuário não localizado");

                var token = GerarToken();

                // Salvar o token na tabela AspNetUserTokens
                await _userManager.SetAuthenticationTokenAsync(user, "Default", "RecuperacaoSenha", token);

                var email = new EmailDto(request.UserDtoRequestEmail.Email, "Recupeção de Email", $"Segue o código para recuperação  de senha: {token}");
                var email_aletar = new EmailDto(request.UserDtoRequestEmail.Email, "ALERTA SEGURANÇA", "Atenção. Foi solicitado código para recuperação de senha.");


                var notificacao_alertar = new EmailNotification(email_aletar);
                var notificacao = new EmailNotification(email);

                await Task.WhenAll(_mediator.Publish(notificacao_alertar),
                                   _mediator.Publish(notificacao));

                var recuperacaoSenhaResult = new UserDtoSolicitarTokenResult();

                return RequestResult<UserDtoSolicitarTokenResult>.Ok(recuperacaoSenhaResult);
            }
            catch (Exception ex)
            {
                return RequestResult<UserDtoSolicitarTokenResult>.BadRequest(ex.Message);
            }
        }

        private string GerarToken()
        {
            var token = Guid.NewGuid().ToString().Replace("-", string.Empty).Trim();
            var token6Char = token.Substring(0, 6);
            return token6Char;
        }
    }
}
