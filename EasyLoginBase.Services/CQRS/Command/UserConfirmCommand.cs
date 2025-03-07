using EasyLoginBase.Application.Dto;
using EasyLoginBase.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EasyLoginBase.Services.CQRS.Command;

public class UserConfirmCommand : IRequest<RequestResult<bool>>
{
    public required string Email { get; set; }
    public required string Codigo { get; set; }

    public class UserConfirmCommandHandler(UserManager<UserEntity> _userManager)
        : IRequestHandler<UserConfirmCommand, RequestResult<bool>>
    {
        public async Task<RequestResult<bool>> Handle(UserConfirmCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    return RequestResult<bool>.BadRequest("Usuário não encontrado.");

                // Recuperar o token salvo no Identity
                var tokenSalvo = await _userManager.GetAuthenticationTokenAsync(user, "Default", "AberturaContaToken");

                if (tokenSalvo != request.Codigo)
                    return RequestResult<bool>.BadRequest("Código inválido ou expirado.");

                // Confirmar o e-mail e desbloquear o usuário
                user.EmailConfirmed = true;
                user.LockoutEnd = null; // Desbloqueia a conta

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                    return RequestResult<bool>.BadRequest("Erro ao ativar a conta.");

                return RequestResult<bool>.Ok(true, "Conta ativada com sucesso!");
            }
            catch (Exception ex)
            {
                return RequestResult<bool>.BadRequest(ex.Message);
            }
        }
    }
}
