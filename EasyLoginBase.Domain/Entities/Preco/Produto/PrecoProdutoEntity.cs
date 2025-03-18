using EasyLoginBase.Domain.Entities.Base;
using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Entities.Produto;
using EasyLoginBase.Domain.Enuns.Preco.Produto;

namespace EasyLoginBase.Domain.Entities.Preco.Produto;

public class PrecoProdutoEntity : BaseClienteEntity
{
    // um preco contem um produto
    public Guid ProdutoEntityId { get; private set; }
    public ProdutoEntity? ProdutoEntity { get; private set; }

    // um preco contem uma filial
    public Guid FilialEntityId { get; private set; }
    public FilialEntity? FilialEntity { get; private set; }

    public decimal PrecoProduto { get; private set; }
    public PrecoProdutoEnum TipoPrecoProdutoEnum { get; private set; }
}
