using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Domain.Entities.Filial;

namespace EasyLoginBase.Services.Tools.UseCase.DtoForEntity.Filial;

public partial class DtoMapper
{
    //entities
    public static FilialDto ParceFilialEntityForDto(FilialEntity filial)
    {
        return new FilialDto(filial.IdFilial, filial.NomeFilial);
    }

    //dtos
    public static FilialEntity ParceFilialDtoEntity(FiliaDtoCreateRequest filial)
    {
        return new FilialEntity(Guid.NewGuid(), filial.NomeFilial);
    }
}
