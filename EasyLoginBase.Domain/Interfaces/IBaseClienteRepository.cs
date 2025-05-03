using EasyLoginBase.Domain.Entities.Base;
using System.Linq.Expressions;

namespace EasyLoginBase.Domain.Interfaces;
public interface IBaseClienteRepository<T> where T : BaseClienteEntity
{
    Task CadastrarAsync(T entidade);
    void AtualizarAsync(T entidade);
    Task RemoverAsync(Guid id, Guid clientId);
    Task<bool> ExisteAsync(Guid id, Guid clientId);
    Task<T?> ConsultarPorIdAsync(Guid id, Guid clientId);
    Task<IEnumerable<T>> ConsultarTodosAsync(Guid clienteId);
    Task<IEnumerable<T>> ConsultarPorFiltroAsync(Expression<Func<T, bool>> filtro, Guid clientId);
}
