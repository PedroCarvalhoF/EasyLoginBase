namespace EasyLoginBase.Application.Dto.Produto.Estoque.Estoque;
public class EstoqueProdutoDto
{
    public Guid ProdutoId { get; private set; }
    public string NomeProduto { get; private set; }
    public Guid FilialId { get; private set; }
    public string NomeFilial { get; private set; }
    public decimal Quantidade { get; private set; }
    public EstoqueProdutoDto(Guid produtoId, string nomeProduto, Guid filialId, string nomeFilial, decimal quantidade)
    {
        ProdutoId = produtoId;
        NomeProduto = nomeProduto;
        FilialId = filialId;
        NomeFilial = nomeFilial;
        Quantidade = quantidade;
    }
}
