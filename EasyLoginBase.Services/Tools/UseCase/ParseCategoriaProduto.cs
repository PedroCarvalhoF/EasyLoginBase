using EasyLoginBase.Application.Dto.Produto.Categoria;
using EasyLoginBase.Domain.Entities.Produto;

namespace EasyLoginBase.Services.Tools.UseCase;

public partial class DtoMapper
{
    public static CategoriaProdutoEntity ParseCategoriaProduto(CategoriaProdutoDtoCreate categoriaProdutoDtoCreate, Guid clienteId, Guid usuarioRegistroId)
    {
        return CategoriaProdutoEntity.Criar(categoriaProdutoDtoCreate.NomeCategoria, clienteId, usuarioRegistroId);
    }
    public static CategoriaProdutoDto ParceCategoriaProduto(CategoriaProdutoEntity categoriaProdutoEntity)
    {
        return new CategoriaProdutoDto
        {
            Id = categoriaProdutoEntity.Id,
            ClienteId = categoriaProdutoEntity.ClienteId,
            UsuarioRegistroId = categoriaProdutoEntity.UsuarioRegistroId,
            NomeCategoria = categoriaProdutoEntity.NomeCategoria!,
            CreateAt = categoriaProdutoEntity.CreateAt,
            Habilitado = categoriaProdutoEntity.Habilitado
        };
    }
    internal static IEnumerable<CategoriaProdutoDto> ParseCategoriaProduto(IEnumerable<CategoriaProdutoEntity> categoriasEntities)
    {
        foreach (var categoriaEntity in categoriasEntities)
        {
            yield return ParceCategoriaProduto(categoriaEntity);
        }
    }
}
