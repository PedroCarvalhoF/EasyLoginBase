using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Entities.Filial;

namespace EasyLoginBase.Domain.Entities.Produto.Estoque;

public class MovimentacaoEstoqueProdutoEntity : BaseClienteEntity
{
    public Guid ProdutoId { get; private set; }
    public virtual ProdutoEntity? Produto { get; private set; }
    public Guid FilialId { get; private set; }
    public virtual FilialEntity? Filial { get; private set; }
    public decimal Quantidade { get; private set; }
    public TipoMovimentacaoEstoque Tipo { get; private set; }
    public string? Observacao { get; private set; }
    public DateTime DataMovimentacao { get; private set; }
    public MovimentacaoEstoqueProdutoEntity() { }
    MovimentacaoEstoqueProdutoEntity(Guid estoqueProdutoId, Guid filialId, TipoMovimentacaoEstoque tipo, decimal quantidade, string? observacao, Guid clienteId, Guid usuarioRegistroId)
        : base(clienteId, usuarioRegistroId)
    {
        if (estoqueProdutoId == Guid.Empty)
            throw new ArgumentException("EstoqueProdutoId não pode ser vazio.");

        if (quantidade <= 0)
            throw new ArgumentException("Quantidade não pode ser menor zero.");

        ProdutoId = estoqueProdutoId;
        FilialId = filialId;
        Tipo = tipo;
        Quantidade = quantidade;
        Observacao = observacao;
        DataMovimentacao = DateTime.Now;


        switch (tipo)
        {
            case TipoMovimentacaoEstoque.Entrada:
                break;
            case TipoMovimentacaoEstoque.Saida:
                Quantidade *= -1;
                break;
            case TipoMovimentacaoEstoque.AjustePositivo:
                break;
            case TipoMovimentacaoEstoque.AjusteNegativo:
                break;
            case TipoMovimentacaoEstoque.TransferenciaEntrada:
                break;
            case TipoMovimentacaoEstoque.TransferenciaSaida:
                break;
            default:
                break;
        }
    }

    public static MovimentacaoEstoqueProdutoEntity Entrada(Guid estoqueProdutoId, Guid filialId, decimal quantidade, string? observacao, Guid clienteId, Guid usuarioRegistroId)
       => new MovimentacaoEstoqueProdutoEntity(estoqueProdutoId, filialId, TipoMovimentacaoEstoque.Entrada, quantidade, "Entrada de mercadoria", clienteId, usuarioRegistroId);
    public static MovimentacaoEstoqueProdutoEntity Saida(Guid estoqueProdutoId, Guid filialId, decimal quantidade, string? observacao, Guid clienteId, Guid usuarioRegistroId)
       => new MovimentacaoEstoqueProdutoEntity(estoqueProdutoId, filialId, TipoMovimentacaoEstoque.Saida, quantidade, "Saída de mercadoria", clienteId, usuarioRegistroId);
}
