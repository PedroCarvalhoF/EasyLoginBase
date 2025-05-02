using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Produto.Estoque.Estoque;
using EasyLoginBase.Application.Dto.Produto.Produto;
using EasyLoginBase.Application.Services.Intefaces.Produto;
using EasyLoginBase.Application.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EstoqueProdutoController : ControllerBase
{
    private readonly IEstoqueProdutoServices<EstoqueProdutoDto> _estoqueProdutoServices;

    public EstoqueProdutoController(IEstoqueProdutoServices<EstoqueProdutoDto> estoqueProdutoServices)
    {
        _estoqueProdutoServices = estoqueProdutoServices;
    }

    [HttpPost]
    public async Task<ActionResult<RequestResult<EstoqueProdutoDto>>> MovimentarEstoqueAsync([FromBody] EstoqueProdutoDtoManter estoque)
    {
        try
        {
            if (estoque == null)
                return new ReturnActionResult<EstoqueProdutoDto>().ParseToActionResult(RequestResult<EstoqueProdutoDto>.BadRequest("Dados Inválidos"));

            return new ReturnActionResult<EstoqueProdutoDto>().ParseToActionResult(await _estoqueProdutoServices.MovimentarEstoque(estoque, User));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<IEnumerable<ProdutoDto>>().ParseToActionResult(RequestResult<IEnumerable<ProdutoDto>>.BadRequest(ex.Message));
        }
    }

    [HttpPost("multiplos-produtos")]
    public async Task<ActionResult<RequestResult<EstoqueProdutoDto>>> MovimentarEstoqueMultiplosAsync([FromBody] EstoqueProdutoDtoManterMultiplosProdutos estoqueProdutoDtoManterMultiplos)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<IEnumerable<ProdutoDto>>().ParseToActionResult(RequestResult<IEnumerable<ProdutoDto>>.BadRequest(ex.Message));
        }
    }


    [HttpGet]
    public async Task<ActionResult<RequestResult<EstoqueProdutoDto>>> SelectAllAsync()
    {
        try
        {
            return new ReturnActionResult<IEnumerable<EstoqueProdutoDto>>().ParseToActionResult(await _estoqueProdutoServices.SelectAllAsync(User, true));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<IEnumerable<ProdutoDto>>().ParseToActionResult(RequestResult<IEnumerable<ProdutoDto>>.BadRequest(ex.Message));
        }
    }
}
