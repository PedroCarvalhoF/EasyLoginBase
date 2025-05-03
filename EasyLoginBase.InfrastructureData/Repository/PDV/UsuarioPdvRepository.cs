using EasyLoginBase.Domain.Entities.PDV;
using EasyLoginBase.Domain.Interfaces.PDV;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.InfrastructureData.Repository.PDV;


//TESTANDO REPOSITORIO MANUAL PARA USUARIO PDV
public class UsuarioPdvRepository : IUsuarioPdvRepository
{
    private readonly MyContext _context;
    private DbSet<UsuarioPdvEntity> _dbSet;
    public UsuarioPdvRepository(MyContext context)
    {
        _context = context;
        _dbSet = _context.Set<UsuarioPdvEntity>();
    }

    public async Task<UsuarioPdvEntity> CadastrarUsuarioPdv(UsuarioPdvEntity entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<UsuarioPdvEntity> ConsultarUsuarioPorId(Guid usuarioCaixaPdvEntityId, Guid clienteId)
    {
        try
        {
            var entity = await _dbSet
                .Where(_dbSet => _dbSet.UsuarioCaixaPdvEntityId == usuarioCaixaPdvEntityId && _dbSet.ClienteId == clienteId)
                .Include(user => user.UserCaixaPdvEntity)
                .FirstOrDefaultAsync();

            return entity ?? throw new Exception("Usuário pdv não localizado");
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<IEnumerable<UsuarioPdvEntity>> ConsultarUsuarios(Guid clienteId)
    {
        try
        {
            var entities = await _dbSet
                .Where(user_pdv => user_pdv.ClienteId == clienteId)
                .Include(user => user.UserCaixaPdvEntity)
                .ToArrayAsync();

            return entities;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
