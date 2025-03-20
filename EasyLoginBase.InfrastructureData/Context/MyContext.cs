using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Entities.PDV;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Entities.Preco.Produto;
using EasyLoginBase.Domain.Entities.Preco.Produto.CategoriaPreco;
using EasyLoginBase.Domain.Entities.Produto;
using EasyLoginBase.Domain.Entities.User;
using EasyLoginBase.InfrastructureData.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyLoginBase.InfrastructureData.Context;
public class MyContext : IdentityDbContext
    <UserEntity, RoleEntity, Guid, IdentityUserClaim<Guid>, UserRoleEntity, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public DbSet<FilialEntity> Filiais { get; set; }
    public DbSet<PessoaClienteEntity> PessoaClientes { get; set; }
    public DbSet<PessoaClienteVinculadaEntity> PessoasClientesVinculadas { get; set; }
    public DbSet<CategoriaProdutoEntity> CategoriasProdutos { get; set; }
    public DbSet<ProdutoEntity> Produtos { get; set; }
    public DbSet<CategoriaPrecoProdutoEntity> CategoriasPrecosProdutos { get; set; }
    public DbSet<PrecoProdutoEntity> PrecosProdutos { get; set; }
    public DbSet<UsuarioPdvEntity> UsuariosPdvs { get; set; }
    public MyContext(DbContextOptions<MyContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FilialMap());
        modelBuilder.ApplyConfiguration(new PessoaClienteMap());
        modelBuilder.ApplyConfiguration(new PessoaClienteVinculadaMap());
        modelBuilder.ApplyConfiguration(new CategoriaProdutoMap());
        modelBuilder.ApplyConfiguration(new ProdutoMap());
        modelBuilder.ApplyConfiguration(new CategoriaPrecoProdutoMap());
        modelBuilder.ApplyConfiguration(new PrecoProdutoMap());
        modelBuilder.ApplyConfiguration(new UsuarioPdvMap());

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRoleEntity>(userRole =>
        {
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });
    }
}
