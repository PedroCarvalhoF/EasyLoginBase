using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Application.Services.Intefaces;
using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Services.Tools.UseCase;

namespace EasyLoginBase.Services.Services.Filial;

public class FilialServices : IFilialServices
{
    private readonly IUnitOfWork _repository;

    public FilialServices(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<FilialDto> CriarFilialAsync(FilialDtoCreate filialCreate)
    {
        try
        {
            var entity = FilialEntity.CriarFilial(filialCreate.PessoaClienteId, filialCreate.NomeFilial);

            var filial = await _repository.FilialRepository.CriarFilialAsync(entity);

            var filialDto = DtoMapper.ParceFilial(filial);

            return filialDto;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message); ;
        }
    }

    public async Task<IEnumerable<FilialDto>> SelecionarFiliaisPorIdPessoaCliente(Guid idPessoaCliente)
    {
        try
        {
            var filiais = await _repository.FilialRepository.SelecionarFiliaisPorIdPessoaCliente(idPessoaCliente);

            return DtoMapper.ParceFilial(filiais);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message); ;
        }
    }

    public async Task<FilialDto?> SelecionarFilialPorId(Guid idFilial)
    {
        try
        {
            var filial = await _repository.FilialRepository.SelecionarFilialPorId(idFilial);

            if (filial == null)
                throw new Exception("Filial não encontrada");

            var dto = DtoMapper.ParceFilial(filial);

            return dto;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
