namespace EasyLoginBase.Application.Dto.UsuarioVinculadoCliente;
public class UsuarioVinculadoClienteDto
{
    public Guid ClienteId { get; set; }
    public string? ClienteNome { get; set; }
    public Guid IdUsuarioVinculado { get; set; }
    public string? NomeUsuarioVinculado { get; set; }
    public string? EmailUsuarioVinculado { get; set; }
    public bool AcessoPermitido { get; set; }
}
