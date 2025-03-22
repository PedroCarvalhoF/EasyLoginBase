using EasyLoginBase.Domain.Entities.PDV;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLoginBase.InfrastructureData.Mapping;

public class PontoVendaMap : BaseClienteEntityMap<PontoVendaEntity>, IEntityTypeConfiguration<PontoVendaEntity>
{
    public void Configure(EntityTypeBuilder<PontoVendaEntity> builder)
    {
        // Define o nome da tabela
        ConfigureBaseProperties(builder, "PontosVendas");       

        // Índices para otimização de busca
        builder.HasIndex(p => p.FilialPdvId);
        builder.HasIndex(p => p.UsuarioPdvId);

        // Relacionamento com FilialEntity (Muitos para Um)
        builder
            .HasOne(pdv => pdv.FilialPdv)
            .WithMany(filial => filial.PontoVendaEntities) // Se houver coleção, altere para `WithMany(f => f.PontosVendas)`
            .HasForeignKey(p => p.FilialPdvId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento com UsuarioPdvEntity (Muitos para Um)
        builder
            .HasOne(pdv => pdv.UsuarioPdv)
            .WithMany(user_pdv => user_pdv.PontoVendaEntities) // Se houver coleção, altere para `WithMany(u => u.PontosVendas)`
            .HasForeignKey(p => p.UsuarioPdvId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuração de propriedades
        builder.Property(p => p.Aberto)
            .IsRequired();

        builder.Property(p => p.Cancelado)
            .IsRequired();
    }
}
