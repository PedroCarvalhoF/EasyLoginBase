using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Domain.Entities.Filial;

namespace EasyLoginBase.Services.Tools.UseCase;

public partial class DtoMapper
{
    //entities
    public static FilialDto ParceFilial(FilialEntity filial)
    {
        if (filial.NomeFilial == null)
            throw new ArgumentNullException(nameof(filial));

        return new FilialDto(filial.Id, filial.NomeFilial);
    }

    //dtos
    public static FilialEntity ParceFilialDtoEntity(FilialDtoCreate filial)
    {
        return FilialEntity.CriarFilial(Guid.NewGuid(), filial.NomeFilial);
    }

    public static IEnumerable<FilialDto> ParceFilial(IEnumerable<FilialEntity> filialEntities)
    {
        foreach (var user in filialEntities)
        {
            yield return ParceFilial(user);
        }
    }
}
