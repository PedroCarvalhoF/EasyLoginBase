using EasyLoginBase.Domain.Entities.PDV;
using EasyLoginBase.Domain.Interfaces.PDV;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.InfrastructureData.Repository.PDV;

public class PontoVendaRepository : BaseClienteRepository<PontoVendaEntity>, IPontoVendaRepository<PontoVendaEntity>
{
    private DbSet<PontoVendaEntity> _dbSet;
    public PontoVendaRepository(MyContext context) : base(context)
    {
        _dbSet = context.PontosVendas;
    }
    private IQueryable<PontoVendaEntity> Include(IQueryable<PontoVendaEntity> query)
    {
        return query
            .Include(pdv => pdv.UsuarioPdv)
            .ThenInclude(usuario => usuario!.UserCaixaPdvEntity)
            .Include(pdv => pdv.FilialPdv);
    }
    public async Task<PontoVendaEntity> NovoPontoVenda(PontoVendaEntity entity)
    {
        try
        {
            await CadastrarAsync(entity);
            return entity;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<PontoVendaEntity> ConsultarPontoVendaByIdPdv(Guid pontoVendaId)
    {
        try
        {
            var query = _dbSet.Where(p => p.Id == pontoVendaId);
            var entity = await Include(query).FirstOrDefaultAsync();
            return entity ?? throw new Exception("Ponto de venda não localizado.");
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao consultar PDV por ID: {ex.Message}", ex);
        }
    }



    public async Task<IEnumerable<PontoVendaEntity>> ConsultarPdvsAbertos(Guid clienteId)
    {
        try
        {
            var query = _dbSet
                .Where(pdv => pdv.ClienteId == clienteId && pdv.Aberto == true);

            var entities = await Include(query).ToArrayAsync();

            return entities;
        }
        catch (Exception ex)
        {

            throw new Exception($"Erro ao consultar pdvs abertos: {ex.Message}", ex);
        }
    }
}
