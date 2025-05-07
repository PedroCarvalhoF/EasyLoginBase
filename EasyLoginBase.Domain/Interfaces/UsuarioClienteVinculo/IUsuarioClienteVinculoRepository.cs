using EasyLoginBase.Domain.Entities.PessoaCliente;

namespace EasyLoginBase.Domain.Interfaces.UsuarioClienteVinculo;
public interface IUsuarioClienteVinculoRepository<U> where U : PessoaClienteVinculadaEntity
{
    Task<U?> SelectUsuarioClienteVinculo(Guid clienteId, Guid usuarioId, bool include = true);
    Task<U?> SelectUsuarioClienteVinculoByUsuarioId(Guid usuarioId, bool include = true);
    Task<IEnumerable<U>> SelectUsuariosVinculadosByClienteAsync(Guid clienteId, bool include = true);
}
