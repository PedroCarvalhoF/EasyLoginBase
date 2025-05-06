using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.PessoaCliente;
using EasyLoginBase.Application.Dto.PessoaClienteVinculada;
using EasyLoginBase.Application.Services.Intefaces.PessoaCliente;
using EasyLoginBase.Application.Services.Intefaces.PessoaClienteVinculada;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Entities.User;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Services.Tools.UseCase;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EasyLoginBase.Services.Services.PessoaClienteVinculada;

public class PessoaClienteVinculadaServices : IPessoaClienteVinculadaServices
{
    private readonly IUnitOfWork _repository;
    private readonly UserManager<UserEntity> _userManager;
    private readonly IPessoaClienteServices<PessoaClienteDto> _pessoaClienteServices;

    public PessoaClienteVinculadaServices(IUnitOfWork repository, UserManager<UserEntity> userManager, IPessoaClienteServices<PessoaClienteDto> pessoaClienteServices)
    {
        _repository = repository;
        _userManager = userManager;
        _pessoaClienteServices = pessoaClienteServices;
    }
    public async Task<PessoaClienteVinculadaDto> AdicionarUsuarioVinculadoAsync(PessoaClienteVinculadaDtoCreate pessoaClienteVinculadaDtoCreate)
    {


        var pessoaVinculadaEntity = PessoaClienteVinculadaEntity.Create(pessoaClienteVinculadaDtoCreate.PessoaClienteId, pessoaClienteVinculadaDtoCreate.UsuarioVinculadoId);

        var pessoaVinculada = await _repository.PessoaClienteVinculadaRepository.AdicionarUsuarioVinculadoAsync(pessoaVinculadaEntity);

        if (await _repository.CommitAsync())
        {
            var pessoaVinculada_include = await _repository.PessoaClienteVinculadaRepository.ObterClientesVinculadosPorUsuarioAsync(pessoaVinculada.UsuarioVinculadoId);

            return DtoMapper.ParsePessoaClienteVinculada(pessoaVinculada);
        }

        throw new Exception("Erro ao adicionar usuário vinculado.");
    }

    public async Task<RequestResult<PessoaClienteDto>> VincularUsuarioClienteByEmail(PessoaClienteVinculadaDtoCreateByEmail pessoaClienteVinculadaDto, ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();

            var userParaVincularEntity = await _userManager.FindByEmailAsync(pessoaClienteVinculadaDto.EmailPessoaParaVincular);

            if (userParaVincularEntity == null)
                return RequestResult<PessoaClienteDto>.BadRequest("Usuário não encontrado.");

            var pessoaClienteVinculadaEntity = PessoaClienteVinculadaEntity.Create(clienteId, userParaVincularEntity.Id);

            var pessoaClienteVinculada = await _repository.PessoaClienteVinculadaRepository.AdicionarUsuarioVinculadoAsync(pessoaClienteVinculadaEntity);

            if (await _repository.CommitAsync())
            {
                var pessoaVinculada = await _pessoaClienteServices.ConsultarClientes(clienteId);

                return RequestResult<PessoaClienteDto>.Ok(pessoaVinculada);
            }

            return RequestResult<PessoaClienteDto>.BadRequest("Erro ao adicionar usuário vinculado.");

        }
        catch (Exception ex)
        {

            return new RequestResult<PessoaClienteDto>(ex);
        }
    }



    public Task<PessoaClienteVinculadaDto> AlterarStatusAcessoAsync(PessoaClienteVinculadaDtoUpdate pessoaClienteVinculadaDtoUpdate)
    {
        throw new NotImplementedException();
    }
    public async Task<IEnumerable<PessoaClienteVinculadaDto>> GetPessoasVinculadas()
    {
        try
        {
            IEnumerable<PessoaClienteVinculadaEntity> entities = await _repository.PessoaClienteVinculadaRepository.GetPessoasVinculas();

            IEnumerable<PessoaClienteVinculadaDto> dtos = DtoMapper.ParsePessoaClienteVinculada(entities);

            return dtos;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<IEnumerable<PessoaClienteVinculadaDto>> GetVinculosPessoa(Guid idPessoaVinculada)
    {
        try
        {
            IEnumerable<PessoaClienteVinculadaEntity> entities = await _repository.PessoaClienteVinculadaRepository.GetVinculosPessoas(idPessoaVinculada);

            return DtoMapper.ParsePessoaClienteVinculada(entities);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public Task<List<PessoaClienteVinculadaDto>> ObterClientesVinculadosPorUsuarioAsync(Guid usuarioVinculadoId)
    {
        throw new NotImplementedException();
    }

    public Task<List<PessoaClienteVinculadaDto>> ObterUsuariosVinculadosPorClienteAsync(Guid pessoaClienteId)
    {
        throw new NotImplementedException();
    }


}
