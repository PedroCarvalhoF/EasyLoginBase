namespace EasyLoginBase.Application.Dto.Produto.UnidadeMedidaProduto;
public class UnidadeMedidaProdutoDtoUpdate
{
    public UnidadeMedidaProdutoDtoUpdate(Guid id, string nome, string sigla, string descricao)
    {
        Id = id;
        Nome = nome;
        Sigla = sigla;
        Descricao = descricao;
    }

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Sigla { get; set; }
    public string Descricao { get; set; }

}
