namespace EasyLoginBase.Application.Dto.Produto.Estoque.Movimento;

public class MovimentoEstoqueDto
{
    public Guid ProdutoId { get; set; }
    public string? NomeProduto { get; set; }
    public Guid FilialId { get; set; }
    public string? NomeFilial { get; set; }
    public decimal Quantidade { get; set; }
    public string? Tipo { get; set; }
    public string? Observacao { get; set; }
    public DateTime DataMovimentacao { get; set; }
    public Guid UsuarioRegistroId { get; set; }
    public string? NomeUsuarioRegistro { get; set; }

}
