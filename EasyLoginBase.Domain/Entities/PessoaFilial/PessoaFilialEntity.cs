using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Entities.User;

namespace EasyLoginBase.Domain.Entities.PessoaFilial;

public class PessoaFilialEntity
{
    public Guid FilialEntityId { get; private set; }
    public virtual FilialEntity? FilialEntity { get; private set; }
    public Guid UserId { get; private set; }
    public virtual UserEntity? UserEntity { get; private set; }
    public bool AcessoLiberado { get; set; }
    public DateTime DataCadastrado { get; set; }
    private PessoaFilialEntity() { }
    public PessoaFilialEntity(Guid filialId, Guid userId, bool acessoLiberado, DateTime dataCadastro)
    {
        FilialEntityId = filialId;
        UserId = userId;
        AcessoLiberado = acessoLiberado;
        DataCadastrado = dataCadastro;
    }
    public static PessoaFilialEntity Create(Guid filialId, Guid userId)
    => new(filialId, userId, true, DateTime.Now);
}
