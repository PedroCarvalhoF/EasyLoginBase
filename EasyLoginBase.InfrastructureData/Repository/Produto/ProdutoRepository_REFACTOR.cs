using EasyLoginBase.Domain.Entities.Produto;
using EasyLoginBase.Domain.Interfaces.Produto;
using EasyLoginBase.InfrastructureData.Context;

namespace EasyLoginBase.InfrastructureData.Repository.Produto;
public class ProdutoRepository_REFACTOR : BaseClienteRepository_REFACTOR<ProdutoEntity>, IProdutoRepository_REFACTOR
{
    public ProdutoRepository_REFACTOR(MyContext context) : base(context)
    {
    }
}
