using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Interfaces.PessoaCliente;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.InfrastructureData.Repository.PessoaCliente;

public class PessoaClienteRepository : IPessoaClienteRepository<PessoaClienteEntity>
{
    private readonly MyContext _context;

    public PessoaClienteRepository(MyContext context)
    {
        _context = context;
    }
    public async Task<PessoaClienteEntity> CadastrarClienteEntity(PessoaClienteEntity pessoaClienteEntity)
    {
        try
        {
            await _context.PessoaClientes.AddAsync(pessoaClienteEntity);
            return pessoaClienteEntity;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<PessoaClienteEntity>> ConsultarClientes()
    {
        try
        {
            var entities = await
                 _context.PessoaClientes
                 .Include(p => p.UsuarioEntityCliente)
                 .Include(p => p.UsuariosVinculados).ThenInclude(user_vinculado => user_vinculado.UsuarioVinculado)
                 .OrderBy(ps => ps.NomeFantasia).ToListAsync();

            return entities;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<PessoaClienteEntity> ConsultarClientes(Guid idCliente)
    {
        try
        {
            var entity = await
                 _context.PessoaClientes
                 .Include(p => p.UsuarioEntityCliente)
                 .Include(p => p.UsuariosVinculados).ThenInclude(user_vinculado => user_vinculado.UsuarioVinculado)
                 .Where(p => p.UsuarioEntityClienteId == idCliente)
                 .OrderBy(ps => ps.NomeFantasia).SingleOrDefaultAsync();

            return entity;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> VerificarUsoNomeFantasia(string? nomeFantasia)
    {
        if (string.IsNullOrWhiteSpace(nomeFantasia))
            return false;

        return await _context.PessoaClientes
            .AnyAsync(ps => ps.NomeFantasia != null && ps.NomeFantasia.ToLower() == nomeFantasia.ToLower());
    }
}
