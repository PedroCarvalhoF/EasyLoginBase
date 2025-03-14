using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Interfaces.PessoaCliente;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.InfrastructureData.Repository.PessoaCliente;

public class PessoaClienteVinculadaRepository : IPessoaClienteVinculadaRepository
{
    private readonly MyContext _context;

    public PessoaClienteVinculadaRepository(MyContext context)
    {
        _context = context;
    }

    public async Task<PessoaClienteVinculadaEntity> AdicionarUsuarioVinculadoAsync(PessoaClienteVinculadaEntity pessoaVinculadaEntity)
    {
        var vinculo = new PessoaClienteVinculadaEntity
        {
            PessoaClienteEntityId = pessoaVinculadaEntity.PessoaClienteEntityId,
            UsuarioVinculadoId = pessoaVinculadaEntity.UsuarioVinculadoId,
            AcessoPermitido = pessoaVinculadaEntity.AcessoPermitido
        };

        await _context.PessoasClientesVinculadas.AddAsync(vinculo);
        return vinculo;
    }

    public async Task<PessoaClienteVinculadaEntity> AlterarStatusAcessoAsync(Guid id, bool acessoPermitido)
    {
        var vinculo = await _context.PessoasClientesVinculadas.FindAsync(id);

        if (vinculo == null)
            throw new KeyNotFoundException("Vínculo não encontrado.");

        vinculo.AcessoPermitido = acessoPermitido;

        _context.PessoasClientesVinculadas.Update(vinculo);
        return vinculo;
    }

    public async Task<IEnumerable<PessoaClienteVinculadaEntity>> GetPessoasVinculas()
    {
        return await _context.PessoasClientesVinculadas
            .Include(v => v.PessoaClienteEntity)
            .Include(psv => psv.UsuarioVinculado)
            .ToListAsync();
    }

    public async Task<List<PessoaClienteVinculadaEntity>> ObterClientesVinculadosPorUsuarioAsync(Guid usuarioVinculadoId)
    {
        return await _context.PessoasClientesVinculadas
            .Where(v => v.UsuarioVinculadoId == usuarioVinculadoId)
            .Include(v => v.PessoaClienteEntity)  // Se precisar dos dados do cliente
            .Include(psv => psv.UsuarioVinculado)
            .ToListAsync();
    }

    public async Task<List<PessoaClienteVinculadaEntity>> ObterUsuariosVinculadosPorClienteAsync(Guid pessoaClienteId)
    {
        return await _context.PessoasClientesVinculadas
            .Where(v => v.PessoaClienteEntityId == pessoaClienteId)
            .Include(v => v.UsuarioVinculado)  // Se precisar dos dados do usuário
            .Include(psv => psv.UsuarioVinculado)
            .ToListAsync();
    }
}
