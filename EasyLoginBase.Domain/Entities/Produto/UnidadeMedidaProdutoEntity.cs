namespace EasyLoginBase.Domain.Entities.Produto;

public class UnidadeMedidaProdutoEntity
{
    public Guid Id { get; private set; }
    public string? Nome { get; private set; }
    public string? Sigla { get; private set; }
    public string? Descricao { get; private set; }
    public ICollection<ProdutoEntity>? ProdutosEntities { get; set; }
    UnidadeMedidaProdutoEntity(Guid id, string nome, string sigla, string descricao)
    {
        Id = id;
        Nome = nome;
        Sigla = sigla;
        Descricao = descricao;
    }

    public static UnidadeMedidaProdutoEntity CriarUnidadeMedidaProdutoEntity(string nome, string sigla, string descricao)
    => new UnidadeMedidaProdutoEntity(Guid.NewGuid(), nome, sigla, descricao);

    public static UnidadeMedidaProdutoEntity AlterarUnidadeProdutoMedidaEntity(Guid id, string nome, string sigla, string descricao)
    => new UnidadeMedidaProdutoEntity(id, nome, sigla, descricao);
}
