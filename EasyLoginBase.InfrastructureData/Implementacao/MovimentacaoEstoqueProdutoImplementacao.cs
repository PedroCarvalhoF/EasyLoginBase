using EasyLoginBase.Domain.Entities;
using EasyLoginBase.Domain.Entities.Produto.Estoque;
using EasyLoginBase.Domain.Interfaces.Produto.MovimentacaoEstoque;
using EasyLoginBase.InfrastructureData.Context;
using EasyLoginBase.InfrastructureData.Repository;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.InfrastructureData.Implementacao;
public class MovimentacaoEstoqueProdutoImplementacao : BaseClienteRepository_REFACTOR<MovimentacaoEstoqueProdutoEntity>, IMovimentacaoEstoqueProdutoRepository<MovimentacaoEstoqueProdutoEntity, FiltroBase>
{

    private readonly DbSet<MovimentacaoEstoqueProdutoEntity> _dbSet;
    public MovimentacaoEstoqueProdutoImplementacao(MyContext context) : base(context)
    {
        _dbSet = context.Set<MovimentacaoEstoqueProdutoEntity>();
    }

    public async Task<IEnumerable<MovimentacaoEstoqueProdutoEntity>> SelectAllAsync(FiltroBase users)
    {
        try
        {
            var clienteId = users.clienteId;

            var entities = await
                _dbSet
                .AsNoTracking()
                .Where(e => e.ClienteId == clienteId)
                .Include(e => e.Produto)
                .Include(e => e.Filial)
                .ToArrayAsync();

            return entities;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<MovimentacaoEstoqueProdutoEntity>> SelectByProdutoIdAsync(Guid produtoId, FiltroBase users)
    {
        try
        {
            var clienteId = users.clienteId;

            var entities = await
                _dbSet
                .AsNoTracking()
                .Where(e => e.ClienteId == clienteId && e.ProdutoId == produtoId)
                .ToArrayAsync();

            return entities;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
