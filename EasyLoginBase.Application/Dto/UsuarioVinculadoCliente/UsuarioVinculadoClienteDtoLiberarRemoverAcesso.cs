namespace EasyLoginBase.Application.Dto.UsuarioVinculadoCliente;
public class UsuarioVinculadoClienteDtoLiberarRemoverAcesso
{
    public required bool LiberarAcesso { get; set; }
    public required string EmailUsuarioVinculado { get; set; }
}
