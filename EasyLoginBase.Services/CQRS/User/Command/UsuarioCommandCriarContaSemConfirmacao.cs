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

    public class UsuarioCommandCriarContaSemConfirmacaoHandler(UserManager<UserEntity> _userManager, IEmailService _emailService)
     : IRequestHandler<UsuarioCommandCriarContaSemConfirmacao, RequestResult<UserDto>>
    {
        public async Task<RequestResult<UserDto>> Handle(UsuarioCommandCriarContaSemConfirmacao request, CancellationToken cancellationToken)
        {
            try
            {
                var userExists = await _userManager.FindByEmailAsync(request.UserDtoCriarContaRequest.Email);
                if (userExists != null)
                    return RequestResult<UserDto>.BadRequest("E-mail já está em uso");

                // Criar usuário com e-mail não confirmado e bloqueado
                var userCreateEntity = UserEntity.Create(
                    request.UserDtoCriarContaRequest.Nome,
                    request.UserDtoCriarContaRequest.SobreNome,
                    request.UserDtoCriarContaRequest.Email,
                    request.UserDtoCriarContaRequest.Email
                );

                userCreateEntity.EmailConfirmed = true; // Não confirmado
                userCreateEntity.LockoutEnabled = false; // Bloqueio ativado
                userCreateEntity.LockoutEnd = DateTimeOffset.Now.AddMonths(1); // Bloqueado indefinidamente

                var userCreateResult = await _userManager.CreateAsync(userCreateEntity, request.UserDtoCriarContaRequest.Senha);
                if (!userCreateResult.Succeeded)
                    return RequestResult<UserDto>.BadRequest(userCreateResult.Errors.Select(r => r.Description).FirstOrDefault()!);

                // Gerar token de confirmação
                var token = GerarToken();
                await _userManager.SetAuthenticationTokenAsync(userCreateEntity, Tokens.Default, Tokens.AberturaContaToken, token);

                // Enviar e-mail de confirmação
                var emailDto = EmailDto.ConfirmacaoEmail(userCreateEntity.Email!, token);
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