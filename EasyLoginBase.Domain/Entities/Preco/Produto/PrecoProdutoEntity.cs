using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Entities.Preco.Produto.CategoriaPreco;
using EasyLoginBase.Domain.Entities.Produto;
using EasyLoginBase.Domain.Enuns.Preco.Produto;
using FluentValidation;

namespace EasyLoginBase.Domain.Entities.Preco.Produto;

public class PrecoProdutoEntity : BaseClienteEntity
{
    private PrecoProdutoEntity() { }
    private PrecoProdutoEntity(Guid produtoEntityId, Guid filialEntityId, Guid categoriaPrecoProdutoEntityId, decimal precoProduto, PrecoProdutoEnum tipoPrecoProdutoEnum, Guid clienteId, Guid usuarioRegistroId)
        : base(clienteId, usuarioRegistroId)
    {
        ProdutoEntityId = produtoEntityId;
        FilialEntityId = filialEntityId;
        CategoriaPrecoProdutoEntityId = categoriaPrecoProdutoEntityId;
        PrecoProduto = precoProduto;
        TipoPrecoProdutoEnum = tipoPrecoProdutoEnum;
    }
    public static PrecoProdutoEntity CriarPrecoProdutoEntity(Guid produtoEntityId, Guid filialEntityId, Guid categoriaPrecoProdutoEntityId, decimal precoProduto, PrecoProdutoEnum tipoPrecoProdutoEnum, Guid clienteId, Guid usuarioRegistroId)
    {
        var precoProdutoEntity = new PrecoProdutoEntity(produtoEntityId, filialEntityId, categoriaPrecoProdutoEntityId, precoProduto, tipoPrecoProdutoEnum, clienteId, usuarioRegistroId);

        if (!precoProdutoEntity.ValidarPrecoProduto(out var erros))
        {
            throw new ValidationException($"Falha na validação: {string.Join("; ", erros)}");
        }

        return precoProdutoEntity;
    }

    public Guid ProdutoEntityId { get; private set; }
    public ProdutoEntity? ProdutoEntity { get; private set; }
    public Guid FilialEntityId { get; private set; }
    public FilialEntity? FilialEntity { get; private set; }
    public Guid CategoriaPrecoProdutoEntityId { get; private set; }
    public CategoriaPrecoProdutoEntity? CategoriaPrecoProdutoEntity { get; private set; }
    public decimal PrecoProduto { get; private set; }
    public PrecoProdutoEnum TipoPrecoProdutoEnum { get; private set; }
    public bool ValidarPrecoProduto(out List<string> erros)
    {
        var validator = new PrecoProdutoValidation();
        var resultado = validator.Validate(this);

        var validarBaseCliente = new BaseClienteEntityValidator();
        var resultadoBaseCliente = validarBaseCliente.Validate(this);

        erros = resultado.Errors.Select(e => e.ErrorMessage)
            .Concat(resultadoBaseCliente.Errors.Select(e => e.ErrorMessage))
            .ToList();

        return !erros.Any();
    }

    public void AlterarPreco(decimal precoProduto)
    {
        PrecoProduto = precoProduto;

        if (!this.ValidarPrecoProduto(out var erros))
        {
            throw new ValidationException($"Falha na validação: {string.Join("; ", erros)}");
        }

        AtualizarData();
    }
}
