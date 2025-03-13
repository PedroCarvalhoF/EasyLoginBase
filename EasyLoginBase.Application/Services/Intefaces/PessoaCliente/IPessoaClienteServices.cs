using EasyLoginBase.Application.Dto.PessoaCliente;

namespace EasyLoginBase.Application.Services.Intefaces.PessoaCliente;

public interface IPessoaClienteServices<T> where T : PessoaClienteDto
{
    Task<T> CadastrarClienteEntity(PessoaClienteDtoCreate pessoaClienteCreate);
    Task<T> ConsultarClientes(Guid idCliente);
    Task<IEnumerable<T>> ConsultarClientes();

}