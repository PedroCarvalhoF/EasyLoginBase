using EasyLoginBase.Domain.Entities.Produto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLoginBase.InfrastructureData.Mapping;

public class CategoriaProdutoMap : BaseClienteEntityMap<CategoriaProdutoEntity>, IEntityTypeConfiguration<CategoriaProdutoEntity>
{
    public void Configure(EntityTypeBuilder<CategoriaProdutoEntity> builder)
    {
        // Chama a configuração base
        ConfigureBaseProperties(builder, "CategoriasProdutos");

        // Propriedade NomeCategoria
        builder
            .Property(x => x.NomeCategoria)
            .IsRequired()
            .HasMaxLength(100); // Limite de tamanho para o nome da categoria             
    }
}