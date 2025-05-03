using EasyLoginBase.Domain.Entities;
using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.InfrastructureData.Repository;

public class BaseRepository<T, F> : IBaseRepository<T, F> where T : BaseEntity where F : FiltroBase
{
    protected readonly MyContext _context;
    private readonly DbSet<T> _dataset;

    public BaseRepository(MyContext context)
    {
        _context = context;
        _dataset = _context.Set<T>();
    }

    public async Task<T> InsertAsync(T item)
    {
        try
        {
            await _dataset.AddAsync(item);
            return item;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    public async Task<T> Update(T item)
    {
        try
        {
            // Tenta encontrar a entidade existente pelo Id
            var getItem = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));

            // Verifica se a entidade foi encontrada
            if (getItem != null)
            {
                item.DataCriacao(getItem.CreateAt);
                // Atualiza os valores da entidade rastreada com os valores da entidade passada
                _context.Entry(getItem).CurrentValues.SetValues(item);

                // Marca a entidade como modificada
                _context.Entry(getItem).State = EntityState.Modified;

                // Não use _context.Update(item) para evitar o erro de múltiplas instâncias
            }
            else
            {
                throw new Exception("Entidade não encontrada para o Id informado.");
            }

            return item;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<int> UpdateRange(IEnumerable<T> itens)
    {
        try
        {
            foreach (var item in itens)
            {
                // Tenta encontrar a entidade existente pelo Id
                var getItem = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));

                // Verifica se a entidade foi encontrada
                if (getItem != null)
                {
                    // Atualiza os valores da entidade rastreada com os valores da entidade passada
                    _context.Entry(getItem).CurrentValues.SetValues(item);

                    // Marca a entidade como modificada
                    _context.Entry(getItem).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception($"Entidade não encontrada para o Id {item.Id}.");
                }
            }

            // Salva as mudanças no banco de dados
            return await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao atualizar entidades: {ex.Message}");
        }
    }
    public async Task<T> SelectAsync(Guid id, FiltroBase filtro, bool includeAll = true)
    {
        try
        {
            IQueryable<T> query = _dataset.AsNoTracking();

            //if (includeAll)
            //{
            //    query = query.IncludeAll();
            //}

            //query = query.FiltroCliente(filtro);

            var result = await query.SingleOrDefaultAsync(t => t.Id == id);
            return result ?? Activator.CreateInstance<T>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<IEnumerable<T>> SelectAsync(FiltroBase filtro, bool includeAll = true)
    {
        try
        {
            IQueryable<T> query = _dataset.AsNoTracking();

            //if (includeAll)
            //{
            //    query = query.IncludeAll();
            //}

            //query = query.FiltroCliente(filtro);

            var result = await query.ToArrayAsync();
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<bool> DeleteRange(IEnumerable<T> itens)
    {
        try
        {
            foreach (var item in itens)
            {
                // Tenta encontrar a entidade existente pelo Id
                var getItem = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));

                // Verifica se a entidade foi encontrada
                if (getItem != null)
                {
                    // Remove a entidade rastreada
                    _dataset.Remove(getItem);
                }
                else
                {
                    throw new Exception($"Entidade não encontrada para o Id {item.Id}.");
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao deletar entidades: {ex.Message}");
        }
    }
}
