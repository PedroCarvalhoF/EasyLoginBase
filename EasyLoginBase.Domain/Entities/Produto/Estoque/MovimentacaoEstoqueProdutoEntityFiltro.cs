namespace EasyLoginBase.Domain.Entities.Produto.Estoque;
public class MovimentacaoEstoqueProdutoEntityFiltro
{
    public Guid? IdMovimento { get; set; }
    public Guid? ProdutoId { get; set; }
    public Guid? FilialId { get; set; }
    public string? Tipo { get; set; }
    public DateTime? DataMovimentacaoInicial { get; set; }
    public DateTime? DataMovimentacaoFinal { get; set; }
    public Guid? UsuarioRegistroId { get; set; }
}
