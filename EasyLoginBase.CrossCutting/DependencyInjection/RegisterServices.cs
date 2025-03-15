using EasyLoginBase.CrossCutting.DependencyInjection.Extensions;
using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Domain.Interfaces.Filial;
using EasyLoginBase.Domain.Interfaces.PessoaCliente;
using EasyLoginBase.InfrastructureData.Repository;
using EasyLoginBase.InfrastructureData.Repository.Filial;
using EasyLoginBase.InfrastructureData.Repository.PessoaCliente;
using EasyLoginBase.Services.Tools.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyLoginBase.CrossCutting.DependencyInjection;

public static class RegisterServices
{
    public static void ConfigureRepositories(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        IdentityConfiguration.Configurar(serviceCollection, configuration);

        serviceCollection.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        serviceCollection.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));

        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        // Alterado para Scoped para melhor uso do DbContext
        serviceCollection.AddScoped<IFilialRepository, FilialRepository>();
        serviceCollection.AddScoped<IPessoaClienteRepository<PessoaClienteEntity>, PessoaClienteRepository>();
        serviceCollection.AddScoped<IPessoaClienteVinculadaRepository, PessoaClienteVinculadaRepository>();

        var myhandlers = AppDomain.CurrentDomain.Load("EasyLoginBase.Services");
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myhandlers));
    }
}
