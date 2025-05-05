using EasyLoginBase.Application.Dto.PessoaCliente;
using EasyLoginBase.Application.Dto.User;
using EasyLoginBase.Domain.Entities.PessoaCliente;

namespace EasyLoginBase.Services.Tools.UseCase;

public partial class DtoMapper
{
    public static PessoaClienteDto ParcePessoaCliente(PessoaClienteEntity pessoaClienteEntity)
    {
        return new PessoaClienteDto
        {
            Id = pessoaClienteEntity.Id,
            UsuarioEntityClienteId = pessoaClienteEntity.UsuarioEntityClienteId,
            NomeUsuarioCliente = pessoaClienteEntity.UsuarioEntityCliente?.UserName,
            NomeFantasia = pessoaClienteEntity.NomeFantasia,
            DataAbertura = pessoaClienteEntity.DataAbertura,
            DataVencimentoUso = pessoaClienteEntity.DataVencimentoUso,
            UsuariosVinculadosDtos = new List<UserDto>(pessoaClienteEntity.UsuariosVinculados.Select
            (userVinculado => new UserDto
            {
                Email = userVinculado.UsuarioVinculado.Email,
                Id = userVinculado.UsuarioVinculado.Id,
                Nome = userVinculado.UsuarioVinculado.UserName,
                SobreNome = userVinculado.UsuarioVinculado.SobreNome,

            }))
        };
    }

    public static IEnumerable<PessoaClienteDto> ParcePessoaCliente(IEnumerable<PessoaClienteEntity> pessoasClientesEntities)
    {
        foreach (var user in pessoasClientesEntities)
        {
            yield return ParcePessoaCliente(user);
        }
    }

    public static PessoaClienteEntity ParcePessoaCliente(PessoaClienteDtoCreate pessoaClienteDtoCreate)
    {
        return PessoaClienteEntity.CriarUsuarioPessoaCliente(pessoaClienteDtoCreate.UsuarioEntityClienteId, pessoaClienteDtoCreate.NomeFantasia);
    }
}
