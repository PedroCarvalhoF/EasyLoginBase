namespace EasyLoginBase.Application.Dto.Preco.Produto.CategoriaPrecoProduto;

public class CategoriaPrecoProdutoDtoCreate
{
    public string CategoriaPreco { get; private set; }
    public CategoriaPrecoProdutoDtoCreate(string categoriaPreco)
    {
        CategoriaPreco = categoriaPreco;
    }
}
