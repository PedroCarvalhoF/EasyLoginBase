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
    public async Task<ActionResult<RequestResult<CategoriaProdutoDto>>> CadastrarCategoriaProduto([FromBody] CategoriaProdutoDtoCreate command)
    {
        if (command == null)
            return BadRequest("Requisição inválida.");

        return new ReturnActionResult<CategoriaProdutoDto>().ParseToActionResult(await _services.CadastrarCategoriaProduto(command, User));
    }

    [HttpPost("consultar-categoria-by-id")]
    public async Task<ActionResult<RequestResult<CategoriaProdutoDto>>> ConsultarCategoriaProdutoById(DtoRequestId id)
    {
        try
        {
            return new ReturnActionResult<CategoriaProdutoDto>()
                .ParseToActionResult(await _services.ConsultarCategoriaProdutoById(User, id));
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
            return new ReturnActionResult<IEnumerable<CategoriaProdutoDto>>()
                .ParseToActionResult(await _services.ConsultarCategoriasProdutos(User));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<CategoriaProdutoDto>().ParseToActionResult(RequestResult<CategoriaProdutoDto>.BadRequest(ex.Message));
        }
    }

    [HttpPost("atualizar-categoria-produto")]

    public async Task<ActionResult<RequestResult<CategoriaProdutoDto>>> AtualizarCategoriaProduto([FromBody] CategoriaProdutoDtoUpdate categoriaProdutoDtoUpdate)
    {
        if (categoriaProdutoDtoUpdate == null)
            return BadRequest("Requisição inválida.");
        return new ReturnActionResult<CategoriaProdutoDto>().
            ParseToActionResult(await _services.AlterarCategoriaProduto(categoriaProdutoDtoUpdate, User));
    }
}