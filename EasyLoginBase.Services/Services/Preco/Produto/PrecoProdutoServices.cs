using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Application.Dto.Preco.Produto;
using EasyLoginBase.Application.Dto.Preco.Produto.CategoriaPrecoProduto;
using EasyLoginBase.Application.Dto.Produto.Produto;
using EasyLoginBase.Application.Services.Intefaces.Filial;
using EasyLoginBase.Application.Services.Intefaces.Preco.Produto;
using EasyLoginBase.Application.Services.Intefaces.Preco.Produto.CategoriaPreco;
using EasyLoginBase.Application.Services.Intefaces.Produto;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Domain.Entities.Preco.Produto;
using EasyLoginBase.Domain.Enuns.Preco.Produto;
using EasyLoginBase.Domain.Interfaces;
using System.Security.Claims;

namespace EasyLoginBase.Services.Services.Preco.Produto;

public class PrecoProdutoServices : IPrecoProdutoServices
{
    private readonly IUnitOfWork _repository;
    private readonly IFilialServices _filialServices;
    private readonly IProdutoServices _produtoServices;
    private readonly ICategoriaPrecoProdutoServices _categoriaPrecoProdutoServices;
    public PrecoProdutoServices(IUnitOfWork repository, IFilialServices filialServices, IProdutoServices produtoServices, ICategoriaPrecoProdutoServices categoriaPrecoProdutoServices)
    {
        _repository = repository;
        _filialServices = filialServices;
        _produtoServices = produtoServices;
        _categoriaPrecoProdutoServices = categoriaPrecoProdutoServices;
    }

