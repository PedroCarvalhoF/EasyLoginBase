using EasyLoginBase.Domain.Entities.PDV;

namespace EasyLoginBase.Domain.Interfaces.PDV;

public interface IPontoVendaRepository<T> where T : PontoVendaEntity
{
    Task<T> NovoPontoVenda(T entity);
    Task<T> ConsultarPontoVendaByIdPdv(Guid pontoVendaId);
    Task<IEnumerable<PontoVendaEntity>> ConsultarPdvsAbertos(Guid clienteId);
}
