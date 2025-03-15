using System;
using System.Threading.Tasks;
using EasyLoginBase.Domain.Entities.PessoaCliente;

namespace EasyLoginBase.Domain.Interfaces.PessoaCliente;

public interface IPessoaClienteVinculadaRepository
{
    /// <summary>
    /// Adiciona um novo vínculo entre um usuário e um cliente.
    /// </summary>
    /// <param name="pessoaClienteId">ID do cliente.</param>
    /// <param name="usuarioVinculadoId">ID do usuário a ser vinculado.</param>
    /// <param name="acessoPermitido">Define se o acesso já estará habilitado.</param>
    Task<PessoaClienteVinculadaEntity> AdicionarUsuarioVinculadoAsync(PessoaClienteVinculadaEntity pessoaVinculadaEntity);

    /// <summary>
    /// Habilita ou desabilita o acesso de um usuário vinculado.
    /// </summary>
    /// <param name="id">ID da entidade PessoaClienteVinculada.</param>
    /// <param name="acessoPermitido">Define se o acesso será habilitado (true) ou desabilitado (false).</param>
    Task<PessoaClienteVinculadaEntity> AlterarStatusAcessoAsync(Guid id, bool acessoPermitido);

    /// <summary>
    /// Obtém todos os usuários vinculados a um determinado cliente.
    /// </summary>
    Task<List<PessoaClienteVinculadaEntity>> ObterUsuariosVinculadosPorClienteAsync(Guid pessoaClienteId);

    /// <summary>
    /// Obtém todos os clientes aos quais um usuário está vinculado.
    /// </summary>
    Task<List<PessoaClienteVinculadaEntity>> ObterClientesVinculadosPorUsuarioAsync(Guid usuarioVinculadoId);
    Task<IEnumerable<PessoaClienteVinculadaEntity>> GetPessoasVinculas();
    Task<IEnumerable<PessoaClienteVinculadaEntity>> GetVinculosPessoas(Guid idPessoaVinculada);
}
