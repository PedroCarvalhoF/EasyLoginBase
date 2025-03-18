using EasyLoginBase.Application.Dto.Preco.Produto;
using System.Security.Claims;

namespace EasyLoginBase.Application.Services.Intefaces.Preco.Produto;

public interface IPrecoProdutoServices
{
    Task<PrecoProdutoDto> CadastrarPrecoProduto(PrecoProdutoDtoCreate preco, ClaimsPrincipal user);
    Task<PrecoProdutoDto> PrecoProdutoJaCadastrado(PrecoProdutoDtoCreate preco, ClaimsPrincipal user);
}
