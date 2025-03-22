using EasyLoginBase.Domain.Entities.Base;

namespace EasyLoginBase.Domain.Interfaces;

public interface IBaseRepository<T, F> where T : BaseEntity /*where F : FiltroBase*/
{
    Task<T> InsertAsync(T item);
    Task<T> Update(T item);
    Task<int> UpdateRange(IEnumerable<T> itens);
    //Task<IEnumerable<T>> SelectAsync(FiltroBase filtro, bool includeAll = false);
    //Task<T> SelectAsync(Guid id, FiltroBase filtro, bool includeAll = false);
    Task<bool> DeleteRange(IEnumerable<T> itens);

}
