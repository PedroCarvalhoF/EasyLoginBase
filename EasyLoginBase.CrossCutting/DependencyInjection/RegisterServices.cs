using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Application.Services.Intefaces;
using EasyLoginBase.CrossCutting.DependencyInjection.Extensions;
using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Domain.Interfaces.Filial;
using EasyLoginBase.InfrastructureData.Repository;
using EasyLoginBase.InfrastructureData.Repository.Filial;
using EasyLoginBase.Services.Services.Email;
using EasyLoginBase.Services.Services.Filial;
using EasyLoginBase.Services.Tools.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyLoginBase.CrossCutting.DependencyInjection;

public static class RegisterServices
{
    public static void ConfigureDependenciesRepository(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        IdentityConfiguration.Configurar(serviceCollection, configuration);
        serviceCollection.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        serviceCollection.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));

        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        serviceCollection.AddTransient<IEmailService, EmailService>();

        serviceCollection.AddTransient<IFilialRepository<FilialEntity>, FilialRepository>();
        serviceCollection.AddTransient<IFilialServices<FilialDto>, FilialServices>();

        var myhandlers = AppDomain.CurrentDomain.Load("EasyLoginBase.Services");
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myhandlers));
    }
}
