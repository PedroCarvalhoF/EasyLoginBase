using EasyLoginBase.Domain.Entities.Produto;
using EasyLoginBase.Domain.Interfaces.Produto;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.InfrastructureData.Repository.Produto;

public class ProdutoRepository : BaseClienteRepository<ProdutoEntity>, IProdutoRepository
{
    private readonly DbSet<ProdutoEntity> _dataset;
    public ProdutoRepository(MyContext context) : base(context)
    {
        _dataset = context.Set<ProdutoEntity>();
    }
    public async Task<bool> NomeProdutoUso(string nomeProduto)
    {
        return await _dataset.AnyAsync(c => c.NomeProduto!.Equals(nomeProduto));
    }
}

