using EasyLoginBase.Domain.Entities.User;

namespace EasyLoginBase.Domain.Entities.PessoaCliente;

public class PessoaClienteEntity
{
    public Guid Id { get; set; }
    public Guid UsuarioEntityClienteId { get; set; }
    public UserEntity? UsuarioEntityCliente { get; set; }
    public string? NomeFantasia { get; set; }
    public DateTime DataAbertura { get; set; }
    public DateTime DataVencimentoUso { get; set; }
    public bool EntidadeValidada => ValidarEntidade();
    private bool ValidarEntidade()
    {
        return true;
    }

    public PessoaClienteEntity() { }
    PessoaClienteEntity(Guid id, Guid usuarioEntityClienteId, UserEntity? usuarioEntityCliente, string? nomeFantasia, DateTime dataAbertura, DateTime dataVencimentoUso)
    {
        Id = id;
        UsuarioEntityClienteId = usuarioEntityClienteId;
        UsuarioEntityCliente = usuarioEntityCliente;
        NomeFantasia = nomeFantasia;
        DataAbertura = dataAbertura;
        DataVencimentoUso = dataVencimentoUso;
    }
    public static PessoaClienteEntity CriarUsuarioPessoaCliente(Guid usuarioEntityClienteId, string nomeFantasia)
    => new PessoaClienteEntity(usuarioEntityClienteId, usuarioEntityClienteId, null, nomeFantasia, DateTime.Now, DateTime.Now.AddMonths(1));

}
