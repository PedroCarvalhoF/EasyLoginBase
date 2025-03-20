namespace EasyLoginBase.Application.Dto.PDV.Usuario;

public class UsuarioPdvDto
{
    public bool AcessoCaixa { get;  set; }
    public Guid UsuarioCaixaPdvEntityId { get;  set; }
    public string? UsuarioCaixaPdvEntityNome { get;  set; }
    public string? Email { get;  set; }
}
