using EasyLoginBase.Application.Dto.Produto.Produto;
using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EasyLoginBase.Application.Dto.Preco.Produto;
using EasyLoginBase.Application.Services.Intefaces.Preco.Produto;
using EasyLoginBase.Application.Services.Intefaces.Filial;

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
}
