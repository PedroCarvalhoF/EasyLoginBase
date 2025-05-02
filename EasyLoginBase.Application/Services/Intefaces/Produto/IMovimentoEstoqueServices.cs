using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Produto.Estoque.Movimento;
using System.Security.Claims;

namespace EasyLoginBase.Application.Services.Intefaces.Produto;
public interface IMovimentoEstoqueServices<E> where E : MovimentoEstoqueDto
{
    Task<RequestResult<IEnumerable<E>>> SelectAllAsync(ClaimsPrincipal user, bool include = true);
}
