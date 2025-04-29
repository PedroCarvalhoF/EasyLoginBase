using EasyLoginBase.Application.Dto;
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

            if (await NomeProdutoUso(produtoDtoCreate.NomeProduto, user))
                throw new ArgumentException("Nome do produto já está em uso");

            if (await CodigoProdutoUso(produtoDtoCreate.CodigoProduto, user))
                throw new ArgumentException("Código do produto já está em uso");


            ProdutoEntity produtoEntity = ProdutoEntity.CriarProdutoEntity(produtoDtoCreate.NomeProduto, produtoDtoCreate.CodigoProduto, produtoDtoCreate.CategoriaProdutoEntityId, produtoDtoCreate.UnidadeMedidaProdutoId, clienteId, user_logado);

            await _repository.ProdutoRepository.CreateAsync(produtoEntity);

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
    public async Task<RequestResult<ProdutoDto>> UpdateAsync(ProdutoDtoUpdate produtoDtoUpdate, ClaimsPrincipal claims)
    {
        try
        {
            var clienteId = claims.GetClienteIdVinculo();
            var user_logado = claims.GetUserId();

            var produtoExists = await _repository.ProdutoRepository.SelectAsync(produtoDtoUpdate.Id, clienteId);

            if (produtoExists is null)
                throw new ArgumentException("Produto não localizado.");

            var validarNomeExists = await _repository.GetRepository<ProdutoEntity>().ConsultarPorFiltroAsync(p => p.NomeProduto.ToLower() == produtoDtoUpdate.NomeProduto.ToLower() && p.Id != produtoDtoUpdate.Id, clienteId);

            if (validarNomeExists != null && validarNomeExists.Count() > 0)
                throw new ArgumentException("Nome do produto está em uso");

            var validarCodigoExists = await _repository.GetRepository<ProdutoEntity>().ConsultarPorFiltroAsync(p => p.CodigoProduto.ToLower() == produtoDtoUpdate.CodigoProduto.ToLower() && p.Id != produtoDtoUpdate.Id, clienteId);

            if (validarCodigoExists != null && validarCodigoExists.Count() > 0)
                throw new ArgumentException("Código do produto está em uso");

            produtoExists.AlterarNome(produtoDtoUpdate.NomeProduto);
            produtoExists.AlterarCodigo(produtoDtoUpdate.CodigoProduto);
            produtoExists.AlterarCategoria(produtoDtoUpdate.CategoriaProdutoEntityId);
            produtoExists.AlterarUnidadeMedidaProduto(produtoDtoUpdate.UnidadeMedidaProdutoId);

            _repository.ProdutoRepository.UpdateAsync(produtoExists);

            if (await _repository.CommitAsync())
            {
                var produtoEntityUpdate = await _repository.GetRepository<ProdutoEntity>().ConsultarPorIdAsync(produtoExists.Id, clienteId);
                if (produtoEntityUpdate is null)
                    throw new ArgumentException("Erro ao atualizar Produto");
                ProdutoDto produtoDtoUpdateResult = DtoMapper.ParseProduto(produtoEntityUpdate);
                return new RequestResult<ProdutoDto>(produtoDtoUpdateResult);
            }

            throw new ArgumentException("Erro inesperado ao atualizar produto.");
        }
        catch (Exception ex)
        {

            return new RequestResult<ProdutoDto>(ex);
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
    public async Task<bool> CodigoProdutoUso(string codigoProduto, ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            var result = await _repository.GetRepository<ProdutoEntity>().ConsultarPorFiltroAsync(p => p.CodigoProduto == codigoProduto, clienteId);

            if (result is null)
                return false;

            return result.Count() > 0;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<bool> NomeProdutoUso(string nomeProduto, ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            var result = await _repository.GetRepository<ProdutoEntity>().ConsultarPorFiltroAsync(p => p.NomeProduto.ToLower() == nomeProduto.ToLower(), clienteId);

            if (result is null)
                return false;

            return result.Count() > 0;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<ProdutoDto> ConsultarProdutoById(Guid produtoEntityId, ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            var entity = await _repository.GetRepository<ProdutoEntity>().ConsultarPorIdAsync(produtoEntityId, clienteId);

            return entity is null ? throw new Exception("Produto não localizado.") : DtoMapper.ParseProduto(entity);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
