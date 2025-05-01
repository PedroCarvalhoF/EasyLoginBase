namespace EasyLoginBase.Application.Dto.Produto.Estoque.Estoque;
public class EstoqueProdutoDto
{
    public Guid ProdutoId { get; private set; }
    public string NomeProduto { get; private set; }
    public Guid CategoriaProdutoEntityId { get; private set; }
    public string CategoriaProduto { get; private set; }
    public Guid FilialId { get; private set; }
    public string NomeFilial { get; private set; }
    public decimal Quantidade { get; private set; }
    public string UnidadeMedidaProduto { get; private set; }
    public EstoqueProdutoDto(Guid produtoId, string nomeProduto, Guid filialId, string nomeFilial, decimal quantidade, string unidadeMedidaProduto, Guid categoriaProdutoEntityId = default, string categoriaProduto = null)
    {
        ProdutoId = produtoId;
        NomeProduto = nomeProduto;
        FilialId = filialId;
        NomeFilial = nomeFilial;
        Quantidade = quantidade;
        UnidadeMedidaProduto = unidadeMedidaProduto;
        CategoriaProdutoEntityId = categoriaProdutoEntityId;
        CategoriaProduto = categoriaProduto;
    }
}
