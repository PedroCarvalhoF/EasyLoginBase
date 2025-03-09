using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Interfaces.Filial;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.InfrastructureData.Repository.Filial;

public class FilialRepository : IFilialRepository<FilialEntity>
{
    private readonly MyContext _context;

    public FilialRepository(MyContext context)
    {
        _context = context;
    }
    public async Task<FilialEntity> CreateFilialAsync(FilialEntity filial)
    {
        try
        {
            await _context.Filiais.AddAsync(filial);           
            return filial;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<IEnumerable<FilialEntity>> SelectFilialAsync()
    {
        try
        {
            return await _context.Filiais.ToListAsync();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<IEnumerable<FilialEntity>> SelectFilialAsync(Guid idFilial)
    {
        try
        {
            return await _context.Filiais.Where(f => f.IdFilial == idFilial).ToListAsync();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<FilialEntity> UpdateFilialAsync(FilialEntity filial)
    {
        try
        {
            var existingFilial = await _context.Filiais.FindAsync(filial.IdFilial);
            if (existingFilial == null)
                throw new KeyNotFoundException("Filial não encontrada");

            existingFilial.NomeFilial = filial.NomeFilial;
            _context.Filiais.Update(existingFilial);           
            return existingFilial;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
