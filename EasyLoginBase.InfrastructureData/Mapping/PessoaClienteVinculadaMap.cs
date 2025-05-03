using EasyLoginBase.Domain.Entities.PessoaCliente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLoginBase.InfrastructureData.Mapping;

public class PessoaClienteVinculadaMap : IEntityTypeConfiguration<PessoaClienteVinculadaEntity>
{
    public void Configure(EntityTypeBuilder<PessoaClienteVinculadaEntity> builder)
    {// Nome da tabela no banco de dados
        builder.ToTable("PessoasClientesVinculadas");

        // Definição da chave primária composta
        builder.HasKey(pcv => new { pcv.PessoaClienteEntityId, pcv.UsuarioVinculadoId });

        // Relacionamento com PessoaClienteEntity (1 Cliente pode ter vários usuários vinculados)
        builder.HasOne(pcv => pcv.PessoaClienteEntity)
            .WithMany(pc => pc.UsuariosVinculados)
            .HasForeignKey(pcv => pcv.PessoaClienteEntityId)
            .OnDelete(DeleteBehavior.Cascade); // Remove vinculações se o cliente for deletado

        // Relacionamento com UserEntity (1 Usuário pode estar vinculado a vários clientes)
        builder.HasOne(pcv => pcv.UsuarioVinculado)
            .WithMany(u => u.ClientesVinculados)
            .HasForeignKey(pcv => pcv.UsuarioVinculadoId)
            .OnDelete(DeleteBehavior.Restrict); // Evita deletar um usuário se houver vínculos

        // Configuração da coluna AcessoPermitido
        builder.Property(pcv => pcv.AcessoPermitido)
            .IsRequired()
            .HasDefaultValue(false);
    }
}
