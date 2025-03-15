using EasyLoginBase.Domain.Entities.PessoaCliente;

namespace EasyLoginBase.Domain.Entities.Filial;

public class FilialEntity
{
    public Guid Id { get; private set; }

    // Uma filial pertence a um cliente
    public Guid PessoaClienteId { get; private set; }
    public PessoaClienteEntity? PessoaCliente { get; private set; }
    public string? NomeFilial { get; private set; }
    public DateTime DataCriacaoFilial { get; private set; }
    public bool Habilitada { get; private set; }

    public FilialEntity() { }
    private FilialEntity(Guid id, Guid pessoaClienteId, PessoaClienteEntity? pessoaCliente, string nomeFilial, DateTime dataCriacaoFilial, bool habilitada)
    {
        Id = id;
        PessoaClienteId = pessoaClienteId;
        PessoaCliente = pessoaCliente;
        NomeFilial = nomeFilial;
        DataCriacaoFilial = dataCriacaoFilial;
        Habilitada = habilitada;
    }

    public static FilialEntity CriarFilial(Guid pessoaClienteId, string nomeFilial, DateTime? dataCriacao = null)
        => new FilialEntity(Guid.NewGuid(), pessoaClienteId, null, nomeFilial, dataCriacao ?? DateTime.Now, true);

    public void AtualizarNome(string nomeFilial)
    {
        if (string.IsNullOrWhiteSpace(nomeFilial))
            throw new ArgumentException("O nome da filial não pode ser vazio ou nulo.", nameof(nomeFilial));

        NomeFilial = nomeFilial;
    }

    public void DesativarFilial() => Habilitada = false;
    public void AtivarFilial() => Habilitada = true;
}
