using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.PDV.PDV;
using EasyLoginBase.Application.Services.Intefaces.PDV;
using EasyLoginBase.Application.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PontoVendaController(IPontoVendaServices _services) : ControllerBase
    {
        [HttpPost("abrir-novo-pdv")]
        public async Task<ActionResult<RequestResult<PontoVendaDto>>> CadastrarFilial([FromBody] PontoVendaDtoCreate command)
        {
            try
            {
                if (command == null)
                    return BadRequest("Requisição inválida.");

                var result = await _services.AbrirPontoVenda(command, User);

                return new ReturnActionResult<PontoVendaDto>().ParseToActionResult(RequestResult<PontoVendaDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return new ReturnActionResult<PontoVendaDto>().ParseToActionResult(RequestResult<PontoVendaDto>.BadRequest(ex.Message));
            }
        }

        [HttpGet("consultar-pdvs-abertos")]
        public async Task<ActionResult<RequestResult<IEnumerable<PontoVendaDto>>>> ConsultarPdvsAbertos()
        {
            try
            {


                IEnumerable<PontoVendaDto> result = await _services.ConsultarPdvsAbertos(User);

                return new ReturnActionResult<IEnumerable<PontoVendaDto>>().ParseToActionResult(RequestResult<IEnumerable<PontoVendaDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return new ReturnActionResult<IEnumerable<PontoVendaDto>>().ParseToActionResult(RequestResult<IEnumerable<PontoVendaDto>>.BadRequest(ex.Message));
            }
        }

        [HttpPost("consultar-pdvs-filtro")]
        public async Task<ActionResult<RequestResult<IEnumerable<PontoVendaDto>>>> ConsultarPdvsFiltro([FromBody] PontoVendaDtoFiltroConsulta filtro)
        {
            try
            {


                IEnumerable<PontoVendaDto> result = await _services.ConsultarPdvsFiltro(filtro, User);

                return new ReturnActionResult<IEnumerable<PontoVendaDto>>().ParseToActionResult(RequestResult<IEnumerable<PontoVendaDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return new ReturnActionResult<IEnumerable<PontoVendaDto>>().ParseToActionResult(RequestResult<IEnumerable<PontoVendaDto>>.BadRequest(ex.Message));
            }
        }


    }
}
