﻿using EasyLoginBase.Domain.Entities.Produto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLoginBase.InfrastructureData.Mapping;

public class ProdutoMap : BaseClienteEntityMap<ProdutoEntity>, IEntityTypeConfiguration<ProdutoEntity>
{
    public void Configure(EntityTypeBuilder<ProdutoEntity> builder)
    {
        ConfigureBaseProperties(builder, "Produtos");

        builder
            .Property(p => p.NomeProduto)
            .IsRequired()
            .HasMaxLength(100);
        builder
            .HasIndex(p => p.NomeProduto);

        builder.Property(p => p.CodigoProduto)
            .HasMaxLength(50);

        builder
            .HasIndex(p => p.CodigoProduto)
            .IsUnique();

        builder.HasOne(p => p.CategoriaProdutoEntity)
            .WithMany(cat => cat.Produtos)
            .HasForeignKey(p => p.CategoriaProdutoEntityId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.UnidadeMedidaProdutoEntity)
            .WithMany(ump => ump.ProdutosEntities)
            .HasForeignKey(p => p.UnidadeMedidaProdutoEntityId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
