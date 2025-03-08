using EasyLoginBase.InfrastructureData.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EasyLoginBase.Extensions;

public static class AuthenticationSetup
{
    public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions!.SecurityKey!));

        services.Configure<JwtOptions>(options =>
        {
            options.Issuer = jwtOptions.Issuer;
            options.Audience = jwtOptions.Audience;
            options.SecurityKey = jwtOptions.SecurityKey;
            options.AccessTokenExpiration = jwtOptions.AccessTokenExpiration;
            options.RefreshTokenExpiration = jwtOptions.RefreshTokenExpiration;
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
        });

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,

            RequireExpirationTime = true,
            ValidateLifetime = true,

            ClockSkew = TimeSpan.Zero
        };

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = tokenValidationParameters;
        });
    }
}
