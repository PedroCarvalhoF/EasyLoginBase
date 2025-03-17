using EasyLoginBase.Domain.Entities.Produto;

namespace EasyLoginBase.Domain.Interfaces.Produto;

public interface ICategoriaProdutoRepository<T> where T : CategoriaProdutoEntity
{
    Task<bool> NomeCategoriaProdutoUso(string nomeCategoria);
}
