using EasyLoginBase.Domain.Entities.Base;
using System.Linq.Expressions;

namespace EasyLoginBase.Domain.Interfaces;
public interface IBaseClienteRepository<T> where T : BaseClienteEntity
{
    Task CadastrarAsync(T entidade);
    void AtualizarAsync(T entidade);
    Task RemoverAsync(Guid id);
    Task<bool> ExisteAsync(Guid id);
    Task<T?> ConsultarPorIdAsync(Guid id);
    Task<IEnumerable<T>> ConsultarTodosAsync(Guid clienteId);
    Task<IEnumerable<T>> ConsultarPorFiltroAsync(Expression<Func<T, bool>> filtro);   
}
