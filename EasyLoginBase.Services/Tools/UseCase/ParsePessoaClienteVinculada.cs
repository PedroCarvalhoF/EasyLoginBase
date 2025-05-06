using EasyLoginBase.Application.Dto.Produto.Produto;
using EasyLoginBase.Domain.Entities.Produto;

namespace EasyLoginBase.Services.Tools.UseCase;

public partial class DtoMapper
{

    public static ProdutoDto ParseProduto(ProdutoEntity produtoEntity)
    {
        return new ProdutoDto
        {
            Id = produtoEntity.Id,
            NomeProduto = produtoEntity.NomeProduto,
            CodigoProduto = produtoEntity.CodigoProduto,
            CategoriaProdutoEntityId = produtoEntity.CategoriaProdutoEntityId,
            CategoriaProduto = produtoEntity.CategoriaProdutoEntity.NomeCategoria,
            UnidadeMedidaProdutoEntityId = produtoEntity.UnidadeMedidaProdutoEntityId,
            UnidadeMedidaProduto = produtoEntity.UnidadeMedidaProdutoEntity.Sigla
        };
    }

    public static IEnumerable<ProdutoDto> ParseProdutos(IEnumerable<ProdutoEntity> entities)
    {
        foreach (var entity in entities)
        {
            yield return ParseProduto(entity);
        }
    }
}
