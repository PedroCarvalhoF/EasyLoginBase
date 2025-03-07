using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.User;
using EasyLoginBase.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EasyLoginBase.Services.CQRS.Command;

public class UserCriarContaConfirmarCommand : IRequest<RequestResult<bool>>
{
    public required UserCriarContaConfirmarDtoRequest UserConfirmarContaDto { get; set; }

    public class UserConfirmCommandHandler(UserManager<UserEntity> _userManager)
        : IRequestHandler<UserCriarContaConfirmarCommand, RequestResult<bool>>
    {
        public async Task<RequestResult<bool>> Handle(UserCriarContaConfirmarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.UserConfirmarContaDto.Email);
                if (user == null)
                    return RequestResult<bool>.BadRequest("Usuário não encontrado.");

                // Recuperar o token salvo no Identity
                var tokenSalvo = await _userManager.GetAuthenticationTokenAsync(user, "Default", "AberturaContaToken");

                if (tokenSalvo != request.UserConfirmarContaDto.Codigo)
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
