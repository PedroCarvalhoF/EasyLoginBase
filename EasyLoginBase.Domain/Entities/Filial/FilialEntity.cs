using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Entities.Preco.Produto;
using System.ComponentModel.DataAnnotations;

namespace EasyLoginBase.Domain.Entities.Filial;

public class FilialEntity : BaseClienteEntity
{
    // Uma filial pertence a um cliente
    public Guid PessoaClienteId { get; private set; }
    public virtual PessoaClienteEntity? PessoaCliente { get; private set; }
    public string? NomeFilial { get; private set; }

    // Uma filial pode ter varios precos
    public virtual ICollection<PrecoProdutoEntity>? PrecoProdutoEntities { get; private set; }
    public bool EntidadeValidada => ValidarProduto();
    public FilialEntity() { }
    FilialEntity(Guid pessoaClienteId, string nomeFilial, Guid clienteId, Guid usuarioRegistroId) : base(clienteId, usuarioRegistroId)
    {
        PessoaClienteId = pessoaClienteId;
        NomeFilial = nomeFilial;
    }
    public static FilialEntity CriarFilial(Guid pessoaClienteId, string nomeFilial, Guid clienteId, Guid usuarioRegistroId)
    => new FilialEntity(pessoaClienteId, nomeFilial, clienteId, usuarioRegistroId);
    private bool ValidarProduto()
    {
        var validator = new FilialValidator();
        var resultado = validator.Validate(this);

        if (!resultado.IsValid)
        {
            var erros = string.Join("; ", resultado.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Validação falhou: {erros}");
        }

        return true;
    }
    public void AtualizarNome(string nomeFilial)
    {
        if (string.IsNullOrWhiteSpace(nomeFilial))
            throw new ArgumentException("O nome da filial não pode ser vazio ou nulo.", nameof(nomeFilial));

        NomeFilial = nomeFilial;
    }
}
