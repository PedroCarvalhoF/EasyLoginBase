namespace EasyLoginBase.Domain.Interfaces.Produto;

public interface IProdutoRepository
{
    Task<bool> NomeProdutoUso(string nomeProduto);
}
