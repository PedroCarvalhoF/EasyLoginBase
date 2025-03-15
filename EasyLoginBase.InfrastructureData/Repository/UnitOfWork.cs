using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Domain.Interfaces.Filial;
using EasyLoginBase.Domain.Interfaces.PessoaCliente;
using EasyLoginBase.InfrastructureData.Context;
using EasyLoginBase.InfrastructureData.Repository.Filial;
using EasyLoginBase.InfrastructureData.Repository.PessoaCliente;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.InfrastructureData.Repository;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly MyContext _context;
    private IFilialRepository? _filialRepository;
    private IPessoaClienteRepository<PessoaClienteEntity>? _pessoaClienteRepository;
    private IPessoaClienteVinculadaRepository? _pessoaClienteVinculadaRepository;
    public UnitOfWork(MyContext context)
    {
        _context = context;
    }
    public async Task<bool> CommitAsync()
    {
        try
        {
            var result = await _context.SaveChangesAsync();
            if (result > 0)
                return true;
            return false;
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException!.Message != null)
                throw new Exception(ex.InnerException.Message);

            throw new Exception(ex.Message);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
        finally
        {

        }
    }
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
    public void FinalizarContexto()
    {
        Dispose();
    }

    //Filial
    public IFilialRepository FilialRepository
    {
        get
        {
            return _filialRepository = _filialRepository ?? new FilialRepository(_context);
        }
    }
    public IPessoaClienteRepository<PessoaClienteEntity> PessoaClienteRepository
    {
        get
        {
            return _pessoaClienteRepository = _pessoaClienteRepository ?? new PessoaClienteRepository(_context);
        }
    }
    public IPessoaClienteVinculadaRepository PessoaClienteVinculadaRepository
    {
        get
        {
            return _pessoaClienteVinculadaRepository = _pessoaClienteVinculadaRepository ?? new PessoaClienteVinculadaRepository(_context);
        }
    }

    //REPOSITORIOS
    //BASE REPOSITORIOS
}
