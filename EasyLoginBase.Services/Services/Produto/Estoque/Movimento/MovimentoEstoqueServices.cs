using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Produto.Estoque.Movimento;
using EasyLoginBase.Application.Services.Intefaces.Produto;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Domain.Entities;
using EasyLoginBase.Domain.Entities.User;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Services.Tools.UseCase;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EasyLoginBase.Services.Services.Produto.Estoque.Movimento;
public class MovimentoEstoqueServices : IMovimentoEstoqueServices<MovimentoEstoqueDto>
{
    private readonly IUnitOfWork _repository;
    private readonly UserManager<UserEntity> _userManager;
    public MovimentoEstoqueServices(IUnitOfWork repository, UserManager<UserEntity> userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }

    public async Task<RequestResult<IEnumerable<MovimentoEstoqueDto>>> SelectAllAsync(ClaimsPrincipal user, bool include = true)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            var users = await _userManager.Users.ToArrayAsync();

            FiltroBase filtro_user = new FiltroBase(clienteId, user_logado);

            var entities = await _repository.MovimentacaoEstoqueProdutoImplementacao.SelectAllAsync(filtro_user);

            var dtos = ParseMovimentacaoEstoque.EstoqueProdutoEntityForDto(entities, users);

            return new RequestResult<IEnumerable<MovimentoEstoqueDto>>(dtos);
        }
        catch (Exception ex)
        {

            return new RequestResult<IEnumerable<MovimentoEstoqueDto>>(ex);
        }
    }
}
