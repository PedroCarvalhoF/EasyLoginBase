using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Domain.Entities.Filial;

namespace EasyLoginBase.Services.Tools.UseCase.DtoForEntity.Filial;

public partial class DtoMapper
{
    //entities
    public static FilialDto ParceFilial(FilialEntity filial)
    {
        return new FilialDto(filial.IdFilial, filial.NomeFilial);
    }

    //dtos
    public static FilialEntity ParceFilialDtoEntity(FiliaDtoCreateRequest filial)
    {
        return new FilialEntity(Guid.NewGuid(), filial.NomeFilial);
    }

    public static IEnumerable<FilialDto> ParceFilial(IEnumerable<FilialEntity> filialEntities)
    {
        foreach (var user in filialEntities)
        {
            yield return ParceFilial(user);
        }
    }
}
