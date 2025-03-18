using EasyLoginBase.Domain.Entities.Preco.Produto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLoginBase.InfrastructureData.Mapping;

public class PrecoProdutoMap : BaseClienteEntityMap<PrecoProdutoEntity>, IEntityTypeConfiguration<PrecoProdutoEntity>
{
    public void Configure(EntityTypeBuilder<PrecoProdutoEntity> builder)
    {
        // Configurações herdadas da base
        ConfigureBaseProperties(builder, "PrecosProdutos");

        // Configuração do campo PrecoProduto
        builder.Property(p => p.PrecoProduto)
               .HasColumnType("decimal(18,2)") // Define precisão para valores monetários
               .IsRequired();

        // Configuração do campo TipoPrecoProdutoEnum
        builder.Property(p => p.TipoPrecoProdutoEnum)
               .HasConversion<int>() // Armazena como inteiro no banco
               .IsRequired();

        // Índice para otimizar buscas
        builder.HasIndex(p => new { p.ProdutoEntityId, p.FilialEntityId, p.CategoriaPrecoProdutoEntityId })
               .HasDatabaseName("IX_Produto_Filial_Categoria");

        // Definição das relações
        builder.HasOne(p => p.ProdutoEntity)
               .WithMany(prod => prod.PrecosEntities)
               .HasForeignKey(p => p.ProdutoEntityId)
               .OnDelete(DeleteBehavior.Restrict); // Evita deleção em cascata

        builder.HasOne(p => p.FilialEntity)
               .WithMany(filial => filial.PrecoProdutoEntities)
               .HasForeignKey(p => p.FilialEntityId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.CategoriaPrecoProdutoEntity)
               .WithMany(cat_preco=>cat_preco.PrecoProdutoEntities)
               .HasForeignKey(p => p.CategoriaPrecoProdutoEntityId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
