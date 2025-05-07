using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Cliente;
using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Application.Dto.UsuarioVinculadoCliente;
using EasyLoginBase.Application.Services.Intefaces.Cliente;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Entities.User;
using EasyLoginBase.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EasyLoginBase.Services.Services.Cliente;
public class ClienteServices : IClienteServices<ClienteDto>
{
    private readonly IUnitOfWork _repository;
    private readonly UserManager<UserEntity> _userManager;
    public ClienteServices(IUnitOfWork repository, UserManager<UserEntity> userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }

    public async Task<RequestResult<IEnumerable<ClienteDto>>> SelectAllAsync(bool include = true)
    {
        try
        {
            var entities = await _repository.ClienteImplementacao.SelectAllAsync(include: include);

            var dtos = entities.Select(e => new ClienteDto
            {
                Id = e.Id,
                ClienteId = e.UsuarioEntityClienteId,
                ClienteNome = e.UsuarioEntityCliente.Nome ?? "N/A",
                ClienteEmail = e.UsuarioEntityCliente.Email ?? "N/A",
                NomeFantasia = e.NomeFantasia,
                DataAbertura = e.DataAbertura,
                DataVencimentoUso = e.DataVencimentoUso,
                UsuariosVinculados = e.UsuariosVinculados?.Select(u => new UsuarioVinculadoClienteDto
                {
                    IdUsuarioVinculado = u.UsuarioVinculadoId,
                    NomeUsuarioVinculado = u.UsuarioVinculado?.Nome,
                    EmailUsuarioVinculado = u.UsuarioVinculado?.Email,
                    AcessoPermitido = u.AcessoPermitido
                }).ToList() ?? new List<UsuarioVinculadoClienteDto>(),
                Filiais = e.Filiais?.Select(f => new FilialDto
                {
                    Id = f.Id,
                    NomeFilial = f.NomeFilial
                }).ToList() ?? new List<FilialDto>()
            });

            return new RequestResult<IEnumerable<ClienteDto>>(dtos);
        }
        catch (Exception ex)
        {

            return new RequestResult<IEnumerable<ClienteDto>>(ex);
        }
    }
    public Task<RequestResult<ClienteDto?>> SelectByUsuarioClienteId(Guid UsuarioEntityClienteId, bool include = true)
    {
        throw new NotImplementedException();
    }
    public async Task<RequestResult<ClienteDto>> RegistrarClienteAsync(ClienteDtoRegistrar clienteRegistro, bool v)
    {
        try
        {
            var userEntity = await _userManager.FindByEmailAsync(clienteRegistro.EmailCliente);
            if (userEntity == null)
                throw new Exception("Usuário não encontrado");

            var clienteEntity = PessoaClienteEntity.CriarUsuarioPessoaCliente(userEntity.Id, clienteRegistro.NomeFantasia);

            await _repository.ClienteRepostory.InsertAsync(clienteEntity);
            if (!await _repository.CommitAsync())
                throw new Exception("Erro ao salvar as alterações no banco de dados.");

            var e = await _repository.ClienteImplementacao.SelectByUsuarioClienteId(clienteEntity.UsuarioEntityClienteId, include: true);
            if (e == null)
                throw new Exception("Erro ao buscar o cliente após o registro.");

            var dto = new ClienteDto
            {
                Id = e.Id,
                ClienteId = e.UsuarioEntityClienteId,
                ClienteNome = e.UsuarioEntityCliente.Nome ?? "N/A",
                ClienteEmail = e.UsuarioEntityCliente.Email ?? "N/A",
                NomeFantasia = e.NomeFantasia,
                DataAbertura = e.DataAbertura,
                DataVencimentoUso = e.DataVencimentoUso,
                UsuariosVinculados = e.UsuariosVinculados?.Select(u => new UsuarioVinculadoClienteDto
                {
                    IdUsuarioVinculado = u.UsuarioVinculadoId,
                    NomeUsuarioVinculado = u.UsuarioVinculado?.Nome,
                    EmailUsuarioVinculado = u.UsuarioVinculado?.Email,
                    AcessoPermitido = u.AcessoPermitido
                }).ToList() ?? new List<UsuarioVinculadoClienteDto>(),
                Filiais = e.Filiais?.Select(f => new FilialDto
                {
                    Id = f.Id,
                    NomeFilial = f.NomeFilial
                }).ToList() ?? new List<FilialDto>()
            };

            return new RequestResult<ClienteDto>(dto);
        }
        catch (Exception ex)
        {

            return new RequestResult<ClienteDto>(ex);
        }
    }
}
