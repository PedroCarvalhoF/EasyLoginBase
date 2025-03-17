using EasyLoginBase.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLoginBase.InfrastructureData.Mapping;

public class BaseClienteEntityMap<TEntity> where TEntity : BaseClienteEntity
{
    public virtual void ConfigureBaseProperties(EntityTypeBuilder<TEntity> builder,string nomeTabela)
    {
        // Configuração da tabela
        builder.ToTable(nomeTabela);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ClienteId).IsRequired();
        builder.Property(x => x.UsuarioRegistroId).IsRequired();
        builder.Property(x => x.Habilitado).IsRequired();
        builder.Property(x => x.CreateAt).IsRequired();
        builder.Property(x => x.UpdateAt);
    }
}
