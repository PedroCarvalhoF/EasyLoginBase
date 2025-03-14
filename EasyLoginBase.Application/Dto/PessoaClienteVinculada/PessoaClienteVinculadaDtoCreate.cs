namespace EasyLoginBase.Application.Dto.PessoaClienteVinculada;

public class PessoaClienteVinculadaDtoCreate
{
    public Guid PessoaClienteId { get; private set; }
    public Guid UsuarioVinculadoId { get; private set; }
    public PessoaClienteVinculadaDtoCreate(Guid pessoaClienteId, Guid usuarioVinculadoId)
    {
        PessoaClienteId = pessoaClienteId;
        UsuarioVinculadoId = usuarioVinculadoId;
    }
}
