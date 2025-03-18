namespace EasyLoginBase.Application.Dto.Preco.Produto;
public class PrecoProdutoDtoCreate
{
    public PrecoProdutoDtoCreate(Guid produtoEntityId, Guid filialEntityId, Guid categoriaPrecoProdutoEntityId, decimal precoProduto, int tipoPrecoProduto)
    {
        ProdutoEntityId = produtoEntityId;
        FilialEntityId = filialEntityId;
        CategoriaPrecoProdutoEntityId = categoriaPrecoProdutoEntityId;
        PrecoProduto = precoProduto;
        TipoPrecoProduto = tipoPrecoProduto;
    }

    public Guid ProdutoEntityId { get; private set; }
    public Guid FilialEntityId { get; private set; }
    public Guid CategoriaPrecoProdutoEntityId { get; private set; }
    public decimal PrecoProduto { get; private set; }
    public int TipoPrecoProduto { get; private set; }
}
