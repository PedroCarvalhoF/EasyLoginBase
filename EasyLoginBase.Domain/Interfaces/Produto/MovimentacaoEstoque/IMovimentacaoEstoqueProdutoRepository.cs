using EasyLoginBase.Domain.Entities;
using EasyLoginBase.Domain.Entities.Produto.Estoque;

namespace EasyLoginBase.Domain.Interfaces.Produto.MovimentacaoEstoque;
public interface IMovimentacaoEstoqueProdutoRepository<E, U> where E : MovimentacaoEstoqueProdutoEntity where U : FiltroBase
{
    Task<IEnumerable<E>> SelectAllAsync(U users);
    Task<IEnumerable<MovimentacaoEstoqueProdutoEntity>> SelectByFiltroAsync(MovimentacaoEstoqueProdutoEntityFiltro entityFiltro, FiltroBase user, bool include);
    Task<IEnumerable<E>> SelectByProdutoIdAsync(Guid produtoId, U users);
}
