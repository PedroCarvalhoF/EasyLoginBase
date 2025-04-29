using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasyLoginBase.InfrastructureData.Repository;
public class BaseClienteRepository_REFACTOR<T> : IBaseClienteRepository_REFACTOR<T> where T : BaseClienteEntity
{
    private readonly MyContext _context;
    private readonly DbSet<T> _dbSet;
    public BaseClienteRepository_REFACTOR(MyContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> CreateAsync(T entity)
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
    public T UpdateAsync(T entity)
    {
        try
        {
            _dbSet.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<IEnumerable<T>> SelectAsync(Guid clientId)
    {
        try
        {
            var entities = await
                _dbSet
                .AsNoTracking()
                .Where(e => e.ClienteId == clientId)
                .ToListAsync();

            return entities;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<bool> DeleteAsync(Guid id, Guid clientId)
    {
        try
        {
            var entity = await
                _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id && e.ClienteId == clientId);

            if (entity is null)
                throw new Exception("Entidade não encontrada");


            _dbSet.Remove(entity);

            return true;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<T?> SelectAsync(Guid id, Guid clientId)
    {
        try
        {
            var entity = await
                _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id && e.ClienteId == clientId);

            return entity;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<IEnumerable<T>> SelectAscyn(Expression<Func<T, bool>> filtro, Guid clientId)
    {
        try
        {
            var entities = await
                _dbSet
                .AsNoTracking()
                .Where(e => e.ClienteId == clientId)
                .Where(filtro)
                .ToArrayAsync();

            return entities;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }


}
