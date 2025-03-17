using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasyLoginBase.InfrastructureData.Repository
{
    public class BaseClienteRepository<T> : IBaseClienteRepository<T> where T : BaseClienteEntity
    {
        private readonly MyContext _context;
        private readonly DbSet<T> _dataset;
        public BaseClienteRepository(MyContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public void AtualizarAsync(T entidade)
        {
            try
            {
                _dataset.Update(entidade);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar entidade: {ex.Message}", ex);
            }
        }

        public async Task CadastrarAsync(T entidade)
        {
            try
            {
                await _dataset.AddAsync(entidade);

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar entidade: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<T>> ConsultarPorFiltroAsync(Expression<Func<T, bool>> filtro)
        {
            try
            {
                return await _dataset.Where(filtro).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar por filtro: {ex.Message}", ex);
            }
        }

        public async Task<T?> ConsultarPorIdAsync(Guid id)
        {
            try
            {
                return await _dataset.FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar por ID: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<T>> ConsultarTodosAsync(Guid clienteId)
        {
            try
            {
                return await _dataset.Where(cliente=>cliente.ClienteId== clienteId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar todos: {ex.Message}", ex);
            }
        }

        public async Task<bool> ExisteAsync(Guid id)
        {
            try
            {
                return await _dataset.AnyAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao verificar existência: {ex.Message}", ex);
            }
        }

        public async Task RemoverAsync(Guid id)
        {
            try
            {
                var entidade = await ConsultarPorIdAsync(id);
                if (entidade == null)
                    throw new KeyNotFoundException("Entidade não encontrada.");

                _dataset.Remove(entidade);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao remover entidade: {ex.Message}", ex);
            }
        }
    }
}
