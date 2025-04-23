namespace EasyLoginBase.Application.Dto.Produto.UnidadeMedidaProduto;

public class UnidadeMedidaProdutoDtoCreate
{
    public string Nome { get; set; }
    public string Sigla { get; set; }
    public string Descricao { get; set; }
    public UnidadeMedidaProdutoDtoCreate(string nome, string sigla, string descricao)
    {
        Nome = nome;
        Sigla = sigla;
        Descricao = descricao;
    }
}
