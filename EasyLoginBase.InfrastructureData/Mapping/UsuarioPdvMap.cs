using EasyLoginBase.Domain.Entities.PDV;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLoginBase.InfrastructureData.Mapping;

public class UsuarioPdvMap : BaseClienteEntityMap<UsuarioPdvEntity>, IEntityTypeConfiguration<UsuarioPdvEntity>
{
    public void Configure(EntityTypeBuilder<UsuarioPdvEntity> builder)
    {
        ConfigureBaseProperties(builder, "UsuariosPdvs");

        builder.Property(x => x.AcessoCaixa)
            .IsRequired();

        builder.Property(x => x.UsuarioCaixaPdvEntityId)
            .IsRequired();

        builder
            .HasOne(x => x.UserCaixaPdvEntity)
            .WithOne(y => y.UsuarioPdv).HasForeignKey<UsuarioPdvEntity>(x => x.UsuarioCaixaPdvEntityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
