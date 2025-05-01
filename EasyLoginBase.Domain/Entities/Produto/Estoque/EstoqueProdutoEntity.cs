using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Entities.Filial;
using FluentValidation;

namespace EasyLoginBase.Domain.Entities.Produto.Estoque;
public class EstoqueProdutoEntity : BaseClienteEntity
{
    public Guid ProdutoId { get; private set; }
    public virtual ProdutoEntity? Produto { get; private set; }
    public Guid FilialId { get; private set; }
    public virtual FilialEntity? Filial { get; private set; }
    public decimal Quantidade { get; private set; }
    public EstoqueProdutoEntity() { }

    EstoqueProdutoEntity(Guid produtoId, Guid filialId, decimal quantidade, Guid clienteId, Guid usuarioRegistroId)
        : base(clienteId, usuarioRegistroId)
    {
        if (produtoId == Guid.Empty)
            throw new ArgumentException("ProdutoId não pode ser vazio.", nameof(produtoId));

        if (filialId == Guid.Empty)
            throw new ArgumentException("FilialId não pode ser vazio.", nameof(filialId));        

        ProdutoId = produtoId;
        FilialId = filialId;
        Quantidade = quantidade;

        ValidarEstoqueProdutoEntity();
    }

    public static EstoqueProdutoEntity Criar(Guid produtoId, Guid filialId, decimal quantidade, Guid clienteId, Guid usuarioRegistroId)
        => new EstoqueProdutoEntity(produtoId, filialId, quantidade, clienteId, usuarioRegistroId);

    public void ReporEstoque(decimal quantidade)
    {
        if (quantidade <= 0) throw new ArgumentException("Quantidade inválida.");
        Quantidade += quantidade;
        AtualizarData();
    }

    public void BaixarEstoque(decimal quantidade)
    {
        if (quantidade <= 0 || quantidade > Quantidade)
            throw new ArgumentException("Quantidade insuficiente ou inválida.");
        Quantidade -= quantidade;
        AtualizarData();
    }
    public void ValidarEstoqueProdutoEntity()
    {
        var validator = new EstoqueProdutoEntityValidator();
        var result = validator.Validate(this);

        if (!result.IsValid)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Validação falhou: {errors}");
        }
    }

    public void AtualizarQuantidade(decimal quantidade)
    {
        Quantidade += quantidade;
        AtualizarData();
    }
}
