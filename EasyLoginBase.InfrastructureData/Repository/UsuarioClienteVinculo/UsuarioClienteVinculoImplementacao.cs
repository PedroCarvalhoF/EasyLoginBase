using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Interfaces.UsuarioClienteVinculo;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.InfrastructureData.Repository.UsuarioClienteVinculo;
public class UsuarioClienteVinculoImplementacao : GenericRepository<PessoaClienteVinculadaEntity>, IUsuarioClienteVinculoRepository<PessoaClienteVinculadaEntity>
{
    private DbSet<PessoaClienteVinculadaEntity> _dbSet;
    public UsuarioClienteVinculoImplementacao(MyContext context) : base(context)
    {
        _dbSet = context.Set<PessoaClienteVinculadaEntity>();
    }
    private IQueryable<PessoaClienteVinculadaEntity> Include(IQueryable<PessoaClienteVinculadaEntity> query)
    {
        query = query.Include(cliente => cliente.PessoaClienteEntity);
        query = query.Include(user_vinculado => user_vinculado.UsuarioVinculado);
        return query;
    }
    public async Task<PessoaClienteVinculadaEntity?> SelectUsuarioClienteVinculo(Guid clienteId, Guid usuarioId, bool include = true)
    {
        try
        {
            var query = _dbSet.AsQueryable();

            if (include)
                query = Include(query);

            query = query.Where(x => x.PessoaClienteEntityId == clienteId && x.UsuarioVinculadoId == usuarioId);

            var entity = await query.SingleOrDefaultAsync() ?? null;

            return entity;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<PessoaClienteVinculadaEntity?> SelectUsuarioClienteVinculoByUsuarioId(Guid usuarioId, bool include = true)
    {
        try
        {
            var query = _dbSet.AsQueryable();

            if (include)
                query = Include(query);

            query = query.Where(x => x.UsuarioVinculadoId == usuarioId);

            var entity = await query.SingleOrDefaultAsync() ?? null;

            return entity;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
