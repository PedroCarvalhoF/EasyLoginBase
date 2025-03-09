using EasyLoginBase.Application.Dto.Filial;

namespace EasyLoginBase.Application.Services.Intefaces;

public interface IFilialServices<F> where F : FilialDto
{
    Task<F> CreateFilialAsync(FiliaDtoCreateRequest filial);
    Task<F> UpdateFilialAsync(FilialDtoUpdateRequest filial);
    Task<IEnumerable<F>> SelectFilialAsync();
    Task<IEnumerable<F>> SelectFilialAsync(Guid idFilial);
}
