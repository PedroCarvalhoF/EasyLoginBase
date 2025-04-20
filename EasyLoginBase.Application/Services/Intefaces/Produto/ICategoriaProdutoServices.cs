using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Produto.Categoria;
using System.Security.Claims;

namespace EasyLoginBase.Application.Services.Intefaces.Produto;

public interface ICategoriaProdutoServices
{
    Task<RequestResult<CategoriaProdutoDto>> AlterarCategoriaProduto(CategoriaProdutoDtoUpdate categoriaProdutoDtoUpdate, ClaimsPrincipal user);
    Task<RequestResult<CategoriaProdutoDto>> CadastrarCategoriaProduto(CategoriaProdutoDtoCreate categoriaProdutoDtoCreate, ClaimsPrincipal claims);
    Task<RequestResult<CategoriaProdutoDto>> ConsultarCategoriaProdutoById(ClaimsPrincipal user, DtoRequestId id);
    Task<RequestResult<IEnumerable<CategoriaProdutoDto>>> ConsultarCategoriasProdutos(ClaimsPrincipal user);
}
