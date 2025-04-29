namespace EasyLoginBase.Application.Dto.Produto.Estoque.Estoque;
public class EstoqueProdutoDtoManter
{
    public Guid ProdutoId { get; private set; }
    public Guid FilialId { get; private set; }
    public decimal Quantidade { get; private set; }
    public EstoqueProdutoDtoOperacao EstoqueProdutoDtoOperacao { get; private set; }
    public EstoqueProdutoDtoManter(Guid produtoId, Guid filialId, decimal quantidade, EstoqueProdutoDtoOperacao estoqueProdutoDtoOperacao)
    {
        ProdutoId = produtoId;
        FilialId = filialId;
        Quantidade = quantidade;
        EstoqueProdutoDtoOperacao = estoqueProdutoDtoOperacao;
    }
}
