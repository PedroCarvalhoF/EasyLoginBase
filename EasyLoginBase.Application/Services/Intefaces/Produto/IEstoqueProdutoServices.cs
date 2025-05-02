using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Produto.Estoque.Estoque;
using System.Security.Claims;

namespace EasyLoginBase.Application.Services.Intefaces.Produto;
public interface IEstoqueProdutoServices<E> where E : EstoqueProdutoDto
{
    Task<RequestResult<E>> MovimentarEstoque(EstoqueProdutoDtoManter estoqueProdutoDto, ClaimsPrincipal user);
    Task<RequestResult<IEnumerable<E>>> SelectAllAsync(ClaimsPrincipal user, bool include = true);
    Task<RequestResult<IEnumerable<E>>> SelectByProdutoId(Guid clienteId, Guid produtoId, bool include = true);
    Task<RequestResult<IEnumerable<E>>> SelectByFiliaId(Guid clienteId, Guid filialId, bool include = true);
    Task<RequestResult<IEnumerable<EstoqueProdutoDto>>> MovimentarEstoqueMultiplos(EstoqueProdutoDtoManterMultiplosProdutos estoqueProdutoDtoManterMultiplos, ClaimsPrincipal user);
}
