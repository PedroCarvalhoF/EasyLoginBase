using EasyLoginBase.Domain.Entities.Filial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLoginBase.InfrastructureData.Mapping;
public class FilialMap : BaseClienteEntityMap<FilialEntity>, IEntityTypeConfiguration<FilialEntity>
{
    public void Configure(EntityTypeBuilder<FilialEntity> builder)
    {
        //configuração das propriedades base
        ConfigureBaseProperties(builder, "Filiais");

        //pessoa cliente id
        builder
            .Property(f => f.PessoaClienteId)
            .IsRequired();

        //nome filial
        builder
            .Property(f => f.NomeFilial)
            .IsRequired()
            .HasMaxLength(100);
        builder
            .HasIndex(f => f.NomeFilial)
            .IsUnique();

        //relacionamento com pessoa cliente
        builder
            .HasOne(f => f.PessoaCliente)
            .WithMany(pf => pf.Filiais)
            .HasForeignKey(f => f.PessoaClienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
