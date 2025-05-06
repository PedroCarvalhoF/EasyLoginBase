using EasyLoginBase.Application.Dto.PessoaCliente;
using EasyLoginBase.Application.Dto.User;
using EasyLoginBase.Application.Services.Intefaces.PessoaCliente;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Entities.User;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Services.Tools.UseCase;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.Services.Services.PessoaCliente;

public class PessoaClienteServices : IPessoaClienteServices<PessoaClienteDto>
{
    private readonly IUnitOfWork _repository;
    private readonly UserManager<UserEntity> _userManager;

    public PessoaClienteServices(IUnitOfWork repository, UserManager<UserEntity> userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }
    public async Task<PessoaClienteDto> CadastrarClienteEntity(PessoaClienteDtoCreate pessoaClienteCreate)
    {
        try
        {
            var usuarioExist = await _userManager.Users.SingleOrDefaultAsync(user => user.Id == pessoaClienteCreate.UsuarioEntityClienteId);

            if (usuarioExist == null)
                throw new Exception("Usuário não localizado.");

            var usuarioClienteExists = await _repository.PessoaClienteRepository.ConsultarClientes(pessoaClienteCreate.UsuarioEntityClienteId);

            if (usuarioClienteExists != null)
                throw new Exception("Usuário já é um cliente.");

            bool nomeFantasioEmUso = await _repository.PessoaClienteRepository.VerificarUsoNomeFantasia(pessoaClienteCreate.NomeFantasia);
            if (nomeFantasioEmUso)
                throw new Exception("Nome fantasia já está em uso");

            var pessoaEntity = DtoMapper.ParcePessoaCliente(pessoaClienteCreate);

            var resultCreate = await _repository.PessoaClienteRepository.CadastrarClienteEntity(pessoaEntity);
            if (!await _repository.CommitAsync())
                throw new Exception("Erro ao salvar cliente");

            var pessoaVinculo = await _repository.PessoaClienteVinculadaRepository.AdicionarUsuarioVinculadoAsync(PessoaClienteVinculadaEntity.Create(pessoaEntity.Id, usuarioExist.Id));

            if (!await _repository.CommitAsync())
                throw new Exception("Erro ao vincular cliente");

            return await ConsultarClientes(usuarioExist.Id);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<PessoaClienteDto> ConsultarClientes(Guid idCliente)
    {
        try
        {
            var entities = await _repository.PessoaClienteRepository.ConsultarClientes(idCliente);

            if (entities == null)
                throw new Exception("Cliente não localizado.");

            var dtos = new PessoaClienteDto
            {
                Id = entities.Id,
                UsuarioEntityClienteId = entities.UsuarioEntityClienteId,
                NomeFantasia = entities.NomeFantasia,
                DataAbertura = entities.DataAbertura,
                DataVencimentoUso = entities.DataVencimentoUso,
                NomeUsuarioCliente = entities.UsuarioEntityCliente?.UserName,
                UsuariosVinculadosDtos = new List<UserDto>(entities.UsuariosVinculados.Select(userVinculado => new UserDto
                {
                    Id = userVinculado.UsuarioVinculado.Id,
                    Nome = userVinculado.UsuarioVinculado.UserName,
                    Email = userVinculado.UsuarioVinculado.Email
                }))
            };

            return dtos;

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<PessoaClienteDto>> ConsultarClientes()
    {
        try
        {
            var entities = await _repository.PessoaClienteRepository.ConsultarClientes();

            IEnumerable<PessoaClienteDto> dtos = DtoMapper.ParcePessoaCliente(entities);

            return dtos;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
