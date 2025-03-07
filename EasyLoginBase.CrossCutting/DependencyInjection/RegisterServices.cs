using EasyLoginBase.CrossCutting.DependencyInjection.Extensions;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.InfrastructureData.Repository;
using EasyLoginBase.Services.Services.Email;
using EasyLoginBase.Services.Tools.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyLoginBase.CrossCutting.DependencyInjection;

public static class RegisterServices
{
    public static void ConfigureDependenciesRepository(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));

        IdentityConfiguration.Configurar(serviceCollection, configuration);

        var myhandlers = AppDomain.CurrentDomain.Load("EasyLoginBase.Services");
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myhandlers));

        serviceCollection.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        serviceCollection.AddTransient<IEmailService, EmailService>();
    }
}
