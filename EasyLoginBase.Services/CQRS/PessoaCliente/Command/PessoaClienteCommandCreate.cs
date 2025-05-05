using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.PessoaCliente;
using EasyLoginBase.Application.Dto.User.Role;
using EasyLoginBase.Application.Services.Intefaces.PessoaCliente;
using EasyLoginBase.Application.Services.Intefaces.PessoaClienteVinculada;
using EasyLoginBase.Application.Tools.Roles;
using EasyLoginBase.Services.Services.User.Roles;
using MediatR;

namespace EasyLoginBase.Services.CQRS.PessoaCliente.Command;

public class PessoaClienteCommandCreate : BaseCommands<PessoaClienteDto>
{
    public required PessoaClienteDtoCreate pessoaClienteDtoCreate { get; set; }

    public class PessoaClienteCommandCreateHandler : IRequestHandler<PessoaClienteCommandCreate, RequestResult<PessoaClienteDto>>
    {
        private readonly IPessoaClienteServices<PessoaClienteDto> _services;
        private readonly IPessoaClienteVinculadaServices _IPessoaClienteVinculadaServices;
        private readonly IUserRoleServices<RoleDto, RoleUserDto> _userRoleServices;

        public PessoaClienteCommandCreateHandler(IPessoaClienteServices<PessoaClienteDto> services, IPessoaClienteVinculadaServices iPessoaClienteVinculadaServices, IUserRoleServices<RoleDto, RoleUserDto> userRoleServices)
        {
            _services = services;
            _IPessoaClienteVinculadaServices = iPessoaClienteVinculadaServices;
            _userRoleServices = userRoleServices;
        }
        public async Task<RequestResult<PessoaClienteDto>> Handle(PessoaClienteCommandCreate request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _services.CadastrarClienteEntity(request.pessoaClienteDtoCreate);


                var pessoaVinculada = await _IPessoaClienteVinculadaServices.AdicionarUsuarioVinculadoAsync(new Application.Dto.PessoaClienteVinculada.PessoaClienteVinculadaDtoCreate(result.Id, result.UsuarioEntityClienteId));



                //como nao foi feito um serviço para pessoa cliente 
                //neste momento estamos direto na controller
                //futuramente fazer o serviço


                //1 PEGAR A ROLE ADMIN
                //2 ADD ROLE AO USUARIO

                //criar metodo para pegar role por nome
                var role = (await _userRoleServices.SelectAsync()).Data.Where(p => p.RoleName.ToLower() == RolesEnum.Admin.ToString().ToLower()).SingleOrDefault();


                var userRoleAddResult = await _userRoleServices.AdcionarRoleUser(new RoleDtoAddRoleUser
                {
                    UserId = request.pessoaClienteDtoCreate.UsuarioEntityClienteId,
                    RoleId = role.Id
                });

                if (!userRoleAddResult.Status)
                    return RequestResult<PessoaClienteDto>.BadRequest("Não foi possível cadastrar permissão de admin");

                return RequestResult<PessoaClienteDto>.Ok(result);
            }
            catch (Exception ex)
            {

                return RequestResult<PessoaClienteDto>.ServerError(ex.Message);
            }
        }
    }

}
