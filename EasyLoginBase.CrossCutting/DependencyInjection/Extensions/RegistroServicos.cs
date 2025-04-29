using EasyLoginBase.Application.Dto.PessoaCliente;
using EasyLoginBase.Application.Dto.Produto.Estoque.Estoque;
using EasyLoginBase.Application.Services.Intefaces.Filial;
using EasyLoginBase.Application.Services.Intefaces.PDV;
using EasyLoginBase.Application.Services.Intefaces.PessoaCliente;
using EasyLoginBase.Application.Services.Intefaces.PessoaClienteVinculada;
using EasyLoginBase.Application.Services.Intefaces.Preco.Produto;
using EasyLoginBase.Application.Services.Intefaces.Preco.Produto.CategoriaPreco;
using EasyLoginBase.Application.Services.Intefaces.Produto;
using EasyLoginBase.Services.Services.Email;
using EasyLoginBase.Services.Services.Filial;
using EasyLoginBase.Services.Services.PDV;
using EasyLoginBase.Services.Services.PessoaCliente;
using EasyLoginBase.Services.Services.PessoaClienteVinculada;
using EasyLoginBase.Services.Services.Preco.Produto;
using EasyLoginBase.Services.Services.Preco.Produto.CategoriaPreco;
using EasyLoginBase.Services.Services.Produto;
using EasyLoginBase.Services.Services.Produto.Estoque.Estoque;
using Microsoft.Extensions.DependencyInjection;

namespace EasyLoginBase.CrossCutting.DependencyInjection.Extensions;
public static class RegistroServicos
{
    public static void ConfigurarServicos(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IEmailService, EmailService>();
        serviceCollection.AddTransient<IFilialServices, FilialServices>();
        serviceCollection.AddTransient<IPessoaClienteServices<PessoaClienteDto>, PessoaClienteServices>();
        serviceCollection.AddTransient<IPessoaClienteVinculadaServices, PessoaClienteVinculadaServices>();
        serviceCollection.AddTransient<ICategoriaProdutoServices, CategoriaProdutoServices>();
        serviceCollection.AddTransient<IProdutoServices, ProdutoServices>();
        serviceCollection.AddTransient<ICategoriaPrecoProdutoServices, CategoriaPrecoProdutoServices>();
        serviceCollection.AddTransient<IPrecoProdutoServices, PrecoProdutoServices>();
        serviceCollection.AddTransient<IUsuarioPdvServices, UsuarioPdvServices>();
        serviceCollection.AddTransient<IPontoVendaServices, PontoVendaServices>();
        serviceCollection.AddTransient<IUnidadeMedidaProdutoServices, UnidadeMedidaProdutoServices>();
        serviceCollection.AddTransient<IEstoqueProdutoServices<EstoqueProdutoDto>, EstoqueProdutoServices>();
    }
}
