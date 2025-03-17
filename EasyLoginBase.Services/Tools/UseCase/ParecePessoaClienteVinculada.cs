using EasyLoginBase.Application.Dto.PessoaClienteVinculada;
using EasyLoginBase.Application.Dto.Produto.Categoria;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Entities.Produto;
using System.Collections.Generic;
using System.Linq;

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
