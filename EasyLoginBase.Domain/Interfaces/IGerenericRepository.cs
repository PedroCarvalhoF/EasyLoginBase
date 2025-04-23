namespace EasyLoginBase.Domain.Interfaces;
public interface IGerenericRepository<T> where T : class
{
    Task<T> InsertAsync(T item);
    T Update(T item);
    Task<T> SelectAsync(Guid id);
    Task<IEnumerable<T>> Select();
}
