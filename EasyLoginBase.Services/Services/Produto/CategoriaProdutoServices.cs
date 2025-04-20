using EasyLoginBase.Application.Dto;
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

    public async Task<RequestResult<CategoriaProdutoDto>> CadastrarCategoriaProduto(CategoriaProdutoDtoCreate categoriaProdutoDtoCreate, ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            CategoriaProdutoEntity createEntity = DtoMapper.ParseCategoriaProduto(categoriaProdutoDtoCreate, clienteId, user_logado);

            await _repository.GetRepository<CategoriaProdutoEntity>().CadastrarAsync(createEntity);

            if (await _repository.CommitAsync())
            {
                var dto = new CategoriaProdutoDto
                {
                    Id = createEntity.Id,
                    ClienteId = createEntity.ClienteId,
                    UsuarioRegistroId = createEntity.UsuarioRegistroId,
                    NomeCategoria = createEntity.NomeCategoria!,
                    CreateAt = createEntity.CreateAt,
                    Habilitado = createEntity.Habilitado
                };

                return RequestResult<CategoriaProdutoDto>.Ok(dto);
            }

            return RequestResult<CategoriaProdutoDto>.BadRequest("Erro ao salvar a categoria do produto.");

        }
        catch (Exception ex)
        {

            return RequestResult<CategoriaProdutoDto>.BadRequest(ex.Message);
        }
    }
    public async Task<RequestResult<CategoriaProdutoDto>> ConsultarCategoriaProdutoById(ClaimsPrincipal user, DtoRequestId id)
    {
        try
        {
            var entity = await _repository.GetRepository<CategoriaProdutoEntity>().ConsultarPorIdAsync(id.Id, user.GetClienteIdVinculo());

            if (entity == null)
                return RequestResult<CategoriaProdutoDto>.BadRequest("Categoria não encontrada.");

            var dto = DtoMapper.ParseCategoriaProduto(entity);

            return new RequestResult<CategoriaProdutoDto>(dto);
        }
        catch (Exception ex)
        {

            return new RequestResult<CategoriaProdutoDto>(ex);
        }
    }
    public async Task<RequestResult<IEnumerable<CategoriaProdutoDto>>> ConsultarCategoriasProdutos(ClaimsPrincipal user)
    {
        try
        {
            IEnumerable<CategoriaProdutoEntity> categoriasEntities = await _repository.GetRepository<CategoriaProdutoEntity>().ConsultarTodosAsync(user.GetClienteIdVinculo());

            IEnumerable<CategoriaProdutoDto> categoriasDtos = DtoMapper.ParseCategoriaProduto(categoriasEntities).OrderBy(cat => cat.NomeCategoria);

            return new RequestResult<IEnumerable<CategoriaProdutoDto>>(categoriasDtos);

        }
        catch (Exception ex)
        {

            return new RequestResult<IEnumerable<CategoriaProdutoDto>>(ex);
        }
    }
    public async Task<RequestResult<CategoriaProdutoDto>> AlterarCategoriaProduto(CategoriaProdutoDtoUpdate categoriaProdutoDtoUpdate, ClaimsPrincipal user)
    {
        try
        {
            var clienteId = user.GetClienteIdVinculo();
            var user_logado = user.GetUserId();

            var categoriaEntityExists = await _repository.GetRepository<CategoriaProdutoEntity>().ConsultarPorIdAsync(categoriaProdutoDtoUpdate.Id, clienteId);

            if (categoriaEntityExists == null)
                return RequestResult<CategoriaProdutoDto>.BadRequest("Categoria não encontrada.");

            categoriaEntityExists.AlterarNome(categoriaProdutoDtoUpdate.NomeCategoria);

            if (categoriaProdutoDtoUpdate.habilitado)
                categoriaEntityExists.Habilitar();
            else
                categoriaEntityExists.Desabilitar();

            _repository.GetRepository<CategoriaProdutoEntity>().AtualizarAsync(categoriaEntityExists);

            if (await _repository.CommitAsync())
            {
                var dto = new CategoriaProdutoDto
                {
                    Id = categoriaEntityExists.Id,
                    ClienteId = categoriaEntityExists.ClienteId,
                    UsuarioRegistroId = categoriaEntityExists.UsuarioRegistroId,
                    NomeCategoria = categoriaEntityExists.NomeCategoria!,
                    CreateAt = categoriaEntityExists.CreateAt,
                    Habilitado = categoriaEntityExists.Habilitado
                };

                return RequestResult<CategoriaProdutoDto>.Ok(dto);
            }

            return RequestResult<CategoriaProdutoDto>.BadRequest("Erro ao salvar a categoria do produto.");

        }
        catch (Exception ex)
        {

            return new RequestResult<CategoriaProdutoDto>(ex);
        }
    }
}