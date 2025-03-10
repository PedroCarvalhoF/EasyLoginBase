using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Application.Services.Intefaces;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Services.Tools.UseCase.DtoForEntity.Filial;

namespace EasyLoginBase.Services.Services.Filial;

public class FilialServices : IFilialServices<FilialDto>
{
    private readonly IUnitOfWork _repository;

    public FilialServices(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<FilialDto> CreateFilialAsync(FiliaDtoCreateRequest filial)
    {
        try
        {
            var entity = await _repository.FilialRepository.CreateFilialAsync(DtoMapper.ParceFilialDtoEntity(filial));
            if (await _repository.CommitAsync())
                return DtoMapper.ParceFilial(entity);

            throw new ArgumentException("Não foi possível salvar filial.");

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public Task<FilialDto> UpdateFilialAsync(FilialDtoUpdateRequest filial)
    {
        throw new NotImplementedException();
    }
    public async Task<IEnumerable<FilialDto>> SelectFilialAsync()
    {
        try
        {
            var entities = await _repository.FilialRepository.SelectFilialAsync();
            return DtoMapper.ParceFilial(entities);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public Task<IEnumerable<FilialDto>> SelectFilialAsync(Guid idFilial)
    {
        throw new NotImplementedException();
    }


}
