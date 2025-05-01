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

                    await RegistrarMovimentoEstoque(estoqueMov, clienteId, user_logado);

                    return new RequestResult<EstoqueProdutoDto>(dto);
                }

                throw new Exception("Erro ao criar o estoque do produto.");
            }
            else
            {
                decimal quantidade = estoqueMov.Quantidade;
                if (estoqueMov.EstoqueProdutoDtoOperacao == EstoqueProdutoDtoOperacao.Saida)
                {
                    quantidade = estoqueMov.Quantidade * -1;

                }


                estoqueProdutoExists.AtualizarQuantidade(quantidade);

                _repository.EstoqueProdutoRepository.UpdateAsync(estoqueProdutoExists);

                if (await _repository.CommitAsync())
                {

                    await RegistrarMovimentoEstoque(estoqueMov, clienteId, user_logado);

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

    private async Task RegistrarMovimentoEstoque(EstoqueProdutoDtoManter estoqueMov, Guid clienteId, Guid user_logado)
    {
        try
        {
            switch (estoqueMov.EstoqueProdutoDtoOperacao)
            {
                case EstoqueProdutoDtoOperacao.Entrada:

                    var mov_estoque_entrada = MovimentacaoEstoqueProdutoEntity.Entrada(estoqueMov.ProdutoId, estoqueMov.FilialId, estoqueMov.Quantidade, null, clienteId, user_logado);
                    await _repository.MovimentacaoEstoqueProdutoRepository.CreateAsync(mov_estoque_entrada);
                    if (!await _repository.CommitAsync())
                        throw new ArgumentException("Erro ao registrar movimentação do estoque");

                    break;
                case EstoqueProdutoDtoOperacao.Saida:


                    var mov_estoque_saida = MovimentacaoEstoqueProdutoEntity.Saida(estoqueMov.ProdutoId, estoqueMov.FilialId, estoqueMov.Quantidade, null, clienteId, user_logado);
                    await _repository.MovimentacaoEstoqueProdutoRepository.CreateAsync(mov_estoque_saida);
                    if (!await _repository.CommitAsync())
                        throw new ArgumentException("Erro ao registrar movimentação do estoque");
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }


    }

    public async Task<RequestResult<IEnumerable<EstoqueProdutoDto>>> SelectAllAsync(ClaimsPrincipal user, bool include = true)
    {
        try
        {

            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

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
