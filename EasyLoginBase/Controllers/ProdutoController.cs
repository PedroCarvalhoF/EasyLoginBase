using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Produto.Produto;
using EasyLoginBase.Application.Services.Intefaces.Produto;
using EasyLoginBase.Application.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProdutoController(IProdutoServices _services) : ControllerBase
{
    [HttpGet("consultar-produtos")]
    public async Task<ActionResult<RequestResult<IEnumerable<ProdutoDto>>>> ConsultarProdutosAsync()
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

    [HttpPost("cadastrar-produto")]
    public async Task<ActionResult<RequestResult<ProdutoDto>>> CadastrarProdutoAsync([FromBody] ProdutoDtoCreate command)
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

    [HttpPost("atualizar-produto")]
    public async Task<ActionResult<RequestResult<ProdutoDto>>> AtualizarProdutoAcync([FromBody] ProdutoDtoUpdate update)
    {
        try
        {
            if (update == null)
                return BadRequest("Requisição inválida.");

            return new ReturnActionResult<ProdutoDto>().ParseToActionResult(await _services.UpdateAsync(update, User));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<ProdutoDto>().ParseToActionResult(RequestResult<ProdutoDto>.BadRequest(ex.Message));
        }
    }
}
