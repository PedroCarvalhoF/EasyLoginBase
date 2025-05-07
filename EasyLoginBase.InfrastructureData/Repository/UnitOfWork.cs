using EasyLoginBase.Domain.Entities;
using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Entities.PDV;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Entities.Produto;
using EasyLoginBase.Domain.Entities.Produto.Estoque;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Domain.Interfaces.Cliente;
using EasyLoginBase.Domain.Interfaces.Filial;
using EasyLoginBase.Domain.Interfaces.PDV;
using EasyLoginBase.Domain.Interfaces.Produto.Estoque;
using EasyLoginBase.Domain.Interfaces.Produto.MovimentacaoEstoque;
using EasyLoginBase.Domain.Interfaces.UsuarioClienteVinculo;
using EasyLoginBase.InfrastructureData.Context;
using EasyLoginBase.InfrastructureData.Implementacao;
using EasyLoginBase.InfrastructureData.Repository.Cliente;
using EasyLoginBase.InfrastructureData.Repository.Filial;
using EasyLoginBase.InfrastructureData.Repository.PDV;
using EasyLoginBase.InfrastructureData.Repository.UsuarioClienteVinculo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace EasyLoginBase.InfrastructureData.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Metodos Unit Of Work
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
        #endregion

        private readonly MyContext? _context;
        private readonly ConcurrentDictionary<Type, object>? _repositories = new();
        public UnitOfWork(MyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //Cliente
        private IGerenericRepository<PessoaClienteEntity>? _clienteRepostory;
        private IClienteRepository<PessoaClienteEntity>? _clienteImplementacao;

        //Usuario Vinculo Cliente
        private IGerenericRepository<PessoaClienteVinculadaEntity>? _pessoaVinculadaRepository;
        private IUsuarioClienteVinculoRepository<PessoaClienteVinculadaEntity>? _usuarioClienteVinculoImplementacao;

        public IGerenericRepository<PessoaClienteEntity> ClienteRepostory
        {
            get
            {
                if (_clienteRepostory == null)
                    _clienteRepostory = new GenericRepository<PessoaClienteEntity>(_context ?? throw new Exception("Não foi possível instanciar contexto"));

                return _clienteRepostory;
            }
        }
        public IClienteRepository<PessoaClienteEntity> ClienteImplementacao
        {
            get
            {
                if (_clienteImplementacao == null)
                    _clienteImplementacao = new ClienteImplementacao(_context);
                return _clienteImplementacao;
            }
        }

        public IGerenericRepository<PessoaClienteVinculadaEntity> UsuarioClienteVinculoRepostory
        {
            get
            {
                if (_pessoaVinculadaRepository == null)
                    _pessoaVinculadaRepository = new GenericRepository<PessoaClienteVinculadaEntity>(_context);
                return _pessoaVinculadaRepository;
            }
        }

        public IUsuarioClienteVinculoRepository<PessoaClienteVinculadaEntity> UsuarioClienteVinculoImplementacao
        {
            get
            {
                if (_usuarioClienteVinculoImplementacao == null)
                    _usuarioClienteVinculoImplementacao = new UsuarioClienteVinculoImplementacao(_context);
                return _usuarioClienteVinculoImplementacao;
            }
        }














        //REFATORAR DAQUI PRA BAIXO




        private IFilialRepository<FilialEntity, ClaimsPrincipal>? _filialRepository;
        private IUsuarioPdvRepository? _usuarioPdvRepository;

        private IPontoVendaRepository<PontoVendaEntity>? _pontoVendaRepository;
        private IBaseClienteRepository<PontoVendaEntity>? _pontoVendaRepositoryBase;

        //NOVO REPOSITORIO

        private IBaseClienteRepository_REFACTOR<ProdutoEntity>? _produtoRepository;

        //ESTOQUE
        private IBaseClienteRepository_REFACTOR<EstoqueProdutoEntity>? _produtoEstoqueRepository;
        private IEstoqueProdutoRepository<EstoqueProdutoEntity>? _estoqueProdutoImplementacao;

        //MOVIMENTACAO ESTOQUE
        private IBaseClienteRepository_REFACTOR<MovimentacaoEstoqueProdutoEntity>? _movimentacaoEstoqueProdutoRepository;
        private IMovimentacaoEstoqueProdutoRepository<MovimentacaoEstoqueProdutoEntity, FiltroBase>? _movimentacaoEstoqueProdutoImplementacao;






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

        public IFilialRepository<FilialEntity, ClaimsPrincipal> FilialRepository
            => _filialRepository ??= new FilialRepository(_context);
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
        public IBaseClienteRepository_REFACTOR<EstoqueProdutoEntity> EstoqueProdutoRepository
        {
            get
            {
                if (_produtoEstoqueRepository == null)
                    _produtoEstoqueRepository = new BaseClienteRepository_REFACTOR<EstoqueProdutoEntity>(_context);
                return _produtoEstoqueRepository;
            }
        }
        public IEstoqueProdutoRepository<EstoqueProdutoEntity> EstoqueProdutoImplementacao
        {
            get
            {
                if (_estoqueProdutoImplementacao == null)
                    _estoqueProdutoImplementacao = new ProdutoEstoqueImplementacao(_context);

                return _estoqueProdutoImplementacao;
            }
        }

        public IBaseClienteRepository_REFACTOR<MovimentacaoEstoqueProdutoEntity> MovimentacaoEstoqueProdutoRepository
        {
            get
            {
                if (_movimentacaoEstoqueProdutoRepository == null)
                    _movimentacaoEstoqueProdutoRepository = new BaseClienteRepository_REFACTOR<MovimentacaoEstoqueProdutoEntity>(_context);

                return _movimentacaoEstoqueProdutoRepository;
            }
        }

        public IMovimentacaoEstoqueProdutoRepository<MovimentacaoEstoqueProdutoEntity, FiltroBase> MovimentacaoEstoqueProdutoImplementacao
        {
            get
            {
                if (_movimentacaoEstoqueProdutoImplementacao == null)
                    _movimentacaoEstoqueProdutoImplementacao = new MovimentacaoEstoqueProdutoImplementacao(_context);
                return _movimentacaoEstoqueProdutoImplementacao;
            }
        }


    }
}
