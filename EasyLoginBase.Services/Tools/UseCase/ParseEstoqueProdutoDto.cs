using EasyLoginBase.Application.Dto.Produto.Estoque.Estoque;
using EasyLoginBase.Domain.Entities.Produto.Estoque;

namespace EasyLoginBase.Services.Tools.UseCase;
public static class ParseEstoqueProdutoDto
{
    public static EstoqueProdutoDto EstoqueProdutoEntityToDto(this EstoqueProdutoEntity estoqueEntity)
    {
        if (estoqueEntity == null)
            throw new Exception("Entidade estoque não pode ser nula.");

        if (estoqueEntity.Produto == null)
            throw new Exception("Entidade estoque não pode ser nula.");

        if (estoqueEntity.Filial == null)
            throw new Exception("Entidade estoque não pode ser nula.");

        if (estoqueEntity.Produto.UnidadeMedidaProdutoEntity == null)
            throw new Exception("Entidade estoque não pode ser nula.");

        if (estoqueEntity.Produto.CategoriaProdutoEntity == null)
            throw new Exception("Entidade estoque não pode ser nula.");


        return new EstoqueProdutoDto(produtoId: estoqueEntity.Produto.Id,
                                   nomeProduto: estoqueEntity.Produto.NomeProduto ?? "N/E",
                                      filialId: estoqueEntity.Filial.Id,
                                    nomeFilial: estoqueEntity.Filial.NomeFilial ?? "N/E",
                                    quantidade: estoqueEntity.Quantidade,
                          unidadeMedidaProduto: estoqueEntity.Produto.UnidadeMedidaProdutoEntity.Sigla ?? "N/A",
                      categoriaProdutoEntityId: estoqueEntity.Produto.CategoriaProdutoEntityId,
                              categoriaProduto: estoqueEntity.Produto.CategoriaProdutoEntity.NomeCategoria ?? "N/A");
    }

    public static IEnumerable<EstoqueProdutoDto> EstoqueProdutoEntityToDto(this IEnumerable<EstoqueProdutoEntity> estoqueEntities)
    {
        if (estoqueEntities == null)
            throw new Exception("Entidade estoque não pode ser nula.");

        return estoqueEntities.Select(e => e.EstoqueProdutoEntityToDto());
    }
}
