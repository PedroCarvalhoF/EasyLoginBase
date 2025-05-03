using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Preco.Produto.CategoriaPrecoProduto;
using EasyLoginBase.Application.Services.Intefaces.Preco.Produto.CategoriaPreco;
using EasyLoginBase.Application.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriaPrecoProdutoController(ICategoriaPrecoProdutoServices _services) : ControllerBase
{
    [HttpPost("cadastrar-categoria-preco-produto")]
    public async Task<ActionResult<RequestResult<CategoriaPrecoProdutoDto>>> CadastrarFilial([FromBody] CategoriaPrecoProdutoDtoCreate command)
    {
        try
        {
            if (command == null)
                return BadRequest("Requisição inválida.");

            var result = await _services.CadastrarCategoriaPrecoProduto(command, User);

            return new ReturnActionResult<CategoriaPrecoProdutoDto>().ParseToActionResult(RequestResult<CategoriaPrecoProdutoDto>.Ok(result));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<CategoriaPrecoProdutoDto>().ParseToActionResult(RequestResult<CategoriaPrecoProdutoDto>.BadRequest(ex.Message));
        }
    }

    [HttpGet("consultar-categorias-precos-produtos")]
    public async Task<ActionResult<RequestResult<IEnumerable<CategoriaPrecoProdutoDto>>>> ConsultarCategoriasPrecosProdutos()
    {
        try
        {
            IEnumerable<CategoriaPrecoProdutoDto> result = await _services.ConsultarCategoriasPrecosProdutos(User);

            return new ReturnActionResult<IEnumerable<CategoriaPrecoProdutoDto>>().ParseToActionResult(RequestResult<IEnumerable<CategoriaPrecoProdutoDto>>.Ok(result));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<IEnumerable<CategoriaPrecoProdutoDto>>().ParseToActionResult(RequestResult<IEnumerable<CategoriaPrecoProdutoDto>>.BadRequest(ex.Message));
        }
    }
}
