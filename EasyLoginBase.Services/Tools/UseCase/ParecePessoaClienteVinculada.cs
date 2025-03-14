using EasyLoginBase.Application.Dto.PessoaClienteVinculada;
using EasyLoginBase.Domain.Entities.PessoaCliente;
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
}
