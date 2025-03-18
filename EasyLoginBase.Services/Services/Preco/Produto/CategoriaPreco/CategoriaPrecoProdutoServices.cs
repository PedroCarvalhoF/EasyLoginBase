using EasyLoginBase.Application.Dto.Preco.Produto.CategoriaPrecoProduto;
using EasyLoginBase.Application.Services.Intefaces.Preco.Produto.CategoriaPreco;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Domain.Entities.Preco.Produto.CategoriaPreco;
using EasyLoginBase.Domain.Interfaces;
using System.Security.Claims;

namespace EasyLoginBase.Services.Services.Preco.Produto.CategoriaPreco;

public class CategoriaPrecoProdutoServices : ICategoriaPrecoProdutoServices
{
    private readonly IUnitOfWork _repository;
    public CategoriaPrecoProdutoServices(IUnitOfWork repository)
    {
        _repository = repository;
    }
    public async Task<CategoriaPrecoProdutoDto> CadastrarCategoriaPrecoProduto(CategoriaPrecoProdutoDtoCreate create, ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            if (await NomeCategoriaPrecoProdutoEmUso(create.CategoriaPreco, user))
                throw new Exception("Nome da categoria de preço já está em uso.");

            CategoriaPrecoProdutoEntity entity = CategoriaPrecoProdutoEntity.CriarCategoriaPrecoProduto(create.CategoriaPreco, clienteId, user_logado);

            if (!entity.EntidadeValidada)
                throw new Exception("Entidade inválida.");

            await _repository.GetRepository<CategoriaPrecoProdutoEntity>().CadastrarAsync(entity);

            if (await _repository.CommitAsync())
                return new CategoriaPrecoProdutoDto { Id = entity.Id, CategoriaPreco = entity.CategoriaPreco! };

            throw new Exception("Erro ao salvar a categoria de preço do produto.");

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    public async Task<IEnumerable<CategoriaPrecoProdutoDto>> ConsultarCategoriasPrecosProdutos(ClaimsPrincipal user)
    {
        try
        {
            IEnumerable<CategoriaPrecoProdutoEntity> entities = await _repository.GetRepository<CategoriaPrecoProdutoEntity>().ConsultarPorFiltroAsync(cat_preco => cat_preco.ClienteId == user.GetClienteIdVinculo(), user.GetClienteIdVinculo());

            return entities.Select(entity => new CategoriaPrecoProdutoDto { Id = entity.Id, CategoriaPreco = entity.CategoriaPreco! }).OrderBy(e => e.CategoriaPreco);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> NomeCategoriaPrecoProdutoEmUso(string nomeCategoriaPrecoProduto, ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            var exist = await _repository.GetRepository<CategoriaPrecoProdutoEntity>().ConsultarPorFiltroAsync(cat_preco => cat_preco.CategoriaPreco.ToLower() == nomeCategoriaPrecoProduto.ToLower(), clienteId);

            if (exist == null)
                return false;

            return exist.Count() > 0;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
}
