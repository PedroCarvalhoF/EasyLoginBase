using EasyLoginBase.Application.Dto.PessoaClienteVinculada;
using EasyLoginBase.Application.Services.Intefaces.PessoaClienteVinculada;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Services.Tools.UseCase;

namespace EasyLoginBase.Services.Services.PessoaClienteVinculada;

public class PessoaClienteVinculadaServices : IPessoaClienteVinculadaServices
{
    private readonly IUnitOfWork _repository;

    public PessoaClienteVinculadaServices(IUnitOfWork repository)
    {
        _repository = repository;
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

    public Task<List<PessoaClienteVinculadaDto>> ObterClientesVinculadosPorUsuarioAsync(Guid usuarioVinculadoId)
    {
        throw new NotImplementedException();
    }

    public Task<List<PessoaClienteVinculadaDto>> ObterUsuariosVinculadosPorClienteAsync(Guid pessoaClienteId)
    {
        throw new NotImplementedException();
    }
}
