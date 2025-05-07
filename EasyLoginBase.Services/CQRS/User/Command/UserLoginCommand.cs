using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.User;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Entities.User;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.InfrastructureData.Configuration;
using EasyLoginBase.Services.CQRS;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class UserLoginCommand : BaseCommands<UserDtoLoginResponse>
{
    public required UserDtoLoginRequest UserLoginDtoRequest { get; set; }
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, RequestResult<UserDtoLoginResponse>>
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly JwtOptions _jwtOptions;
        private readonly IUnitOfWork _repository;

        public UserLoginCommandHandler(SignInManager<UserEntity> signInManager,
                           UserManager<UserEntity> userManager,
                           IOptions<JwtOptions> jwtOptions,
                           IUnitOfWork repository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
            _repository = repository;
        }
        public async Task<RequestResult<UserDtoLoginResponse>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var usuarioLoginResponse = new UserDtoLoginResponse();
                var result = await _signInManager.PasswordSignInAsync(request.UserLoginDtoRequest.Email, request.UserLoginDtoRequest.Senha, false, true);

                if (!result.Succeeded)
                {
                    usuarioLoginResponse.AdicionarErro(result.IsLockedOut ? "Essa conta está bloqueada" :
                                                      result.IsNotAllowed ? "Essa conta não tem permissão para fazer login" :
                                                      result.RequiresTwoFactor ? "É necessário confirmar o login no seu segundo fator de autenticação" :
                                                      "Usuário ou senha estão incorretos");

                    return RequestResult<UserDtoLoginResponse>.BadRequest(usuarioLoginResponse, usuarioLoginResponse.Erros.First());
                }

                var userSelecionado = await _userManager.FindByEmailAsync(request.UserLoginDtoRequest.Email) ?? throw new Exception("Usuário não encontrado.");

                var usuarioVinculo = await _repository.UsuarioClienteVinculoImplementacao.SelectUsuarioClienteVinculoByUsuarioId(userSelecionado.Id);

                if (usuarioVinculo == null)
                    throw new Exception("Solicite sua Credencial");

                if (!usuarioVinculo.AcessoPermitido)
                    throw new ArgumentException("Solicite sua permissão para continuar");


                usuarioLoginResponse = await GerarCredenciais(userSelecionado, usuarioVinculo);
                usuarioLoginResponse.DefinirDetalhesUsuario(userSelecionado.Id, userSelecionado.Nome!, userSelecionado.Email!);

                return RequestResult<UserDtoLoginResponse>.Ok(usuarioLoginResponse, "Login realizado com sucesso.");
            }
            catch (Exception ex)
            {
                return RequestResult<UserDtoLoginResponse>.BadRequest(ex.Message);
            }
        }
        private async Task<UserDtoLoginResponse> GerarCredenciais(UserEntity user, PessoaClienteVinculadaEntity usuarioVinculo)
        {
            var accessTokenClaims = await ObterClaims(user, true, usuarioVinculo);
            var refreshTokenClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!)
            };

            var accessToken = GerarToken(accessTokenClaims, _jwtOptions.AccessTokenExpiration);
            var refreshToken = GerarToken(refreshTokenClaims, _jwtOptions.RefreshTokenExpiration);

            return new UserDtoLoginResponse(accessToken, refreshToken);
        }
        private async Task<IList<Claim>> ObterClaims(UserEntity user, bool adicionarClaimsUsuario, PessoaClienteVinculadaEntity usuarioVinculo)
        {
            var claims = new List<Claim>
            {
               new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
               new Claim(JwtRegisteredClaimNames.Email, user.Email!),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(JwtRegisteredClaimNames.Nbf, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
               new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            claims.Add(new Claim("UserId", user.Id.ToString()));
            claims.Add(new Claim("ClienteIdVinculo", usuarioVinculo.PessoaClienteEntityId.ToString()));

            if (adicionarClaimsUsuario)
            {
                claims.AddRange(await _userManager.GetClaimsAsync(user));
                foreach (var role in await _userManager.GetRolesAsync(user))
                    claims.Add(new Claim("roles", role));
            }

            return claims;
        }

        private string GerarToken(IEnumerable<Claim> claims, int segundosExpiracao)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.SecurityKey!));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddSeconds(segundosExpiracao),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}

