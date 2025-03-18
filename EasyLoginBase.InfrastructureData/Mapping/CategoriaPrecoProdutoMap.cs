using EasyLoginBase.Domain.Entities.Preco.Produto.CategoriaPreco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLoginBase.InfrastructureData.Mapping;

public class CategoriaPrecoProdutoMap : BaseClienteEntityMap<CategoriaPrecoProdutoEntity>, IEntityTypeConfiguration<CategoriaPrecoProdutoEntity>
{
    public void Configure(EntityTypeBuilder<CategoriaPrecoProdutoEntity> builder)
    {
        // Chama a configuração base
        ConfigureBaseProperties(builder, "CategoriasPrecosProdutos");

        // Propriedade CategoriaPreco
        builder
            .Property(x => x.CategoriaPreco)
            .IsRequired()
            .HasMaxLength(100); // Limite de tamanho para o nome da categoria       
        builder
            .HasIndex(builder => builder.CategoriaPreco)
            .IsUnique(); // Exemplo de índice único para CategoriaPreco
    }
}