    public async Task<PrecoProdutoDto> CadastrarPrecoProduto(PrecoProdutoDtoCreate preco, ClaimsPrincipal user)
    {
        var clienteId = user.GetClienteIdVinculo();
        var user_logado = user.GetUserId();

        //verificar se filial existe
        FilialDto filialExists = await _filialServices.ConsultarFilialById(preco.FilialEntityId, user);

        //verificar se produto existe
        ProdutoDto produtoExists = await _produtoServices.ConsultarProdutoById(preco.ProdutoEntityId, user);

        // Verifica se o preço já existe para o produto na mesma filial e categoria
        CategoriaPrecoProdutoDto categoriaPrecoExists = await _categoriaPrecoProdutoServices.ConsultarCategoriaPrecoProdutoById(preco.CategoriaPrecoProdutoEntityId, user);


        var precoProdutoEntityExists = await _repository.GetRepository<PrecoProdutoEntity>().ConsultarPorFiltroAsync(pr =>
            pr.ProdutoEntityId == preco.ProdutoEntityId &&
            pr.FilialEntityId == preco.FilialEntityId &&
            pr.CategoriaPrecoProdutoEntityId == preco.CategoriaPrecoProdutoEntityId &&
            pr.TipoPrecoProdutoEnum == (PrecoProdutoEnum)preco.TipoPrecoProduto, clienteId);

        if (precoProdutoEntityExists != null && precoProdutoEntityExists.Any())
        {
            // Se já existir, apenas altera o preço
            precoProdutoEntityExists.First().AlterarPreco(preco.PrecoProduto);
            _repository.GetRepository<PrecoProdutoEntity>().AtualizarAsync(precoProdutoEntityExists.Single());
        }
        else
        {
            // Caso não exista, cadastra um novo preço
            var novoPrecoProduto = PrecoProdutoEntity.CriarPrecoProdutoEntity(
                preco.ProdutoEntityId,
                preco.FilialEntityId,
                preco.CategoriaPrecoProdutoEntityId,
                preco.PrecoProduto,
                (PrecoProdutoEnum)preco.TipoPrecoProduto,
                clienteId,
                user_logado
            );

            await _repository.GetRepository<PrecoProdutoEntity>().CadastrarAsync(novoPrecoProduto);
        }

        // Confirma a transação
        if (await _repository.CommitAsync())
        {
            var precoProdutoCreateResult = (await _repository.GetRepository<PrecoProdutoEntity>().ConsultarPorFiltroAsync(pr =>
                pr.ProdutoEntityId == preco.ProdutoEntityId &&
                pr.FilialEntityId == preco.FilialEntityId &&
                pr.CategoriaPrecoProdutoEntityId == preco.CategoriaPrecoProdutoEntityId &&
                pr.TipoPrecoProdutoEnum == (PrecoProdutoEnum)preco.TipoPrecoProduto, clienteId)).SingleOrDefault();

            return new PrecoProdutoDto
            {
                Id = precoProdutoCreateResult.Id,
                ProdutoEntityId = precoProdutoCreateResult.ProdutoEntityId,
                NomeProduto = precoProdutoCreateResult.ProdutoEntity?.NomeProduto,
                FilialEntityId = precoProdutoCreateResult.FilialEntityId,
                NomeFilial = precoProdutoCreateResult.FilialEntity?.NomeFilial,
                CategoriaPrecoProdutoEntityId = precoProdutoCreateResult.CategoriaPrecoProdutoEntityId,
                CategoriaPreco = precoProdutoCreateResult.CategoriaPrecoProdutoEntity?.CategoriaPreco,
                PrecoProduto = precoProdutoCreateResult.PrecoProduto,
                TipoPrecoProduto = precoProdutoCreateResult.TipoPrecoProdutoEnum.ToString()
            };
        }

        throw new Exception("Erro ao cadastrar preço do produto");
    }
    public async Task<IEnumerable<PrecoProdutoDto>> ConsultarPrecosProdutos(ClaimsPrincipal user)
    {
        try
        {
            var precosProdutosEntities = await _repository.GetRepository<PrecoProdutoEntity>().ConsultarTodosAsync(user.GetClienteIdVinculo());

            return precosProdutosEntities.Select(precoProdutoEntity => new PrecoProdutoDto
            {
                Id = precoProdutoEntity.Id,
                ProdutoEntityId = precoProdutoEntity.ProdutoEntityId,
                NomeProduto = precoProdutoEntity.ProdutoEntity?.NomeProduto,
                FilialEntityId = precoProdutoEntity.FilialEntityId,
                NomeFilial = precoProdutoEntity.FilialEntity?.NomeFilial,
                CategoriaPrecoProdutoEntityId = precoProdutoEntity.CategoriaPrecoProdutoEntityId,
                CategoriaPreco = precoProdutoEntity.CategoriaPrecoProdutoEntity?.CategoriaPreco,
                PrecoProduto = precoProdutoEntity.PrecoProduto,
                TipoPrecoProduto = precoProdutoEntity.TipoPrecoProdutoEnum.ToString()
            }).OrderBy(f => f.NomeFilial).ThenBy(p => p.NomeProduto);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<IEnumerable<PrecoProdutoDto>> ConsultarProdutoByProdutoId(Guid idProduto, ClaimsPrincipal user)
    {
        try
        {
            var precoProdutoEntities = await _repository
                .GetRepository<PrecoProdutoEntity>()
                .ConsultarPorFiltroAsync(pr => pr.ProdutoEntityId == idProduto, user.GetClienteIdVinculo());

            if (!precoProdutoEntities?.Any() ?? true)
                throw new KeyNotFoundException("Preço do produto não encontrado");

            return precoProdutoEntities.Select(pr => new PrecoProdutoDto
            {
                Id = pr.Id,
                ProdutoEntityId = pr.ProdutoEntityId,
                NomeProduto = pr.ProdutoEntity?.NomeProduto, // Se necessário, verifique se ProdutoEntity está carregado
                FilialEntityId = pr.FilialEntityId,
                NomeFilial = pr.FilialEntity?.NomeFilial, // Se necessário, verifique se FilialEntity está carregado
                CategoriaPrecoProdutoEntityId = pr.CategoriaPrecoProdutoEntityId,
                CategoriaPreco = pr.CategoriaPrecoProdutoEntity?.CategoriaPreco, // Se necessário, verifique se CategoriaPrecoProdutoEntity está carregado
                PrecoProduto = pr.PrecoProduto,
                TipoPrecoProduto = pr.TipoPrecoProdutoEnum.ToString()
            });
        }
        catch (KeyNotFoundException ex)
        {
            throw; // Mantém a exceção específica
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao consultar preço do produto.", ex);
        }
    }

}
