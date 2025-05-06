using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Application.Services.Intefaces.Filial;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Services.Tools.UseCase;
using System.Security.Claims;

namespace EasyLoginBase.Services.Services.Filial;

public class FilialServices : IFilialServices
{
    private readonly IUnitOfWork _repository;

    public FilialServices(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<FilialDto> CadastrarFilial(FilialDtoCreate filialDtoCreate, ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            if (await NomeFilialUso(filialDtoCreate.NomeFilial, user))
                throw new Exception("Nome da filial já está em uso.");




            var filialEntity = FilialEntity.CriarFilial(Guid.NewGuid(), filialDtoCreate.NomeFilial, clienteId, user_logado);

            await _repository.GetRepository<FilialEntity>().CadastrarAsync(filialEntity);

            if (await _repository.CommitAsync())
                return DtoMapper.ParseFilial(filialEntity);

            throw new Exception("Erro ao cadastrar filial.");
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<IEnumerable<FilialDto>> ConsultarFiliais(ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            IEnumerable<FilialEntity> result = await _repository.FilialRepository.ConsultarFiliais(user, clienteId)
                ?? throw new Exception("Nenhum filial localizada.");

            IEnumerable<FilialDto> filiaisDtos = DtoMapper.ParseFiliais(result);
            return filiaisDtos;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<FilialDto> ConsultarFilialById(Guid filialEntityId, ClaimsPrincipal user)
    {
        try
        {
            var filiaisEntities = await _repository.GetRepository<FilialEntity>().ConsultarPorFiltroAsync(f => f.Id == filialEntityId, user.GetClienteIdVinculo());

            return filiaisEntities is null || filiaisEntities.Count() == 0 ? throw new Exception("Filial não localizada.") : DtoMapper.ParseFilial(filiaisEntities.Single());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> NomeFilialUso(string nomeFilial, ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            var result = await _repository.GetRepository<FilialEntity>().ConsultarPorFiltroAsync(f => f.NomeFilial == nomeFilial, clienteId);

            if (result is null)
                return false;

            return result.Count() > 0;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
