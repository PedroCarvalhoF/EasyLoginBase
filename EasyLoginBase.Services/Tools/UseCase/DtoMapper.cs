using EasyLoginBase.Application.Dto.Filial;
using EasyLoginBase.Domain.Entities.Filial;

namespace EasyLoginBase.Services.Tools.UseCase;

public static partial class DtoMapper
{
    public static FilialDto ParseFilial(FilialEntity filialEntity)
    => new FilialDto(filialEntity.Id, filialEntity.NomeFilial!);

    internal static IEnumerable<FilialDto> ParseFiliais(IEnumerable<FilialEntity> result)
    {
        foreach (var item in result)
        {
            yield return ParseFilial(item);
        }
    }
}
