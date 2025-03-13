using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Application.Dto.PessoaCliente;
using EasyLoginBase.Application.Services.Intefaces;
using EasyLoginBase.Application.Services.Intefaces.PessoaCliente;
using EasyLoginBase.Services.Services.Email;
using EasyLoginBase.Services.Services.Filial;
using EasyLoginBase.Services.Services.PessoaCliente;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyLoginBase.CrossCutting.DependencyInjection.Extensions;

public static class RegistroServicos
{
    public static void ConfigurarServicos(this IServiceCollection serviceCollection)
    {        
        serviceCollection.AddTransient<IEmailService, EmailService>();
        serviceCollection.AddTransient<IFilialServices<FilialDto>, FilialServices>();
        serviceCollection.AddTransient<IPessoaClienteServices<PessoaClienteDto>, PessoaClienteServices>();
    }
}
