using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Entities.PDV;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Entities.Produto;
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

        private IPontoVendaRepository<PontoVendaEntity>? _pontoVendaRepository;
        private IBaseClienteRepository<PontoVendaEntity>? _pontoVendaRepositoryBase;

        //NOVO REPOSITORIO

        private IBaseClienteRepository_REFACTOR<ProdutoEntity>? _produtoRepository;


        public UnitOfWork(MyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Salva as mudanças no banco de dados de forma assíncrona.
        /// </summary>
        public async Task<bool> CommitAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex) when (ex.InnerException is not null)
            {
                throw new Exception($"Erro ao salvar mudanças: {ex.InnerException.Message}", ex);
            }
        }

        /// <summary>
        /// Reverte as mudanças feitas no contexto.
        /// </summary>
        public void Rollback()
        {
            foreach (var entry in _context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
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
                new BaseClienteRepository<T>(_context) as object
                ?? throw new InvalidOperationException($"Falha ao criar repositório para {typeof(T).Name}"));
        }

        public IGerenericRepository<T> GetGenericRepository<T>() where T : class
        {
            return (IGerenericRepository<T>)_repositories.GetOrAdd(typeof(T), _ =>
                new GenericRepository<T>(_context) as object
                ?? throw new InvalidOperationException($"Falha ao criar repositório genérico para {typeof(T).Name}"));
        }

        /// <summary>
        /// Repositório para Filiais.
        /// </summary>
        public IFilialRepository<FilialEntity, ClaimsPrincipal> FilialRepository
            => _filialRepository ??= new FilialRepository(_context);

        /// <summary>
        /// Repositório para Pessoas Clientes.
        /// </summary>
        public IPessoaClienteRepository<PessoaClienteEntity> PessoaClienteRepository
            => _pessoaClienteRepository ??= new PessoaClienteRepository(_context);

        /// <summary>
        /// Repositório para Pessoas Clientes Vinculadas.
        /// </summary>
        public IPessoaClienteVinculadaRepository PessoaClienteVinculadaRepository
            => _pessoaClienteVinculadaRepository ??= new PessoaClienteVinculadaRepository(_context);

        /// <summary>
        /// Repositório para Usuários PDV.
        /// </summary>
        public IUsuarioPdvRepository UsuarioPdvRepository
            => _usuarioPdvRepository ??= new UsuarioPdvRepository(_context);

        public IPontoVendaRepository<PontoVendaEntity> PontoVendaRepository
        => _pontoVendaRepository ??= new PontoVendaRepository(_context);

        public IBaseClienteRepository<PontoVendaEntity> PontoVendaRepositoryBase
        => _pontoVendaRepositoryBase ??= new BaseClienteRepository<PontoVendaEntity>(_context);


        //NOVOS REPOSITORIOS
        public IBaseClienteRepository_REFACTOR<ProdutoEntity> ProdutoRepository
        {
            get
            {
                if (_produtoRepository == null)
                    _produtoRepository = new BaseClienteRepository_REFACTOR<ProdutoEntity>(_context);

                return _produtoRepository;
            }
        }
    }
}
