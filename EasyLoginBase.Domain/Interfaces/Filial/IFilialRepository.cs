using EasyLoginBase.Domain.Entities.Filial;

namespace EasyLoginBase.Domain.Interfaces.Filial;

public interface IFilialRepository
{
    Task<FilialEntity> CriarFilialAsync(FilialEntity filial);
    FilialEntity AlterarFilialAsync(FilialEntity filial);
    Task<FilialEntity?> SelecionarFilialPorId(Guid idFilial);
    Task<IEnumerable<FilialEntity>> SelecionarFiliaisPorIdPessoaCliente(Guid idPessoaCliente);
}
