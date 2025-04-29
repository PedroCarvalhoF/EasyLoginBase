using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Entities.Preco.Produto;
using EasyLoginBase.Domain.Entities.Produto.Estoque;
using System.ComponentModel.DataAnnotations;

namespace EasyLoginBase.Domain.Entities.Produto;

public class ProdutoEntity : BaseClienteEntity
{
    public string? NomeProduto { get; private set; }
    public string? CodigoProduto { get; private set; }
    public Guid CategoriaProdutoEntityId { get; private set; }
    public virtual CategoriaProdutoEntity? CategoriaProdutoEntity { get; private set; }
    public Guid UnidadeMedidaProdutoEntityId { get; private set; }
    public virtual UnidadeMedidaProdutoEntity? UnidadeMedidaProdutoEntity { get; private set; }     
    public virtual ICollection<PrecoProdutoEntity>? PrecosEntities { get; private set; }
    public virtual ICollection<EstoqueProdutoEntity>? EstoqueProdutoEntities { get; private set; }
    public bool EntidadeValidada => ValidarProduto();
    private bool ValidarProduto()
    {
        var validator = new ProdutoValidator();
        var resultado = validator.Validate(this);

        if (!resultado.IsValid)
        {
            var erros = string.Join("; ", resultado.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Validação falhou: {erros}");
        }

        return true;
    }
    public ProdutoEntity() { }
    ProdutoEntity(string nomeProduto, string codigoProduto, Guid categoriaProdutoEntityId, Guid unidadeProdutoId, Guid clienteId, Guid usuarioRegistroId) : base(clienteId, usuarioRegistroId)
    {
        NomeProduto = nomeProduto;
        CodigoProduto = codigoProduto;
        CategoriaProdutoEntityId = categoriaProdutoEntityId;
        UnidadeMedidaProdutoEntityId = unidadeProdutoId;
    }

    public static ProdutoEntity CriarProdutoEntity(string nomeProduto, string codigoProduto, Guid categoriaProdutoEntityId, Guid unidadeProdutoId, Guid clienteId, Guid usuarioRegistroId)
        => new ProdutoEntity(nomeProduto, codigoProduto, categoriaProdutoEntityId, unidadeProdutoId, clienteId, usuarioRegistroId);

    public void AlterarNome(string novoNome)
    {
        if (string.IsNullOrWhiteSpace(novoNome))
            throw new ArgumentException("O nome do produto não pode ser vazio ou nulo.", nameof(novoNome));

        NomeProduto = novoNome;
        AtualizarData();
    }

    public void AlterarCodigo(string novoCodigo)
    {
        if (string.IsNullOrWhiteSpace(novoCodigo))
            throw new ArgumentException("O código do produto não pode ser vazio ou nulo.", nameof(novoCodigo));

        CodigoProduto = novoCodigo;
        AtualizarData();
    }

    public void AlterarCategoria(Guid novaCategoriaId)
    {
        CategoriaProdutoEntityId = novaCategoriaId;
        AtualizarData();
    }

    public void AlterarUnidadeMedidaProduto(Guid unidadeMedidaProdutoId)
    {
        UnidadeMedidaProdutoEntityId = unidadeMedidaProdutoId;
        AtualizarData();
    }
}
