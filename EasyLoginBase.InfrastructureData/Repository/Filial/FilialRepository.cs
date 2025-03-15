using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Interfaces.Filial;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.InfrastructureData.Repository.Filial;

public class FilialRepository : IFilialRepository
{
    private readonly MyContext _context;

    public FilialRepository(MyContext context)
    {
        _context = context;
    }

    public FilialEntity AlterarFilialAsync(FilialEntity filial)
    {
        _context.Filiais.Update(filial);
        return filial;
    }

    public async Task<FilialEntity> CriarFilialAsync(FilialEntity filial)
    {
        await _context.Filiais.AddAsync(filial);
        return filial;
    }

    public async Task<IEnumerable<FilialEntity>> SelecionarFiliaisPorIdPessoaCliente(Guid idPessoaCliente)
    {
        return await _context.Filiais
            .AsNoTracking()
            .Where(f => f.PessoaClienteId == idPessoaCliente)
            .ToListAsync();
    }

    public async Task<FilialEntity?> SelecionarFilialPorId(Guid idFilial)
    {
        return await _context.Filiais
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == idFilial);
    }
}
