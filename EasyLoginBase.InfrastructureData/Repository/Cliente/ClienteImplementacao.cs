using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Interfaces.Cliente;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.InfrastructureData.Repository.Cliente;
public class ClienteImplementacao : GenericRepository<PessoaClienteEntity>, IClienteRepository<PessoaClienteEntity>
{
    private DbSet<PessoaClienteEntity> _dbSet;
    public ClienteImplementacao(MyContext context) : base(context)
    {
        _dbSet = context.Set<PessoaClienteEntity>();
    }

    private IQueryable<PessoaClienteEntity> Include(IQueryable<PessoaClienteEntity> query)
    {
        query = query.Include(x => x.UsuarioEntityCliente);
        query = query.Include(x => x.Filiais);
        query = query.Include(x => x.UsuariosVinculados).ThenInclude(user=>user.UsuarioVinculado);

        return query;
    }

    public async Task<IEnumerable<PessoaClienteEntity>> SelectAllAsync(bool include = true)
    {
        try
        {
            IQueryable<PessoaClienteEntity> query = _dbSet.AsQueryable();

            if (include)
                query = Include(query);

            var result = await query.ToArrayAsync();

            return result;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<PessoaClienteEntity?> SelectByUsuarioClienteId(Guid UsuarioEntityClienteId, bool include = true)
    {
        try
        {
            IQueryable<PessoaClienteEntity> query = _dbSet.AsQueryable();

            if (include)
                query = Include(query);

            query = query.Where(c => c.UsuarioEntityClienteId == UsuarioEntityClienteId);


            var result = await query.SingleOrDefaultAsync() ?? null;

            return result;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
