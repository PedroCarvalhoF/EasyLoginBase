using EasyLoginBase.Domain.Entities.User;

namespace EasyLoginBase.Domain.Entities.PessoaCliente;

public class PessoaClienteVinculadaEntity
{
    public Guid PessoaClienteEntityId { get; set; }
    public virtual PessoaClienteEntity? PessoaClienteEntity { get; set; }
    public Guid UsuarioVinculadoId { get; set; }
    public virtual UserEntity? UsuarioVinculado { get; set; }
    public bool AcessoPermitido { get; set; }
    public PessoaClienteVinculadaEntity() { }
    PessoaClienteVinculadaEntity(Guid pessoaClienteEntityId, PessoaClienteEntity? pessoaClienteEntity, Guid usuarioVinculadoId, UserEntity? usuarioVinculado, bool acessoPermitido)
    {
        PessoaClienteEntityId = pessoaClienteEntityId;
        PessoaClienteEntity = pessoaClienteEntity;
        UsuarioVinculadoId = usuarioVinculadoId;
        UsuarioVinculado = usuarioVinculado;
        AcessoPermitido = acessoPermitido;
    }

    public static PessoaClienteVinculadaEntity Create(Guid pessoaClienteEntityId, Guid usuarioVinculadoId)
        => new(pessoaClienteEntityId, null, usuarioVinculadoId, null, true);
}
