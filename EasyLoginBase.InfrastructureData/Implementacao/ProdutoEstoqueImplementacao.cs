using EasyLoginBase.Domain.Entities.Produto.Estoque;
using EasyLoginBase.Domain.Interfaces.Produto.Estoque;
using EasyLoginBase.InfrastructureData.Context;
using EasyLoginBase.InfrastructureData.Repository;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.InfrastructureData.Implementacao;
public class ProdutoEstoqueImplementacao : BaseClienteRepository_REFACTOR<EstoqueProdutoEntity>, IEstoqueProdutoRepository<EstoqueProdutoEntity>
{
    //private readonly MyContext _context;
    private readonly DbSet<EstoqueProdutoEntity> _dbSet;

    public ProdutoEstoqueImplementacao(MyContext context) : base(context)
    {
        //_context = context;
        _dbSet = context.Set<EstoqueProdutoEntity>();
    }

    private IQueryable<EstoqueProdutoEntity> IncludeQuery(IQueryable<EstoqueProdutoEntity> query)
    {
        query = query
                .Include(e => e.Produto)
                .ThenInclude(e => e.UnidadeMedidaProdutoEntity)
                .Include(e => e.Produto.CategoriaProdutoEntity)

                .Include(e => e.Filial);

        query = query.OrderBy(e => e.Produto.NomeProduto);

        return query;
    }
    public async Task<IEnumerable<EstoqueProdutoEntity>> SelectAllAsync(Guid clienteId, bool include = false)
    {
        try
        {
            var query = _dbSet.AsNoTracking()
                 .Where(e => e.ClienteId == clienteId);

            if (include)
                query = IncludeQuery(query);

            return await query.ToArrayAsync();

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    public async Task<IEnumerable<EstoqueProdutoEntity>> SelectByFiliaId(Guid clienteId, Guid filialId, bool include = false)
    {
        try
        {
            var query = _dbSet.AsNoTracking()
                 .Where(e => e.ClienteId == clienteId && e.FilialId == filialId);

            if (include)
                query = IncludeQuery(query);

            return await query.ToArrayAsync();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    public async Task<IEnumerable<EstoqueProdutoEntity>> SelectByProdutoId(Guid clienteId, Guid produtoId, bool include = false)
    {
        try
        {
            var query = _dbSet.AsNoTracking()
                 .Where(e => e.ClienteId == clienteId && e.ProdutoId == produtoId);

            if (include)
                query = IncludeQuery(query);

            return await query.ToArrayAsync();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    public async Task<EstoqueProdutoEntity?> SelectByProdutoIdFilialId(Guid clienteId, Guid produtoId, Guid filialId, bool include = false)
    {
        try
        {
            var query = _dbSet
                 .Where(e => e.ClienteId == clienteId)
                 .Where(e => e.ProdutoId == produtoId && e.FilialId == filialId);

            if (include)
                query = IncludeQuery(query);

            return await query.SingleOrDefaultAsync();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
}
