using EasyLoginBase.Application.Dto.Preco.Produto.CategoriaPrecoProduto;
using System.Security.Claims;

namespace EasyLoginBase.Application.Services.Intefaces.Preco.Produto.CategoriaPreco;

public interface ICategoriaPrecoProdutoServices
{
    Task<CategoriaPrecoProdutoDto> CadastrarCategoriaPrecoProduto(CategoriaPrecoProdutoDtoCreate create, ClaimsPrincipal user);
    Task<IEnumerable<CategoriaPrecoProdutoDto>> ConsultarCategoriasPrecosProdutos(ClaimsPrincipal user);
    Task<bool> NomeCategoriaPrecoProdutoEmUso(string nomeCategoriaPrecoProduto, ClaimsPrincipal user);

}
