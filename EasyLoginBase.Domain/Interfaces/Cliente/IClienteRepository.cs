using EasyLoginBase.Domain.Entities.PessoaCliente;

namespace EasyLoginBase.Domain.Interfaces.Cliente;

public interface IClienteRepository<C> where C : PessoaClienteEntity
{
    Task<IEnumerable<C>> SelectAllAsync(bool include = true);
    Task<PessoaClienteEntity?> SelectByUsuarioClienteId(Guid UsuarioEntityClienteId, bool include = true);    
}
