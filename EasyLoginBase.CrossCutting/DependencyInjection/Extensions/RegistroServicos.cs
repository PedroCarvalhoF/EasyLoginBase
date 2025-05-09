using EasyLoginBase.Application.Dto.Cliente;
using EasyLoginBase.Application.Dto.Produto.Estoque.Estoque;
using EasyLoginBase.Application.Dto.Produto.Estoque.Movimento;
using EasyLoginBase.Application.Dto.User.Role;
using EasyLoginBase.Application.Dto.UsuarioVinculadoCliente;
using EasyLoginBase.Application.Services.Intefaces.Cliente;
using EasyLoginBase.Application.Services.Intefaces.Filial;
using EasyLoginBase.Application.Services.Intefaces.PDV;
using EasyLoginBase.Application.Services.Intefaces.Preco.Produto;
using EasyLoginBase.Application.Services.Intefaces.Preco.Produto.CategoriaPreco;
using EasyLoginBase.Application.Services.Intefaces.Produto;
using EasyLoginBase.Application.Services.Intefaces.UsuarioClienteVinculo;
using EasyLoginBase.Services.Services.Cliente;
using EasyLoginBase.Services.Services.Email;
using EasyLoginBase.Services.Services.Filial;
using EasyLoginBase.Services.Services.PDV;
using EasyLoginBase.Services.Services.Preco.Produto;
using EasyLoginBase.Services.Services.Preco.Produto.CategoriaPreco;
using EasyLoginBase.Services.Services.Produto;
using EasyLoginBase.Services.Services.Produto.Estoque.Estoque;
using EasyLoginBase.Services.Services.Produto.Estoque.Movimento;
using EasyLoginBase.Services.Services.UsuarioVinculadoCliente;
using Microsoft.Extensions.DependencyInjection;

namespace EasyLoginBase.CrossCutting.DependencyInjection.Extensions;
public static class RegistroServicos
{
    public static void ConfigurarServicos(this IServiceCollection serviceCollection)
    {

        serviceCollection.AddTransient<IClienteServices<ClienteDto>, ClienteServices>();
        serviceCollection.AddTransient<IUsuarioClienteVinculoServices<UsuarioVinculadoClienteDto>, UsuarioClienteVinculoServices>();

        serviceCollection.AddTransient<IEmailService, EmailService>();
        serviceCollection.AddTransient<IFilialServices, FilialServices>();
        serviceCollection.AddTransient<ICategoriaProdutoServices, CategoriaProdutoServices>();
        serviceCollection.AddTransient<IProdutoServices, ProdutoServices>();
        serviceCollection.AddTransient<ICategoriaPrecoProdutoServices, CategoriaPrecoProdutoServices>();
        serviceCollection.AddTransient<IPrecoProdutoServices, PrecoProdutoServices>();
        serviceCollection.AddTransient<IUsuarioPdvServices, UsuarioPdvServices>();
        serviceCollection.AddTransient<IPontoVendaServices, PontoVendaServices>();
        serviceCollection.AddTransient<IUnidadeMedidaProdutoServices, UnidadeMedidaProdutoServices>();
        serviceCollection.AddTransient<IEstoqueProdutoServices<EstoqueProdutoDto>, EstoqueProdutoServices>();
        serviceCollection.AddTransient<IMovimentoEstoqueServices<MovimentoEstoqueDto>, MovimentoEstoqueServices>();

        serviceCollection.AddTransient<Services.Services.User.Roles.IUserRoleServices<Application.Dto.User.Role.RoleDto, RoleUserDto>, Services.Services.User.Roles.UserRoleServices>();
    }
}
