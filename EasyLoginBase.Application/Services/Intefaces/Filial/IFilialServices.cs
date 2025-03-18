using EasyLoginBase.Application.Dto.Filial;
using System.Security.Claims;

namespace EasyLoginBase.Application.Services.Intefaces.Filial;

public interface IFilialServices
{
    Task<FilialDto> CadastrarFilial(FilialDtoCreate filialDtoCreate, ClaimsPrincipal user);
    Task<IEnumerable<FilialDto>> ConsultarFiliais(ClaimsPrincipal user);
    Task<FilialDto> ConsultarFilialById(Guid filialEntityId, ClaimsPrincipal user);
    Task<bool> NomeFilialUso(string nomeFilial, ClaimsPrincipal user);
}
