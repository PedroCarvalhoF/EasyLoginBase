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
            //Verifica se o usuário existe
            var userEntityParaVincularAoCliente = await _userManager.FindByEmailAsync(dtoRegistrarVinculo.EmailUsuarioParaVincular);
            if (userEntityParaVincularAoCliente == null)
                throw new Exception("Usuario não encontrado");

            //pega o id do cliente
            var clienteId = users.GetClienteIdVinculo();

            var pessoaVincularCliente = PessoaClienteVinculadaEntity.Create(clienteId, userEntityParaVincularAoCliente.Id);

            var usuarioVinculadoExists = await _repository.UsuarioClienteVinculoImplementacao.SelectUsuarioClienteVinculo(clienteId, userEntityParaVincularAoCliente.Id);

            if (usuarioVinculadoExists != null)
                throw new ArgumentException("Usuário já é vinculado.");

            var usuarioVinculadoCliente = await _repository.UsuarioClienteVinculoRepostory.InsertAsync(pessoaVincularCliente);

            if (!await _repository.CommitAsync())
                throw new Exception("Erro ao vincular usuario ao cliente");

            var usuarioVinculadoCreateResult = await _repository.UsuarioClienteVinculoImplementacao.SelectUsuarioClienteVinculo(clienteId, userEntityParaVincularAoCliente.Id);

            if (usuarioVinculadoCreateResult == null)
                throw new Exception("Erro ao vincular usuario ao cliente");

            var dto = new UsuarioVinculadoClienteDto
            {
                ClienteId = usuarioVinculadoCreateResult.PessoaClienteEntityId,
                ClienteNome = usuarioVinculadoCreateResult.PessoaClienteEntity.NomeFantasia,
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
    public async Task<RequestResult<UsuarioVinculadoClienteDto>> LiberarBloquearAcessoUsuarioVinculadoAsync(UsuarioVinculadoClienteDtoLiberarRemoverAcesso liberarRemoverAcesso, ClaimsPrincipal user)
    {
        try
        {
            var userEntityParaVincularAoCliente = await _userManager.FindByEmailAsync(liberarRemoverAcesso.EmailUsuarioVinculado);
            if (userEntityParaVincularAoCliente == null)
                throw new Exception("Usuario não encontrado");

            var clienteId = user.GetClienteIdVinculo();

            var usuarioVinculadoExists = await _repository.UsuarioClienteVinculoImplementacao.SelectUsuarioClienteVinculo(clienteId, userEntityParaVincularAoCliente.Id);
            if (usuarioVinculadoExists == null)
                throw new ArgumentException("Usuário não vinculado.");

            usuarioVinculadoExists.AlterarAcesso(liberarRemoverAcesso.LiberarAcesso);

            _repository.UsuarioClienteVinculoRepostory.Update(usuarioVinculadoExists);

            if (!await _repository.CommitAsync())
                throw new Exception("Erro ao atualizar acesso do usuário");

            var usuarioVinculadoCreateResult = await _repository.UsuarioClienteVinculoImplementacao.SelectUsuarioClienteVinculo(clienteId, userEntityParaVincularAoCliente.Id);           

            var dto = new UsuarioVinculadoClienteDto
            {
                ClienteId = usuarioVinculadoCreateResult.PessoaClienteEntityId,
                ClienteNome = usuarioVinculadoCreateResult.PessoaClienteEntity.NomeFantasia,
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
