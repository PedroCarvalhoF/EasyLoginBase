using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Interfaces.Filial;

namespace EasyLoginBase.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
    void FinalizarContexto();

    //repository
    IFilialRepository<FilialEntity> FilialRepository { get; }
}
