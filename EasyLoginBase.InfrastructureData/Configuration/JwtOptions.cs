namespace EasyLoginBase.InfrastructureData.Configuration;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecurityKey { get; set; }
    public int AccessTokenExpiration { get; set; }
    public int RefreshTokenExpiration { get; set; }
    public JwtOptions(string issuer, string audience, string securityKey, int accessTokenExpiration, int refreshTokenExpiration)
    {
        Issuer = issuer;
        Audience = audience;
        SecurityKey = securityKey;
        AccessTokenExpiration = accessTokenExpiration;
        RefreshTokenExpiration = refreshTokenExpiration;
    }
}
