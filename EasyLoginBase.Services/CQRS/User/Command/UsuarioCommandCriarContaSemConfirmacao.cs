using EasyLoginBase.Application.Constants;
using EasyLoginBase.Application.Dto.Email;
using EasyLoginBase.Application.Dto.User;
using EasyLoginBase.Application.Dto;
using EasyLoginBase.Domain.Entities.User;
using EasyLoginBase.Services.Services.Email;
using MediatR;
using Microsoft.AspNetCore.Identity;
using EasyLoginBase.Services.Tools.UseCase;

namespace EasyLoginBase.Services.CQRS.User.Command;

public class UsuarioCommandCriarContaSemConfirmacao : IRequest<RequestResult<UserDto>>
{
    public required UserDtoCriarContaRequest UserDtoCriarContaRequest { get; set; }

    public class UsuarioCommandCriarContaSemConfirmacaoHandler(UserManager<UserEntity> _userManager)
     : IRequestHandler<UsuarioCommandCriarContaSemConfirmacao, RequestResult<UserDto>>
    {
        public async Task<RequestResult<UserDto>> Handle(UsuarioCommandCriarContaSemConfirmacao request, CancellationToken cancellationToken)
        {
            try
            {
                var userExists = await _userManager.FindByEmailAsync(request.UserDtoCriarContaRequest.Email);
                if (userExists != null)
                    return RequestResult<UserDto>.BadRequest("E-mail já está em uso");

                // Criar usuário com e-mail não confirmado e desbloqueado
                var userCreateEntity = UserEntity.Create(
                    request.UserDtoCriarContaRequest.Nome,
                    request.UserDtoCriarContaRequest.SobreNome,
                    request.UserDtoCriarContaRequest.Email,
                    request.UserDtoCriarContaRequest.Email
                );               

                userCreateEntity.EmailConfirmed = false; // Não confirmado
                userCreateEntity.LockoutEnabled = false; // NÃO permite bloqueio automático
                

                var userCreateResult = await _userManager.CreateAsync(userCreateEntity, request.UserDtoCriarContaRequest.Senha);
                if (!userCreateResult.Succeeded)
                    return RequestResult<UserDto>.BadRequest(userCreateResult.Errors.Select(r => r.Description).FirstOrDefault()!);
                
                // **Garantir que o usuário está desbloqueado**
                await _userManager.SetLockoutEndDateAsync(userCreateEntity, null);

                userCreateEntity.LockoutEnabled = false;
                await _userManager.UpdateAsync(userCreateEntity);


                var user = await _userManager.FindByEmailAsync(userCreateEntity.Email);
                if (user is not null && user.LockoutEnd != null)
                {
                    await _userManager.SetLockoutEndDateAsync(user, null);
                    await _userManager.UpdateAsync(user);
                }


                

                var userDto = DtoMapper.ParceUserDto(userCreateEntity);
                return RequestResult<UserDto>.Ok(userDto, "Cadastro realizado com sucesso, sem confirmação de e-email.");
            }
            catch (Exception ex)
            {
                return RequestResult<UserDto>.BadRequest(ex.Message);
            }
        }

        private string GerarToken()
        {
            var token = Guid.NewGuid().ToString().Replace("-", string.Empty).Trim();
            return token.Substring(0, 6);
        }
    }
}