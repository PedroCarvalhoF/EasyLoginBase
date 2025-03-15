using EasyLoginBase.Domain.Entities.Filial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLoginBase.InfrastructureData.Mapping;
public class FilialMap : IEntityTypeConfiguration<FilialEntity>
{
    public void Configure(EntityTypeBuilder<FilialEntity> builder)
    {
        builder.ToTable("Filiais"); // Nome da tabela

        builder.HasKey(f => f.Id);

        builder.Property(f => f.NomeFilial)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(f => f.DataCriacaoFilial)
             .HasColumnType("datetime");

        builder.Property(f => f.Habilitada)
             .IsRequired();

        builder.HasOne(f => f.PessoaCliente)
            .WithMany(pf => pf.Filiais)
            .HasForeignKey(f => f.PessoaClienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
