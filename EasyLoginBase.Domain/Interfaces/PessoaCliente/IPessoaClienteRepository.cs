using EasyLoginBase.Domain.Entities.PessoaCliente;

namespace EasyLoginBase.Domain.Interfaces.PessoaCliente;

public interface IPessoaClienteRepository<T> where T : PessoaClienteEntity
{
    Task<T> CadastrarClienteEntity(T pessoaClienteEntity);
    Task<T> ConsultarClientes(Guid idCliente);
    Task<IEnumerable<T>> ConsultarClientes();
    Task<bool> VerificarUsoNomeFantasia(string nomeFantasia);
}
