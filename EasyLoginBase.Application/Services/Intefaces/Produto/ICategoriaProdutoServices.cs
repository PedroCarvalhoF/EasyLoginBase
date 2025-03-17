using EasyLoginBase.Application.Dto.Produto.Categoria;
using System.Security.Claims;

namespace EasyLoginBase.Application.Services.Intefaces.Produto;

public interface ICategoriaProdutoServices
{
    Task<CategoriaProdutoDto> CadastrarCategoriaProduto(CategoriaProdutoDtoCreate categoriaProdutoDtoCreate, ClaimsPrincipal claims);
    Task<IEnumerable<CategoriaProdutoDto>> ConsultarCategoriasProdutos(ClaimsPrincipal user);
}
