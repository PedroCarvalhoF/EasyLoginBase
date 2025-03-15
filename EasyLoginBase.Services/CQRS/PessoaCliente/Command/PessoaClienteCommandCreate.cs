using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.PessoaCliente;
using EasyLoginBase.Application.Services.Intefaces.PessoaCliente;
using EasyLoginBase.Application.Services.Intefaces.PessoaClienteVinculada;
using MediatR;

namespace EasyLoginBase.Services.CQRS.PessoaCliente.Command;

public class PessoaClienteCommandCreate : BaseCommands<PessoaClienteDto>
{
    public required PessoaClienteDtoCreate pessoaClienteDtoCreate { get; set; }

    public class PessoaClienteCommandCreateHandler : IRequestHandler<PessoaClienteCommandCreate, RequestResult<PessoaClienteDto>>
    {
        private readonly IPessoaClienteServices<PessoaClienteDto> _services;
        private readonly IPessoaClienteVinculadaServices _IPessoaClienteVinculadaServices;

        public PessoaClienteCommandCreateHandler(IPessoaClienteServices<PessoaClienteDto> services, IPessoaClienteVinculadaServices iPessoaClienteVinculadaServices)
        {
            _services = services;
            _IPessoaClienteVinculadaServices = iPessoaClienteVinculadaServices;
        }

        public async Task<RequestResult<PessoaClienteDto>> Handle(PessoaClienteCommandCreate request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _services.CadastrarClienteEntity(request.pessoaClienteDtoCreate);

                var pessoaVinculada = await _IPessoaClienteVinculadaServices.AdicionarUsuarioVinculadoAsync(new Application.Dto.PessoaClienteVinculada.PessoaClienteVinculadaDtoCreate(result.Id, result.UsuarioEntityClienteId));


                return RequestResult<PessoaClienteDto>.Ok(result);
            }
            catch (Exception ex)
            {

                return RequestResult<PessoaClienteDto>.ServerError(ex.Message);
            }
        }
    }

}
