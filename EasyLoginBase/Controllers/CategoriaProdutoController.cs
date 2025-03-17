using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Produto.Categoria;
using EasyLoginBase.Application.Services.Intefaces.Produto;
using EasyLoginBase.Application.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriaProdutoController(ICategoriaProdutoServices _services) : ControllerBase
{
    [HttpPost("cadastrar-categoria-produto")]
    public async Task<ActionResult<RequestResult<CategoriaProdutoDto>>> CadastrarFilial([FromBody] CategoriaProdutoDtoCreate command)
    {
        try
        {
            if (command == null)
                return BadRequest("Requisição inválida.");

            var result = await _services.CadastrarCategoriaProduto(command, User);

            return new ReturnActionResult<CategoriaProdutoDto>().ParseToActionResult(RequestResult<CategoriaProdutoDto>.Ok(result));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<CategoriaProdutoDto>().ParseToActionResult(RequestResult<CategoriaProdutoDto>.BadRequest(ex.Message));
        }
    }


    [HttpGet("consultar-categorias-produtos")]
    public async Task<ActionResult<RequestResult<IEnumerable<CategoriaProdutoDto>>>> ConsultarCategoriasProdutos()
    {
        try
        {
            IEnumerable<CategoriaProdutoDto> result = await _services.ConsultarCategoriasProdutos(User);

            return new ReturnActionResult<IEnumerable<CategoriaProdutoDto>>().ParseToActionResult(RequestResult<IEnumerable<CategoriaProdutoDto>>.Ok(result));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<CategoriaProdutoDto>().ParseToActionResult(RequestResult<CategoriaProdutoDto>.BadRequest(ex.Message));
        }
    }


}
