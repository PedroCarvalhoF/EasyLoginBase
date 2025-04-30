using EasyLoginBase.Domain.Entities.Produto.Estoque;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLoginBase.InfrastructureData.Mapping;
public class MovimentacaoEstoqueProdutoMap : BaseClienteEntityMap<MovimentacaoEstoqueProdutoEntity>, IEntityTypeConfiguration<MovimentacaoEstoqueProdutoEntity>
{
    public void Configure(EntityTypeBuilder<MovimentacaoEstoqueProdutoEntity> builder)
    {
        ConfigureBaseProperties(builder, "EstoqueProdutosMovimentacao");

        builder.Property(est => est.ProdutoId).IsRequired();
        builder.Property(est => est.FilialId).IsRequired();
        builder.Property(est => est.Quantidade)
               .IsRequired()
               .HasPrecision(18, 3); // Suporte para quantidades decimais com precisão

        builder.Property(est => est.Tipo)
               .IsRequired();

        builder.Property(est => est.Observacao)
               .HasMaxLength(500); // Defina o tamanho máximo desejado para a observação

        builder.Property(est => est.DataMovimentacao)
               .IsRequired();


        // Relacionamento com Produto
        builder.HasOne(est => est.Produto)
               .WithMany(p => p.MovimentacaoEstoqueProdutoEntities) // Adicione `public ICollection<EstoqueProdutoEntity> Estoques` em ProdutoEntity
               .HasForeignKey(est => est.ProdutoId)
               .OnDelete(DeleteBehavior.Restrict); // ou Cascade se preferir

        // Relacionamento com Filial
        builder.HasOne(est => est.Filial)
               .WithMany(f => f.MovimentacaoEstoqueProdutoEntities) // Adicione `public ICollection<EstoqueProdutoEntity> Estoques` em FilialEntity
               .HasForeignKey(est => est.FilialId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
