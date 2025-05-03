namespace EasyLoginBase.Application.Dto.Produto.Produto
{
    public class ProdutoDto
    {
        public Guid Id { get; set; }
        public string? NomeProduto { get; set; }
        public string? CodigoProduto { get; set; }
        public Guid CategoriaProdutoEntityId { get; set; }
        public string? CategoriaProduto { get; set; }
        public Guid UnidadeMedidaProdutoEntityId { get; set; }
        public string? UnidadeMedidaProduto { get; set; }
    }
}
