using EasyLoginBase.Domain.Entities.Filial;
using System.Security.Claims;

namespace EasyLoginBase.Domain.Interfaces.Filial;

public interface IFilialRepository<F, USER> where F : FilialEntity where USER : ClaimsPrincipal
{
    Task<IEnumerable<FilialEntity>?> ConsultarFiliais(ClaimsPrincipal user, Guid clienteId);
}
