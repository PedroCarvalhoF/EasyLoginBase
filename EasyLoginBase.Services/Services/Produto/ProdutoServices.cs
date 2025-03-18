using EasyLoginBase.Application.Dto.Produto.Produto;
using EasyLoginBase.Application.Services.Intefaces.Produto;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Domain.Entities.Produto;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Services.Tools.UseCase;
using System.Security.Claims;

namespace EasyLoginBase.Services.Services.Produto;

public class ProdutoServices : IProdutoServices
{
    private readonly IUnitOfWork _repository;

    public ProdutoServices(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<ProdutoDto> CadastrarProduto(ProdutoDtoCreate produtoDtoCreate, ClaimsPrincipal user)
    {
        try
        {

            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            ProdutoEntity produtoEntity = ProdutoEntity.CriarProdutoEntity(produtoDtoCreate.NomeProduto, produtoDtoCreate.CodigoProduto, produtoDtoCreate.CategoriaProdutoEntityId, clienteId, user_logado);

            await _repository.GetRepository<ProdutoEntity>().CadastrarAsync(produtoEntity);

            if (await _repository.CommitAsync())
            {

                var produtoEntityCreate = await _repository.GetRepository<ProdutoEntity>().ConsultarPorIdAsync(produtoEntity.Id, clienteId);

                if (produtoEntityCreate is null)
                    throw new ArgumentException("Erro ao cadastrar Produto");

                ProdutoDto produtoDtoCreateResult = DtoMapper.ParseProduto(produtoEntityCreate);

                return produtoDtoCreateResult;
            }

            throw new Exception("Erro ao cadastrar produto");
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<ProdutoDto>> ConsultarProdutos(ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            var entities = await _repository.GetRepository<ProdutoEntity>().ConsultarTodosAsync(clienteId);

            if (entities is null)
                throw new ArgumentException("Erro ao consultar produtos");

            IEnumerable<ProdutoDto> dtos = DtoMapper.ParseProdutos(entities);

            return dtos;

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
