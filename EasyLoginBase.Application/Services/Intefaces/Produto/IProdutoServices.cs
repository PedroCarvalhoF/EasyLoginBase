using EasyLoginBase.Application.Dto.Produto.Produto;
using System.Security.Claims;

namespace EasyLoginBase.Application.Services.Intefaces.Produto;

public interface IProdutoServices
{
    Task<ProdutoDto> CadastrarProduto(ProdutoDtoCreate produtoDtoCreate, ClaimsPrincipal claims);
    Task<IEnumerable<ProdutoDto>> ConsultarProdutos(ClaimsPrincipal user);
}
