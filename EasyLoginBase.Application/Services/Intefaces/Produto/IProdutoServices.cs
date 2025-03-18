using EasyLoginBase.Application.Dto.Produto.Produto;
using System.Security.Claims;

namespace EasyLoginBase.Application.Services.Intefaces.Produto;

public interface IProdutoServices
{
    Task<ProdutoDto> CadastrarProduto(ProdutoDtoCreate produtoDtoCreate, ClaimsPrincipal claims);
    Task<IEnumerable<ProdutoDto>> ConsultarProdutos(ClaimsPrincipal user);

    Task<bool> NomeProdutoUso(string nomeProduto, ClaimsPrincipal user);
    Task<bool> CodigoProdutoUso(string codigoProduto, ClaimsPrincipal user);
    Task<ProdutoDto> ConsultarProdutoById(Guid produtoEntityId, ClaimsPrincipal user);
}
