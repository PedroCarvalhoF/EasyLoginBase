using EasyLoginBase.Application.Dto.PessoaClienteVinculada;

namespace EasyLoginBase.Application.Services.Intefaces.PessoaClienteVinculada;

public interface IPessoaClienteVinculadaServices
{
    /// <summary>
    /// Adiciona um novo vínculo entre um usuário e um cliente.
    /// </summary>    
    Task<PessoaClienteVinculadaDto> AdicionarUsuarioVinculadoAsync(PessoaClienteVinculadaDtoCreate pessoaClienteVinculadaDtoCreate);

    /// <summary>
    /// Habilita ou desabilita o acesso de um usuário vinculado.
    /// </summary>    
    Task<PessoaClienteVinculadaDto> AlterarStatusAcessoAsync(PessoaClienteVinculadaDtoUpdate pessoaClienteVinculadaDtoUpdate);

    /// <summary>
    /// Obtém todos os usuários vinculados a um determinado cliente.
    /// </summary>
    Task<List<PessoaClienteVinculadaDto>> ObterUsuariosVinculadosPorClienteAsync(Guid pessoaClienteId);

    /// <summary>
    /// Obtém todos os clientes aos quais um usuário está vinculado.
    /// </summary>
    Task<List<PessoaClienteVinculadaDto>> ObterClientesVinculadosPorUsuarioAsync(Guid usuarioVinculadoId);
    Task<IEnumerable<PessoaClienteVinculadaDto>> GetPessoasVinculadas();
}
