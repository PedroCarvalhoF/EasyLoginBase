using EasyLoginBase.Application.Dto.PessoaCliente;
using EasyLoginBase.Application.Services.Intefaces;
using EasyLoginBase.Application.Services.Intefaces.PessoaCliente;
using EasyLoginBase.Application.Services.Intefaces.PessoaClienteVinculada;
using EasyLoginBase.Application.Services.Intefaces.Produto;
using EasyLoginBase.Services.Services.Email;
using EasyLoginBase.Services.Services.Filial;
using EasyLoginBase.Services.Services.PessoaCliente;
using EasyLoginBase.Services.Services.PessoaClienteVinculada;
using EasyLoginBase.Services.Services.Produto;
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
    }
}
