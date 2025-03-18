using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Interfaces.Filial;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EasyLoginBase.InfrastructureData.Repository.Filial;

public class FilialRepository : IFilialRepository<FilialEntity, ClaimsPrincipal>
{
    private readonly MyContext _context;

    public FilialRepository(MyContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FilialEntity>?> ConsultarFiliais(ClaimsPrincipal user, Guid clienteId)
    {
        try
        {
            var entities = await _context.Filiais
                .AsNoTracking()
                .Where(x => x.ClienteId == clienteId)
                .Include(pessoaCliente => pessoaCliente.PessoaCliente)
                .OrderBy(f => f.NomeFilial)
                .ToListAsync();

            return entities;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
