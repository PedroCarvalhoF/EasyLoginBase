using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Entities.Produto;
using FluentValidation;
using FluentValidation.Results;

namespace EasyLoginBase.Domain.Entities.PDV;

public class ItemPedidoEntity : BaseClienteEntity
{
    #region Construtor
    private ItemPedidoEntity() { } // Para uso do EF Core
    private ItemPedidoEntity(Guid produtoId, decimal quantidade, decimal preco, decimal desconto, string? observacao, Guid pedidoId, Guid clienteId, Guid usuarioRegistroId)
      : base(pedidoId, usuarioRegistroId)
    {
        ProdutoId = produtoId;
        Quantidade = quantidade;
        Preco = preco;
        Desconto = desconto;
        Observacao = observacao;
        PedidoId = pedidoId;

        Validar(); // Garantir que o objeto é válido no momento da criação
    }
    public static ItemPedidoEntity CriarItemPedido(Guid produtoId, decimal quantidade, decimal preco, decimal desconto, string? observacao, Guid pedidoId, Guid clienteId, Guid usuarioRegistroId)
    => new ItemPedidoEntity(produtoId, quantidade, preco, desconto, observacao, pedidoId, clienteId, usuarioRegistroId);

    #endregion

    #region Propriedades
    public Guid ProdutoId { get; private set; }
    public ProdutoEntity? Produto { get; private set; }
    public decimal Quantidade { get; private set; }
    public decimal Preco { get; private set; }
    public decimal Desconto { get; private set; }
    public string? Observacao { get; private set; }
    public Guid PedidoId { get; private set; }
    public PedidosEntity? Pedido { get; private set; }
    public decimal SubTotal => Quantidade * Preco;
    public decimal Total => SubTotal - Desconto;
    #endregion

    #region Métodos de Negócio
    public void AtualizarQuantidade(decimal novaQuantidade)
    {
        Quantidade = novaQuantidade;
        Validar();
    }
    public void AtualizarPreco(decimal novoPreco)
    {
        Preco = novoPreco;
        Validar();
    }
    public void AplicarDesconto(decimal novoDesconto)
    {
        Desconto = novoDesconto;
        Validar();
    }
    public void DefinirObservacao(string? novaObservacao)
    {
        Observacao = novaObservacao;
        Validar();
    }
    private void Validar()
    {
        var validator = new ItemPedidoValidation();
        ValidationResult result = validator.Validate(this);

        if (!result.IsValid)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Validação falhou: {errors}");
        }

        AtualizarData();
    }
    #endregion
}
