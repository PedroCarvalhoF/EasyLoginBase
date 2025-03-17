using EasyLoginBase.Application.Dto.Produto.Categoria;
using EasyLoginBase.Application.Services.Intefaces.Produto;
using EasyLoginBase.Application.Tools;
using EasyLoginBase.Domain.Entities.Produto;
using EasyLoginBase.Domain.Interfaces;
using EasyLoginBase.Services.Tools.UseCase;
using System.Security.Claims;

namespace EasyLoginBase.Services.Services.Produto;

public class CategoriaProdutoServices : ICategoriaProdutoServices
{
    private readonly IUnitOfWork _repository;

    public CategoriaProdutoServices(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<CategoriaProdutoDto> CadastrarCategoriaProduto(CategoriaProdutoDtoCreate categoriaProdutoDtoCreate, ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            CategoriaProdutoEntity createEntity = DtoMapper.ParseCategoriaProduto(categoriaProdutoDtoCreate, clienteId, user_logado);

            await _repository.GetRepository<CategoriaProdutoEntity>().CadastrarAsync(createEntity);

            if (await _repository.CommitAsync())
            {
                return new CategoriaProdutoDto
                {
                    Id = createEntity.Id,
                    ClienteId = createEntity.ClienteId,
                    UsuarioRegistroId = createEntity.UsuarioRegistroId,
                    NomeCategoria = createEntity.NomeCategoria!,
                    CreateAt = createEntity.CreateAt,
                    Habilitado = createEntity.Habilitado
                };
            }

            throw new Exception("Erro ao salvar a categoria do produto.");
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<CategoriaProdutoDto>> ConsultarCategoriasProdutos(ClaimsPrincipal user)
    {
        try
        {
            IEnumerable<CategoriaProdutoEntity> categoriasEntities = await _repository.GetRepository<CategoriaProdutoEntity>().ConsultarTodosAsync(user.GetClienteIdVinculo());

            IEnumerable<CategoriaProdutoDto> categoriasDtos = DtoMapper.ParseCategoriaProduto(categoriasEntities).OrderBy(cat => cat.NomeCategoria);

            return categoriasDtos;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}