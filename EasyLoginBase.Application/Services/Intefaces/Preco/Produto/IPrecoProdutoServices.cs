using EasyLoginBase.Application.Dto.Preco.Produto;
using System.Security.Claims;

namespace EasyLoginBase.Application.Services.Intefaces.Preco.Produto;

public interface IPrecoProdutoServices
{
    Task<PrecoProdutoDto> CadastrarPrecoProduto(PrecoProdutoDtoCreate preco, ClaimsPrincipal user);
    Task<IEnumerable<PrecoProdutoDto>> ConsultarPrecosProdutos(ClaimsPrincipal user);
    Task<IEnumerable<PrecoProdutoDto>> ConsultarProdutoByProdutoId(Guid idProduto, ClaimsPrincipal user);
}
