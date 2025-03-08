using EasyLoginBase.Application.Constants;
using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Email;
using EasyLoginBase.Application.Dto.User;
using EasyLoginBase.Domain.Entities.User;
using EasyLoginBase.Services.Services.Email;
using EasyLoginBase.Services.Tools.UseCase;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EasyLoginBase.Services.CQRS.Command;

public class UserCriarContaCommand : IRequest<RequestResult<UserDto>>
{
    public required UserCriarContaDtoRequest UserCreate { get; set; }

    public class UserCreateCommandHandler(UserManager<UserEntity> _userManager, IEmailService _emailService)
     : IRequestHandler<UserCriarContaCommand, RequestResult<UserDto>>
    {
        public async Task<RequestResult<UserDto>> Handle(UserCriarContaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userExists = await _userManager.FindByEmailAsync(request.UserCreate.Email);
                if (userExists != null)
                    return RequestResult<UserDto>.BadRequest("E-mail já está em uso");

                // Criar usuário com e-mail não confirmado e bloqueado
                var userCreateEntity = UserEntity.Create(
                    request.UserCreate.Nome,
                    request.UserCreate.SobreNome,
                    request.UserCreate.Email,
                    request.UserCreate.Email
                );

                userCreateEntity.EmailConfirmed = false; // Não confirmado
                userCreateEntity.LockoutEnabled = true; // Bloqueio ativado
                userCreateEntity.LockoutEnd = DateTimeOffset.Now.AddMonths(1); // Bloqueado indefinidamente

                var userCreateResult = await _userManager.CreateAsync(userCreateEntity, request.UserCreate.Senha);
                if (!userCreateResult.Succeeded)
                    return RequestResult<UserDto>.BadRequest(userCreateResult.Errors.Select(r => r.Description).FirstOrDefault());

                // Gerar token de confirmação
                var token = GerarToken();
                await _userManager.SetAuthenticationTokenAsync(userCreateEntity, Tokens.Default, Tokens.AberturaContaToken, token);

                // Enviar e-mail de confirmação
                var emailDto = EmailDto.ConfirmacaoEmail(userCreateEntity.Email, token);
                await _emailService.EnviarEmailAsync(emailDto);

                var userDto = DtoMapper.ParceUserDto(userCreateEntity);
                return RequestResult<UserDto>.Ok(userDto, "Cadastro realizado com sucesso! Verifique seu e-mail para confirmar a conta.");
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
