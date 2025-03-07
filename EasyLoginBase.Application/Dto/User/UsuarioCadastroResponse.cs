namespace EasyLoginBase.Application.Dto.User;

public class UsuarioCadastroResponse
{
    public bool? Sucesso { get; private set; }
    public List<string>? Erros { get; private set; }
    public Guid IdUserCreate { get; private set; }
    public UsuarioCadastroResponse() => Erros = new List<string>();
    public UsuarioCadastroResponse(bool sucesso, Guid idUserCreate)
    {
        Sucesso = sucesso;
        IdUserCreate = idUserCreate;
    }
    public void AdicionarErros(IEnumerable<string> erros) => Erros!.AddRange(erros);
}
