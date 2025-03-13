using EasyLoginBase.Domain.Entities.PessoaCliente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLoginBase.InfrastructureData.Mapping;

public class PessoaClienteMap : IEntityTypeConfiguration<PessoaClienteEntity>
{
    public void Configure(EntityTypeBuilder<PessoaClienteEntity> builder)
    {
        builder.ToTable("PessoaClientes"); // Define o nome da tabela

        builder.HasKey(p => p.Id); // Define a chave primária

        builder
            .Property(p => p.Id)
            .ValueGeneratedNever(); // Define que o ID não será gerado automaticamente pelo anco

        builder
            .Property(p => p.NomeFantasia)
            .HasMaxLength(150)
            .IsRequired();

        builder
            .Property(p => p.DataAbertura)
            .IsRequired(); // Obrigatório

        builder
            .Property(p => p.DataVencimentoUso)
            .IsRequired(); // Obrigatório

        builder
            .Property(p => p.UsuarioEntityClienteId)
            .IsRequired(); // Define que o campo é obrigatório

        builder
            .HasOne(p => p.UsuarioEntityCliente) // Relacionamento com UserEntity
            .WithMany(u => u.PessoasClientes)
            .HasForeignKey(p => p.UsuarioEntityClienteId) // Usa a propriedade manual como chave estrangeira
            .OnDelete(DeleteBehavior.Restrict); // Evita deleção em cascata
    }
}
