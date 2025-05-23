﻿using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Produto.Estoque.Movimento;
using EasyLoginBase.Application.Services.Intefaces.Produto;
using EasyLoginBase.Application.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyLoginBase.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MovimentoEstoqueController : ControllerBase
{
    private readonly IMovimentoEstoqueServices<MovimentoEstoqueDto> _estoqueProdutoServices;

    public MovimentoEstoqueController(IMovimentoEstoqueServices<MovimentoEstoqueDto> estoqueProdutoServices)
    {
        _estoqueProdutoServices = estoqueProdutoServices;
    }

    [HttpGet]
    public async Task<ActionResult<RequestResult<IEnumerable<MovimentoEstoqueDto>>>> SelectAllAsync()
    {
        try
        {
            return new ReturnActionResult<IEnumerable<MovimentoEstoqueDto>>().ParseToActionResult(await _estoqueProdutoServices.SelectAllAsync(User, true));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<IEnumerable<MovimentoEstoqueDto>>().ParseToActionResult(RequestResult<IEnumerable<MovimentoEstoqueDto>>.BadRequest(ex.Message));
        }
    }

    [HttpPost("select-filtro-movimentacao-produto-estoque")]
    public async Task<ActionResult<RequestResult<IEnumerable<MovimentoEstoqueDto>>>> SelectByFiltroAsync([FromBody] MovimentoEstoqueDtoFiltro filtroS)
    {
        try
        {
            return new ReturnActionResult<IEnumerable<MovimentoEstoqueDto>>().ParseToActionResult(await _estoqueProdutoServices.SelectByFiltroAsync(filtroS, User, true));
        }
        catch (Exception ex)
        {
            return new ReturnActionResult<IEnumerable<MovimentoEstoqueDto>>().ParseToActionResult(RequestResult<IEnumerable<MovimentoEstoqueDto>>.BadRequest(ex.Message));
        }
    }
}
