using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Interfaces.Filial;
using EasyLoginBase.Domain.Interfaces.PessoaCliente;

namespace EasyLoginBase.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
    void FinalizarContexto();

    // Repositórios específicos
    IFilialRepository FilialRepository { get; }
    IPessoaClienteRepository<PessoaClienteEntity> PessoaClienteRepository { get; }
    IPessoaClienteVinculadaRepository PessoaClienteVinculadaRepository { get; }
    
    // Método para obter um repositório genérico
    IBaseClienteRepository<T> GetRepository<T>() where T : BaseClienteEntity;
}
