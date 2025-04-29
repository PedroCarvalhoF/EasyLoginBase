using EasyLoginBase.Domain.Entities.Base;
using System.Linq.Expressions;

namespace EasyLoginBase.Domain.Interfaces;

public interface IBaseClienteRepository_REFACTOR<T> where T : BaseClienteEntity
{
    Task<T> CreateAsync(T entity);
    T UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id, Guid clientId);
    Task<T?> SelectAsync(Guid id, Guid clientId);
    Task<IEnumerable<T>> SelectAsync(Guid clientId);
    Task<IEnumerable<T>> SelectAscyn(Expression<Func<T, bool>> filtro, Guid clientId);
}
