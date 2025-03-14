namespace EasyLoginBase.Application.Dto.PessoaClienteVinculada;

public class PessoaClienteVinculadaDtoUpdate
{
    public Guid PessoaClienteEntityId { get; private set; }
    public Guid UsuarioVinculadoId { get; private set; }
    public bool AcessoPermitido { get; private set; }
    public PessoaClienteVinculadaDtoUpdate(Guid pessoaClienteEntityId, Guid usuarioVinculadoId, bool acessoPermitido)
    {
        PessoaClienteEntityId = pessoaClienteEntityId;
        UsuarioVinculadoId = usuarioVinculadoId;
        AcessoPermitido = acessoPermitido;
    }
}