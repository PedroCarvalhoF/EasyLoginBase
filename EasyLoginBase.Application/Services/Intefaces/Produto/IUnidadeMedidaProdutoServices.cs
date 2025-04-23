using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Produto.UnidadeMedidaProduto;

namespace EasyLoginBase.Application.Services.Intefaces.Produto;

public interface IUnidadeMedidaProdutoServices
{
    Task<RequestResult<UnidadeMedidaProdutoDto>> InsertAsync(UnidadeMedidaProdutoDtoCreate unidadeMedidaProdutoDtoCreate);
    Task<RequestResult<UnidadeMedidaProdutoDto>> Update(UnidadeMedidaProdutoDtoUpdate unidadeMedidaProdutoDtoUpdate);
    Task<RequestResult<UnidadeMedidaProdutoDto>> SelectAsync(Guid id);
    Task<RequestResult<IEnumerable<UnidadeMedidaProdutoDto>>> Select();
}
