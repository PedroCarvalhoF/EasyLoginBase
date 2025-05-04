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

    private IQueryable<MovimentacaoEstoqueProdutoEntity> IncludeQuery(IQueryable<MovimentacaoEstoqueProdutoEntity> query)
    {
        query = query
            .Include(e => e.Produto)
            .Include(e => e.Filial);

        query = query.OrderByDescending(e => e.DataMovimentacao);
        return query;
    }

    public async Task<IEnumerable<MovimentacaoEstoqueProdutoEntity>> SelectAllAsync(FiltroBase users)
    {
        try
        {
            var clienteId = users.clienteId;

            var query = _dbSet
                .AsNoTracking()
                .Where(e => e.ClienteId == clienteId);

            query = IncludeQuery(query);

            var entities = await query.ToArrayAsync();

            return entities;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<MovimentacaoEstoqueProdutoEntity>> SelectByFiltroAsync(MovimentacaoEstoqueProdutoEntityFiltro filtro, FiltroBase user, bool include)
    {
        try
        {
            var clienteId = user.clienteId;

            var query = _dbSet
                .AsNoTracking()
                .Where(e => e.ClienteId == clienteId);

            query = IncludeQuery(query);

            if (filtro.IdMovimento.HasValue)
            {
                query = query.Where(mov => mov.Id == filtro.IdMovimento);
            }
            else
            {
                if (filtro.ProdutoId.HasValue)
                    query = query.Where(mov => mov.ProdutoId == filtro.ProdutoId);

                if (filtro.FilialId.HasValue)
                    query = query.Where(mov => mov.FilialId == filtro.FilialId);

                if (filtro.UsuarioRegistroId.HasValue)
                    query = query.Where(mov => mov.UsuarioRegistroId == filtro.UsuarioRegistroId);

                if (filtro.DataMovimentacaoInicial != null && filtro.DataMovimentacaoFinal != null)
                    query = query.Where(mov => mov.DataMovimentacao.Date >= filtro.DataMovimentacaoInicial.Value.Date && mov.DataMovimentacao <= filtro.DataMovimentacaoFinal.Value.Date);

            }



            var entities = await query.ToArrayAsync();

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
