using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.UsuarioVinculadoCliente;
using EasyLoginBase.Application.Services.Intefaces.UsuarioClienteVinculo;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Entities.User;
using EasyLoginBase.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EasyLoginBase.Services.Services.UsuarioVinculadoCliente;
public class UsuarioClienteVinculoServices : IUsuarioClienteVinculoServices<UsuarioVinculadoClienteDto>
{
    private readonly IUnitOfWork _repository;
    private readonly UserManager<UserEntity> _userManager;

    public UsuarioClienteVinculoServices(IUnitOfWork repository, UserManager<UserEntity> userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }
    public async Task<RequestResult<UsuarioVinculadoClienteDto>> VincularUsuarioAoClienteAsync(UsuarioVinculadoClienteDtoRegistrarVinculo dtoRegistrarVinculo, ClaimsPrincipal users)
    {
        try
        {
            var userEntityParaVincularAoCliente = await _userManager.FindByEmailAsync(dtoRegistrarVinculo.EmailUsuarioParaVincular);
            if (userEntityParaVincularAoCliente == null)
                throw new Exception("Usuario não encontrado");

            var clienteId = users.GetClienteIdVinculo();

            var pessoaVincularCliente = PessoaClienteVinculadaEntity.Create(clienteId, userEntityParaVincularAoCliente.Id);

            var usuarioVinculadoCliente = await _repository.UsuarioClienteVinculoRepostory.InsertAsync(pessoaVincularCliente);

            if (!await _repository.CommitAsync())
                throw new Exception("Erro ao vincular usuario ao cliente");


            var usuarioVinculadoCreateResult = await _repository.UsuarioClienteVinculoImplementacao.SelectUsuarioClienteVinculo(clienteId, userEntityParaVincularAoCliente.Id);

            if (usuarioVinculadoCreateResult == null)
                throw new Exception("Erro ao vincular usuario ao cliente");

            var dto = new UsuarioVinculadoClienteDto
            {
                IdUsuarioVinculado = usuarioVinculadoCreateResult.UsuarioVinculadoId,
                NomeUsuarioVinculado = userEntityParaVincularAoCliente.Nome,
                EmailUsuarioVinculado = userEntityParaVincularAoCliente.Email,
                AcessoPermitido = usuarioVinculadoCreateResult.AcessoPermitido,

            };


            return new RequestResult<UsuarioVinculadoClienteDto>(dto);

        }
        catch (Exception ex)
        {

            return new RequestResult<UsuarioVinculadoClienteDto>(ex);
        }
    }
    public async Task<RequestResult<UsuarioVinculadoClienteDto>> VincularClienteAoClienteAsync(Guid clienteId, Guid usuarioId)
    {
        try
        {
            var pessoaVincularCliente = PessoaClienteVinculadaEntity.Create(clienteId, usuarioId);

            await _repository.UsuarioClienteVinculoRepostory.InsertAsync(pessoaVincularCliente);

            if (!await _repository.CommitAsync())
                throw new Exception("Erro ao vincular usuario ao cliente");


            var usuarioVinculadoCreateResult = await _repository.UsuarioClienteVinculoImplementacao.SelectUsuarioClienteVinculo(clienteId, usuarioId);

            if (usuarioVinculadoCreateResult == null)
                throw new Exception("Erro ao vincular usuario ao cliente");

            var dto = new UsuarioVinculadoClienteDto
            {
                IdUsuarioVinculado = usuarioVinculadoCreateResult.UsuarioVinculadoId,
                NomeUsuarioVinculado = usuarioVinculadoCreateResult.UsuarioVinculado.Nome,
                EmailUsuarioVinculado = usuarioVinculadoCreateResult.UsuarioVinculado.Email,
                AcessoPermitido = usuarioVinculadoCreateResult.AcessoPermitido,

            };


            return new RequestResult<UsuarioVinculadoClienteDto>(dto);
        }
        catch (Exception ex)
        {

            return new RequestResult<UsuarioVinculadoClienteDto>(ex);
        }
    }


}
