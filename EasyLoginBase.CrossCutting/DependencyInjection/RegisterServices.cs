using EasyLoginBase.CrossCutting.DependencyInjection.Extensions;
using EasyLoginBase.Domain.Entities;
using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Entities.PDV;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Entities.Produto;
using EasyLoginBase.Domain.Entities.Produto.Estoque;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Domain.Interfaces.Filial;
using EasyLoginBase.Domain.Interfaces.PDV;
using EasyLoginBase.Domain.Interfaces.PessoaCliente;
using EasyLoginBase.Domain.Interfaces.Produto;
using EasyLoginBase.Domain.Interfaces.Produto.Estoque;
using EasyLoginBase.Domain.Interfaces.Produto.MovimentacaoEstoque;
using EasyLoginBase.InfrastructureData.Implementacao;
using EasyLoginBase.InfrastructureData.Repository;
using EasyLoginBase.InfrastructureData.Repository.Filial;
using EasyLoginBase.InfrastructureData.Repository.PDV;
using EasyLoginBase.InfrastructureData.Repository.PessoaCliente;
using EasyLoginBase.InfrastructureData.Repository.Produto;
using EasyLoginBase.Services.Tools.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace EasyLoginBase.CrossCutting.DependencyInjection;

public static class RegisterServices
{
    public static void ConfigureRepositories(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        IdentityConfiguration.Configurar(serviceCollection, configuration);
        serviceCollection.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        serviceCollection.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
        serviceCollection.AddScoped(typeof(IBaseClienteRepository<>), typeof(BaseClienteRepository<>));
        serviceCollection.AddScoped(typeof(IGerenericRepository<>), typeof(GenericRepository<>));

        // Alterando Repository para melhor perfomace
        // Repository 
        serviceCollection.AddScoped(typeof(IBaseClienteRepository_REFACTOR<>), typeof(BaseClienteRepository_REFACTOR<>));



        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        serviceCollection.AddScoped<IBaseClienteRepository_REFACTOR<ProdutoEntity>, BaseClienteRepository_REFACTOR<ProdutoEntity>>();
        serviceCollection.AddScoped<IProdutoRepository_REFACTOR, ProdutoRepository_REFACTOR>();

        //ESTOQUE
        serviceCollection.AddScoped<IBaseClienteRepository_REFACTOR<EstoqueProdutoEntity>, BaseClienteRepository_REFACTOR<EstoqueProdutoEntity>>();
        serviceCollection.AddScoped<IEstoqueProdutoRepository<EstoqueProdutoEntity>, ProdutoEstoqueImplementacao>();

        //MOVIMENTACAO ESTOQUE
        serviceCollection.AddScoped<IBaseClienteRepository_REFACTOR<MovimentacaoEstoqueProdutoEntity>, BaseClienteRepository_REFACTOR<MovimentacaoEstoqueProdutoEntity>>();
        serviceCollection.AddScoped<IMovimentacaoEstoqueProdutoRepository<MovimentacaoEstoqueProdutoEntity, FiltroBase>, MovimentacaoEstoqueProdutoImplementacao>();


        // Alterado para Scoped para melhor uso do DbContext
        serviceCollection.AddScoped<IFilialRepository<FilialEntity, ClaimsPrincipal>, FilialRepository>();
        serviceCollection.AddScoped<IPessoaClienteRepository<PessoaClienteEntity>, PessoaClienteRepository>();
        serviceCollection.AddScoped<IPessoaClienteVinculadaRepository, PessoaClienteVinculadaRepository>();
        serviceCollection.AddScoped<IBaseClienteRepository<ProdutoEntity>, BaseClienteRepository<ProdutoEntity>>();
        serviceCollection.AddScoped<IUsuarioPdvRepository, UsuarioPdvRepository>();

        serviceCollection.AddScoped<IPontoVendaRepository<PontoVendaEntity>, PontoVendaRepository>();

        var myhandlers = AppDomain.CurrentDomain.Load("EasyLoginBase.Services");
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myhandlers));
    }
}
