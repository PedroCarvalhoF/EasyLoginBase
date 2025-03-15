using EasyLoginBase.Application.Dto.Filial;

namespace EasyLoginBase.Application.Services.Intefaces;

public interface IFilialServices
{
    Task<FilialDto> CriarFilialAsync(FilialDtoCreate filialCreate);    
    Task<FilialDto?> SelecionarFilialPorId(Guid idFilial);
    Task<IEnumerable<FilialDto>> SelecionarFiliaisPorIdPessoaCliente(Guid idPessoaCliente);
}
