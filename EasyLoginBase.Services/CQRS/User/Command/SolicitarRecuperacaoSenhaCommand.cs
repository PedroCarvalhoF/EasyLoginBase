using EasyLoginBase.Application.Constants;
using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.User;
using EasyLoginBase.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EasyLoginBase.Services.CQRS.User.Command;

public class SolicitarRecuperacaoSenhaCommand : BaseCommands<UserDtoRecuperarSenhaResult>
{
    public required UserDtoRecuperarSenha UserDto { get; set; }
    public class SolicitarRecuperacaoSenhaCommandHandler(UserManager<UserEntity> _userManager) : IRequestHandler<SolicitarRecuperacaoSenhaCommand, RequestResult<UserDtoRecuperarSenhaResult>>
    {
        public async Task<RequestResult<UserDtoRecuperarSenhaResult>> Handle(SolicitarRecuperacaoSenhaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.UserDto.Email);
                if (user == null)
                    return RequestResult<UserDtoRecuperarSenhaResult>.BadRequest("Usuário não localizado.");

                var storedToken = await _userManager.GetAuthenticationTokenAsync(user!, Tokens.Default, Tokens.RecuperacaoSenha);

                if (storedToken == null || storedToken != request.UserDto.Token)
                {
                    return RequestResult<UserDtoRecuperarSenhaResult>.BadRequest("Código inválido.");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, request.UserDto.NovaSenha);
                if (result.Succeeded)
                {
                    // Remova o token após a redefinição bem-sucedida da senha
                    await _userManager.RemoveAuthenticationTokenAsync(user, Tokens.Default, Tokens.RecuperacaoSenha);

                    var resultRecuperarSenha = new UserDtoRecuperarSenhaResult(true);
                    return RequestResult<UserDtoRecuperarSenhaResult>.Ok(resultRecuperarSenha);
                }

                return RequestResult<UserDtoRecuperarSenhaResult>.BadRequest();
            }
            catch (Exception ex)
            {

                return RequestResult<UserDtoRecuperarSenhaResult>.BadRequest(ex.Message);
            }
        }
    }
}
