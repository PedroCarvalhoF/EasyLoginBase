namespace EasyLoginBase.Application.Dto.User;

public class UsuarioLoginResponse
{
    public bool Sucesso => !Erros.Any();
    public List<string> Erros { get; } = new();
    public Guid IdUser { get; private set; }
    public string? Nome { get; private set; }
    public string? Email { get; private set; }
    public string? AccessToken { get; private set; }
    public string? RefreshToken { get; private set; }

    public UsuarioLoginResponse() { }
    public UsuarioLoginResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
    public void DefinirDetalhesUsuario(Guid idUser, string nome, string email)
    {
        IdUser = idUser;
        Nome = nome;
        Email = email;
    }

    public void AdicionarErro(string erro) => Erros.Add(erro);
    public void AdicionarErros(params string[] erros) => Erros.AddRange(erros);
    public void AdicionarErros(IEnumerable<string> erros) => Erros.AddRange(erros);
}
