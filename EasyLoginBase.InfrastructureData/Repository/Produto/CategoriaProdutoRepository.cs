using EasyLoginBase.Domain.Entities.Produto;
using EasyLoginBase.Domain.Interfaces.Produto;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.InfrastructureData.Repository.Produto;

public class CategoriaProdutoRepository : BaseClienteRepository<CategoriaProdutoEntity>, ICategoriaProdutoRepository<CategoriaProdutoEntity>
{
    private readonly DbSet<CategoriaProdutoEntity> _dataset;
    public CategoriaProdutoRepository(MyContext context) : base(context)
    {
        _dataset = context.Set<CategoriaProdutoEntity>();
    }

    public async Task<bool> NomeCategoriaProdutoUso(string nomeCategoria)
    {
        return await _dataset.AnyAsync(c => c.NomeCategoria!.Equals(nomeCategoria));
    }
}

