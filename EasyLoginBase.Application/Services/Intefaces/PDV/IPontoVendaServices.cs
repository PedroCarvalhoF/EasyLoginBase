using EasyLoginBase.Application.Dto.PDV.PDV;
using System.Security.Claims;

namespace EasyLoginBase.Application.Services.Intefaces.PDV;

public interface IPontoVendaServices
{
    Task<PontoVendaDto> AbrirPontoVenda(PontoVendaDtoCreate create, ClaimsPrincipal user);
    Task<IEnumerable<PontoVendaDto>> ConsultarPdvsAbertos(ClaimsPrincipal user);
    Task<IEnumerable<PontoVendaDto>> ConsultarPdvsFiltro(PontoVendaDtoFiltroConsulta filtro, ClaimsPrincipal user);
}
