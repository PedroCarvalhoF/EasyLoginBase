using EasyLoginBase.Application.Dto.PessoaCliente;
using EasyLoginBase.Application.Dto.User;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Entities.User;

namespace EasyLoginBase.Services.Tools.UseCase;

public partial class DtoMapper
{
    public static PessoaClienteDto ParcePessoaCliente(PessoaClienteEntity pessoaClienteEntity)
    {
        return new PessoaClienteDto(pessoaClienteEntity.Id, pessoaClienteEntity.UsuarioEntityClienteId, pessoaClienteEntity.UsuarioEntityCliente.Nome, pessoaClienteEntity.NomeFantasia, pessoaClienteEntity.DataAbertura, pessoaClienteEntity.DataVencimentoUso);
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
