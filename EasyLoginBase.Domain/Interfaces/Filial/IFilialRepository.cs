using EasyLoginBase.Domain.Entities.Filial;

namespace EasyLoginBase.Domain.Interfaces.Filial;

public interface IFilialRepository<F> where F : FilialEntity
{
    Task<F> CreateFilialAsync(F filial);
    Task<F> UpdateFilialAsync(F filial);
    Task<IEnumerable<F>> SelectFilialAsync();
    Task<IEnumerable<F>> SelectFilialAsync(Guid idFilial);
}
