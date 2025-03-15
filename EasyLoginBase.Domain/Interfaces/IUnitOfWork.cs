using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Interfaces.Filial;
using EasyLoginBase.Domain.Interfaces.PessoaCliente;

namespace EasyLoginBase.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
    void FinalizarContexto();

    //repository
    IFilialRepository FilialRepository { get; }
    IPessoaClienteRepository<PessoaClienteEntity> PessoaClienteRepository { get; }
    IPessoaClienteVinculadaRepository PessoaClienteVinculadaRepository { get; }
}
