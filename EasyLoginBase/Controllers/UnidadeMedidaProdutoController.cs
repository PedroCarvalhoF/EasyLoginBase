using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Produto.UnidadeMedidaProduto;
using EasyLoginBase.Application.Services.Intefaces.Produto;
using EasyLoginBase.Application.Tools;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadeMedidaProdutoController(IUnidadeMedidaProdutoServices _unidadeMedidaProdutoServices) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<RequestResult<UnidadeMedidaProdutoDto>>> CadastrarUnidadeMedidaProduto([FromBody] UnidadeMedidaProdutoDtoCreate create)
        {
            try
            {
                if (create == null) return BadRequest("Requisição inválida.");

                return new ReturnActionResult<UnidadeMedidaProdutoDto>().ParseToActionResult(await _unidadeMedidaProdutoServices.InsertAsync(create));
            }
            catch (Exception ex)
            {
                return new ReturnActionResult<UnidadeMedidaProdutoDto>().ParseToActionResult(RequestResult<UnidadeMedidaProdutoDto>.BadRequest(ex.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<RequestResult<IEnumerable<UnidadeMedidaProdutoDto>>>> ConsultarUnidadesProdutosAsync()
        {
            try
            {
                var result = await _unidadeMedidaProdutoServices.Select();

                return new ReturnActionResult<IEnumerable<UnidadeMedidaProdutoDto>>().ParseToActionResult(result);
            }
            catch (Exception ex)
            {

                return new ReturnActionResult<UnidadeMedidaProdutoDto>().ParseToActionResult(RequestResult<UnidadeMedidaProdutoDto>.BadRequest(ex.Message));
            }
        }
    }
}
