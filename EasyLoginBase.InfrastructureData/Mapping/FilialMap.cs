using EasyLoginBase.Domain.Entities.Filial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLoginBase.InfrastructureData.Mapping;
public class FilialMap : IEntityTypeConfiguration<FilialEntity>
{
    public void Configure(EntityTypeBuilder<FilialEntity> builder)
    {
        builder.ToTable("Filiais");
        builder.HasKey(f => f.IdFilial);

        builder.Property(f => f.NomeFilial).IsRequired().HasMaxLength(100).HasColumnType("varchar(100)");
        builder.HasIndex(f => f.NomeFilial).IsUnique();
       
    }
}
