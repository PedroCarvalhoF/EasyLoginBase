namespace EasyLoginBase.Application.Dto.Produto.Estoque.Estoque;
public class EstoqueProdutoDtoManterMultiplosProdutos
{
    public IEnumerable<EstoqueProdutoDtoManter> EstequeProdutos { get; private set; }

    public EstoqueProdutoDtoManterMultiplosProdutos(IEnumerable<EstoqueProdutoDtoManter> estequeProdutos)
    {
        EstequeProdutos = estequeProdutos;
    }
}

