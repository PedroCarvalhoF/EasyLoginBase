using EasyLoginBase.Application.Dto.Produto.Categoria;
using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EasyLoginBase.Application.Dto.Produto.Produto;
using EasyLoginBase.Application.Services.Intefaces.Produto;

namespace EasyLoginBase.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProdutoController(IProdutoServices _services) : ControllerBase
{
    [HttpPost("cadastrar-produto")]
    public async Task<ActionResult<RequestResult<ProdutoDto>>> CadastrarFilial([FromBody] ProdutoDtoCreate command)
    {
        try
        {
            if (command == null)
                return BadRequest("Requisição inválida.");

            var result = await _services.CadastrarProduto(command, User);

            return new ReturnActionResult<ProdutoDto>().ParseToActionResult(RequestResult<ProdutoDto>.Ok(result));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<ProdutoDto>().ParseToActionResult(RequestResult<ProdutoDto>.BadRequest(ex.Message));
        }
    }


    [HttpGet("consultar-produtos")]
    public async Task<ActionResult<RequestResult<IEnumerable<ProdutoDto>>>> ConsultarProdutos()
    {
        try
        {
            IEnumerable<ProdutoDto> dtos = await _services.ConsultarProdutos(User);

            return new ReturnActionResult<IEnumerable<ProdutoDto>>().ParseToActionResult(RequestResult<IEnumerable<ProdutoDto>>.Ok(dtos));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<IEnumerable<ProdutoDto>>().ParseToActionResult(RequestResult<IEnumerable<ProdutoDto>>.BadRequest(ex.Message));
        }
    }
}
