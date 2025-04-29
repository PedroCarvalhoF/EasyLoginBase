using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Produto.Estoque.Estoque;
using EasyLoginBase.Application.Services.Intefaces.Produto;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Domain.Entities.Produto.Estoque;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Services.Tools.UseCase;
using System.Security.Claims;

namespace EasyLoginBase.Services.Services.Produto.Estoque.Estoque;
public class EstoqueProdutoServices : IEstoqueProdutoServices<EstoqueProdutoDto>
{
    private readonly IUnitOfWork _repository;
    public EstoqueProdutoServices(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<RequestResult<EstoqueProdutoDto>> MovimentarEstoque(EstoqueProdutoDtoManter estoqueMov, ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            var estoqueProdutoExists = await _repository.EstoqueProdutoImplementacao.SelectByProdutoIdFilialId(clienteId, estoqueMov.ProdutoId, estoqueMov.FilialId, true);

            if (estoqueProdutoExists == null)
            {
                var estoqueProdutoEntity = EstoqueProdutoEntity.Criar(estoqueMov.ProdutoId, estoqueMov.FilialId, estoqueMov.Quantidade, clienteId, user_logado);

                await _repository.EstoqueProdutoRepository.CreateAsync(estoqueProdutoEntity);

                if (await _repository.CommitAsync())
                {
                    var estoqueProdutoResultCreate = await _repository.EstoqueProdutoImplementacao.SelectByProdutoIdFilialId(clienteId, estoqueMov.ProdutoId, estoqueMov.FilialId);

                    var dto = estoqueProdutoResultCreate!.EstoqueProdutoEntityToDto();

                    return new RequestResult<EstoqueProdutoDto>(dto);
                }

                throw new Exception("Erro ao criar o estoque do produto.");
            }
            else
            {
                decimal quantidade = estoqueMov.Quantidade;
                if (estoqueMov.EstoqueProdutoDtoOperacao == EstoqueProdutoDtoOperacao.Saida)
                    quantidade = estoqueMov.Quantidade * -1;

                estoqueProdutoExists.AtualizarQuantidade(quantidade);

                _repository.EstoqueProdutoRepository.UpdateAsync(estoqueProdutoExists);

                if (await _repository.CommitAsync())
                {
                    var dto = estoqueProdutoExists.EstoqueProdutoEntityToDto();
                    return new RequestResult<EstoqueProdutoDto>(dto);
                }


                throw new Exception("Erro ao criar o estoque do produto.");
            }


            throw new Exception("Erro inesperado ao criar o estoque do produto.");
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<RequestResult<IEnumerable<EstoqueProdutoDto>>> SelectAllAsync(Guid clienteId, bool include = true)
    {
        try
        {
            var entities = await _repository.EstoqueProdutoImplementacao.SelectAllAsync(clienteId, include);

            var dto = entities.EstoqueProdutoEntityToDto();

            return new RequestResult<IEnumerable<EstoqueProdutoDto>>(dto);
        }
        catch (Exception ex)
        {

            return new RequestResult<IEnumerable<EstoqueProdutoDto>>(ex);
        }
    }

    public async Task<RequestResult<IEnumerable<EstoqueProdutoDto>>> SelectByFiliaId(Guid clienteId, Guid filialId, bool include = true)
    {
        try
        {
            var entities = await _repository.EstoqueProdutoImplementacao.SelectByFiliaId(clienteId, filialId, include);

            var dto = entities.EstoqueProdutoEntityToDto();

            return new RequestResult<IEnumerable<EstoqueProdutoDto>>(dto);
        }
        catch (Exception ex)
        {

            return new RequestResult<IEnumerable<EstoqueProdutoDto>>(ex);
        }
    }

    public async Task<RequestResult<IEnumerable<EstoqueProdutoDto>>> SelectByProdutoId(Guid clienteId, Guid produtoId, bool include = true)
    {
        try
        {
            var entities = await _repository.EstoqueProdutoImplementacao.SelectByProdutoId(clienteId, produtoId, include);

            var dto = entities.EstoqueProdutoEntityToDto();

            return new RequestResult<IEnumerable<EstoqueProdutoDto>>(dto);
        }
        catch (Exception ex)
        {

            return new RequestResult<IEnumerable<EstoqueProdutoDto>>(ex);
        }
    }
}
