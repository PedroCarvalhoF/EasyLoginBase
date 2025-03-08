using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.User;
using EasyLoginBase.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EasyLoginBase.Services.CQRS.User.Command;

public class UserAlterarSenhaCommand : IRequest<RequestResult<bool>>
{
    public required UserDtoAlterarSenhaRequest UserAlterarSenhaDtoRequest { get; set; }

    public class AlterarSenhaUserCommandHandler : IRequestHandler<UserAlterarSenhaCommand, RequestResult<bool>>
    {
        private readonly UserManager<UserEntity> _userManager;

        public AlterarSenhaUserCommandHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RequestResult<bool>> Handle(UserAlterarSenhaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.UserAlterarSenhaDtoRequest.email);
                if (user == null)
                    return RequestResult<bool>.BadRequest("Usuário não localizado.");

                // Verificar se a senha antiga está correta
                var senhaCorreta = await _userManager.CheckPasswordAsync(user, request.UserAlterarSenhaDtoRequest.SenhaAntiga);
                if (!senhaCorreta)
                    return RequestResult<bool>.BadRequest("A senha antiga está incorreta.");

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resultUpdatePassword = await _userManager.ResetPasswordAsync(user, token, request.UserAlterarSenhaDtoRequest.NovaSenha);

                if (resultUpdatePassword.Succeeded)
                    return RequestResult<bool>.Ok(mensagem: "Senha alterada com sucesso.");

                var erros = string.Join("; ", resultUpdatePassword.Errors.Select(e => e.Description));
                return RequestResult<bool>.BadRequest($"Erro ao alterar senha: {erros}");
            }
            catch (Exception ex)
            {
                return RequestResult<bool>.ServerError($"Erro inesperado: {ex.Message} {ex.InnerException?.Message}");
            }
        }
    }
}
