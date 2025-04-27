using EasyLoginBase.Application.Dto.PessoaClienteVinculada;
using EasyLoginBase.Application.Dto.Produto.Produto;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Entities.Produto;

namespace EasyLoginBase.Services.Tools.UseCase;

public partial class DtoMapper
{
    public static PessoaClienteVinculadaDto ParsePessoaClienteVinculada(PessoaClienteVinculadaEntity entity)
    {
        return new PessoaClienteVinculadaDto
        {
            AcessoPermitido = entity.AcessoPermitido,
            NomeFantasia = entity.PessoaClienteEntity?.NomeFantasia,
            NomeUsuarioVinculado = entity.UsuarioVinculado?.Nome,
            PessoaClienteEntityId = entity.PessoaClienteEntityId,
            UsuarioEntityClienteId = entity.UsuarioVinculadoId,
            UsuarioVinculadoId = entity.UsuarioVinculadoId
        };
    }

    public static IEnumerable<PessoaClienteVinculadaDto> ParsePessoaClienteVinculada(IEnumerable<PessoaClienteVinculadaEntity> entities)
    {
        return entities.Select(ParsePessoaClienteVinculada);
    }

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
