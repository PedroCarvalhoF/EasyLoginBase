using EasyLoginBase.Domain.Entities.Produto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLoginBase.InfrastructureData.Mapping;
public class UnidadeMedidaProdutoMap : IEntityTypeConfiguration<UnidadeMedidaProdutoEntity>
{
    public void Configure(EntityTypeBuilder<UnidadeMedidaProdutoEntity> builder)
    {
        builder.ToTable("UnidadeMedidaProduto");
        builder.HasKey(x => x.Id);

        // Propriedade NomeUnidadeMedida
        builder
            .Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(50); // Limite de tamanho para o nome da unidade de medida

        // Propriedade SiglaUnidadeMedida
        builder
            .Property(x => x.Sigla)
            .IsRequired()
            .HasMaxLength(10); // Limite de tamanho para a sigla da unidade de medida

        // Propriedade TipoUnidadeMedida
        builder
            .Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(200); // Limite de tamanho para o tipo da unidade de medida
    }
}