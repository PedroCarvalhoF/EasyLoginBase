using EasyLoginBase.Application.Dto;
using EasyLoginBase.Application.Dto.Produto.UnidadeMedidaProduto;
using EasyLoginBase.Application.Services.Intefaces.Produto;
using EasyLoginBase.Domain.Entities.Produto;
using EasyLoginBase.Domain.Interfaces;
using System.Threading.Tasks;

namespace EasyLoginBase.Services.Services.Produto;

public class UnidadeMedidaProdutoServices : IUnidadeMedidaProdutoServices
{
    private readonly IUnitOfWork _ufw;
    private readonly IGerenericRepository<UnidadeMedidaProdutoEntity> _unidadeMedidaProdutoRepositoryGeneric;

    public UnidadeMedidaProdutoServices(IUnitOfWork unidadeMedidaProdutoRepository)
    {
        _ufw = unidadeMedidaProdutoRepository;
        _unidadeMedidaProdutoRepositoryGeneric = _ufw.GetGenericRepository<UnidadeMedidaProdutoEntity>();
    }

    public async Task<RequestResult<UnidadeMedidaProdutoDto>> InsertAsync(UnidadeMedidaProdutoDtoCreate unidade)
    {
        try
        {
            var unidadeEntity = UnidadeMedidaProdutoEntity.CriarUnidadeMedidaProdutoEntity(unidade.Nome, unidade.Sigla, unidade.Descricao);

            var result = await _unidadeMedidaProdutoRepositoryGeneric.InsertAsync(unidadeEntity);

            if (await _ufw.CommitAsync())
                return new RequestResult<UnidadeMedidaProdutoDto>(new UnidadeMedidaProdutoDto
                {
                    Id = result.Id,
                    Nome = result.Nome,
                    Sigla = result.Sigla,
                    Descricao = result.Descricao
                });

            return new RequestResult<UnidadeMedidaProdutoDto>(new Exception("Erro ao inserir unidade de medida do produto"));
        }
        catch (Exception ex)
        {

            return new RequestResult<UnidadeMedidaProdutoDto>(ex);
        }
    }

    public async Task<RequestResult<UnidadeMedidaProdutoDto>> Update(UnidadeMedidaProdutoDtoUpdate unidade)
    {
        try
        {
            var entity = await _unidadeMedidaProdutoRepositoryGeneric.SelectAsync(unidade.Id);

            var entityAlterado = UnidadeMedidaProdutoEntity.AlterarUnidadeProdutoMedidaEntity(entity.Id, unidade.Nome, unidade.Sigla, unidade.Descricao);

            _unidadeMedidaProdutoRepositoryGeneric.Update(entityAlterado);

            if (await _ufw.CommitAsync())
                return new RequestResult<UnidadeMedidaProdutoDto>(new UnidadeMedidaProdutoDto
                {
                    Id = entityAlterado.Id,
                    Nome = entityAlterado.Nome,
                    Sigla = entityAlterado.Sigla,
                    Descricao = entityAlterado.Descricao
                });

            return new RequestResult<UnidadeMedidaProdutoDto>(new Exception("Erro ao atualizar unidade de medida do produto"));

        }
        catch (Exception ex)
        {

            return new RequestResult<UnidadeMedidaProdutoDto>(ex);
        }
    }

    public async Task<RequestResult<IEnumerable<UnidadeMedidaProdutoDto>>> Select()
    {
        try
        {
            var unidadesMedidasEntities = await _unidadeMedidaProdutoRepositoryGeneric.Select();

            var unidadesMedidasDto = unidadesMedidasEntities.Select(x => new UnidadeMedidaProdutoDto
            {
                Id = x.Id,
                Nome = x.Nome,
                Sigla = x.Sigla,
                Descricao = x.Descricao
            });

            return new RequestResult<IEnumerable<UnidadeMedidaProdutoDto>>(unidadesMedidasDto);
        }
        catch (Exception ex)
        {

            return new RequestResult<IEnumerable<UnidadeMedidaProdutoDto>>(ex);
        }
    }

    public async Task<RequestResult<UnidadeMedidaProdutoDto>> SelectAsync(Guid id)
    {
        try
        {
            var entity = await _unidadeMedidaProdutoRepositoryGeneric.SelectAsync(id);

            return new RequestResult<UnidadeMedidaProdutoDto>(new UnidadeMedidaProdutoDto
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Sigla = entity.Sigla,
                Descricao = entity.Descricao
            });
        }
        catch (Exception ex)
        {

            return new RequestResult<UnidadeMedidaProdutoDto>(ex);
        }
    }


}
