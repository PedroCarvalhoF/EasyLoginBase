namespace EasyLoginBase.Application.Dto.Preco.Produto;

public class PrecoProdutoDto
{
    public Guid Id { get; set; }
    public Guid ProdutoEntityId { get; set; }
    public string? NomeProduto { get; set; }
    public Guid FilialEntityId { get; set; }
    public string? NomeFilial { get; set; }
    public Guid CategoriaPrecoProdutoEntityId { get; set; }
    public string? CategoriaPreco { get; set; }
    public decimal PrecoProduto { get; set; }
    public string? TipoPrecoProduto { get; set; }
}
