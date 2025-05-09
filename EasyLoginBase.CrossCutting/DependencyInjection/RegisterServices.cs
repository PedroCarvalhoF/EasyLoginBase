using EasyLoginBase.CrossCutting.DependencyInjection.Extensions;
using EasyLoginBase.Domain.Entities;
using EasyLoginBase.Domain.Entities.Filial;
using EasyLoginBase.Domain.Entities.PDV;
using EasyLoginBase.Domain.Entities.PessoaCliente;
using EasyLoginBase.Domain.Entities.Produto;
using EasyLoginBase.Domain.Entities.Produto.Estoque;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Domain.Interfaces.Cliente;
using EasyLoginBase.Domain.Interfaces.Filial;
using EasyLoginBase.Domain.Interfaces.PDV;
using EasyLoginBase.Domain.Interfaces.Produto.Estoque;
using EasyLoginBase.Domain.Interfaces.Produto.MovimentacaoEstoque;
using EasyLoginBase.Domain.Interfaces.UsuarioClienteVinculo;
using EasyLoginBase.InfrastructureData.Implementacao;
using EasyLoginBase.InfrastructureData.Repository;
using EasyLoginBase.InfrastructureData.Repository.Cliente;
using EasyLoginBase.InfrastructureData.Repository.Filial;
using EasyLoginBase.InfrastructureData.Repository.PDV;
using EasyLoginBase.InfrastructureData.Repository.UsuarioClienteVinculo;
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
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped(typeof(IBaseClienteRepository<>), typeof(BaseClienteRepository<>));
        serviceCollection.AddScoped(typeof(IGerenericRepository<>), typeof(GenericRepository<>));
        serviceCollection.AddScoped(typeof(IBaseClienteRepository_REFACTOR<>), typeof(BaseClienteRepository_REFACTOR<>));
        
        //Cliente Repository
        serviceCollection.AddScoped<IClienteRepository<PessoaClienteEntity>, ClienteImplementacao>();
        serviceCollection.AddScoped<IGerenericRepository<PessoaClienteEntity>, GenericRepository<PessoaClienteEntity>>();

        //Usuario Vinculo Cliente
        serviceCollection.AddScoped<IUsuarioClienteVinculoRepository<PessoaClienteVinculadaEntity>, UsuarioClienteVinculoImplementacao>();
        serviceCollection.AddScoped<IGerenericRepository<PessoaClienteVinculadaEntity>, GenericRepository<PessoaClienteVinculadaEntity>>();



        serviceCollection.AddScoped<IBaseClienteRepository_REFACTOR<ProdutoEntity>, BaseClienteRepository_REFACTOR<ProdutoEntity>>();

        //ESTOQUE
        serviceCollection.AddScoped<IBaseClienteRepository_REFACTOR<EstoqueProdutoEntity>, BaseClienteRepository_REFACTOR<EstoqueProdutoEntity>>();
        serviceCollection.AddScoped<IEstoqueProdutoRepository<EstoqueProdutoEntity>, ProdutoEstoqueImplementacao>();

        //MOVIMENTACAO ESTOQUE
        serviceCollection.AddScoped<IBaseClienteRepository_REFACTOR<MovimentacaoEstoqueProdutoEntity>, BaseClienteRepository_REFACTOR<MovimentacaoEstoqueProdutoEntity>>();
        serviceCollection.AddScoped<IMovimentacaoEstoqueProdutoRepository<MovimentacaoEstoqueProdutoEntity, FiltroBase>, MovimentacaoEstoqueProdutoImplementacao>();


        // Alterado para Scoped para melhor uso do DbContext
        serviceCollection.AddScoped<IFilialRepository<FilialEntity, ClaimsPrincipal>, FilialRepository>();

        serviceCollection.AddScoped<IBaseClienteRepository<ProdutoEntity>, BaseClienteRepository<ProdutoEntity>>();
        serviceCollection.AddScoped<IUsuarioPdvRepository, UsuarioPdvRepository>();

        serviceCollection.AddScoped<IPontoVendaRepository<PontoVendaEntity>, PontoVendaRepository>();

        var myhandlers = AppDomain.CurrentDomain.Load("EasyLoginBase.Services");
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myhandlers));
    }
}
