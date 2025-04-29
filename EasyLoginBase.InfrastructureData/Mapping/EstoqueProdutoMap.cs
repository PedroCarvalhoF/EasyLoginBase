using EasyLoginBase.Domain.Entities.Produto.Estoque;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLoginBase.InfrastructureData.Mapping;

public class EstoqueProdutoMap : BaseClienteEntityMap<EstoqueProdutoEntity>, IEntityTypeConfiguration<EstoqueProdutoEntity>
{
    public void Configure(EntityTypeBuilder<EstoqueProdutoEntity> builder)
    {
        ConfigureBaseProperties(builder, "EstoqueProdutos");

        builder.Property(est => est.ProdutoId).IsRequired();
        builder.Property(est => est.FilialId).IsRequired();
        builder.Property(est => est.Quantidade)
               .IsRequired()
               .HasPrecision(18, 3); // Suporte para quantidades decimais com precisão

        // Relacionamento com Produto
        builder.HasOne(est => est.Produto)
               .WithMany(p => p.EstoqueProdutoEntities) // Adicione `public ICollection<EstoqueProdutoEntity> Estoques` em ProdutoEntity
               .HasForeignKey(est => est.ProdutoId)
               .OnDelete(DeleteBehavior.Restrict); // ou Cascade se preferir

        // Relacionamento com Filial
        builder.HasOne(est => est.Filial)
               .WithMany(f => f.EstoqueProdutoEntities) // Adicione `public ICollection<EstoqueProdutoEntity> Estoques` em FilialEntity
               .HasForeignKey(est => est.FilialId)
               .OnDelete(DeleteBehavior.Restrict);
    }

}
