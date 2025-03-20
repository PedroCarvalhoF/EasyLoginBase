using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Domain.Interfaces.Filial;
using EasyLoginBase.Domain.Interfaces.PDV;
using EasyLoginBase.Domain.Interfaces.PessoaCliente;
using EasyLoginBase.InfrastructureData.Context;
using EasyLoginBase.InfrastructureData.Repository.Filial;
using EasyLoginBase.InfrastructureData.Repository.PDV;
using EasyLoginBase.InfrastructureData.Repository.PessoaCliente;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace EasyLoginBase.InfrastructureData.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MyContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories = new();

        private IFilialRepository<FilialEntity, ClaimsPrincipal>? _filialRepository;
        private IPessoaClienteRepository<PessoaClienteEntity>? _pessoaClienteRepository;
        private IPessoaClienteVinculadaRepository? _pessoaClienteVinculadaRepository;
        private IUsuarioPdvRepository? _usuarioPdvRepository;

        public UnitOfWork(MyContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex) when (ex.InnerException is not null)
            {
                throw new Exception(ex.InnerException.Message);
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
        public IBaseClienteRepository<T> GetRepository<T>() where T : BaseClienteEntity
        {
            return (IBaseClienteRepository<T>)_repositories.GetOrAdd(typeof(T), _ =>
                new BaseClienteRepository<T>(_context));
        }

        public IFilialRepository<FilialEntity, ClaimsPrincipal> FilialRepository => _filialRepository ??= new FilialRepository(_context);

        public IPessoaClienteRepository<PessoaClienteEntity> PessoaClienteRepository => _pessoaClienteRepository ??= new PessoaClienteRepository(_context);

        public IPessoaClienteVinculadaRepository PessoaClienteVinculadaRepository => _pessoaClienteVinculadaRepository ??= new PessoaClienteVinculadaRepository(_context);

        public IUsuarioPdvRepository UsuarioPdvRepository => _usuarioPdvRepository ??= new UsuarioPdvRepository(_context);
    }
}
