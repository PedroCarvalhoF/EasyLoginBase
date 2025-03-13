using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.PessoaCliente;
using EasyLoginBase.Application.Services.Intefaces.PessoaCliente;
using MediatR;

namespace EasyLoginBase.Services.CQRS.PessoaCliente.Command;

public class PessoaClienteCommandaConsultarClientes : BaseCommands<IEnumerable<PessoaClienteDto>>
{
    public class PessoaClienteCommandaConsultarClientesHandler : IRequestHandler<PessoaClienteCommandaConsultarClientes, RequestResult<IEnumerable<PessoaClienteDto>>>
    {
        private readonly IPessoaClienteServices<PessoaClienteDto> _pessoaClienteServices;

        public PessoaClienteCommandaConsultarClientesHandler(IPessoaClienteServices<PessoaClienteDto> pessoaClienteServices)
        {
            this._pessoaClienteServices = pessoaClienteServices;
        }

        public async Task<RequestResult<IEnumerable<PessoaClienteDto>>> Handle(PessoaClienteCommandaConsultarClientes request, CancellationToken cancellationToken)
        {
            try
            {
                var clientes = await _pessoaClienteServices.ConsultarClientes();

                return RequestResult<IEnumerable<PessoaClienteDto>>.Ok(clientes);
            }
            catch (Exception ex)
            {

                return RequestResult<IEnumerable<PessoaClienteDto>>.ServerError(ex.Message);
            }
        }
    }
}
