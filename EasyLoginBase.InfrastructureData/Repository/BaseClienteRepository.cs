using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasyLoginBase.InfrastructureData.Repository;

public class BaseClienteRepository<T> : IBaseClienteRepository<T> where T : BaseClienteEntity
{
    private readonly MyContext _context;
    private readonly DbSet<T> _dataset;

    public BaseClienteRepository(MyContext context)
    {
        _context = context;
        _dataset = _context.Set<T>();
    }

    private IQueryable<T> IncludeProperties(IQueryable<T> query)
    {
        return IncludeRecursive(query, typeof(T), new HashSet<Type>(), string.Empty);
    }

    private IQueryable<T> IncludeRecursive(IQueryable<T> query, Type entityType, HashSet<Type> processedEntities, string parentPath)
    {
        if (processedEntities.Contains(entityType))
            return query; // Evita ciclo infinito

        processedEntities.Add(entityType);

        var navigationProperties = entityType.GetProperties()
            .Where(p => p.PropertyType.IsClass
                     && p.PropertyType != typeof(string)
                     && !processedEntities.Contains(p.PropertyType)); // Evita loops

        foreach (var property in navigationProperties)
        {
            string propertyPath = string.IsNullOrEmpty(parentPath) ? property.Name : $"{parentPath}.{property.Name}";

            query = query.Include(propertyPath);

            // Se for uma coleção, pega o tipo do item
            if (property.PropertyType.IsGenericType && typeof(IEnumerable<>).IsAssignableFrom(property.PropertyType.GetGenericTypeDefinition()))
            {
                var itemType = property.PropertyType.GetGenericArguments().FirstOrDefault();
                if (itemType != null)
                {
                    query = IncludeRecursive(query, itemType, processedEntities, propertyPath);
                }
            }
            else
            {
                query = IncludeRecursive(query, property.PropertyType, processedEntities, propertyPath);
            }
        }

        return query;
    }

    public void AtualizarAsync(T entidade)
    {
        try
        {
            _dataset.Update(entidade);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao atualizar entidade: {ex.Message}", ex);
        }
    }
    public async Task CadastrarAsync(T entidade)
    {
        try
        {
            await _dataset.AddAsync(entidade);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao cadastrar entidade: {ex.Message}", ex);
        }
    }
    public async Task<IEnumerable<T>> ConsultarPorFiltroAsync(Expression<Func<T, bool>> filtro, Guid clientId)
    {
        try
        {
            return await IncludeProperties(_dataset.AsNoTracking())
                .Where(filtro)
                .Where(e => e.ClienteId == clientId)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao consultar por filtro: {ex.Message}", ex);
        }
    }
    public async Task<T?> ConsultarPorIdAsync(Guid id, Guid clienteId)
    {
        try
        {
            return await IncludeProperties(_dataset.AsNoTracking())
                .FirstOrDefaultAsync(e => e.Id == id && e.ClienteId == clienteId);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao consultar por ID: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<T>> ConsultarTodosAsync(Guid clienteId){
        try
        {
            return await IncludeProperties(_dataset.AsNoTracking())
                .Where(cliente => cliente.ClienteId == clienteId)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao consultar todos: {ex.Message}", ex);
        }
    }

    public async Task<bool> ExisteAsync(Guid id, Guid clienteId)
    {
        try
        {
            return await _dataset.AsNoTracking()
                .AnyAsync(e => e.Id == id && e.ClienteId == clienteId);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao verificar existência: {ex.Message}", ex);
        }
    }
    public async Task RemoverAsync(Guid id, Guid clienteId)
    {
        try
        {
            var entidade = await ConsultarPorIdAsync(id, clienteId);
            if (entidade == null)
                throw new KeyNotFoundException("Entidade não encontrada.");

            _dataset.Remove(entidade);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao remover entidade: {ex.Message}", ex);
        }
    }
   
}
