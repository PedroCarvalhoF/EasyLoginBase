using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.PessoaCliente;
using EasyLoginBase.Application.Services.Intefaces.PessoaCliente;
using MediatR;

namespace EasyLoginBase.Services.CQRS.PessoaCliente.Command;

public class PessoaClienteCommandCreate : BaseCommands<PessoaClienteDto>
{
    public required PessoaClienteDtoCreate pessoaClienteDtoCreate { get; set; }

    public class PessoaClienteCommandCreateHandler : IRequestHandler<PessoaClienteCommandCreate, RequestResult<PessoaClienteDto>>
    {
        private readonly IPessoaClienteServices<PessoaClienteDto> _services;

        public PessoaClienteCommandCreateHandler(IPessoaClienteServices<PessoaClienteDto> services)
        {
            _services = services;
        }

        public async Task<RequestResult<PessoaClienteDto>> Handle(PessoaClienteCommandCreate request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _services.CadastrarClienteEntity(request.pessoaClienteDtoCreate);

                return RequestResult<PessoaClienteDto>.Ok(result);
            }
            catch (Exception ex)
            {

                return RequestResult<PessoaClienteDto>.ServerError(ex.Message);
            }
        }
    }

}
