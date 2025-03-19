using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Preco.Produto;
using EasyLoginBase.Application.Services.Intefaces.Preco.Produto;
using EasyLoginBase.Application.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PrecoProdutoController(IPrecoProdutoServices _precoProdutoServices) : ControllerBase
{
    [HttpPost("cadastrar-preco-produto")]
    public async Task<ActionResult<RequestResult<PrecoProdutoDto>>> CadastrarFilial([FromBody] PrecoProdutoDtoCreate command)
    {
        try
        {
            if (command == null)
                return BadRequest("Requisição inválida.");

            var result = await _precoProdutoServices.CadastrarPrecoProduto(command, User);

            return new ReturnActionResult<PrecoProdutoDto>().ParseToActionResult(RequestResult<PrecoProdutoDto>.Ok(result));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<PrecoProdutoDto>().ParseToActionResult(RequestResult<PrecoProdutoDto>.BadRequest(ex.Message));
        }
    }

    [HttpGet("consultar-preco-produto/produto={idProduto}")]
    public async Task<ActionResult<RequestResult<IEnumerable<PrecoProdutoDto>>>> ConsultarProdutoByProdutoId(Guid idProduto)
    {
        try
        {
            IEnumerable<PrecoProdutoDto> result = await _precoProdutoServices.ConsultarProdutoByProdutoId(idProduto, User);
            return new ReturnActionResult<IEnumerable<PrecoProdutoDto>>().ParseToActionResult(RequestResult<IEnumerable<PrecoProdutoDto>>.Ok(result));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<IEnumerable<PrecoProdutoDto>>().ParseToActionResult(RequestResult<IEnumerable<PrecoProdutoDto>>.BadRequest(ex.Message));
        }
    }

    [HttpGet("consultar-precos-produtos")]
    public async Task<ActionResult<RequestResult<IEnumerable<PrecoProdutoDto>>>> ConsultarPrecosProdutos()
    {
        try
        {
            var result = await _precoProdutoServices.ConsultarPrecosProdutos(User);
            return new ReturnActionResult<IEnumerable<PrecoProdutoDto>>().ParseToActionResult(RequestResult<IEnumerable<PrecoProdutoDto>>.Ok(result));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<IEnumerable<PrecoProdutoDto>>().ParseToActionResult(RequestResult<IEnumerable<PrecoProdutoDto>>.BadRequest(ex.Message));
        }
    }
}
