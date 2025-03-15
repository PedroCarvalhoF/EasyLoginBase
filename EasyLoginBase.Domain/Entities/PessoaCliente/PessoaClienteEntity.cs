using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Entities.User;

namespace EasyLoginBase.Domain.Entities.PessoaCliente;

public class PessoaClienteEntity
{
    public Guid Id { get; set; }
    public Guid UsuarioEntityClienteId { get; set; }
    public virtual UserEntity? UsuarioEntityCliente { get; set; }
    public string? NomeFantasia { get; set; }
    public DateTime DataAbertura { get; set; }
    public DateTime DataVencimentoUso { get; set; }

    public virtual ICollection<PessoaClienteVinculadaEntity>? UsuariosVinculados { get; set; }
    //uma pessoa cliente pode ter varias filiais
    public virtual ICollection<FilialEntity>? Filiais { get; set; }

    public bool EntidadeValidada => ValidarEntidade();

    private bool ValidarEntidade()
    {
        return true;
    }

    public PessoaClienteEntity() { }

    public PessoaClienteEntity(Guid usuarioEntityClienteId, string nomeFantasia)
    {
        Id = Guid.NewGuid();
        UsuarioEntityClienteId = usuarioEntityClienteId;
        NomeFantasia = nomeFantasia;
        DataAbertura = DateTime.Now;
        DataVencimentoUso = DateTime.Now.AddMonths(1);
    }

    public static PessoaClienteEntity CriarUsuarioPessoaCliente(Guid usuarioEntityClienteId, string nomeFantasia)
    {
        return new PessoaClienteEntity(usuarioEntityClienteId, nomeFantasia);
    }
}
