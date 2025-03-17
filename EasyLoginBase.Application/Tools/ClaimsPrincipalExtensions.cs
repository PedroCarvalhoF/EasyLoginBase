using System.Security.Claims;

namespace EasyLoginBase.Application.Tools;

public static class ClaimsPrincipalExtensions
{
    private static string? GetClaimValue(this ClaimsPrincipal user, string claimType)
    {
        return user?.FindFirst(claimType)?.Value;
    }

    public static string GetUserName(this ClaimsPrincipal user)
    {
        return user.GetClaimValue(ClaimTypes.Name) ?? throw new InvalidOperationException("O usuário não possui um nome associado.");
    }

    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var claimValue = user.GetClaimValue(CustomClaimTypes.UserId);

        if (claimValue is null)
            throw new InvalidOperationException("O token não contém o ID do usuário.");

        if (!Guid.TryParse(claimValue, out var userId))
            throw new InvalidOperationException($"O ID do usuário no token não é um GUID válido: {claimValue}");

        return userId;
    }

    public static Guid GetClienteIdVinculo(this ClaimsPrincipal user)
    {
        var claimValue = user.GetClaimValue(CustomClaimTypes.ClienteIdVinculo);
        if (!Guid.TryParse(claimValue, out var clienteId))
            throw new InvalidOperationException("Não foi localizado o origem do cliente. Verifiquei com admin sua seu cadastro para o acesso.");

        return clienteId;
    }
}

/// <summary>
/// Classe para armazenar os nomes dos claims customizados da aplicação.
/// </summary>
public static class CustomClaimTypes
{
    public const string ClienteIdVinculo = "ClienteIdVinculo";
    public const string UserId = "UserId";
}
