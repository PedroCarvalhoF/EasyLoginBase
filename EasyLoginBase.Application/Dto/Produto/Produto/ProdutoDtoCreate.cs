namespace EasyLoginBase.Application.Dto.Produto.Produto;

public class ProdutoDtoCreate
{
    public string NomeProduto { get; set; }
    public string CodigoProduto { get; set; }
    public Guid CategoriaProdutoEntityId { get; set; }
    public ProdutoDtoCreate(string nomeProduto, string codigoProduto, Guid categoriaProdutoEntityId)
    {
        NomeProduto = nomeProduto;
        CodigoProduto = codigoProduto;
        CategoriaProdutoEntityId = categoriaProdutoEntityId;
    }
}
