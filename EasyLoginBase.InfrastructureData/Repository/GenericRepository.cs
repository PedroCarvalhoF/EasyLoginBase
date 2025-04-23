using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.InfrastructureData.Repository;

public class GenericRepository<T> : IGerenericRepository<T> where T : class
{
    private readonly MyContext _context;
    private readonly DbSet<T> _dataset;

    public GenericRepository(MyContext context)
    {
        _context = context;
        _dataset = _context.Set<T>();
    }

    public async Task<T> InsertAsync(T item)
    {
        await _dataset.AddAsync(item);        
        return item;
    }

    public T Update(T item)
    {
        _dataset.Update(item);       
        return item;
    }

    public async Task<T> SelectAsync(Guid id)
    {
        var entity = await _dataset.FindAsync(id);
        if (entity == null)
            throw new KeyNotFoundException($"Id NÂO Localizado.");

        return entity;
    }

    public async Task<IEnumerable<T>> Select()
    {
        return await _dataset.ToListAsync();
    }
}
