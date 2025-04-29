using EasyLoginBase.Domain.Entities.Produto.Estoque;

namespace EasyLoginBase.Domain.Interfaces.Produto.Estoque;
public interface IEstoqueProdutoRepository<E> where E : EstoqueProdutoEntity
{
    Task<IEnumerable<E>> SelectAllAsync(Guid clienteId, bool include = true);
    Task<IEnumerable<E>> SelectByProdutoId(Guid clienteId, Guid produtoId, bool include = true);
    Task<IEnumerable<E>> SelectByFiliaId(Guid clienteId, Guid filialId, bool include = true);
    Task<E?> SelectByProdutoIdFilialId(Guid clienteId, Guid produtoId, Guid filialId, bool include = true);
}
