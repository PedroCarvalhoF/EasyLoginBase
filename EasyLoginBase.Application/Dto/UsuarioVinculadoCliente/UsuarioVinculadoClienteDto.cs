namespace EasyLoginBase.Application.Dto.UsuarioVinculadoCliente;
public class UsuarioVinculadoClienteDto
{
    public Guid IdUsuarioVinculado { get; set; }
    public string? NomeUsuarioVinculado { get; set; }
    public string? EmailUsuarioVinculado { get; set; }
    public bool AcessoPermitido { get; set; }
}
